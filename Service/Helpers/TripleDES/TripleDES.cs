using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FoodSys.Service.Helper.TripleDES
{
    public class TripleDES : ITripleDES
    {
        #region Methods
     
        public string Encrypt(string toEncrypt, bool useHashing = true)
        {
            #region Declare return type with initial value
            string result = string.Empty;
            #endregion
            try
            {
                byte[] keyArray;
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConfigHelper.TripleDESKey));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(ConfigHelper.TripleDESKey);
                }
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (System.Exception exception)
            {
                //Logger.Instance.WriteLog(logType: LogType.Error, methodBase: MethodBase.GetCurrentMethod(), exception: exception);
            }
            return result;
        }
   
        public string Decrypt(string cipherString, bool useHashing = true)
        {
            #region Declare return type with initial value
            string result = string.Empty;
            #endregion
            try
            {
                byte[] keyArray;
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConfigHelper.TripleDESKey));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(ConfigHelper.TripleDESKey);
                }
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (System.Exception exception)
            {
                // Logger.Instance.WriteLog(logType: LogType.Error, methodBase: MethodBase.GetCurrentMethod(), exception: exception);
            }
            return result;
        }

        public string GenerateRandomPassword()
        {

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 9;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        #endregion
    }
}
