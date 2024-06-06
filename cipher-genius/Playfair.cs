using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Two2
{
    public class Playfair
    {
        public const int Limite = 5;
        public char[,] Matriz = new char[Limite, Limite];

        public char[] CaracteresAlfabeticos = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public Playfair()
        {

        }

        public void run(String clave)
        {
            CargarClave(clave);
            CargarAlfabetoRestante();
        }

        public String ImprimirMatriz()
        {
            String resultado = "";
            for (int i = 0; i < Limite; i++)
            {
                for (int j = 0; j < Limite; j++)
                {
                    resultado += Matriz[i, j] + "\t"; ;
                }
                resultado += "\n";
            }
            return resultado;
        }

        public void CargarClave(String clave)
        {
            string claveMayuscula = clave.ToUpper();
            int columna = 0, fila = 0;
            for (int i = 0; i < clave.Length; i++)
            {
                if (BuscarCaracter(claveMayuscula[i]) == false)
                {
                    if (columna == Limite)
                    {
                        columna = 0; fila++;
                    }
                    Matriz[fila, columna] = claveMayuscula[i];
                    columna++;
                }
            }
        }
        public Boolean BuscarCaracter(char caracter)
        {
            for (int i = 0; i < Limite; i++)
            {
                for (int j = 0; j < Limite; j++)
                {
                    if (Matriz[i, j] == caracter)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void CargarAlfabetoRestante()
        {
            for (int i = 0; i < Limite; i++)
            {
                for (int j = 0; j < Limite; j++)
                {
                    if (Matriz[i, j] == '\0')
                    {
                        for (int k = 0; k < CaracteresAlfabeticos.Length; k++)
                        {
                            if (!BuscarCaracter(CaracteresAlfabeticos[k]))
                            {
                                Matriz[i, j] = CaracteresAlfabeticos[k];
                                break;
                            }
                        }
                    }
                }
            }
        }

        public int[] Coordenada(char k)
        {
            int[] r = new int[2];
            for (int i = 0; i < Limite; i++)
            {
                for (int j = 0; j < Limite; j++)
                {
                    if (Matriz[i, j] == k)
                    {
                        r[0] = i;
                        r[1] = j;
                        break;

                    }
                }
            }
            return r;
        }

        public String Encriptar(String texto, String clave)
        {

            int[] vectorCordenadaA = new int[2];
            int[] vectorCordenadaB = new int[2];
            char xA, xB;

            StringBuilder s = new StringBuilder();
            List<List<char>> lista = CargarPares(texto.ToUpper());


            for (int i = 0; i < lista.Count; i++)
            {
                List<char> nsima = lista[i];
                vectorCordenadaA = Coordenada(nsima[0]);
                vectorCordenadaB = Coordenada(nsima[1]);


                int x_1 = vectorCordenadaA[0];
                int y_1 = vectorCordenadaA[1];

                int x_2 = vectorCordenadaB[0];
                int y_2 = vectorCordenadaB[1];

                //si se encuetran en la misma fila
                if (x_1 == x_2)
                {
                    if (y_1 + 1 == 5)
                    {
                        xA = Matriz[x_1, 0];
                    }
                    else
                    {
                        xA = Matriz[x_1, y_1 + 1];
                    }
                    if (y_2 + 1 == 5)
                    {
                        xB = Matriz[x_2, 0];
                    }
                    else
                    {
                        xB = Matriz[x_2, y_2 + 1];
                    }
                    s.Append(xA);
                    s.Append(xB);
                }
                //si se encuentran en la misma columna
                else if (y_1 == y_2)
                {
                    if (x_1 + 1 == 5)
                    {
                        xA = Matriz[0, y_1];
                    }
                    else
                    {
                        xA = Matriz[x_1 + 1, y_1];
                    }

                    if (x_2 + 1 == 5)
                    {
                        xB = Matriz[0, y_2];
                    }
                    else
                    {
                        xB = Matriz[x_2 + 1, y_2];
                    }
                    s.Append(xA);
                    s.Append(xB);

                }
                else
                {
                    xA = Matriz[vectorCordenadaA[0], vectorCordenadaB[1]];
                    xB = Matriz[vectorCordenadaB[0], vectorCordenadaA[1]];
                    s.Append(xA);
                    s.Append(xB);

                }

            }

            return s.ToString();
        }

        public String Desencriptar(String texto, String clave)
        {

            int[] vectorCordenadaA = new int[2];
            int[] vectorCordenadaB = new int[2];
            char xA, xB;
            StringBuilder s = new StringBuilder();
            List<List<char>> lista = new List<List<char>>();
            int x = 0;
            while (x <= texto.Length - 1)
            {
                List<Char> li = new List<char>();
                li.Add(texto[x]);
                li.Add(texto[x + 1]);
                lista.Add(li);
                x = x + 2;
            }

            //for (int i = 0; i < lista.Count; i++)
            //{
            //    Console.Write("Lista {0}: ", i + 1); // Print the list index

            //    for (int j = 0; j < lista[i].Count; j++)
            //    {
            //        Console.Write(lista[i][j] + " "); // Print each character
            //    }

            //    Console.WriteLine(); // New line after each list
            //}

            for (int i = 0; i < lista.Count; i++)
            {
                List<char> nsima = lista[i];
                vectorCordenadaA = Coordenada(nsima[0]);
                vectorCordenadaB = Coordenada(nsima[1]);

                int x_1 = vectorCordenadaA[0];
                int y_1 = vectorCordenadaA[1];

                int x_2 = vectorCordenadaB[0];
                int y_2 = vectorCordenadaB[1];

                //si se encuetran en la misma fila
                if (x_1 == x_2)
                {
                    if (y_1 - 1 == -1)
                    {
                        xA = Matriz[x_1, 4];
                    }
                    else
                    {
                        xA = Matriz[x_1, y_1 - 1];
                    }

                    if (y_2 - 1 == -1)
                    {
                        xB = Matriz[y_2, 4];
                    }
                    else
                    {
                        xB = Matriz[x_1, y_2 - 1];
                    }

                    s.Append(xA);
                    s.Append(xB);

                }
                //si se encuentran en la misma columna
                else if (y_1 == y_2)
                {
                    if (x_1 - 1 == -1)
                    {
                        xA = Matriz[4, y_1];
                    }
                    else
                    {
                        xA = Matriz[x_1 - 1, y_1];
                    }

                    if (x_2 - 1 == -1)
                    {
                        xB = Matriz[4, y_2];
                    }
                    else
                    {
                        xB = Matriz[x_2 - 1, y_2];

                    }
                    s.Append(xA);
                    s.Append(xB);
                }
                else
                {
                    // Different row and column, swap positions
                    xA = Matriz[x_1, y_2];
                    xB = Matriz[x_2, y_1];
                    s.Append(xA);
                    s.Append(xB);
                }

            }
            return s.ToString();

        }

        public List<List<char>> CargarPares(String texto)
        {
            List<List<char>> lista = new List<List<char>>();
            String textSinEspacios = EliminarEspacios(texto);
            textSinEspacios = InsertarX(textSinEspacios);
            int x = 0;
            while (x < textSinEspacios.Length)
            {
                List<char> n = new List<char>();
                n.Add(textSinEspacios[x]);
                n.Add(textSinEspacios[x + 1]);

                // Agregamos a la lista de listas
                lista.Add(n);
                x += 2;
            }
            return lista;
        }

        public string EliminarEspacios(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }
            StringBuilder modifiedString = new StringBuilder();
            foreach (char character in texto)
            {

                if (!char.IsWhiteSpace(character))
                {

                    modifiedString.Append(character);
                }
            }

            return modifiedString.ToString();

        }

        public string InsertarX(string texto)
        {
            StringBuilder modifiedString = new StringBuilder();
            List<char> listaCaracteres = new List<char>();

            if (texto.Length >= 2)
            {
                foreach (char caracter in texto.ToUpper())
                {
                    listaCaracteres.Add(caracter);

                }
                while (listaCaracteres.Count != 0)
                {
                    if (listaCaracteres[0] == listaCaracteres[1])
                    {
                        modifiedString.Append(listaCaracteres[0]);
                        listaCaracteres.RemoveAt(0);
                        modifiedString.Append('X');
                    }
                    else
                    {
                        modifiedString.Append(listaCaracteres[0]);
                        modifiedString.Append(listaCaracteres[1]);
                        listaCaracteres.RemoveAt(0);
                        listaCaracteres.RemoveAt(0);
                    }

                    if ((listaCaracteres.Count) == 1)
                    {
                        listaCaracteres.Add('X');
                    }
                }

            }
            else if (texto.Length == 1)
            {
                modifiedString.Append(texto[0]);
                modifiedString.Append('X');
            }
            return modifiedString.ToString();

        }
        // ... other methods (omitted for brevity)
    }
}





