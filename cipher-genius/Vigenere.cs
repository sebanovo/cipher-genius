using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Vigenere
    {
        private static readonly char[] abecedario = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ ".ToCharArray();
        private static readonly int abecedarioLength = abecedario.Length;

        private static int GetCharIndex(char c)
        {
            return Array.IndexOf(abecedario, char.ToUpper(c));
        }

        // cualquier cambio 

        private static char GetCharAt(int index)
        {
            return abecedario[index % abecedarioLength];
        }

        public string Cifrar(string mensaje, string clave)
        {
            // Remover espacios y convertir a mayúsculas
            mensaje = mensaje.Replace(" ", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            string result = string.Empty;
            int claveIndex = 0;

            foreach (char ch in mensaje)
            {
                if (char.IsLetter(ch) || ch == ' ')
                {
                    int mensajeIndex = GetCharIndex(ch);
                    int claveIndexValue = GetCharIndex(clave[claveIndex % clave.Length]);
                    int encryptedIndex = (mensajeIndex + claveIndexValue) % abecedarioLength;
                    result += GetCharAt(encryptedIndex);
                    claveIndex++;
                }
                else
                {
                    result += ch;
                }
            }
            return result;
        }

        // Método para descifrar el texto usando la clave
        public string Descifrar(string mensaje, string clave)
        {
            // Convertir a mayúsculas
            mensaje = mensaje.Replace(" ", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();

            string result = string.Empty;
            int claveIndex = 0;

            foreach (char ch in mensaje)
            {
                if (char.IsLetter(ch) || ch == ' ')
                {
                    int mensajeIndex = GetCharIndex(ch);
                    int claveIndexValue = GetCharIndex(clave[claveIndex % clave.Length]);
                    int decryptedIndex = (mensajeIndex - claveIndexValue + abecedarioLength) % abecedarioLength;
                    result += GetCharAt(decryptedIndex);
                    claveIndex++;
                }
                else
                {
                    result += ch;
                }
            }
            return result;
        }
    }
}
