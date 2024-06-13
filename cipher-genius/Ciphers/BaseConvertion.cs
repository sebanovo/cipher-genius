using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class BaseConvertion
    {
        public string ConvertFromBase10(int number, int baseTo)
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

        public int ConvertToBase10(string number, int baseFrom)
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

        public string Encrypt(string text, int baseTo)
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

        public string Decrypt(string encryptedText, int baseFrom)
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
    }
}
