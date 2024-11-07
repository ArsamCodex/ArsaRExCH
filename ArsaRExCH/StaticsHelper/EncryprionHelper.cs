using System.Security.Cryptography;
using System.Text;

namespace ArsaRExCH.StaticsHelper
{
    public class EncryptionHelper
    {
        private readonly string Key = "YourSecretKey123"; // AES key must be 16 bytes for AES-128

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key using UTF-8 encoding and the given string Key
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.GenerateIV(); // Generate a random IV (Initialization Vector)
                byte[] iv = aesAlg.IV;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv))
                using (var msEncrypt = new MemoryStream())
                {
                    // Prepend the IV to the encrypted content for decryption later
                    msEncrypt.Write(iv, 0, iv.Length);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // Write the plain text to the stream
                        swEncrypt.Write(plainText);
                    }

                    // Return the encrypted data as a Base64 string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText); // Convert the Base64 string back to bytes

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key); // Set the same key for decryption

                // Extract the IV from the first 16 bytes of the fullCipher (since AES block size is 128 bits / 16 bytes)
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aesAlg.IV = iv; // Set the IV in the AES algorithm

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted content and return it as a string
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
