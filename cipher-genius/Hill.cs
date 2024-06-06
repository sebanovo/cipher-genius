using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace One1
{
    public class Hill
    {
        public char[] Alfabeto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '_' };
        public double[] ValoresDelAlfabeto = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };
        public int Modulo;
        public double[,] MatrizA;
        public double[,] MatrizClave;
        public double[,] MatrizCriptograma;
        int n;
        //este es el comentadio de esta becha
        public Hill()
        {

        }

        public void run(String clave, int n)
        {
            Modulo = 27;
            this.n = n;
            MatrizClave = new double[n, n];
            CargarMatrizClave(clave);
        }

        public String Encriptar(String textClaro, String clave, int cuadrada)
        {
            String solucion = "";
            if (ClaveValida(clave))
            {
                textClaro = textClaro.ToUpper();
                String textoClaroSinEspacios = QuitarEspacios(textClaro);
                int cantidadComodines = CalcularCantidadComodines(textoClaroSinEspacios.Length, n);
                if (cantidadComodines != 0)
                {
                    textoClaroSinEspacios = AgregarComodines(textoClaroSinEspacios, n);
                }
                Console.WriteLine("la nueva longitud es =" + textoClaroSinEspacios.Length);
                //  Dimensionar la Matriz A que contiene los numeros de la matriz
                MatrizA = new double[cuadrada, (textoClaroSinEspacios.Length / cuadrada)];
                //  cargado por bloques MATRIZ A
                CargarPorBloques(textoClaroSinEspacios);
                Console.WriteLine("begin=========matriz cargado por bloques");
                ImprimirMatriz(MatrizA);
                Console.WriteLine("==========end");
                double[,] operacionIntermedia = MultiplicarMatrices(MatrizClave, MatrizA);
                Console.WriteLine("begin============el resulatado de la nulitplcaicon  ");
                ImprimirMatriz(operacionIntermedia);
                Console.WriteLine("==========end");
                //  Operacion Modulo
                MatrizCriptograma = OperacionModulo(operacionIntermedia);
                Console.WriteLine("begin============matriz ciptograma  ");
                ImprimirMatriz(MatrizCriptograma);
                Console.WriteLine("==========end");
                //  Extraemos del criptograma
                solucion = Extracccion();
            }
            return solucion;
        }


        public String Extracccion()
        {
            String criptograma = "";
            for (int j = 0; j < MatrizCriptograma.GetLength(1); j++)
            {

                for (int i = 0; i < MatrizCriptograma.GetLength(0); i++)
                {
                    int indice = (int)MatrizCriptograma[i, j];
                    criptograma += ParameterIntReturnChar(indice);
                }
            }
            return criptograma;
        }

        public String AgregarComodines(String texto, int cantidadComodines)
        {
            String textoConComodines = texto;
            for (int i = 0; i < cantidadComodines; i++)
            {
                textoConComodines += "_";
            }
            return textoConComodines;

        }
        public int CalcularCantidadComodines(int longitudTextoPlano, int cuadrada)
        {
            int cantComodines = 0;
            bool flag = false;
            if (longitudTextoPlano % cuadrada != 0)
            {
                while (flag == false)
                {
                    if ((longitudTextoPlano + cantComodines) % 3 == 0)
                    {
                        flag = true;
                        break;
                    }
                    cantComodines++;
                }
            }
            return cantComodines;
        }
        public int[] ConvetiAlfabetoIndices(String clave)
        {
            int[] valores = new int[clave.Length];//CREAMOS UN VECTOR DE TAMANO N CON LA DIMENSION DE 

            for (int i = 0; i < clave.Length; i++)
            {
                int indice = ParameterCharReturnInt(clave[i]);
                valores[i] = indice;
            }
            return valores;

        }

        public void CargarMatrizClave(String clave)
        {
            int[] valores = ConvetiAlfabetoIndices(clave.ToUpper());
            int indice = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrizClave[i, j] = valores[indice];
                    indice++;
                }
            }


        }
        public String QuitarEspacios(String s)
        {
            String nuevo = ""; ;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    nuevo += s[i];
                }
            }
            return nuevo;
        }
        public char ParameterIntReturnChar(int indice)
        {
            return Alfabeto[indice];
        }
        public int ParameterCharReturnInt(char k)
        {
            int x = -1;
            for (int i = 0; i < Alfabeto.Length; i++)
            {
                if (Alfabeto[i] == k)
                {
                    x = i;
                    break;
                }
            }
            return x;
        }
        //este metodo carga la matriz en bloques
        public void CargarPorBloques(String texto)
        {
            int indice = 0;
            for (int col = 0; col < MatrizA.GetLength(1); col++)
            {
                for (int row = 0; row < MatrizA.GetLength(0); row++)
                {
                    MatrizA[row, col] = ParameterCharReturnInt(texto[indice]);
                    indice++;
                }
            }

        }

        //  Verifica si la clave ingresa por el usuario es valida
        public bool ClaveValida(String clave)
        {
            bool f = true;
            int det = (int)CalcularDeterminante(MatrizClave);
            int modi = ModInverso(det, Modulo);
            Console.WriteLine("tiene la determinante " + det);
            Console.WriteLine("tiene la modulo" + modi);

            if (det == 0 || modi == -1 || SonCoprimos(det, Modulo) == false)
            {
                f = false;
            }
            return f;
        }



        // Función booleana para verificar si dos números son coprimos
        public bool SonCoprimos(int a, int b)
        {
            return MCD(a, b) == 1;
        }



        public double[,] Submatriz(double[,] matriz, int fila, int columna)
        {
            int n = matriz.GetLength(0);
            int m = matriz.GetLength(1);

            double[,] submatriz = new double[n - 1, m - 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i != fila && j != columna)
                    {
                        int newi = i < fila ? i : i - 1;
                        int newj = j < columna ? j : j - 1;
                        submatriz[newi, newj] = matriz[i, j];
                    }
                }
            }

            return submatriz;
        }

        public String ImprimirAlfabeto()
        {
            String cadena = "|";
            int k = 1;
            for (int i = 0; i < Alfabeto.Length; i++)
            {
                if (k >= 11)
                {
                    cadena += Alfabeto[i] + "  |";
                }
                else
                {
                    cadena += Alfabeto[i] + " |";
                }
                k++;

            }
            cadena += "\n|";
            for (int i = 0; i < Alfabeto.Length; i++)
            {
                cadena += i + " |";


            }
            return cadena;
        }
        public void ImprimirMatriz(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write(matriz[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public double[,] MultiplicarMatrices(double[,] matriz1, double[,] matriz2)
        {

            if (matriz1.GetLength(1) != matriz2.GetLength(0))
            {
                throw new Exception("Las dimensiones de las matrices no son compatibles para la multiplicación.");
            }


            int filas = matriz1.GetLength(0);
            int columnas = matriz2.GetLength(1);


            double[,] matrizResultado = new double[filas, columnas];


            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    for (int k = 0; k < matriz1.GetLength(1); k++)
                    {
                        matrizResultado[i, j] += matriz1[i, k] * matriz2[k, j];
                    }
                }
            }


            return matrizResultado;


        }
        public double[,] OperacionModulo(double[,] operacionIntermedia)

        {
            double[,] resultado = new double[operacionIntermedia.GetLength(0), operacionIntermedia.GetLength(1)];
            for (int i = 0; i < operacionIntermedia.GetLength(0); i++)
            {
                for (int j = 0; j < operacionIntermedia.GetLength(1); j++)
                {
                    double moduloEnesimo = (operacionIntermedia[i, j]) % Modulo;

                    resultado[i, j] = moduloEnesimo;

                }
            }
            return resultado;

        }


        public bool TieneInversa(double[,] matriz)
        {
            if (matriz.GetLength(0) != matriz.GetLength(1))
            {
                return false;
            }

            double determinante = CalcularDeterminante(matriz);

            if (determinante == 0)
            {
                return false;

            }

            return true;
        }

        public double[,] MatrizAdjuntiva(double[,] matriz)
        {
            int n = matriz.GetLength(0);

            // Calcular el determinante de la matriz
            double determinante = CalcularDeterminante(matriz);

            // Si el determinante es 0, la matriz no tiene inversa
            if (determinante == 0)
            {
                throw new Exception("LA MATRIZ NO TIENE INVERSA");
            }

            // Calcular la matriz adjunta
            double[,] adjunta = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    adjunta[i, j] = Cofactor(matriz, i, j) * Math.Pow(-1, i + j);
                }
            }

            // Transponer la matriz adjunta
            double[,] inversa = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inversa[i, j] = adjunta[j, i];
                }
            }

            return inversa;
        }

        // Función para calcular el determinante de una matriz

        public double[,] MultiplicarPorEscalar(double[,] matrix, int scalar)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;


                }
            }

            return result;
        }
        public double[,] ModMatrix(double[,] matrix, int mod)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] resultMatrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    resultMatrix[i, j] = ModuloPositivo((int)matrix[i, j], Modulo);
                }
            }

            return resultMatrix;
        }
        public int ModuloPositivo(int a, int b)
        {
            int result = a % b;
            if (result < 0)
            {
                result += b;
            }
            return result;
        }

        private double CalcularDeterminante(double[,] matriz)
        {
            int n = matriz.GetLength(0);

            if (n == 1)
            {
                return matriz[0, 0];
            }

            double determinante = 0;
            for (int i = 0; i < n; i++)
            {
                double cofactor = Cofactor(matriz, 0, i);
                determinante += matriz[0, i] * cofactor * Math.Pow(-1, i);
            }

            return determinante;
        }

        // Función para calcular el cofactor de un elemento de una matriz
        private double Cofactor(double[,] matriz, int fila, int columna)
        {
            int n = matriz.GetLength(0);
            double[,] submatriz = new double[n - 1, n - 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != fila && j != columna)
                    {
                        int newFila = i < fila ? i : i - 1;
                        int newColumna = j < columna ? j : j - 1;
                        submatriz[newFila, newColumna] = matriz[i, j];
                    }
                }
            }

            return CalcularDeterminante(submatriz);
        }


        public string Desencriptar(String texto, String clave)
        {
            StringBuilder a = new StringBuilder();
            int f = 0, c = 0;
            double[,] Ayuda = new double[n, texto.Length / n];
            for (int i = 0; i < texto.Length; i++)
            {
                if (f == n)
                {
                    f = 0;
                    c++;
                }
                Ayuda[f, c] = ParameterCharReturnInt(texto[i]);
                f++;
            }

            Console.WriteLine("la ayuda cargada en bloques");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < Ayuda.GetLength(1); j++)
                {

                    Console.Write(Ayuda[i, j] + "\t");

                }
                Console.WriteLine();

            }
            Console.WriteLine("la ayuda cargada en bloques");
            int dete = (int)CalcularDeterminante(MatrizClave);
            int inverso = ModInverso(dete, Modulo);

            double[,] adjunt = Adjunta(MatrizClave);

            double[,] matrix = MultiplicarPorEscalar(adjunt, inverso);
            double[,] nueva = ModMatrix(matrix, Modulo);// esta es la nueva clave

            Console.WriteLine("esta es la nueva matris");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    Console.Write(nueva[i, j] + " ");

                }
                Console.WriteLine();
            }
            //aqui se encuentra el criptograma

            double[,] textoClaro = MultiplicarMatrices(nueva, Ayuda);
            double[,] lista = ModMatrix(textoClaro, Modulo);

            Console.WriteLine("esta es el plato final");
            for (int j = 0; j < lista.GetLength(1); j++)
            {
                for (int i = 0; i < lista.GetLength(0); i++)
                {
                    a.Append(ParameterIntReturnChar((int)lista[i, j]));
                }
            }
            return a.ToString();
        }

        // Función para encontrar el MCD usando el algoritmo de Euclides
        public int MCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Función para encontrar la inversa modular utilizando el algoritmo extendido de Euclides
        public int ModInverso(int a, int m)
        {
            int m0 = m, t, q;
            int x0 = 0, x1 = 1;

            if (MCD(a, m) != 1)
                return -1; // No existe inversa modular

            while (a > 1)
            {
                q = a / m;
                t = m;

                // m es el resto ahora, procesa el próximo
                m = a % m;
                a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            // Asegúrate de que x1 sea positivo
            if (x1 < 0)
                x1 += m0;

            return x1;
        }

        //ALGORITMOS PARA CALCULAR LA CONJUNTA DE UNA MATRIZ
        public double Determinante(double[,] matriz)
        {
            int n = matriz.GetLength(0);
            if (n == 1)
            {
                return matriz[0, 0];
            }
            else if (n == 2)
            {
                return matriz[0, 0] * matriz[1, 1] - matriz[0, 1] * matriz[1, 0];
            }
            else
            {
                double determinante = 0;
                for (int i = 0; i < n; i++)
                {
                    double[,] subMatriz = ObtenerSubMatriz(matriz, 0, i);
                    determinante += matriz[0, i] * Determinante(subMatriz) * (i % 2 == 0 ? 1 : -1);
                }
                return determinante;
            }
        }

        // Función para obtener una submatriz excluyendo una fila y una columna específicas
        private double[,] ObtenerSubMatriz(double[,] matriz, int filaExcluir, int columnaExcluir)
        {
            int n = matriz.GetLength(0);
            double[,] subMatriz = new double[n - 1, n - 1];
            int filaSub = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == filaExcluir) continue;
                int columnaSub = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == columnaExcluir) continue;
                    subMatriz[filaSub, columnaSub] = matriz[i, j];
                    columnaSub++;
                }
                filaSub++;
            }
            return subMatriz;
        }

        // Función para calcular la matriz de cofactores
        private double[,] MatrizDeCofactores(double[,] matriz)
        {
            int n = matriz.GetLength(0);
            double[,] cofactores = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double[,] subMatriz = ObtenerSubMatriz(matriz, i, j);
                    cofactores[i, j] = Determinante(subMatriz) * ((i + j) % 2 == 0 ? 1 : -1);
                }
            }
            return cofactores;
        }

        // Función para transponer una matriz
        private double[,] Transponer(double[,] matriz)
        {
            int n = matriz.GetLength(0);
            double[,] transpuesta = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transpuesta[j, i] = matriz[i, j];
                }
            }
            return transpuesta;
        }

        // Función para calcular la adjunta de una matriz
        public double[,] Adjunta(double[,] matriz)
        {
            double[,] cofactores = MatrizDeCofactores(matriz);
            double[,] adjunta = Transponer(cofactores);
            return adjunta;
        }
    }
}






