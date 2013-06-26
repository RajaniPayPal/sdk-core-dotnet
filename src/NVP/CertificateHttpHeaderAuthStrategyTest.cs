using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.NVP;
using PayPal.Authentication;

namespace PayPal
{
    [TestClass]
    public class CertificateHttpHeaderAuthStrategyTest
    {
        CertificateHttpHeaderAuthStrategy certHttpHeaderAuthStrategy;
        CertificateCredential certCredential;

        [TestMethod]
        public void GenerateHeaderStrategyWithTokenTest()
        {
            certHttpHeaderAuthStrategy = new CertificateHttpHeaderAuthStrategy(UnitTestConstants.APIEndpointSOAP);
            TokenAuthorization toknAuthorization = new TokenAuthorization(UnitTestConstants.AccessToken, UnitTestConstants.TokenSecret);
            certCredential = new CertificateCredential("testusername", "testpassword", "sdk-cert.p12", "KJAERUGBLVF6Y", toknAuthorization);
            Dictionary<string, string> header = certHttpHeaderAuthStrategy.GenerateHeaderStrategy(certCredential);            
            string authHeader = header[BaseConstants.PayPalAuthorizationPlatformHeader];
            string[] headers = authHeader.Split(',');
            Assert.AreEqual("token=" + UnitTestConstants.AccessToken, headers[0]);
        }

        [TestMethod]
        public void GenerateHeaderStrategyWithoutTokenTest()
        {
            certHttpHeaderAuthStrategy = new CertificateHttpHeaderAuthStrategy(UnitTestConstants.APIEndpointNVP);
            certCredential = new CertificateCredential("testusername", "testpassword", "sdk-cert.p12", "KJAERUGBLVF6Y");
            Dictionary<string, string> header = certHttpHeaderAuthStrategy.GenerateHeaderStrategy(certCredential);            
            string username = header[BaseConstants.PayPalSecurityUserIDHeader];
            string password = header[BaseConstants.PayPalSecurityPasswordHeader];
            Assert.AreEqual("testusername", username);
            Assert.AreEqual("testpassword", password);
        }       
    }
}
