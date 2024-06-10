using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace cipher_genius
{
    class Beufort
    {
        private readonly string abecedario = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        private int n;

        public Beufort()
        {
            n = abecedario.Length;
        }

        private int Mod(int A, int n)
        {
            return ((A % n) + n) % n;
        }

        private string AjustarClave(string mensaje, string clave)
        {
            int lengthMensaje = mensaje.Length;
            int lengthClave = clave.Length;
            int indexClave = 0;
            string claveCopia = clave;

            while (lengthClave < lengthMensaje)
            {
                claveCopia += clave[indexClave];
                lengthClave++;
                if (indexClave >= clave.Length - 1)
                {
                    indexClave = 0;
                }
                else
                {
                    indexClave++;
                }
            }

            return claveCopia.Substring(0, mensaje.Length);
        }

        public string Cifrar(string mensaje, string clave)
        {
            clave = clave.Replace(" ", "").ToUpper();
            mensaje = mensaje.Replace(" ", "").ToUpper();
            clave = AjustarClave(mensaje, clave);
            string criptograma = "";

            for (int i = 0; i < mensaje.Length; i++)
            {
                int m = abecedario.IndexOf(mensaje[i]);
                int k = abecedario.IndexOf(clave[i]);
                criptograma += abecedario[Mod(m - k, n)];
            }

            return criptograma;
        }

        public string Descifrar(string mensaje, string clave)
        {
            clave = clave.Replace(" ", "").ToUpper();
            mensaje = mensaje.Replace(" ", "").ToUpper();
            clave = AjustarClave(mensaje, clave);

            string criptograma = "";

            for (int i = 0; i < mensaje.Length; i++)
            {
                int m = abecedario.IndexOf(mensaje[i]);
                int k = abecedario.IndexOf(clave[i]);
                criptograma += abecedario[Mod(m + k, n)];
            }

            return criptograma;
        }

        public void generarTablaBeufort(ref DataGridView dataGridView)
        {
            dataGridView.ColumnCount = n;
            dataGridView.RowCount = n;

            for (int c = 0; c < n; c++)
            {
                dataGridView.Columns[c].HeaderText = $"{abecedario[c]}";
                dataGridView.Columns[c].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int f = 0; f < n; f++)
            {
                dataGridView.Rows[f].HeaderCell.Value = $"{abecedario[f]}";
                for (int c = 0; c < n; c++)
                {
                    dataGridView.Rows[f].Cells[c].Value = abecedario[(n + f - c) % n];
                }
            }

            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Turquoise;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Turquoise;
        }
    }
}
