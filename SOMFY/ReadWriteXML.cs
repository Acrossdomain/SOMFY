using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

namespace SOMFY
{
    internal class ReadWriteXML
    {
        protected internal string SERVERNAME = null;
        protected internal string EINVUSERNAME = null;
        protected internal string EINVPASSWORD = null;
        protected internal string EFUSERNAME = null;
        protected internal string EFPASSWORD = null;
        protected internal string SAGEDB = null;
        protected internal string SAA = null;
        protected internal string SAPSS = null;
        protected internal string SGSTIN = null;
        protected internal string BGSTIN = null;
        protected internal string VERSION = null;
        protected internal string CDKEY = null;
        protected internal string APIURL = null;
        protected internal string FLRDATE = null;
        // private static string configPassword = "SecretKey";
        private static string configPassword = "AcrossDomain";
        private static byte[] _salt = Encoding.ASCII.GetBytes("0123456789abcdef");
        XmlDocument xmldoc = new XmlDocument();

        protected internal bool ReadXML()
        {
            bool result = true;
            try
            {
                bool checkRes = File.Exists(@"SOMFYCRD.xml");
                if (checkRes == true)
                {
                    string xmlFile = File.ReadAllText(@"SOMFYCRD.xml");
                    xmldoc.LoadXml(xmlFile);
                    XmlNodeList dblist = xmldoc.SelectNodes("dbconfig");
                    foreach (XmlNode xn in dblist)
                    {
                        SERVERNAME = xn["SERVERNAME"].InnerText;
                        CDKEY = xn["CDKEY"].InnerText;
                        EINVUSERNAME = xn["EINVUSERNAME"].InnerText;
                        EINVPASSWORD = xn["EINVPASSWORD"].InnerText;

                        EFUSERNAME = xn["EFUSERNAME"].InnerText;
                        EFPASSWORD = xn["EFPASSWORD"].InnerText;

                        SAGEDB = xn["SAGEDB"].InnerText;
                        SAA = xn["SAA"].InnerText;
                        SAPSS = DecryptString(xn["SAPSS"].InnerText, configPassword);

                        SGSTIN = xn["SGSTIN"].InnerText;
                        BGSTIN = xn["BGSTIN"].InnerText;
                        VERSION = xn["VERSION"].InnerText;
                        APIURL = xn["APIURL"].InnerText;
                        FLRDATE = xn["FLRDATE"].InnerText;
                    }
                }
                else
                {
                    result = checkRes;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public string EncryptString(string plainText, string sharedSecret)
        {
            string result = null;
            RijndaelManaged aesAlg = null;

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    result = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return result;
        }

        public string DecryptString(string cipherText, string sharedSecret)
        {
            RijndaelManaged aesAlg = null;
            string result = null;

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return result;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }
            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}