using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class BoolLogic
    {
        public string Encrypt(string texto, string Key)
        {
            StringBuilder encriptarTexto = new StringBuilder();

            for (int i = 0; i < texto.Length; i++)
            {
                char encryptarCaracter = (char)(texto[i] ^ Key[i % Key.Length]);
                encriptarTexto.Append(encryptarCaracter);
            }

            return encriptarTexto.ToString();
        }

        public string Decrypt(string encriptarTexto, string clave)
        {
            return Encrypt(encriptarTexto, clave);
        }

        // pending
        //public string EncryptArray(string text, int[,] keyMatriz)
        //{
        //    StringBuilder encryptedText = new StringBuilder();
        //    int keySize = keyMatriz.GetLength(0);

        //    for (int i = 0; i < text.Length; i += keySize)
        //    {
        //        for (int j = 0; j < keySize; j++)
        //        {
        //            int sum = 0;

        //            for (int k = 0; k < keySize; k++)
        //            {
        //                sum += text[i + k] * keyMatriz[k, j];
        //            }

        //            encryptedText.Append((char)(sum % 256));
        //        }


        //    }

        //    return encryptedText.ToString();
        //}

        // pending
        //public string DecryptArray(string encryptedText, int[,] inverseKeyMatriz)
        //{
        //    StringBuilder decryptedText = new StringBuilder();
        //    int keySize = inverseKeyMatriz.GetLength(0);

        //    for (int i = 0; i < encryptedText.Length; i += keySize)
        //    {
        //        for (int j = 0; j < keySize; j++)
        //        {
        //            int sum = 0;

        //            for (int k = 0; k < keySize; k++)
        //            {
        //                sum += encryptedText[i + k] * inverseKeyMatriz[k, j];
        //            }

        //            decryptedText.Append((char)(sum % 256));
        //        }
        //    }

        //    return decryptedText.ToString();
        //}
    }
}
