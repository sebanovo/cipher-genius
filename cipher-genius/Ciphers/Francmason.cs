using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Francmason
    {
        public List<int> Cifrar(string textoClaro, int num)
        {
            string[] caracteres;
            if (num == 1)
            {
                caracteres = new string[]
                {
                     "A", "B", "C", "D", "E", "F", "G", "H", "I","J", "K", "L", "M",
                     "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
                };
            }
            else
            {
                caracteres = new string[]
                {
                     "[", "!", "?", "{", "}", "*", "♥", "♦", "♣", "♠", "≠", "#", "@",
                     "%", "&", "(", ")", "=", ">", "<", "0", "1", "2", "3", "4", "5"
                };
            }
            List<string> letras = new List<string>();

            for (int i = textoClaro.Length - 1; i >= 0; i--)
            {
                if (textoClaro[i] == 'Ñ' | textoClaro[i] == 'ñ')
                {
                    letras.Add('N'.ToString());
                }
                else
                {
                    letras.Add(textoClaro[i].ToString());
                }

            }

            List<int> listaPosiciones = new List<int>();

            foreach (string letra in letras)
            {
                int pos = Array.IndexOf(caracteres, letra.ToUpper());
                if (pos != -1) // Verifica si la letra está en caracteresMixtos
                {
                    listaPosiciones.Add(pos);
                }
            }

            listaPosiciones.Reverse();

            return listaPosiciones;
        }

        public List<string> Descifrar(int[] posiciones, int num)
        {
            List<string> listaTextoCifrado = new List<string>();
            int i = posiciones.Length - 1; // Comenzamos desde la última posición
            while (i >= 0)
            {
                int pos = posiciones[i];
                string nuevoCaracterCifrado;
                if (num == 1)
                {
                    nuevoCaracterCifrado = ConvertirAlfanumerico(pos);
                }
                else
                {
                    nuevoCaracterCifrado = ConvertirMixto(pos);
                }

                listaTextoCifrado.Add(nuevoCaracterCifrado);
                i--;
            }
            return listaTextoCifrado;
        }

        public String ConvertirMixto(int pos)
        {
            String[] caracteresMixtos = { "[", "<<", ">>", "{", "}", "*", "♥", "♦", "♣", "♣", "≠", "#", "@", "%", "&", "(", ")", "=", ">", "<", "0", "1", "2", "3", "4", "5", "]" };
            // Asegúrate de que pos esté dentro del rango válido
            if (pos >= 0 && pos < caracteresMixtos.Length)
            {
                return caracteresMixtos[pos];
            }
            else
            {
                return "Carácter no válido"; // O maneja el caso fuera de rango de otra manera
            }
        }

        public string ConvertirAlfanumerico(int pos)
        {
            String[] caracteresNormales = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            if (pos >= 0 && pos < caracteresNormales.Length)
            {
                return caracteresNormales[pos];
            }
            else
            {
                return "Carácter no válido"; // O maneja el caso fuera de rango de otra manera
            }
        }
    }
}
