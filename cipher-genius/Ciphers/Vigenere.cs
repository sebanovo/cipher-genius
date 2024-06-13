using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Vigenere
    {
        private Dictionary<char, int> abecedarioEspañol = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 9}, {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13},
             {'Ñ', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20},
             {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}
         };

        private char ObtenerLetraDeValor(int valor)
        {
            if (valor >= 0 && valor <= 26)
            {
                foreach (var kvp in abecedarioEspañol)
                {
                    if (kvp.Value == valor)
                    {
                        return kvp.Key;
                    }
                }
                throw new ArgumentException($"No se encontró una letra para el valor {valor}.");
            }
            else
            {
                throw new ArgumentOutOfRangeException("El valor debe estar entre 0 y 26.");
            }
        }


        public List<char> Cifrar(string textoClaro, string clave)
        {
            // Elimina los espacios en blanco y convierte todo a mayúsculas
            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            // Verifica si la clave contiene caracteres especiales
            foreach (char c in clave)
            {
                if (!abecedarioEspañol.ContainsKey(c))
                {
                    throw new ArgumentException("La clave no debe contener caracteres especiales.");
                }
            }

            // Inicializa la lista de letras
            List<char> listaLetras = new List<char>();

            // Recorre el texto claro en orden inverso
            for (int i = textoClaro.Length - 1; i >= 0; i--)
            {
                if (textoClaro[i] == 'Ñ' | textoClaro[i] == 'ñ')
                {
                    listaLetras.Add('N');
                }
                listaLetras.Add(textoClaro[i]);
            }

            List<char> listaCifrado = new List<char>();
            int k = 0;
            listaLetras.Reverse();
            foreach (char letra in listaLetras)
            {
                if (k >= clave.Length)
                {
                    k = 0;
                }
                int A = abecedarioEspañol[letra];
                int B = abecedarioEspañol[clave[k]];
                int num = (A + B) % 27;
                char cifrado = ObtenerLetraDeValor(num); // Obtén el carácter correspondiente
                listaCifrado.Add(cifrado);
                k++;
            }
            return listaCifrado;
        }

        public List<char> Descifrar(string textoClaro, string clave)
        {
            // Elimina los espacios en blanco y convierte todo a mayúsculas
            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            // Verifica si la clave contiene caracteres especiales
            foreach (char c in clave)
            {
                if (!abecedarioEspañol.ContainsKey(c))
                {
                    throw new ArgumentException("La clave no debe contener caracteres especiales.");
                }
            }

            // Inicializa la lista de letras
            List<char> listaLetras = new List<char>();

            // Recorre el texto claro en orden inverso
            for (int i = textoClaro.Length - 1; i >= 0; i--)
            {
                listaLetras.Add(textoClaro[i]);
            }

            List<char> listaCifrado = new List<char>();
            int k = 0;
            listaLetras.Reverse();
            foreach (char letra in listaLetras)
            {
                if (k >= clave.Length)
                {
                    k = 0;
                }
                int A = abecedarioEspañol[letra];
                int B = abecedarioEspañol[clave[k]];
                int num = ((A - B) % 27) + 27;
                if (num >= 27)
                {
                    num = num - 27;
                }
                char cifrado = ObtenerLetraDeValor(num); // Obtén el carácter correspondiente
                listaCifrado.Add(cifrado);
                k++;
            }
            return listaCifrado;
        }
    }
}
