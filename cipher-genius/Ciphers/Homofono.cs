using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Homofono
    {
        private Dictionary<char, int> abecedarioEspañolAux = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 8}, {'K', 9}, {'L', 10}, {'M', 11}, {'N', 12},
             {'Ñ', 12}, {'O', 13}, {'P', 14}, {'Q', 15}, {'R', 16}, {'S', 17}, {'T', 18},
             {'U', 19}, {'V', 20}, {'W', 21}, {'X', 22}, {'Y', 23}, {'Z', 24}
        };

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

        public List<int> Cifrar(int[,] matriz, string palabra, string clave)
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

        public List<char> Descifrar(int[,] matriz, List<int> textoCifrado, string clave)
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
    }
}
