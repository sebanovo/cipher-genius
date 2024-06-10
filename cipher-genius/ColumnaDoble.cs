using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cipher_genius
{
    class ColumnaDoble
    {
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

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

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

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

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

            LlenarDataGridView(matriz, nf, nc, ref dataGridView2);

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

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

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

            LlenarDataGridView(matriz, nf, nc, ref dataGridView2);

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
            char[,] matriz = new char[nf, nc];

            int indexDescifradoIntermedio = 0;
            string mensajeDescifrado = "";

            for (int c = 0; c < nc; c++)
            {
                for (int f = 0; f < nf; f++)
                {
                    char characterToAdd;
                    if (indexDescifradoIntermedio < descifradoIntermedio.Length)
                    {
                        characterToAdd = descifradoIntermedio[indexDescifradoIntermedio];
                        indexDescifradoIntermedio++;
                    }
                    else
                    {
                        characterToAdd = ' '; // ocupar el espacio 
                    }
                    matriz[f, c] = characterToAdd;
                }
            }

            LlenarDataGridView(matriz, nf, nc, ref dataGridView1);

            for (int f = 0; f < nf; f++)
            {
                for (int c = 0; c < nc; c++)
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
