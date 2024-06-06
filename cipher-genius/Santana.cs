using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    internal class Santana
    {
        //TRANSFORMACION POR CONVERSION DE BASE
        public static string ConvertFromBase10(int number, int baseTo)
        {
            StringBuilder result = new StringBuilder();

            while (number > 0)
            {
                int resto = number % baseTo;
                char digit = resto >= 10 ? (char)('A' + (resto - 10)) : (char)('0' + resto);
                result.Insert(0, digit);
                number /= baseTo;
            }

            return result.ToString();
        }

        public static int ConvertToBase10(string number, int baseFrom)
        {
            int base10 = 0;

            for (int i = 0; i < number.Length; i++)
            {
                char digit = number[number.Length - 1 - i];
                int value = digit >= '0' && digit <= '9' ? digit - '0' : digit - 'A' + 10;
                base10 += value * (int)Math.Pow(baseFrom, i);
            }

            return base10;
        }

        public static string Encryptar(string text, int baseTo)
        {
            StringBuilder encriptarTexto = new StringBuilder();

            foreach (char c in text)
            {
                int valorAscci = (int)c;
                string baseValor = ConvertFromBase10(valorAscci, baseTo);
                encriptarTexto.Append(baseValor).Append(" ");
            }
            return encriptarTexto.ToString().Trim();
        }

        public static string Decrypt(string encryptedText, int baseFrom)
        {
            StringBuilder decryptedText = new StringBuilder();
            string[] baseValues = encryptedText.Split(' ');

            foreach (string baseValue in baseValues)
            {
                int asciiValue = ConvertToBase10(baseValue, baseFrom);
                char c = (char)asciiValue;
                decryptedText.Append(c);
            }

            return decryptedText.ToString();
        }

        //TRANSFORMACION POR CONVERSION BOOLEANA
        public static string EncryptarBool(string texto, string Key)
        {
            StringBuilder encriptarTexto = new StringBuilder();

            for (int i = 0; i < texto.Length; i++)
            {
                char encryptarCaracter = (char)(texto[i] ^ Key[i % Key.Length]);
                encriptarTexto.Append(encryptarCaracter);
            }

            return encriptarTexto.ToString();

        }

        public static string Desencriptar(string encriptarTexto, string clave)
        {
            return EncryptarBool(encriptarTexto, clave);
        }

        public static string EncriptarMatriz(string text, int[,] keyMatriz)
        {
            StringBuilder encryptedText = new StringBuilder();
            int keySize = keyMatriz.GetLength(0);

            for (int i = 0; i < text.Length; i += keySize)
            {
                for (int j = 0; j < keySize; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < keySize; k++)
                    {
                        sum += text[i + k] * keyMatriz[k, j];
                    }

                    encryptedText.Append((char)(sum % 256));
                }


            }

            return encryptedText.ToString();
        }

        public static string DesencriptarMatriz(string encryptedText, int[,] inverseKeyMatriz)
        {
            StringBuilder decryptedText = new StringBuilder();
            int keySize = inverseKeyMatriz.GetLength(0);

            for (int i = 0; i < encryptedText.Length; i += keySize)
            {
                for (int j = 0; j < keySize; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < keySize; k++)
                    {
                        sum += encryptedText[i + k] * inverseKeyMatriz[k, j];
                    }

                    decryptedText.Append((char)(sum % 256));
                }
            }

            return decryptedText.ToString();
        }
    }
}
