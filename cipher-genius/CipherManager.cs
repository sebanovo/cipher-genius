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
        public string CifrarPorGrupos(string mensaje, int[] permutacion)
        {
            mensaje = mensaje.Replace(" ", "");
            // Convertir el mensaje a un array de caracteres
            var mensajeArray = mensaje.Trim().ToCharArray().ToList();

            // Calcular la longitud de cada grupo
            var longitudGrupo = permutacion.Length;

            // Calcular la cantidad de grupos necesarios
            var cantidadGrupos = (int)Math.Ceiling((double)mensajeArray.Count / longitudGrupo);


            // Calcular la cantidad de caracteres necesarios para completar el último grupo
            var caracteresFaltantes = longitudGrupo - (mensajeArray.Count % longitudGrupo);

            // Rellenar el mensaje con 'X' para completar el último grupo
            for (int i = 0; i < caracteresFaltantes; i++)
            {
                mensajeArray.Add('X');
            }

            // Crear una lista para almacenar los grupos cifrados
            var gruposCifrados = new List<string>();

            // Iterar sobre cada grupo
            for (int i = 0; i < cantidadGrupos; i++)
            {
                var grupoCifrado = "";

                // Iterar sobre cada posición de la permutación
                for (int j = 0; j < longitudGrupo; j++)
                {
                    // Calcular la posición real en el mensaje
                    var posicionMensaje = i * longitudGrupo + permutacion[j];

                    // Añadir el carácter al grupo cifrado
                    grupoCifrado += mensajeArray[posicionMensaje];
                }

                // Agregar el grupo cifrado a la lista de grupos cifrados
                gruposCifrados.Add(grupoCifrado);
            }

            // Unir los grupos cifrados en un solo mensaje cifrado
            var mensajeCifrado = string.Concat(gruposCifrados);

            return mensajeCifrado;
        }

        public string DescifrarPorGrupos(string mensajeCifrado, int[] permutacion)
        {
            mensajeCifrado = mensajeCifrado.Replace(" ", "");
            // Calcular la longitud de cada grupo
            var longitudGrupo = permutacion.Length;

            // Calcular la cantidad de grupos
            var cantidadGrupos = mensajeCifrado.Length / longitudGrupo;

            // Crear un array para almacenar los grupos descifrados
            var gruposDescifrados = new List<string>();

            // Iterar sobre cada grupo
            for (int i = 0; i < cantidadGrupos; i++)
            {
                var grupoDescifrado = "";

                // Iterar sobre cada posición de la permutación inversa
                for (int j = 0; j < longitudGrupo; j++)
                {
                    // Calcular la posición real en el mensaje cifrado
                    var posicionCifrado = i * longitudGrupo + Array.IndexOf(permutacion, j);

                    // Añadir el carácter al grupo descifrado
                    grupoDescifrado += mensajeCifrado[posicionCifrado];
                }

                // Agregar el grupo descifrado a la lista de grupos descifrados
                gruposDescifrados.Add(grupoDescifrado);
            }

            // Unir los grupos descifrados en un solo mensaje descifrado
            var mensajeDescifrado = string.Concat(gruposDescifrados);

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
                        characterToAdd = ' '; // O cualquier otro carácter predeterminado.
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