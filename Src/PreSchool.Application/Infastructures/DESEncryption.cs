using PreSchool.Application.Infastructures;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PreSchool.Infrastructure.Services
{
    public class DESEncryption : IDESEncryption
    {
        #region gloabal variables

        /// <summary>
        /// Key for encryption
        /// </summary>
        private string _key { get; set; }

        /// <summary>
        /// Initialization vector for encryption
        /// </summary>
        private string _iv { get; set; }

        // public static DESEncryption _instance;

        #endregion


        #region Constructor
        /// <summary>
        ///  COnstructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public DESEncryption()
        {
            _key = "jdsg43238sfsafdasdfasfasfsfagafAASGASFSFA7";
            _iv = "LJlsjflkjldfgj87l";
        }



        #endregion


        /// <summary>
        /// This method encrypt the plain text with the appending date before the plain text
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptWithDateAppended(string plainText)
        {
            plainText = DateTime.Now + "END" + plainText;

            // Convert the key to bytes (minimum 24 bytes)
            byte[] EncryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

            // Convert the IV to bytes (minimum 8 bytes)
            byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

            // Convert the plain text to byte array
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);

            //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            // Initialize the cryptosecvice provider
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();

            // Create new memory stream  to store the converted text
            MemoryStream mStream = new MemoryStream();

            // Create new crypto stream to store crypto text and encrypt using key and IV
            CryptoStream cStream = new CryptoStream(mStream, tDes.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();

            // Return the Encrypted string
            return Convert.ToBase64String(mStream.ToArray());
        }



        /// <summary>
        /// This method decrypt the date appended encrypted text and return plain text if date is todays date
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public string DecryptOfDateAppended(string encryptedText)
        {
            try
            {
                // Replace all the blank space with + to avoid error 
                encryptedText = encryptedText.Replace(" ", "+");

                // Convert the key to bytes (minimum 24 bytes)
                byte[] DecryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

                // Convert the IV to bytes (minimum 8 bytes)
                byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

                // Convert the encrypted text to byte array
                byte[] inputByte = Convert.FromBase64String(encryptedText);

                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, tDes.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByte, 0, inputByte.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                var plainText = encoding.GetString(ms.ToArray());
                var dateTime = plainText.Substring(0, plainText.IndexOf("END"));
                var date = DateTime.Parse(dateTime);
                if (date.Date != DateTime.Now.Date)
                    return null;

                return plainText.Substring(plainText.IndexOf("END") + ("END").Length);
            }
            catch (Exception)
            {
                return null;
            }
        }




        /// <summary>
        /// This method encrypt the plain text and return encrypted text
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {

            // Convert the key to bytes (minimum 24 bytes)
            byte[] EncryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

            // Convert the IV to bytes (minimum 8 bytes)
            byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

            // Convert the plain text to byte array
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);

            //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            // Initialize the cryptosecvice provider
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();

            // Create new memory stream  to store the converted text
            MemoryStream mStream = new MemoryStream();

            // Create new crypto stream to store crypto text and encrypt using key and IV
            CryptoStream cStream = new CryptoStream(mStream, tDes.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();

            // Return the Encrypted string
            return Convert.ToBase64String(mStream.ToArray());
        }



        /// <summary>
        /// This method decrypt the encrypted text and return plain text 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            try
            {
                // Replace all the blank space with + to avoid error 
                encryptedText = encryptedText.Replace(" ", "+");

                // Convert the key to bytes (minimum 24 bytes)
                byte[] DecryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

                // Convert the IV to bytes (minimum 8 bytes)
                byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

                // Convert the encrypted text to byte array
                byte[] inputByte = Convert.FromBase64String(encryptedText);

                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, tDes.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByte, 0, inputByte.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string EncryptFile(string filePath, string encryptedFilePath = "EncryptedFile")
        {

            // Convert the key to bytes (minimum 24 bytes)
            byte[] EncryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

            // Convert the IV to bytes (minimum 8 bytes)
            byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

            // Convert the plain text to byte array
            byte[] inputByte = File.ReadAllBytes(filePath);



            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = EncryptKey;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.None;
            tdes.IV = IV;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputByte, 0, inputByte.Length);
            tdes.Clear();

            File.WriteAllBytes(encryptedFilePath, resultArray);
            return encryptedFilePath;
        }


        public string DecryptFile(string encryptedFilePath, string decryptedFilePath = "DecryptedFile")
        {
            try
            {
                // Replace all the blank space with + to avoid error 
                //   encryptedText = encryptedText.Replace(" ", "+");

                // Convert the key to bytes (minimum 24 bytes)
                byte[] DecryptKey = Encoding.UTF8.GetBytes(_key.Substring(0, 24));

                // Convert the IV to bytes (minimum 8 bytes)
                byte[] IV = Encoding.ASCII.GetBytes(_iv.Substring(0, 8));

                // Convert the encrypted text to byte array
                byte[] inputByte = File.ReadAllBytes(encryptedFilePath);


                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = DecryptKey;
                tdes.Mode = CipherMode.CBC;
                tdes.Padding = PaddingMode.None;
                tdes.IV = IV;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputByte, 0, inputByte.Length);
                tdes.Clear();

                File.WriteAllBytes(decryptedFilePath, resultArray);
                return (decryptedFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}