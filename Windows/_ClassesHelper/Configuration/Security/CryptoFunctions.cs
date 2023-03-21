using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

namespace okTools
{
    /// <summary>
    /// Crypto Functions
    /// </summary>
    public static class CryptoFunctions
    {
        private static string inCk = "E6EB00EE-247A-4001-BFF3-DE73E5AFA372".Substring(0, 16);
        private static string extCk = "70D1A512-09EB-4055-8E1C-41C3B1284803".Substring(0, 16);
        private static string securityKeyConst = "38FCD1F6-B938-4C26-A907-9C59B6D57AE5";

        #region Decrypt
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="DecryptText">Text to decrypt.</param>
        /// <returns></returns>
        [Obfuscation(Exclude = true)]
        public static string Decrypt(string decryptText)
        {
            return Decrypt(extCk, decryptText);
        }
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="ExtKey"></param>
        /// <param name="DecryptText"></param>
        /// <returns></returns>
        [Obfuscation(Exclude = true)]
        public static string Decrypt(string extKey, string decryptText)
        {
            if (string.IsNullOrEmpty(decryptText))
                return null;
            string plaintext = null;
            try
            {
                var key = CombineExternalAndInternalKey(extKey, inCk);

                var fullCipher = Convert.FromBase64String(decryptText);

                if (fullCipher == null || fullCipher.Length <= 0)
                    throw new ArgumentNullException("cipherText is null");

                var keyBytes = Encoding.UTF8.GetBytes(key);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;

                    using (MemoryStream msDecrypt = new MemoryStream(fullCipher))
                    {
                        byte[] iv = new byte[16];
                        msDecrypt.Read(iv, 0, 16);
                        aesAlg.IV = iv;

                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return plaintext;
        }
        #endregion

        #region Encrypt
        /// <summary>
        /// Encrypt string
        /// </summary>
        /// <param name="EncryptText"></param>
        /// <returns></returns>
        [Obfuscation(Exclude = true)]
        public static string Encrypt(string encryptText)
        {
            return Encrypt(extCk, encryptText);
        }
        /// <summary>
        /// Encrypt string
        /// </summary>
        /// <param name="ExtKey"></param>
        /// <param name="EncryptText"></param>
        /// <returns></returns>
        [Obfuscation(Exclude = true)]
        public static string Encrypt(string extKey, string encryptText)
        {
            var key = CombineExternalAndInternalKey(extKey, inCk);

            if (encryptText == null || encryptText.Length <= 0)
                throw new ArgumentNullException("text is null");
            byte[] encrypted;

            var keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                var iv = aesAlg.IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // write iv
                            msEncrypt.Write(iv, 0, iv.Length);
                            //Write all data to the stream.
                            swEncrypt.Write(encryptText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        #endregion

        #region GetPasswordCryptSHA512Hash
        /// <summary>
        /// Get Password Crypt SHA512 Hash
        /// </summary>
        /// <param name="Password">Password to Hash</param>
        /// <returns>Hash from the Password</returns>
        public static string GetPasswordCryptSHA512Hash(string Password)
        {
            byte[] byteString = Encoding.Default.GetBytes(Password);

            //var return1 = "";

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(byteString);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                //return1 = hashedInputStringBuilder.ToString();
                return hashedInputStringBuilder.ToString();
            }

            //CA 2000, where is no dispose method in oSHA512Managed ?!?!?
            //SHA512Managed oSHA512Managed = new SHA512Managed();
            //byte[] bReturn = oSHA512Managed.ComputeHash(bString, 0, bString.Length);
            //return Convert.ToBase64String(bReturn, 0, bReturn.Length);
            // https://stackoverflow.com/questions/11367727/how-can-i-sha512-a-string-in-c

            //var error1 = "";
            //var return2 = "";
            //try
            //{
            //    using (SHA512 a = new SHA512Managed())
            //    {
            //        byte[] h = a.ComputeHash(byteString);
            //        return2 = BitConverter.ToString(h).Replace("-", "");
            //    }
            //    var toLower = false;
            //    return2 = (toLower ? return2.ToLowerInvariant() : return2);
            //}
            //catch(Exception Ex)
            //{
            //    error1 = Ex.Message;
            //}
            //if (return1 == return2)
            //    return return1;

            //return "";
        }
        #endregion

        #region CombineExternalAndInternalKey, CutOrPaddString, XOR
        /// <summary>
        /// Combine External and Internal Key
        /// </summary>
        /// <param name="ExtKey">the external key</param>
        /// <param name="IntKey">the internal key</param>
        /// <returns></returns>
        private static string CombineExternalAndInternalKey(string ExtKey, string IntKey)
        {
            string s1 = CutOrPaddString(ExtKey, 16, IntKey);
            string s2 = CutOrPaddString(IntKey, 16, IntKey);
            if (s1 != s2)
            {
                return XOR(s1, s2);
            }
            else
            {
                return s1;
            }
        }
        // pattern must be at least len characters long; 
        private static string CutOrPaddString(string s, int len, string sPattern)
        {
            if (s.Length < len)
            {
                for (int i = s.Length; i < len; i++)
                {
                    s = s + sPattern[i];
                }
            }
            else
            {
                s = s.Substring(0, len);
            }
            return s;

        }
        // assume s1 and s2 are of equal length
        private static string XOR(string s1, string s2)
        {
            char[] b = s2.ToCharArray();
            char[] a = s1.ToCharArray();
            char[] c = new char[s1.Length];
            for (int i = 0; i < a.Length; i++)
            {
                c[i] = Convert.ToChar(a[i] ^ b[i]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(c);
            return sb.ToString();

        }
        #endregion

        #region GenerateTokenForApplicationData, GenerateTokenForData
        /// <summary>
        /// Generates the token for one value and application.
        /// sample: var token = Querplex.Tools.CryptoFunctions.GenerateTokenForApplicationData(account, "identity takeover");
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="application">The application.</param>
        /// <param name="securityKey">The security key.</param>
        /// <param name="expiredInHours">The expired in hours.</param>
        /// <returns></returns>
        public static string GenerateTokenForApplicationData(string value, string application, string securityKey = "", int expiredInHours = 12)
        {
            var data = new Dictionary<string, object> { { "value", value } };
            return GenerateTokenForApplicationData(data, application, securityKey, expiredInHours);
        }
        /// <summary>
        /// Generates the token.
        /// sample: var token = Querplex.Tools.CryptoFunctions.GenerateTokenForApplication(new Dictionary<string, object> { { "account", account } }, "identity takeover");
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="application">The application.</param>
        /// <param name="securityKey">The security key.</param>
        /// <param name="expiredInHours">The expired in hours.</param>
        /// <returns></returns>
        public static string GenerateTokenForApplicationData(Dictionary<string, object> data, string application, string securityKey = "", int expiredInHours = 12)
        {
            if (data == null)
                data = new Dictionary<string, object> { };
            if (!data.ContainsKey("application"))
            {
                data.Add("application", application);
            }
            if (!data.ContainsKey("expires"))
            {
                data.Add("expires", DateTime.Now.AddHours(expiredInHours));
            }
            return GenerateTokenForData(data, securityKey);
        }
        /// <summary>
        /// Gets the token for the given data
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="securityKey">The security key.</param>
        /// <returns></returns>
        public static string GenerateTokenForData(Dictionary<string, object> data, string securityKey = "")
        {
            // default 
            if (string.IsNullOrEmpty(securityKey)) securityKey = securityKeyConst;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var hriJwtPayload = new JwtPayload { };
            // add the Data to JwtPayload
            foreach (var dataItem in data)
            {
                if (!hriJwtPayload.ContainsKey(dataItem.Key))
                    hriJwtPayload.Add(dataItem.Key, dataItem.Value);
                else
                    hriJwtPayload[dataItem.Key] = dataItem.Value;
            }
            var token = new JwtSecurityToken(
                new JwtHeader(creds),
                hriJwtPayload
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region GetApplicationDataFromToken, GetDataFromToken
        /// <summary>
        /// Gets the application value from token and check the applcation and the expired date.
        /// sample: var returnTokenValue = Querplex.Tools.CryptoFunctions.GetApplicationValueFromToken(token, "identity takeover");
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="application">The application.</param>
        /// <param name="securityKey">The security key.</param>
        /// <returns></returns>
        public static string GetApplicationValueFromToken(string token, string application, string securityKey = "")
        {
            var tokenData = GetPayloadFromToken(token, securityKey);
            CheckTokenIsValid(tokenData, application);
            var returnValue = tokenData["value"]?.ToString();
            return returnValue != null ? returnValue : "";
        }
        /// <summary>
        /// Gets the application data from token and check the applcation and the expired date.
        /// sample: var returnTokenData = Querplex.Tools.CryptoFunctions.GetApplicationDataFromToken(token, "identity takeover");
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="application">The application.</param>
        /// <param name="securityKey">The security key.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// GetCallerIdFromIdentityToken - application failed
        /// or
        /// GetCallerIdFromIdentityToken - Token expired
        /// </exception>
        public static Dictionary<string, object> GetApplicationDataFromToken(string token, string application, string securityKey = "")
        {
            var tokenData = GetPayloadFromToken(token, securityKey);
            CheckTokenIsValid(tokenData, application);
            return tokenData;
        }
        /// <summary>
        /// Gets the payload from token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="securityKey">The security key.</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPayloadFromToken(string token, string securityKey = "")
        {
            if (string.IsNullOrEmpty(securityKey)) securityKey = securityKeyConst;

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var theToken = securityTokenHandler.ReadJwtToken(token);
            return theToken.Payload;
        }
        /// <summary>
        /// Checks the token is valid.
        /// </summary>
        /// <param name="tokenData">The token data.</param>
        /// <param name="application">The application.</param>
        /// <exception cref="System.Exception">
        /// GetCallerIdFromIdentityToken - no token data
        /// or
        /// GetCallerIdFromIdentityToken - application failed
        /// or
        /// GetCallerIdFromIdentityToken - Token expired
        /// </exception>
        public static void CheckTokenIsValid(Dictionary<string, object> tokenData, string application)
        {
            if (tokenData == null)
                throw new Exception("GetCallerIdFromIdentityToken - no token data");

            if (application != tokenData["application"].ToString())
                throw new Exception("GetCallerIdFromIdentityToken - application failed");

            DateTime expires;
            if (!DateTime.TryParse(tokenData["expires"]?.ToString(), out expires))
                throw new Exception("GetCallerIdFromIdentityToken - Token expired");
            if (DateTime.Now > expires)
                throw new Exception("GetCallerIdFromIdentityToken - Token expired");
        }
        #endregion
    }
}

