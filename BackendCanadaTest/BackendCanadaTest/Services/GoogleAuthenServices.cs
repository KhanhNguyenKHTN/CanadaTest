using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCanadaTest.Models;
using Google.Authenticator;

namespace BackendCanadaTest.Services
{
    public class GoogleAuthenServices
    {
        private static GoogleAuthenServices m_objInstance;
        public static GoogleAuthenServices Instance
        {
            get
            {
                if (m_objInstance == null)
                {
                    m_objInstance = new GoogleAuthenServices();
                }
                return m_objInstance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private GoogleAuthenServices()
        {
        }

        /// <summary>
        /// Generate authen code to set up
        /// </summary>
        /// <returns></returns>
        public GoogleAuthenData GetAuthData(UserInfor x_objUser)
        {
            if (string.IsNullOrEmpty(x_objUser.Authen) == true)
            {
                return null;
            }

            TwoFactorAuthenticator objTFA = new TwoFactorAuthenticator();
            SetupCode objSetup = objTFA.GenerateSetupCode(x_objUser.UserName, $"{x_objUser.UserName}@example.com", x_objUser.Authen, false, 3);
            return new GoogleAuthenData() {
                ManualKey = objSetup.ManualEntryKey,
                QRImage = objSetup.QrCodeSetupImageUrl
            };
        }

        /// <summary>
        /// Verify input code
        /// </summary>
        /// <returns>Authen code</returns>
        public bool VerifyCode(string x_strKey, string x_strCode)
        {
            TwoFactorAuthenticator objTFA = new TwoFactorAuthenticator();
            return objTFA.ValidateTwoFactorPIN(x_strKey, x_strCode);
        }
    }
}
