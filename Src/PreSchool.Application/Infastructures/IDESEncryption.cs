using System;

namespace PreSchool.Application.Infastructures
{
    public interface IDESEncryption
    {
        string Decrypt(string encryptedText);
        string DecryptFile(string encryptedFilePath, string decryptedFilePath = "DecryptedFile");
        string DecryptOfDateAppended(string encryptedText);
        string Encrypt(string plainText);
        string EncryptFile(string filePath, string encryptedFilePath = "EncryptedFile");
        string EncryptWithDateAppended(string plainText);
    }
}