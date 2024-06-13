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

        public int Modulo = 27;
        public int n;

        public char[] Alfabeto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public double[] ValoresDelAlfabeto = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };

        public string AnadirFinal(String textoSinEspacios)
        {
            int length = textoSinEspacios.Length;
            int remainder = length % n;

            if (remainder == 0)
            {
                return textoSinEspacios; //string es multiplo de m
            }

            int charsToAdd = n - remainder;

            StringBuilder sb = new StringBuilder(textoSinEspacios);

            for (int i = 0; i < charsToAdd; i++)
            {
                sb.Append('X');
            }
            return sb.ToString();
        }
        public int[,] MatrizAdjuntaModulo(int[,] m)
        {
            int[,] matrizAjuntaSinModulo = CalcularAdjunta(m);
            for (int i = 0; i < matrizAjuntaSinModulo.GetLength(0); i++)
            {
                for (int j = 0; j < matrizAjuntaSinModulo.GetLength(1); j++)
                {
                    matrizAjuntaSinModulo[i, j] = ModuloPositivo(matrizAjuntaSinModulo[i, j], Modulo);
                }
            }
            return matrizAjuntaSinModulo;
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
        public char ParameterIntReturnChar(int indice)
        {
            return Alfabeto[indice];
        }
        public int ParameterCharReturnInt(char caracter)
        {
            int x = -1;
            for (int i = 0; i < Alfabeto.Length; i++)
            {
                if (Alfabeto[i] == caracter)
                {
                    x = i;
                    break;
                }
            }
            return x;
        }
        public bool SonCoprimos(int a, int b)
        {
            return MCD(a, b) == 1;
        }

        public bool ClaveValida(String clave)
        {
            bool f = true;
            int[,] matrizClaveCargada = CargarMatrizClave(clave);

            //determinante parcial 
            int deparcial = CalcularDeterminante(matrizClaveCargada);
            //determinante modulo n
            int determinante = DeterminanteModulo(deparcial);

            int moduloInversoMultiplicativo = ModInverso(determinante, Modulo);

            //            Console.WriteLine("La determinantees " + determinante);
            //           Console.WriteLine("El  INVERSO multiplicativo modulo" + moduloInversoMultiplicativo);
            int[,] inversa = Inversa(matrizClaveCargada, deparcial);

            if (determinante == 0 || moduloInversoMultiplicativo == -1 || SonCoprimos(determinante, Modulo) == false || CumpleReglaDeLaInversa(matrizClaveCargada, inversa) == false)
            {
                f = false;
            }
            return f;
        }
        public int[,] CargarMatrizClave(String clave)
        {
            int[,] r = new int[n, n];
            int indice = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    r[i, j] = ParameterCharReturnInt(clave[indice]);
                    indice++;
                }
            }
            return r;

        }
        public List<List<int>> CargarPorBloques(String textoClaroSinEspacios)
        {

            List<List<int>> bloques = new List<List<int>>();
            int indice = 0;
            //quiero cargar en n bloque digamos
            /**
             [][][]   [][][] [][][] [][][] [][][] [][][]
             [][][]
             [][][]
             */
            for (int i = 0; i < textoClaroSinEspacios.Length / n; i++)
            {
                List<int> a = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    //  Console.WriteLine(textoClaroSinEspacios[indice] + "->" + ParameterCharReturnInt(textoClaroSinEspacios[indice]));
                    int x = ParameterCharReturnInt(textoClaroSinEspacios[indice]); ;
                    a.Add(x);
                    indice++;
                }
                bloques.Add(a);
            }

            return bloques;
        }




        public String Encriptar(String textClaro, String clave, int n)
        {
            this.n = n;
            String solucion = "";
            textClaro = textClaro.ToUpper();
            clave = clave.ToUpper();
            if (ClaveValida(clave))
            {
                String textoClaroSinEspacios = QuitarEspacios(textClaro);

                if (textoClaroSinEspacios.Length % n != 0)
                {
                    textoClaroSinEspacios = AnadirFinal(textoClaroSinEspacios);
                }
                List<List<int>> listaCargadaPorBloques = CargarPorBloques(textoClaroSinEspacios);

                int[,] MatrizClave = CargarMatrizClave(clave);

                List<List<char>> mulipliionYModulo = MultiplicionYModulo(listaCargadaPorBloques, MatrizClave);


                for (int i = 0; i < mulipliionYModulo.Count; i++)
                {
                    for (int j = 0; j < mulipliionYModulo[i].Count; j++)
                    {
                        solucion += mulipliionYModulo[i][j];
                    }
                }
            }
            return solucion;

        }

        public bool CumpleReglaDeLaInversa(int[,] clave, int[,] inversaClave)
        {
            int[,] resultado = MultiplicarMatrices(clave, inversaClave);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultado[i, j] = ModuloPositivo(resultado[i, j], Modulo);
                }
            }
            return EsMatrizIdentidad(resultado);
        }
        public bool EsMatrizIdentidad(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows != cols)
            {
                // Si no es una matriz cuadrada, no puede ser una matriz identidad
                return false;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == j)
                    {
                        // Verificar que los elementos de la diagonal principal sean 1
                        if (matrix[i, j] != 1)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // Verificar que todos los demás elementos sean 0
                        if (matrix[i, j] != 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public string Desencriptar(String criptograma, String clave, int n)
        {
            criptograma = criptograma.ToUpper();
            clave = clave.ToUpper();
            this.n = n;
            StringBuilder a = new StringBuilder();
            if (ClaveValida(clave))
            {



                List<List<int>> listaCargadaPorBloques = new List<List<int>>();
                int indice = 0;

                for (int i = 0; i < criptograma.Length / n; i++)
                {
                    List<int> nsima = new List<int>();
                    for (int j = 0; j < n; j++)
                    {
                        nsima.Add(ParameterCharReturnInt(criptograma[indice]));
                        indice++;
                    }
                    listaCargadaPorBloques.Add(nsima);
                }

                int[,] matrizClaveCargada = CargarMatrizClave(clave);

                int deteparcial = CalcularDeterminante(matrizClaveCargada);

                int[,] inversa = Inversa(matrizClaveCargada, deteparcial);
                //esta es el resultado
                List<char> r = new List<char>(); ;

                for (int i = 0; i < listaCargadaPorBloques.Count; i++)
                {
                    int[,] nsima = new int[1, n];

                    List<int> enteros = listaCargadaPorBloques[i];

                    for (int j = 0; j < n; j++)
                    {
                        nsima[0, j] = enteros[j];
                    }
                    //multiplicacion
                    int[,] multiplicacion = MultiplicarMatrices(nsima, inversa);

                    for (int k = 0; k < n; k++)
                    {
                        int modulo = ModuloPositivo(multiplicacion[0, k], Modulo);

                        r.Add(ParameterIntReturnChar(modulo));
                    }
                }

                for (int j = 0; j < r.Count; j++)
                {
                    a.Append(r[j]);
                }
            }
            return a.ToString();
        }


        public HashSet<String> Generaclave(int n, int cantidad)
        {
            this.n = n;

            HashSet<String> r = new HashSet<String>();
            int x = 0;
            while (x <= cantidad)
            {
                String valida = "";
                Random ra = new Random();
                for (int i = 0; i < n * n; i++)
                {
                    int va = ra.Next(0, 25);
                    valida += ParameterIntReturnChar(va);

                }
                // Console.WriteLine(valida);
                if (ClaveValida(valida))
                {
                    r.Add(valida);
                    x++;
                }
            }
            return r;

        }



        public void ImprimirMatriz(int[,] matrix)
        {
            int filas = matrix.GetLength(0);
            int columnas = matrix.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public int[,] MultiplicarMatrices(int[,] matriz1, int[,] matriz2)
        {

            if (matriz1.GetLength(1) != matriz2.GetLength(0))
            {
                throw new Exception("Las dimensiones de las matrices no son compatibles para la multiplicación.");
            }


            int filas = matriz1.GetLength(0);
            int columnas = matriz2.GetLength(1);


            int[,] matrizResultado = new int[filas, columnas];


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
        public List<List<char>> MultiplicionYModulo(List<List<int>> bloques, int[,] MatrizClave)
        {
            List<List<char>> resultado = new List<List<char>>();

            for (int i = 0; i < bloques.Count; i++)
            {
                int[,] horintal = new int[1, n];

                //matriz de bloques
                List<int> block = bloques[i];

                //esta es el la lista de lamuli0lica
                List<char> salida = new List<char>();

                for (int j = 0; j < block.Count; j++)
                {
                    horintal[0, j] = block[j];
                }
                //aqui se encunetra multiplicado

                int[,] muliplicado = MultiplicarMatrices(horintal, MatrizClave);

                //ImprimirMatriz(muliplicado);
                for (int k = 0; k < n; k++)
                {

                    salida.Add(ParameterIntReturnChar(ModuloPositivo(muliplicado[0, k], Modulo)));

                }
                //agregando a la lista
                resultado.Add(salida);

            }
            return resultado;
        }
        public int[,] Inversa(int[,] clave, int determinante)
        {
            //matrz Adjunta modular
            int[,] resultado = new int[n, n];
            int[,] matrizAdjuntaModulo = MatrizAdjuntaModulo(clave);


            //  Determinante Modular
            int deter = DeterminanteModulo(determinante);
            // Console.WriteLine(" la determinante es " +deter);
            int inversoModular = ModInverso(deter, Modulo);
            //Console.WriteLine("el inverso modular es "+inversoModular );
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultado[i, j] = matrizAdjuntaModulo[i, j] * inversoModular;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultado[i, j] = ModuloPositivo(resultado[i, j], Modulo);


                }
            }

            return resultado;
        }
        public bool VerificarPositividad(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int DeterminanteModulo(int a)
        {

            return ModuloPositivo(a, Modulo);
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

        public int[,] CalcularAdjunta(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[,] cofactores = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cofactores[i, j] = CalcularCofactor(matrix, i, j);
                }
            }
            return TransponerMatriz(cofactores);
        }


        public int CalcularCofactor(int[,] matrix, int p, int q)
        {
            int n = matrix.GetLength(0);
            int[,] submatriz = new int[n - 1, n - 1];
            int i = 0, j = 0;

            for (int fila = 0; fila < n; fila++)
            {
                for (int columna = 0; columna < n; columna++)
                {
                    if (fila != p && columna != q)
                    {
                        submatriz[i, j++] = matrix[fila, columna];

                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }

            return (int)Math.Pow(-1, p + q) * CalcularDeterminante(submatriz);
        }

        public int CalcularDeterminante(int[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (n == 1)
            {
                return matrix[0, 0];
            }

            if (n == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            int determinante = 0;

            for (int i = 0; i < n; i++)
            {
                determinante += matrix[0, i] * CalcularCofactor(matrix, 0, i);
            }

            return determinante;
        }


        public int[,] TransponerMatriz(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[,] transpuesta = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transpuesta[j, i] = matrix[i, j];
                }
            }

            return transpuesta;
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

    }
}






