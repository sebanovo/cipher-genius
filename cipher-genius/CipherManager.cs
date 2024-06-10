using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cipher_genius
{
    class CipherManager
    {
        //-----------------------------------------------------------------------------
        //Adolfo Mendoza Ribera


        private Dictionary<char, int> abecedarioEspañol = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 9}, {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13},
             {'Ñ', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20},
             {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}
         };

        /// <summary>
        /// Realiza el cifrado César en un texto dado.
        /// </summary>
        /// <param name="texto">Texto a cifrar.</param>
        /// <param name="clave">Clave de cifrado (número de posiciones a desplazar).</param>
        /// <returns>Texto cifrado.</returns>
        public string CifradoCesar(string mensajeOriginal, int clave)
        {
            string textoCifrado = "";

            foreach (char caracter in mensajeOriginal)
            {
                if (char.IsLetter(caracter))
                {
                    char baseChar = char.IsUpper(caracter) ? 'A' : 'a';
                    int desplazamiento = (caracter - baseChar + clave) % 26;
                    char nuevoCaracter = (char)(baseChar + desplazamiento);
                    textoCifrado += nuevoCaracter;
                }
                else
                {
                    textoCifrado += caracter; // Conserva caracteres no alfabéticos sin cambios
                }
            }

            return textoCifrado;
        }


        public string DescifradoCesar(string textoCifrado, int clave)
        {
            string mensajeOriginal = "";

            foreach (char caracter in textoCifrado)
            {
                if (char.IsLetter(caracter))
                {
                    char baseChar = char.IsUpper(caracter) ? 'A' : 'a';
                    int desplazamiento = (caracter - baseChar - clave + 26) % 26; // Sumamos 26 para manejar valores negativos
                    char nuevoCaracter = (char)(baseChar + desplazamiento);
                    mensajeOriginal += nuevoCaracter;
                }
                else
                {
                    mensajeOriginal += caracter; // Conserva caracteres no alfabéticos sin cambios
                }
            }

            return mensajeOriginal;
        }

        public List<int> CifradorCesarMixto(string textoClaro, int num)
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

        public List<string> DescifradorCesarMixto(int[] posiciones, int num)
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


        public List<char> CifradoVigenere(string textoClaro, string clave)
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

        public List<char> DescifradoVigenere(string textoClaro, string clave)
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

        public int[,] GenerarMatriz()
        {
            int[,] matriz = new int[4, 25];
            int contador = 1;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 25; columna++)
                {
                    matriz[fila, columna] = contador;
                    contador++;
                }
            }

            // Asignar el número 00 al último elemento
            matriz[3, 24] = 0;

            return matriz;
        }

        public int[] ObtenerValoresColumna(int[,] matriz, int columna)
        {
            int filas = matriz.GetLength(0);
            int[] valores = new int[filas];

            for (int fila = 0; fila < filas; fila++)
            {
                valores[fila] = matriz[fila, columna];
            }

            return valores;
        }

        public int[] RotarFila(int[,] matriz, int fila, int posicionInicial)
        {
            int columnas = matriz.GetLength(1);
            int[] filaRotada = new int[columnas];

            for (int columna = 0; columna < columnas; columna++)
            {
                int nuevaPosicion = (columna + posicionInicial) % columnas;
                filaRotada[nuevaPosicion] = matriz[fila, columna];
            }

            return filaRotada;
        }

        public int[] RotarFilaNVeces(int[,] matriz, int fila, int n)
        {
            int columnas = matriz.GetLength(1);
            int[] filaOriginal = ObtenerValoresFila(matriz, fila);
            int[] filaRotada = new int[columnas];

            for (int columna = 0; columna < columnas; columna++)
            {
                int nuevaPosicion = (columna + n) % columnas;
                filaRotada[nuevaPosicion] = filaOriginal[columna];
            }

            return filaRotada;
        }

        public int[] ObtenerValoresFila(int[,] matriz, int fila)
        {
            int columnas = matriz.GetLength(1);
            int[] valores = new int[columnas];

            for (int columna = 0; columna < columnas; columna++)
            {
                valores[columna] = matriz[fila, columna];
            }

            return valores;
        }

        public void RotarFilaEnMatriz(int[,] matriz, int fila, int n)
        {
            int columnas = matriz.GetLength(1);
            int[] filaOriginal = ObtenerValoresFila(matriz, fila);
            int[] filaRotada = new int[columnas];

            for (int columna = 0; columna < columnas; columna++)
            {
                int nuevaPosicion = (columna + n) % columnas;
                filaRotada[nuevaPosicion] = filaOriginal[columna];
            }

            // Actualiza los valores en la matriz
            for (int columna = 0; columna < columnas; columna++)
            {
                matriz[fila, columna] = filaRotada[columna];
            }
        }

        public int BuscarNumeroEnMatriz(int[,] matriz, int numeroBuscado)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    if (matriz[fila, columna] == numeroBuscado)
                    {
                        // El número se encuentra en la fila 'fila' y columna 'columna'
                        return columna;
                    }
                }
            }

            // Si no se encuentra el número, puedes devolver un valor especial (por ejemplo, -1)
            return -1;
        }



        private Dictionary<char, int> abecedarioEspañolAux = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 8}, {'K', 9}, {'L', 10}, {'M', 11}, {'N', 12},
             {'Ñ', 12}, {'O', 13}, {'P', 14}, {'Q', 15}, {'R', 16}, {'S', 17}, {'T', 18},
             {'U', 19}, {'V', 20}, {'W', 21}, {'X', 22}, {'Y', 23}, {'Z', 24}
        };

        public char ObtenerLetraDeValorAux(int valor)
        {
            if (valor >= 0 && valor <= 24)
            {
                foreach (var kvp in abecedarioEspañolAux)
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
                throw new ArgumentOutOfRangeException("El valor debe estar entre 0 y 24.");
            }
        }


        public int ObtenerValorLetraAux(char letra)
        {
            // Convierte la letra a mayúscula para asegurarte de que coincida con las claves del diccionario
            letra = char.ToUpper(letra);

            if (abecedarioEspañolAux.ContainsKey(letra))
            {
                return abecedarioEspañolAux[letra];
            }
            else
            {
                // Si la letra no está en el diccionario, puedes manejarlo como desees (por ejemplo, devolver -1)
                return -1;
            }
        }

        public int EscojerAleatorio(int[] columna)
        {
            if (columna == null || columna.Length == 0)
            {
                throw new ArgumentException("El array no puede estar vacío.");
            }

            // Genera un índice aleatorio dentro del rango de los elementos del array
            Random random = new Random();
            int indiceAleatorio = random.Next(0, columna.Length);

            // Devuelve el elemento correspondiente al índice aleatorio
            return columna[indiceAleatorio];
        }

        private void ProcesarClave(int[,] matriz, string clave)
        {
            for (int i = 0; i < clave.Length; i++)
            {
                char letra = clave[i];
                int num = this.ObtenerValorLetraAux(letra);
                int columna = this.BuscarNumeroEnMatriz(matriz, (num + 1 + 25 * i));
                this.RotarFilaEnMatriz(matriz, i, columna);
            }
        }

        public List<int> CifradorHomofono(int[,] matriz, string palabra, string clave)
        {
            // Convertir la palabra a mayúsculas y eliminar espacios en blanco
            palabra = palabra.ToUpper().Replace(" ", "");

            // Convertir la clave a mayúsculas
            clave = clave.ToUpper();

            this.ProcesarClave(matriz, clave);

            List<int> listaTextoCifrado = new List<int>();
            for (int i = 0; i < palabra.Length; i++)
            {
                char letra = palabra[i];
                int num = this.ObtenerValorLetraAux(letra);
                int[] listaColumna = this.ObtenerValoresColumna(matriz, num);
                int nuevoNumero = this.EscojerAleatorio(listaColumna);
                listaTextoCifrado.Add(nuevoNumero);
            }
            return listaTextoCifrado;
        }

        public List<char> DescifradorHomofono(int[,] matriz, List<int> textoCifrado, string clave)
        {
            // Convertir la clave a mayúsculas
            clave = clave.ToUpper();

            this.ProcesarClave(matriz, clave);
            List<char> textoClaro = new List<char>();
            foreach (int num in textoCifrado)
            {
                int columna = this.BuscarNumeroEnMatriz(matriz, num);
                char letra = this.ObtenerLetraDeValorAux(columna);
                textoClaro.Add(letra);
            }
            return textoClaro;
        }


        //-----------------------------------------------------------
        //-----------------------otra parte-----------------//

        public List<char> CifradorClaveContinua(string textoClaro, string clave)
        {
            // Elimina los espacios en blanco y convierte todo a mayúsculas
            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            if (clave.Length >= textoClaro.Length)
            {
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
                    int A = abecedarioEspañol[letra];
                    int B = abecedarioEspañol[clave[k]];
                    int num = (A + B) % 27;
                    char cifrado = ObtenerLetraDeValor(num); // Obtén el carácter correspondiente
                    listaCifrado.Add(cifrado);
                    k++;
                }
                return listaCifrado;
            }
            else
            {
                throw new ArgumentException("La clave debe tener mayor o igual longitud que el texto claro");
            }

        }

        public List<char> DescifradorClaveContinua(string textoClaro, string clave)
        {
            // Elimina los espacios en blanco y convierte todo a mayúsculas
            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
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

        /*
        public Dictionary<char, int> CodigoBaudot = new Dictionary<char, binary>
         {
             {'A', 00011}, {'B', 11001}, {'C', 01110}, {'D', 01001}, {'E', 00001}, {'F', 01101}, {'G', 11010},
             {'H', 10100}, {'I', 00110}, {'J', 01011}, {'K', 01111}, {'L', 10010}, {'M', 11100}, {'N', 01100},
             {'O', 11000}, {'P', 10110}, {'Q', 10111}, {'R', 01010}, {'S', 00101}, {'T', 10000},
             {'U', 00111}, {'V', 11110}, {'W', 10011}, {'X', 11101}, {'Y', 10101}, {'Z', 10001}
         };
        */

        public Dictionary<char, int> CodigoBaudot = new Dictionary<char, int>
        {
             {'A', 3}, {'B', 25}, {'C', 14}, {'D', 9}, {'E', 1}, {'F', 13}, {'G', 26},
             {'H', 20}, {'I', 6}, {'J', 11}, {'K', 15}, {'L', 18}, {'M', 28}, {'N', 12},
             {'O', 24}, {'P', 22}, {'Q', 23}, {'R', 10}, {'S', 5}, {'T', 16},
             {'U', 7}, {'V', 30}, {'W', 19}, {'X', 29}, {'Y', 21}, {'Z', 17}
        };

        private char ObtenerLetraDeBinario(int valor)
        {
            if (valor >= 0 && valor <= 30)
            {
                foreach (var kvp in CodigoBaudot)
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
                throw new ArgumentOutOfRangeException("El valor debe estar entre 0 y 30.");
            }
        }

        private int ObtenerBinarioDeLetra(char letra)
        {
            if (CodigoBaudot.ContainsKey(letra))
            {
                return CodigoBaudot[letra];
            }
            else
            {
                throw new ArgumentException($"No se encontró un valor binario para la letra '{letra}'.");
            }
        }


        public List<char> CifradorVernam(string textoClaro, string clave)
        {
            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            if (textoClaro.Length <= clave.Length)
            {
                List<char> listaCifrado = new List<char>();

                for (int i = 0; i < textoClaro.Length; i++)
                {
                    int A = this.ObtenerBinarioDeLetra(textoClaro[i]);
                    int B = this.ObtenerBinarioDeLetra(clave[i]);
                    int C = A ^ B;
                    char letraCifrado = this.ObtenerLetraDeBinario(C);
                    listaCifrado.Add(letraCifrado);
                }
                return listaCifrado;
            }
            else
            {
                throw new ArgumentException($"La longitud de la clave debe ser menor o igual a la longitud del texto en claro");
            }
        }

        public List<char> DescifradorVernam(List<int> textoCifrado, string clave)
        {
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            if (textoCifrado.Count < clave.Length)
            {
                List<char> listaCifrado = new List<char>();

                for (int i = 0; i < textoCifrado.Count; i++)
                {
                    int A = textoCifrado[i];
                    int B = this.ObtenerBinarioDeLetra(clave[i]);
                    int C = A ^ B;
                    char letraCifrado = this.ObtenerLetraDeBinario(C);
                    listaCifrado.Add(letraCifrado);
                }
                return listaCifrado;
            }
            else
            {
                throw new ArgumentException($"La longitud de la clave debe ser menor o igual a la longitud del texto en claro");
            }
        }

    }

    /*******************Adolfo FIN **********************/
}