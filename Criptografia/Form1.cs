using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Criptografia
{
    public partial class Form1 : Form
    {
        bool estaCifrando = true;
        public Form1()
        {
            InitializeComponent();
        }

        public void deshabilitarTexBox()
        {
            textBoxMensaje.Enabled = false;
            textBoxClave.Enabled = false;
            textBoxCriptograma.Enabled = false;
        }

        public void validacionesDTG(DataGridView datagridView)
        {
            datagridView.AllowUserToAddRows = false;
            datagridView.AllowUserToDeleteRows = false;
            datagridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridView.ReadOnly = true;
        }

        public void handleChangeRadioButton()
        {
            textBoxPermutacion.Enabled = false;
            textBoxClave.Enabled = true;
            textBoxPermutacion.Enabled = false;
            if (radioPorGrupos.Checked)
            {
                textBoxClave.Enabled = false;
                textBoxPermutacion.Enabled = true;
            }

        }

        private void buttonCifrar_Click(object sender, EventArgs e)
        {
            estaCifrando = true;
            textBoxMensaje.Enabled = true;
            textBoxClave.Enabled = true;
            textBoxCriptograma.Enabled = false;
            if (radioPorGrupos.Checked)
            {
                textBoxClave.Enabled = false;
            }
        }

        private void buttonDescifrar_Click(object sender, EventArgs e)
        {
            estaCifrando = false;
            textBoxMensaje.Enabled = false;
            textBoxCriptograma.Enabled = true;
            textBoxClave.Enabled = true;
            if (radioPorGrupos.Checked)
            {
                textBoxClave.Enabled = false;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            deshabilitarTexBox();
            validacionesDTG(dataGridView1);
            validacionesDTG(dataGridView2);
            validacionesDTG(dataGridView3);
            if (estaCifrando)
            {
                textBoxMensaje.Enabled = true;
                textBoxClave.Enabled = true;
            }
            else
            {
                textBoxClave.Enabled = true;
                textBoxCriptograma.Enabled = true;
            }
            textBoxPermutacion.Enabled = false;
        }

        private void buttonConvertir_Click(object sender, EventArgs e)
        {
            string mensaje = textBoxMensaje.Text;
            string clave = textBoxClave.Text;
            string criptograma = textBoxCriptograma.Text;

            CipherManager cipher = new CipherManager();

            if (radioPorGrupos.Checked)
            {
                // mejorar la logica
                if (estaCifrando)
                {
                    int[] permutacion = textBoxPermutacion.Text.Split(' ').Select(int.Parse).ToArray(); ;
                    string cifradoFinal = cipher.CifrarPorGrupos(mensaje, permutacion);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    int[] permutacion = textBoxPermutacion.Text.Split(' ').Select(int.Parse).ToArray(); ;
                    string cifradoFinal = cipher.DescifrarPorGrupos(criptograma, permutacion);
                    textBoxMensaje.Text = cifradoFinal;
                }
            }
            else if (radioDoble.Checked)
            {
                if (estaCifrando)
                {

                    string cifradoIntermedio = cipher.PrimerCifrado(mensaje, clave, ref dataGridView1);
                    string cifradoFinal = cipher.SegundoCifrado(cifradoIntermedio, clave, ref dataGridView2, ref dataGridView3);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    string descifradoIntemedio = cipher.PrimerDescrifrado(criptograma, clave, ref dataGridView1, ref dataGridView2);
                    string descifradoFinal = cipher.SegundoDescifrado(descifradoIntemedio, clave, ref dataGridView3);
                    textBoxMensaje.Text = descifradoFinal;
                }
            }
            else if (radioSimple.Checked)
            {
                if (estaCifrando)
                {
                    string cifradoFinal = cipher.CifrarPorFilas(mensaje, clave, ref dataGridView1, ref dataGridView2);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    string descifradoFinal = cipher.DescifrarPorFilas(criptograma, clave, ref dataGridView1, ref dataGridView2);
                    textBoxMensaje.Text = descifradoFinal;
                }
            }
        }

        private void radioPorGrupos_CheckedChanged(object sender, EventArgs e)
        {
            handleChangeRadioButton();
        }

        private void radioDoble_CheckedChanged(object sender, EventArgs e)
        {
            handleChangeRadioButton();
        }

        private void radioSimple_CheckedChanged(object sender, EventArgs e)
        {
            handleChangeRadioButton();
        }

        private void buttonReiniciar_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
