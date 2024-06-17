using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Vernam
    {
        public string Encrypt(string mensaje, string llave)
        {
            string mensajeBinario = ConvertToBinary(mensaje);
            string llaveBinaria = ConvertToBinary(llave);
            string resultadoXOR = XORBinaryStrings(mensajeBinario, llaveBinaria);
            return ConvertFromBinary(resultadoXOR);
        }

        public string Decrypt(string mensajeCifrado, string llave)
        {
            string mensajeCifradoBinario = ConvertToBinary(mensajeCifrado);
            string llaveBinaria = ConvertToBinary(llave);
            string resultadoXOR = XORBinaryStrings(mensajeCifradoBinario, llaveBinaria);
            return ConvertFromBinary(resultadoXOR);
        }

        public string ConvertToBinary(string text)
        {
            StringBuilder binary = new StringBuilder();
            foreach (char c in text)
            {
                binary.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return binary.ToString();
        }

        public string ConvertFromBinary(string binary)
        {
            StringBuilder text = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteString = binary.Substring(i, 8);
                char character = (char)Convert.ToInt32(byteString, 2);
                text.Append(character);
            }
            return text.ToString();
        }

        public string XORBinaryStrings(string bin1, string bin2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bin1.Length; i++)
            {
                result.Append(bin1[i] == bin2[i] ? '0' : '1');
            }
            return result.ToString();
        }
    }
}
