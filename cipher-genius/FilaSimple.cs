using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cipher_genius
{
    class FilaSimple
    {
        public string CifrarPorFilas(string mensaje, string clave, ref DataGridView dataGridView1, ref DataGridView dataGridView2)
        {
            clave = clave.Replace(" ", "");
            mensaje = mensaje.Replace(" ", "");

            int nc = (int)Math.Ceiling((double)(clave.Length + mensaje.Length) / clave.Length);
            int nf = clave.Length;
            string contenido = clave + mensaje;
            int indexContenido = 0;

            char[,] matriz = new char[nf, nc];
            string mensajeCifrado = "";

            // Llenar la matriz
            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    char characterToAdd;
                    if (indexContenido >= contenido.Length)
                    {
                        characterToAdd = 'X';
                    }
                    else
                    {
                        characterToAdd = contenido[indexContenido];
                        indexContenido++;
                    }
                    matriz[f, c] = characterToAdd;
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

            // Intercambiar las filas
            for (int f1 = 0; f1 < nf; f1++)
            {
                for (int p = 0; p < nf - 1; p++)
                {
                    if (matriz[p, 0].CompareTo(matriz[p + 1, 0]) > 0)
                    {
                        for (int c1 = 0; c1 < nc; c1++)
                        {
                            char temp = matriz[p, c1];
                            matriz[p, c1] = matriz[p + 1, c1];
                            matriz[p + 1, c1] = temp;
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
                    mensajeCifrado += matriz[f, c];
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

            char[,] matriz = new char[nf, nc];
            string mensajeDescifrado = "";

            int indexClave = 0;
            int indexMensajeCifrado = 0;

            // Preparar la matriz con la clave
            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    char characterToAdd;
                    if (indexClave >= clave.Length)
                    {
                        characterToAdd = ' ';
                    }
                    else
                    {
                        characterToAdd = clave[indexClave];
                        indexClave++;
                    }
                    matriz[f, c] = characterToAdd;
                }
            }

            // Añadir el mensaje cifrado
            for (int f = 0; f < nf; f++)
            {
                for (int c = 1; c < nc; c++)
                {
                    if (indexMensajeCifrado < mensajeCifrado.Length)
                    {
                        matriz[f, c] = mensajeCifrado[indexMensajeCifrado];
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
                    if (claveOriginal.IndexOf(matriz[p, 0]) > claveOriginal.IndexOf(matriz[p + 1, 0]))
                    {
                        for (int c1 = 0; c1 < nc; c1++)
                        {
                            char temp = matriz[p, c1];
                            matriz[p, c1] = matriz[p + 1, c1];
                            matriz[p + 1, c1] = temp;
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
                    mensajeDescifrado += matriz[f, c];
                }
            }

            return mensajeDescifrado;
        }


        private void LlenarDataGridView(char[,] matriz, int nf, int nc, ref DataGridView dataGridView)
        {
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // prepar el dataGridView (sin esto no funciona) 
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
