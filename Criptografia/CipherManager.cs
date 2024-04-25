using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Criptografia
{
    class CipherManager
    {
        public string CifrarPorGrupos(string mensaje, int p, int[] permutacion)
        {
            // Convertir el mensaje en un array de caracteres
            mensaje = mensaje.Replace(" ", "");
            char[] mensajeArray = mensaje.ToCharArray();
            string mensajeCifrado = "";

            // Calcular el número de bloques
            int bloques = (int)Math.Ceiling((double)mensajeArray.Length / p);

            // Rellenar el mensaje con caracteres adicionales si es necesario
            int caracteresFaltantes = bloques * p - mensajeCifrado.Length;
            for (int i = 0; i < caracteresFaltantes; i++)
            {
                mensajeCifrado += " ";
            }

            // Iterar sobre cada bloque
            for (int i = 0; i < bloques; i++)
            {
                // Obtener el bloque actual
                string bloque = mensaje.Substring(i * p, Math.Min(p, mensaje.Length - i * p));

                // Aplicar la permutación a los caracteres del bloque
                string bloquePermutado = "";
                for (int j = 0; j < p; j++)
                {
                    if (permutacion[j] < bloque.Length)
                    {
                        bloquePermutado += bloque[permutacion[j]];
                    }
                }

                // Agregar el bloque permutado al mensaje cifrado
                mensajeCifrado += bloquePermutado;
            }

            return mensajeCifrado;
        }
        public string DescifrarPorGrupos(string mensaje, int p, int[] permutacion)
        {
            // Calcular el número de bloques
            int bloques = (int)Math.Ceiling((double)mensaje.Length / p);

            // Crear la permutación inversa
            int[] permutacionInversa = new int[p];
            for (int i = 0; i < p; i++)
            {
                permutacionInversa[permutacion[i]] = i;
            }

            string mensajeDescifrado = "";

            // Iterar sobre cada bloque
            for (int i = 0; i < bloques; i++)
            {
                // Obtener el bloque cifrado actual
                string bloqueCifrado = mensaje.Substring(i * p, Math.Min(p, mensaje.Length - i * p));

                // Aplicar la permutación inversa a los caracteres del bloque
                string bloqueDescifrado = "";
                for (int j = 0; j < p; j++)
                {
                    if (permutacionInversa[j] < bloqueCifrado.Length)
                    {
                        bloqueDescifrado += bloqueCifrado[permutacionInversa[j]];
                    }
                }

                // Agregar el bloque descifrado al mensaje final
                mensajeDescifrado += bloqueDescifrado;
            }

            // Eliminar los caracteres adicionales al final del mensaje descifrado
            mensajeDescifrado = mensajeDescifrado.Trim();

            return mensajeDescifrado;
        }

        public string PrimerCifrado(string mensaje, string clave, ref DataGridView dataGridView1)
        {
            clave = clave.Replace(" ", "");
            mensaje = mensaje.Replace(" ", "");

            int nc = clave.Length;
            int nf = (int)Math.Ceiling((double)mensaje.Length / clave.Length);

            // Matriz intermedia
            char[,] matriz = new char[nf, nc];
            string primerCifrado = "";

            int indexMensaje = 0;
            for (int f = 0; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
                {
                    if (indexMensaje < mensaje.Length)
                    {
                        matriz[f, c] = mensaje[indexMensaje];
                        indexMensaje++;
                    }
                    else
                    {
                        matriz[f, c] = 'X';
                    }
                }
            }

            LlenarDataGridViewV2(matriz, nf, nc, ref dataGridView1);

            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    primerCifrado += matriz[f, c];
                }
            }

            return primerCifrado;
        }

        public string SegundoCifrado(string cifradoIntermedio, string clave, ref DataGridView dataGridView1, ref DataGridView dataGridView2)
        {
            clave = clave.Replace(" ", "");
            cifradoIntermedio = cifradoIntermedio.Replace(" ", "");

            int nf = (int)Math.Ceiling((double)(clave.Length + cifradoIntermedio.Length) / clave.Length);
            int nc = clave.Length;

            string contenido = clave + cifradoIntermedio;
            int indexContenido = 0;

            // Matriz final
            char[,] matriz = new char[nf, nc];
            string mensajeCifrado = "";

            for (int f = 0; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
                {
                    if (indexContenido < contenido.Length)
                    {
                        matriz[f, c] = contenido[indexContenido];
                        indexContenido++;
                    }
                    else
                    {
                        matriz[f, c] = 'X';
                    }
                }
            }

            LlenarDataGridViewV2(matriz, nf, nc, ref dataGridView1);

            // Intercambiar las columnas
            for (int c1 = 0; c1 < nc; c1++)
            {
                for (int p = 0; p < nc - 1; p++)
                {
                    if (matriz[0, p] > matriz[0, p + 1])
                    {
                        for (int f1 = 0; f1 < nf; f1++)
                        {
                            char temp = matriz[f1, p];
                            matriz[f1, p] = matriz[f1, p + 1];
                            matriz[f1, p + 1] = temp;
                        }
                    }
                }
            }

            LlenarDataGridViewV2(matriz, nf, nc, ref dataGridView2);

            // Agregar la clave
            for (int c = 0; c < nc; c++)
            {
                for (int f = 1; f < nf; f++)
                {
                    mensajeCifrado += matriz[f, c];
                }
            }

            return mensajeCifrado;
        }

        public string PrimerDescrifrado(string mensajeCifrado, string clave, ref DataGridView dataGridView1, ref DataGridView dataGridView2)
        {
            string claveOriginal = clave;
            clave = new string(clave.Replace(" ", "").OrderBy(c => c).ToArray());
            mensajeCifrado = mensajeCifrado.Replace(" ", "").Trim();

            int nf = (int)Math.Ceiling((double)(clave.Length + mensajeCifrado.Length) / clave.Length);
            int nc = clave.Length;
            char[,] matriz = new char[nf, nc];
            string mensajeDescifrado = "";

            int indexClave = 0;
            int indexMensajeCifrado = 0;

            // Preparar la matriz con la clave
            for (int f = 0; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
                {
                    if (indexClave >= clave.Length)
                    {
                        matriz[f, c] = ' ';
                    }
                    else
                    {
                        matriz[f, c] = clave[indexClave];
                        indexClave++;
                    }
                }
            }

            // Añadir el mensaje cifrado
            for (int c = 0; c < nc; c++)
            {
                for (int f = 1; f < nf; f++)
                {
                    matriz[f, c] = mensajeCifrado[indexMensajeCifrado];
                    indexMensajeCifrado++;
                }
            }

            LlenarDataGridViewV2(matriz, nf, nc, ref dataGridView1);

            // Intercambiar las columnas
            for (int c1 = 0; c1 < nc; c1++)
            {
                for (int p = 0; p < nc - 1; p++)
                {
                    if (claveOriginal.IndexOf(matriz[0, p]) > claveOriginal.IndexOf(matriz[0, p + 1]))
                    {
                        for (int f1 = 0; f1 < nf; f1++)
                        {
                            char temp = matriz[f1, p];
                            matriz[f1, p] = matriz[f1, p + 1];
                            matriz[f1, p + 1] = temp;
                        }
                    }
                }
            }

            LlenarDataGridViewV2(matriz, nf, nc, ref dataGridView2);

            // Encontrar el mensaje
            for (int f = 1; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
                {
                    mensajeDescifrado += matriz[f, c];
                }
            }

            return mensajeDescifrado;
        }
        public string SegundoDescifrado(string descifradoIntermedio, string clave, ref DataGridView dataGridView1)
        {
            clave = clave.Replace(" ", "");
            descifradoIntermedio = descifradoIntermedio.Replace(" ", "");

            int nc = clave.Length;
            int nf = (int)Math.Ceiling((double)descifradoIntermedio.Length / clave.Length);
            string[][] matriz = new string[nf][];

            int indexDescifradoIntermedio = 0;
            string mensajeDescifrado = "";

            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    if (f >= matriz.Length || matriz[f] == null)
                    {
                        matriz[f] = new string[nc];
                    }
                    if (indexDescifradoIntermedio < descifradoIntermedio.Length)
                    {
                        matriz[f][c] = descifradoIntermedio[indexDescifradoIntermedio].ToString();
                        indexDescifradoIntermedio++;
                    }
                    else
                    {
                        matriz[f][c] = "";
                    }
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

            for (int f = 0; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
                {
                    mensajeDescifrado += matriz[f][c];
                }
            }

            return mensajeDescifrado;
        }



        public string CifrarPorFilas(string mensaje, string clave, ref DataGridView dataGridView1, ref DataGridView dataGridView2)
        {
            clave = clave.Replace(" ", "");
            mensaje = mensaje.Replace(" ", "");

            int nc = (int)Math.Ceiling((double)(clave.Length + mensaje.Length) / clave.Length);
            int nf = clave.Length;
            string contenido = clave + mensaje;
            int indexContenido = 0;

            string[][] matriz = new string[nf][];
            string mensajeCifrado = "";

            // Llenar la matriz
            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    if (f >= matriz.Length || matriz[f] == null)
                    {
                        matriz[f] = new string[nc];
                    }
                    if (indexContenido >= contenido.Length)
                    {
                        matriz[f][c] = "X";
                    }
                    else
                    {
                        matriz[f][c] = contenido[indexContenido].ToString();
                        indexContenido++;
                    }
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);


            // Intercambiar las filas
            for (int f1 = 0; f1 < nf; f1++)
            {
                for (int p = 0; p < nf - 1; p++)
                {
                    if (matriz[p][0].CompareTo(matriz[p + 1][0]) > 0)
                    {
                        for (int c1 = 0; c1 < nc; c1++)
                        {
                            string temp = matriz[p][c1];
                            matriz[p][c1] = matriz[p + 1][c1];
                            matriz[p + 1][c1] = temp;
                        }
                    }
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView2);

            // Agregar la clave
            for (int f = 0; f < nf; f++)
            {
                for (int c = 1; c < nc; c++)
                {
                    mensajeCifrado += matriz[f][c];
                }
            }

            return mensajeCifrado;
        }
        public string DescifrarPorFilas(string mensajeCifrado, string clave, ref DataGridView dataGridView1, ref DataGridView dataGridView2)
        {
            string claveOriginal = clave;
            clave = new string(clave.Replace(" ", "").OrderBy(c => c).ToArray());
            mensajeCifrado = mensajeCifrado.Replace(" ", "").Trim();

            int nc = (int)Math.Ceiling((double)(clave.Length + mensajeCifrado.Length) / clave.Length);
            int nf = clave.Length;

            string[][] matriz = new string[nf][];
            string mensajeDescifrado = "";

            int indexClave = 0;
            int indexMensajeCifrado = 0;

            // Preparar la matriz con la clave
            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    if (f >= matriz.Length || matriz[f] == null)
                    {
                        matriz[f] = new string[nc];
                    }
                    if (indexClave >= clave.Length)
                    {
                        matriz[f][c] = " ";
                    }
                    else
                    {
                        matriz[f][c] = clave[indexClave].ToString();
                        indexClave++;
                    }
                }
            }

            // Añadir el mensaje cifrado
            for (int f = 0; f < nf; f++)
            {
                for (int c = 1; c < nc; c++)
                {
                    if (indexMensajeCifrado < mensajeCifrado.Length)
                    {
                        matriz[f][c] = mensajeCifrado[indexMensajeCifrado].ToString();
                        indexMensajeCifrado++;
                    }
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

            // Intercambiar las filas
            for (int f1 = 0; f1 < nf; f1++)
            {
                for (int p = 0; p < nf - 1; p++)
                {
                    if (claveOriginal.IndexOf(matriz[p][0]) > claveOriginal.IndexOf(matriz[p + 1][0]))
                    {
                        for (int c1 = 0; c1 < nc; c1++)
                        {
                            string temp = matriz[p][c1];
                            matriz[p][c1] = matriz[p + 1][c1];
                            matriz[p + 1][c1] = temp;
                        }
                    }
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView2);

            // Encontrar el mensaje
            for (int c = 1; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    mensajeDescifrado += matriz[f][c];
                }
            }

            return mensajeDescifrado;
        }
        private void LlenarDataGridView(string[][] matriz, int nf, int nc, ref DataGridView dataGridView)
        {
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // sin esto no funciona
            for (int j = 0; j < nc; j++)
            {
                dataGridView.Columns.Add("", "");
            }

            // Agregar filas al DataGridView
            for (int i = 0; i < nf; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                fila.CreateCells(dataGridView);

                for (int j = 0; j < nc; j++)
                {
                    fila.Cells[j].Value = matriz[i][j];
                }

                dataGridView.Rows.Add(fila);
            }
        }
        private void LlenarDataGridViewV2(char[,] matriz, int nf, int nc, ref DataGridView dataGridView)
        {
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // sin esto no funciona
            for (int j = 0; j < nc; j++)
            {
                dataGridView.Columns.Add("", "");
            }

            // Agregar filas al DataGridView
            for (int i = 0; i < nf; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                fila.CreateCells(dataGridView);

                for (int j = 0; j < nc; j++)
                {
                    fila.Cells[j].Value = matriz[i, j];
                }

                dataGridView.Rows.Add(fila);
            }
        }

    }
}
