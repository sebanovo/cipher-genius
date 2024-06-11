using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using One1;
using Two2;
using System.Drawing.Drawing2D;

namespace cipher_genius
{
    public partial class Form1 : Form
    {
        /**
         * Sebastian Cespedes Rodas 
        */
        Grupos cipherByGroups = new Grupos();
        ColumnaDoble doubleColumnCipher = new ColumnaDoble();
        FilaSimple singleRowCipher = new FilaSimple();
        Beufort beufortCipher = new Beufort();
        bool estaCifrando = true;

        /*
         * Mejia  
        */
        Hill hill = new Hill();
        Playfair playfair = new Playfair();


        /*
         * Adolfo Mendoza Ribera 
        */
        int controlador = -1;
        int tamCuadros = 40;


        public Form1()
        {
            InitializeComponent();
            // Sebastian Cespedes Rodas
            //-----------------------------------------
            deshabilitarTexBox();
            validacionesDTG(dataGridView1);
            validacionesDTG(dataGridView2);
            validacionesDTG(dataGridView3);
            validacionesDTG(dataGridView4);
            beufortCipher.generarTablaBeufort(ref dataGridView4);

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
            // FIN Sebastian 

            //Adolfo Mendoza Ribera
            //-----------------------------------------
            pictureBoxes = new List<PictureBox>();
            lista = new List<int>();

            AMRcomboBoxListasCD.Text = "Selecciona";
            AMRcomboBoxTL.Text = "14";
            AMRcomboBoxA.Text = "Alfanumerico";
            AMRcomboBoxTC.Text = "40";

            AMRbuttonCD.Visible = false;
            AMRbuttonCD.Enabled = false;

            AMRbuttonLIMPIAR.Visible = false;
            AMRbuttonCD.Enabled = false;

            AMRcomboBoxA.Visible = false;
            AMRcomboBoxA.Enabled = false;

            AMRcomboBoxTC.Visible = false;
            AMRcomboBoxTC.Enabled = false;

            AMRcomboBoxTL.Visible = false;
            AMRcomboBoxTL.Enabled = false;

            AMRflowLayoutPanel1.Visible = false;
            AMRflowLayoutPanel1.Enabled = false;

            AMRlabel1.Visible = false;
            AMRlabel1.Enabled = false;
            AMRlabel2.Visible = false;
            AMRlabel2.Enabled = false;
            AMRlabel3.Visible = false;
            AMRlabel3.Enabled = false;
            AMRlabel4.Visible = false;
            AMRlabel4.Enabled = false;

            AMRlabelDesc1.Visible = false;
            AMRlabelDesc1.Enabled = false;

            this.MostrarOcultarCuadros(false);

            AMRrichTextBox1.Visible = false;
            AMRrichTextBox1.Enabled = false;
            AMRrichTextBox2.Visible = false;
            AMRrichTextBox2.Enabled = false;
            AMRrichTextBox3.Visible = false;
            AMRrichTextBox3.Enabled = false;

            // Fin ADOLFO

            // JOSUE
            roundedButton(ref button14);
            roundedButton(ref button15);
            roundedButton(ref button16);
            roundedButton(ref button17);
            // Fin JOSUE
        }

        private void deshabilitarTexBox()
        {
            textBoxMensaje.Enabled = false;
            textBoxClave.Enabled = false;
            textBoxCriptograma.Enabled = false;
        }

        private void validacionesDTG(DataGridView datagridView)
        {
            datagridView.EnableHeadersVisualStyles = false;
            datagridView.AllowUserToAddRows = false;
            datagridView.AllowUserToDeleteRows = false;
            datagridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridView.ReadOnly = true;
            datagridView.AllowUserToResizeColumns = false;
            datagridView.AllowUserToResizeRows = false;
        }

        private void LimpiarDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
        }

        private void handleChangeRadioButton()
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

        private void buttonConvertir_Click(object sender, EventArgs e)
        {
            string mensaje = textBoxMensaje.Text;
            string clave = textBoxClave.Text;
            string criptograma = textBoxCriptograma.Text;

            LimpiarDataGridView();

            if (radioPorGrupos.Checked)
            {
                if (estaCifrando)
                {
                    int[] permutacion = textBoxPermutacion.Text.Split(' ').Select(int.Parse).ToArray(); ;
                    string cifradoFinal = cipherByGroups.CifrarPorGrupos(mensaje, permutacion);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    int[] permutacion = textBoxPermutacion.Text.Split(' ').Select(int.Parse).ToArray(); ;
                    string cifradoFinal = cipherByGroups.DescifrarPorGrupos(criptograma, permutacion);
                    textBoxMensaje.Text = cifradoFinal;
                }
            }
            else if (radioDoble.Checked)
            {
                if (estaCifrando)
                {

                    string cifradoIntermedio = doubleColumnCipher.PrimerCifrado(mensaje, clave, ref dataGridView1);
                    string cifradoFinal = doubleColumnCipher.SegundoCifrado(cifradoIntermedio, clave, ref dataGridView2, ref dataGridView3);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    string descifradoIntemedio = doubleColumnCipher.PrimerDescrifrado(criptograma, clave, ref dataGridView1, ref dataGridView2);
                    string descifradoFinal = doubleColumnCipher.SegundoDescifrado(descifradoIntemedio, clave, ref dataGridView3);
                    textBoxMensaje.Text = descifradoFinal;
                }
            }
            else if (radioSimple.Checked)
            {
                if (estaCifrando)
                {
                    string cifradoFinal = singleRowCipher.CifrarPorFilas(mensaje, clave, ref dataGridView1, ref dataGridView2);
                    textBoxCriptograma.Text = cifradoFinal;
                }
                else
                {
                    string descifradoFinal = singleRowCipher.DescifrarPorFilas(criptograma, clave, ref dataGridView1, ref dataGridView2);
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

        // Sebatian Fin 

        // Adolfo Inicio

        /**
         * Adolfo Mendoza Ribera 
        */
        List<int> lista = new List<int>(); // Inicializa la lista vacía
        CipherManager cipher1 = new CipherManager();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();

        private void button12_Click(object sender, EventArgs e)
        {
            Matriz.RowCount = 5;
            Matriz.ColumnCount = 5;
            String clave = qey.Text;
            clave = clave.Trim();
            String texto = input.Text;
            texto = texto.Trim();
            String salida = playfair.Encriptar(texto, clave);
            output.Text = salida;
            // Limpiar cualquier dato existente en el DataGridView
            Matriz.Rows.Clear();
            Matriz.Columns.Clear();
            // Agregar las columnas al DataGridView
            for (int i = 0; i < 5; i++)
            {
                Matriz.Columns.Add("Columna" + i, "Columna" + i);
            }

            // Agregar las filas al DataGridView
            for (int i = 0; i < 5; i++)
            {
                Matriz.Rows.Add();
                for (int j = 0; j < 5; j++)
                {
                    Matriz.Rows[i].Cells[j].Value = playfair.Matriz[i, j];
                }
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            output.Clear();
            Matriz.RowCount = 5;
            Matriz.ColumnCount = 5;
            ///descencri playfair
            String clave = qey.Text;
            clave = clave.Trim();
            String cripto = input.Text;
            cripto = cripto.Trim();
            String salida = playfair.Desencriptar(cripto, clave);
            output.Text = salida;
            Matriz.Rows.Clear();
            Matriz.Columns.Clear();

            // Agregar las columnas al DataGridView
            for (int i = 0; i < 5; i++)
            {
                Matriz.Columns.Add("Columna" + i, "Columna" + i);
            }

            // Agregar las filas al DataGridView
            for (int i = 0; i < 5; i++)
            {
                Matriz.Rows.Add();
                for (int j = 0; j < 5; j++)
                {
                    Matriz.Rows[i].Cells[j].Value = playfair.Matriz[i, j];
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            qey.Clear();
            input.Clear();
            output.Clear();
            Matriz.Columns.Clear();
            Matriz.Rows.Clear();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

            int nnn = int.Parse(nxn.Text);
            String clave = qey.Text;
            String texto = input.Text;
            texto = texto.Trim();
            String salida = hill.Encriptar(texto, clave, nnn);
            output.Text = salida;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //boton desencriptar hill
            output.Clear();
            String clave = qey.Text;
            clave = clave.Trim();
            String texto = input.Text;
            int nnn = int.Parse(nxn.Text);
            texto = texto.Trim();
            String salida = hill.Desencriptar(texto, clave, nnn);
            output.Text = salida;
        }

        private void buttonVigenere_Click(object sender, EventArgs e)
        {
            Edit3.Text = beufortCipher.Cifrar(Edit1.Text, Edit2.Text);
        }

        private void buttonVigenereDes_Click(object sender, EventArgs e)
        {
            Edit1.Text = beufortCipher.Descifrar(Edit3.Text, Edit2.Text);
        }

        // Adolfo Fin

        private String[] abecedario = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", " " };
        private String[] caracteresMixtos = { "[", "¡", "?", "{", "}", "*", "♥", "♦", "♣", "♣", "≠", "#", "@", "%", "&", "(", ")", "=", ">", "<", "0", "1", "2", "3", "4", "5", "]", " " };

        private Dictionary<char, int> abecedarioEspañol = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 9}, {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13},
             {'Ñ', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20},
             {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}, {' ',27}
         };

        private Dictionary<char, int> alfabetoMixto = new Dictionary<char, int>
         {
             {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6},
             {'H', 7}, {'I', 8}, {'J', 9}, {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13},
             {'Ñ', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20},
             {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}, {' ',27}
         };

        private void button14_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            //AQUI EL SEGUNDO EDIT TENGO QUE MANDARLE UN NUMEERO PERO COMO CONVERITO DE INT A CADENA

            int baseTo;
            if (int.TryParse(textBox4.Text, out baseTo))
            {
                // Llamar al método Encryptar con los valores obtenidos
                string encryptedText = Santana.Encryptar(texto, baseTo);

                // Mostrar el texto encriptado en textBox3 (por ejemplo, si quieres mostrar el resultado en otro TextBox)
                textBox3.Text = encryptedText;
            }
            else
            {
                // Manejar el error si la conversión falla
                MessageBox.Show("Por favor, ingrese un número válido en el segundo campo.");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string texto = textBox3.Text;
            //AQUI EL SEGUNDO EDIT TNEGOQ UE MANDARLE UN NUMEERO PERO COMO CONVERITO DE INT A CADENA

            int baseTo;
            if (int.TryParse(textBox5.Text, out baseTo))
            {
                // Llamar al método Encryptar con los valores obtenidos
                string desencrypt = Santana.Decrypt(texto, baseTo);

                // Mostrar el texto encriptado en textBox3 (por ejemplo, si quieres mostrar el resultado en otro TextBox)
                textBox6.Text = desencrypt;
            }
            else
            {
                // Manejar el error si la conversión falla
                MessageBox.Show("Por favor, ingresa un número válido en el segundo campo.");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string texto = textBox7.Text;
            string text2 = textBox8.Text;

            string encript = Santana.EncryptarBool(texto, text2);

            textBox9.Text = encript;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            //la ultima
            string texto = textBox9.Text;
            string text2 = textBox10.Text;

            string desencript = Santana.Desencriptar(texto, text2);

            textBox11.Text = desencript;

        }

        //--------------ADOLFO--------------------------------------------
        private void AgregarElemento(int elemento)
        {
            lista.Insert(0, elemento); // Agrega el elemento al principio de la lista
        }

        private void EliminarElemento(int elemento)
        {
            lista.Remove(elemento); // Elimina el elemento de la lista
        }

        private void MostrarOcultarCuadros(bool ver)
        {
            AMRpictureBox0.Enabled = ver;
            AMRpictureBox0.Visible = ver;
            AMRpictureBox1.Enabled = ver;
            AMRpictureBox1.Visible = ver;
            AMRpictureBox2.Enabled = ver;
            AMRpictureBox2.Visible = ver;
            AMRpictureBox3.Enabled = ver;
            AMRpictureBox3.Visible = ver;
            AMRpictureBox4.Enabled = ver;
            AMRpictureBox4.Visible = ver;
            AMRpictureBox5.Enabled = ver;
            AMRpictureBox5.Visible = ver;
            AMRpictureBox6.Enabled = ver;
            AMRpictureBox6.Visible = ver;
            AMRpictureBox7.Enabled = ver;
            AMRpictureBox7.Visible = ver;
            AMRpictureBox8.Enabled = ver;
            AMRpictureBox8.Visible = ver;
            AMRpictureBox9.Enabled = ver;
            AMRpictureBox9.Visible = ver;
            AMRpictureBox10.Enabled = ver;
            AMRpictureBox10.Visible = ver;
            AMRpictureBox11.Enabled = ver;
            AMRpictureBox11.Visible = ver;
            AMRpictureBox12.Enabled = ver;
            AMRpictureBox12.Visible = ver;
            AMRpictureBox13.Enabled = ver;
            AMRpictureBox13.Visible = ver;
            AMRpictureBox14.Enabled = ver;
            AMRpictureBox14.Visible = ver;
            AMRpictureBox15.Visible = ver;
            AMRpictureBox15.Enabled = ver;
            AMRpictureBox16.Visible = ver;
            AMRpictureBox16.Enabled = ver;
            AMRpictureBox17.Visible = ver;
            AMRpictureBox17.Enabled = ver;
            AMRpictureBox18.Visible = ver;
            AMRpictureBox18.Enabled = ver;
            AMRpictureBox19.Visible = ver;
            AMRpictureBox19.Enabled = ver;
            AMRpictureBox20.Visible = ver;
            AMRpictureBox20.Enabled = ver;
            AMRpictureBox21.Visible = ver;
            AMRpictureBox21.Enabled = ver;
            AMRpictureBox22.Visible = ver;
            AMRpictureBox22.Enabled = ver;
            AMRpictureBox23.Visible = ver;
            AMRpictureBox23.Enabled = ver;
            AMRpictureBox24.Visible = ver;
            AMRpictureBox24.Enabled = ver;
            AMRpictureBox25.Visible = ver;
            AMRpictureBox25.Enabled = ver;
        }

        private void agregarPintura(int num)
        {
            // Crear un nuevo PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = this.tamCuadros; // Establece el ancho deseado
            pictureBox.Height = this.tamCuadros; // Establece la altura deseada
            pictureBox.BackColor = System.Drawing.Color.LightGray; // Cambia el color de fondo si lo deseas
                                                                   // Construye la ruta completa al archivo de imagen
            string imageName = "img" + (num + 1);
            Type resourceType = typeof(Properties.Resources);
            PropertyInfo imageProperty = resourceType.GetProperty(imageName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            pictureBox.Image = (Image)imageProperty.GetValue(null, null);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


            // Agrega el PictureBox al FlowLayoutPanel
            AMRflowLayoutPanel1.Controls.Add(pictureBox);

            // Opcional: Asigna una posición inicial dentro del FlowLayoutPanel
            // (El FlowLayoutPanel organizará automáticamente los controles)
            pictureBoxes.Add(pictureBox);
        }

        private void agregarPinturaAux(int num)
        {
            // Crear un nuevo PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = this.tamCuadros; // Establece el ancho deseado
            pictureBox.Height = this.tamCuadros; // Establece la altura deseada
            pictureBox.BackColor = System.Drawing.Color.LightGray; // Cambia el color de fondo si lo deseas
                                                                   // Construye la ruta completa al archivo de imagen

            string imageName = "img" + (num + 1);
            Type resourceType = typeof(Properties.Resources);
            PropertyInfo imageProperty = resourceType.GetProperty(imageName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            pictureBox.Image = (Image)imageProperty.GetValue(null, null);

            //pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


            // Agrega el PictureBox al FlowLayoutPanel
            AMRflowLayoutPanel1.Controls.Add(pictureBox);

            // Opcional: Asigna una posición inicial dentro del FlowLayoutPanel
            // (El FlowLayoutPanel organizará automáticamente los controles)
            pictureBoxes.Add(pictureBox);

            // Agrega el controlador de eventos para el clic en el PictureBox
            pictureBox.Click += (sender, e) =>
            {
                // Elimina el PictureBox del FlowLayoutPanel
                AMRflowLayoutPanel1.Controls.Remove(pictureBox);
                this.EliminarElemento(num);
                // También puedes eliminarlo de la lista de PictureBoxes si lo deseas
                pictureBoxes.Remove(pictureBox);
            };
        }

        private void AMRcomboBoxListasCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoCifrado = AMRcomboBoxListasCD.SelectedItem.ToString();

            AMRflowLayoutPanel1.Controls.Clear();
            AMRrichTextBox1.Text = "";
            AMRrichTextBox2.Text = "";
            AMRrichTextBox3.Text = "";

            AMRcomboBoxTC.Visible = true;
            AMRcomboBoxTC.Enabled = true;

            AMRcomboBoxA.Visible = true;
            AMRcomboBoxA.Enabled = true;

            AMRlabel2.Visible = true;
            AMRlabel2.Enabled = true;

            AMRcomboBoxTL.Enabled = true;
            AMRcomboBoxTL.Visible = true;

            AMRlabelDesc1.Visible = true;
            AMRlabelDesc1.Visible = true;

            this.MostrarOcultarCuadros(false);

            //Cifrado Francmason
            if (tipoCifrado == "Cifrador Francmason")
            {
                this.controlador = 0;

                AMRlabelDesc1.Location = new Point(530, 150);
                AMRlabelDesc1.Text = "Escribe el texto claro";
                AMRlabelDesc1.Enabled = true;
                AMRlabelDesc1.Enabled = true;

                AMRlabel1.Visible = false;
                AMRlabel1.Enabled = false;

                AMRlabel3.Visible = true;
                AMRlabel3.Enabled = true;

                AMRlabel4.Visible = true;
                AMRlabel4.Enabled = true;

                AMRbuttonLIMPIAR.Location = new Point(400, 360);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonLIMPIAR.Enabled = true;
                AMRbuttonLIMPIAR.Visible = true;

                AMRbuttonCD.Location = new Point(740, 360);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Cifrar";

                AMRbuttonCD.Enabled = true;
                AMRbuttonCD.Visible = true;

                AMRrichTextBox1.Location = new Point(50, 190);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 150;

                AMRrichTextBox1.Visible = true;
                AMRrichTextBox1.Enabled = true;

                AMRrichTextBox2.Visible = false;
                AMRrichTextBox2.Enabled = false;

                AMRrichTextBox3.Visible = false;
                AMRrichTextBox3.Enabled = false;

                AMRflowLayoutPanel1.Location = new Point(50, 450);
                AMRflowLayoutPanel1.Width = 1230;
                AMRflowLayoutPanel1.Height = 200;
                AMRflowLayoutPanel1.Visible = true;
                AMRflowLayoutPanel1.Enabled = true;
            }
            else if (tipoCifrado == "Descifrador Francmason")
            {
                this.controlador = 1;

                this.MostrarOcultarCuadros(true);

                AMRlabelDesc1.Location = new Point(20, 145);
                AMRlabelDesc1.Text = "Escoje las imagenes que deceas descifrar";

                AMRlabel1.Visible = false;
                AMRlabel1.Enabled = false;

                AMRlabel3.Visible = true;
                AMRlabel3.Enabled = true;

                AMRlabel4.Visible = true;
                AMRlabel4.Enabled = true;

                AMRcomboBoxTC.Visible = true;
                AMRcomboBoxTC.Enabled = true;

                AMRcomboBoxA.Visible = true;
                AMRcomboBoxA.Enabled = true;


                AMRbuttonLIMPIAR.Location = new Point(400, 380);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonCD.Location = new Point(740, 380);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Descifrar";

                AMRrichTextBox1.Location = new Point(50, 500);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 150;

                AMRrichTextBox2.Visible = false;
                AMRrichTextBox2.Enabled = false;

                AMRrichTextBox3.Visible = false;
                AMRrichTextBox3.Enabled = false;

                //50 190
                AMRflowLayoutPanel1.Location = new Point(650, 180);
                AMRflowLayoutPanel1.Width = 670;
                AMRflowLayoutPanel1.Height = 190;
                AMRflowLayoutPanel1.Visible = true;
                AMRflowLayoutPanel1.Enabled = true;
            }
            else if (tipoCifrado == "Cifrador Vigenere")
            {
                this.controlador = 2;

                AMRlabelDesc1.Location = new Point(530, 150);
                AMRlabelDesc1.Text = "Escribre el texto claro";

                AMRlabel3.Visible = false;
                AMRlabel3.Enabled = false;

                AMRlabel4.Visible = false;
                AMRlabel4.Enabled = false;

                AMRcomboBoxTC.Visible = false;
                AMRcomboBoxTC.Visible = false;

                AMRcomboBoxA.Visible = false;
                AMRcomboBoxA.Enabled = false;


                AMRlabel1.Location = new Point(180, 340);
                AMRlabel1.Text = "Escribe la clave, son puras letras del alfabeto ---->";
                AMRlabel1.Visible = true;
                AMRlabel1.Enabled = true;

                AMRbuttonLIMPIAR.Location = new Point(400, 400);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonCD.Location = new Point(740, 400);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Cifrar";

                AMRrichTextBox1.Location = new Point(50, 190);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 100;

                AMRrichTextBox2.Location = new Point(680, 295);
                AMRrichTextBox2.Width = 600;
                AMRrichTextBox2.Height = 100;
                AMRrichTextBox2.Visible = true;
                AMRrichTextBox2.Enabled = true;

                AMRrichTextBox3.Location = new Point(50, 500);
                AMRrichTextBox3.Width = 1230;
                AMRrichTextBox3.Height = 150;
                AMRrichTextBox3.Visible = true;
                AMRrichTextBox3.Enabled = true;

                AMRflowLayoutPanel1.Visible = false;
                AMRflowLayoutPanel1.Enabled = false;

            }
            else if (tipoCifrado == "Descifrador Vigenere")
            {
                this.controlador = 3;

                AMRlabelDesc1.Location = new Point(530, 150);
                AMRlabelDesc1.Text = "Escribre el texto cifrado";

                AMRcomboBoxA.Visible = false;
                AMRcomboBoxA.Enabled = false;

                AMRlabel4.Visible = false;
                AMRlabel4.Enabled = false;

                AMRcomboBoxTC.Visible = false;
                AMRcomboBoxTC.Visible = false;

                AMRlabel1.Location = new Point(180, 340);
                AMRlabel1.Text = "Escribe la clave, son puras letras del alfabeto ---->";
                AMRlabel1.Visible = true;
                AMRlabel1.Enabled = true;

                AMRlabel3.Visible = false;
                AMRlabel3.Enabled = false;

                AMRbuttonLIMPIAR.Location = new Point(400, 400);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonCD.Location = new Point(740, 400);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Descifrar";

                AMRrichTextBox1.Location = new Point(50, 190);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 100;

                AMRrichTextBox2.Location = new Point(680, 295);
                AMRrichTextBox2.Width = 600;
                AMRrichTextBox2.Height = 100;
                AMRrichTextBox2.Visible = true;
                AMRrichTextBox2.Enabled = true;

                AMRrichTextBox3.Location = new Point(50, 500);
                AMRrichTextBox3.Width = 1230;
                AMRrichTextBox3.Height = 150;
                AMRrichTextBox3.Visible = true;
                AMRrichTextBox3.Enabled = true;

                AMRflowLayoutPanel1.Visible = false;
                AMRflowLayoutPanel1.Enabled = false;
            }
            else if (tipoCifrado == "Cifrador Homofono")
            {
                this.controlador = 4;

                AMRlabelDesc1.Location = new Point(530, 150);
                AMRlabelDesc1.Text = "Escribre el texto claro";

                AMRlabel1.Location = new Point(140, 350);
                AMRlabel1.Text = "Escribe la clave, son solo 4 letras ---->";
                AMRlabel1.Visible = true;
                AMRlabel1.Enabled = true;

                AMRlabel3.Visible = false;
                AMRlabel3.Enabled = false;

                AMRlabel4.Visible = false;
                AMRlabel4.Enabled = false;

                AMRcomboBoxTC.Visible = false;
                AMRcomboBoxTC.Visible = false;

                AMRcomboBoxA.Visible = false;
                AMRcomboBoxA.Enabled = false;

                AMRbuttonLIMPIAR.Location = new Point(400, 400);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonCD.Location = new Point(740, 400);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Cifrar";

                AMRrichTextBox1.Location = new Point(50, 190);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 140;

                AMRrichTextBox2.Location = new Point(550, 335);
                AMRrichTextBox2.Width = 280;
                AMRrichTextBox2.Height = 60;
                AMRrichTextBox2.Visible = true;
                AMRrichTextBox2.Enabled = true;

                AMRrichTextBox3.Location = new Point(50, 500);
                AMRrichTextBox3.Width = 1230;
                AMRrichTextBox3.Height = 150;
                AMRrichTextBox3.Visible = true;
                AMRrichTextBox3.Enabled = true;

                AMRflowLayoutPanel1.Visible = false;
                AMRflowLayoutPanel1.Enabled = false;
            }
            else if (tipoCifrado == "Descifrador Homofono")
            {
                this.controlador = 5;

                AMRlabelDesc1.Location = new Point(530, 150);
                AMRlabelDesc1.Text = "Escribre el texto cifrado";

                AMRlabel1.Location = new Point(140, 350);
                AMRlabel1.Text = "Escribe la clave, son solo 4 letras ---->";
                AMRlabel1.Visible = true;
                AMRlabel1.Enabled = true;

                AMRlabel3.Visible = false;
                AMRlabel3.Enabled = false;

                AMRlabel4.Visible = false;
                AMRlabel4.Enabled = false;

                AMRcomboBoxTC.Visible = false;
                AMRcomboBoxTC.Visible = false;

                AMRcomboBoxA.Visible = false;
                AMRcomboBoxA.Enabled = false;

                AMRbuttonLIMPIAR.Location = new Point(400, 400);
                AMRbuttonLIMPIAR.Width = 260;
                AMRbuttonLIMPIAR.Height = 70;

                AMRbuttonCD.Location = new Point(740, 400);
                AMRbuttonCD.Width = 260;
                AMRbuttonCD.Height = 70;
                AMRbuttonCD.Text = "Descifrar";

                AMRrichTextBox1.Location = new Point(50, 190);
                AMRrichTextBox1.Width = 1230;
                AMRrichTextBox1.Height = 140;

                AMRrichTextBox2.Location = new Point(550, 335);
                AMRrichTextBox2.Width = 280;
                AMRrichTextBox2.Height = 60;
                AMRrichTextBox2.Visible = true;
                AMRrichTextBox2.Enabled = true;

                AMRrichTextBox3.Location = new Point(50, 500);
                AMRrichTextBox3.Width = 1230;
                AMRrichTextBox3.Height = 150;
                AMRrichTextBox3.Visible = true;
                AMRrichTextBox3.Enabled = true;

                AMRflowLayoutPanel1.Visible = false;
                AMRflowLayoutPanel1.Enabled = false;
            }
        }

        private void AMRbuttonLIMPIAR_Click(object sender, EventArgs e)
        {
            AMRflowLayoutPanel1.Controls.Clear();
            AMRrichTextBox1.Text = "";
            AMRrichTextBox2.Text = "";
            AMRrichTextBox3.Text = "";

        }

        private void AMRcomboBoxTL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = AMRcomboBoxTL.SelectedItem.ToString();
            int tam;
            if (int.TryParse(selectedItem, out tam))
            {
                AMRrichTextBox1.Font = new Font(AMRrichTextBox1.Font.FontFamily, tam);
                AMRrichTextBox2.Font = new Font(AMRrichTextBox2.Font.FontFamily, tam);
                AMRrichTextBox3.Font = new Font(AMRrichTextBox3.Font.FontFamily, tam);
            }

        }

        private bool VerificarEnAbecedario(string texto, int opcion)
        {
            // Eliminamos espacios en blanco para simplificar la comparación
            texto = texto.Replace(" ", "");

            // Convertimos el texto a mayúsculas para que sea insensible a mayúsculas/minúsculas
            texto = texto.ToUpper();

            if (opcion == 1)
            {
                // Definimos el abecedario español
                string abecedario = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

                // Verificamos si cada carácter está en el abecedario
                foreach (char c in texto)
                {
                    if (!abecedario.Contains(c))
                    {
                        return false; // Si encontramos un carácter que no está en el abecedario, retornamos falso
                    }
                }
            }
            else
            {
                // Definimos la lista de caracteres mixtos
                string[] caracteresMixtos = { "[", "¡", "?", "{", "}", "*", "♥", "♦", "♣", "♣", "≠", "#", "@", "%", "&", "(", ")", "=", ">", "<", "0", "1", "2", "3", "4", "5", "]" };

                // Verificamos si cada carácter está en la lista de caracteres mixtos
                foreach (char c in texto)
                {
                    if (!caracteresMixtos.Contains(c.ToString()))
                    {
                        return false; // Si encontramos un carácter que no está en la lista de caracteres mixtos, retornamos falso
                    }
                }
            }

            return true; // Si todos los caracteres están en el abecedario o en la lista de caracteres mixtos, retornamos verdadero
        }

        private bool VerificarNumerosYComasEnArreglo(string[] arreglo)
        {
            foreach (string elemento in arreglo)
            {
                // Eliminamos espacios en blanco para simplificar la comparación
                string elementoSinEspacios = elemento.Replace(" ", "");

                foreach (char c in elementoSinEspacios)
                {
                    if (!Char.IsDigit(c) && c != ',' || elementoSinEspacios.Length > 2)
                    {
                        return false; // Si encontramos un carácter que no es número ni coma, o el número tiene más de 2 dígitos, retornamos falso
                    }
                }
            }
            return true; // Si todos los elementos cumplen con los requisitos, retornamos verdadero
        }

        private void AMRbuttonCD_Click(object sender, EventArgs e)
        {
            if (this.controlador == 0)
            {
                if (AMRrichTextBox1.Text != "")
                {
                    AMRlabel1.Visible = false;
                    AMRlabel1.Enabled = false;

                    // Inicializa la lista de letras
                    string textoClaro = AMRrichTextBox1.Text; // Asegúrate de obtener el texto del control richTextBox2

                    List<int> listaPosiciones = new List<int>();

                    string texto = AMRcomboBoxA.SelectedItem.ToString();
                    int opcion = 1;
                    if (texto == "Alfanumerico")
                    {
                        opcion = 1;
                    }
                    else if (texto == "Mixto")
                    {
                        opcion = 0;
                    }

                    bool ver = this.VerificarEnAbecedario(textoClaro, opcion);

                    if (ver == true)
                    {
                        // Invoca el método CifradorCesarMixto
                        listaPosiciones = cipher1.CifradorCesarMixto(textoClaro, opcion);
                        AMRflowLayoutPanel1.Controls.Clear();
                        foreach (int num in listaPosiciones)
                        {
                            this.agregarPintura(num);
                        }
                    }
                    else
                    {
                        AMRlabel1.Location = new Point(50, 360);
                        AMRlabel1.Width = 250;
                        AMRlabel1.Height = 100;
                        AMRlabel1.Text = "Hay un problema, revisa que las letras corresponda al tipo del Alfabeto";
                        AMRlabel1.Visible = true;
                        AMRlabel1.Enabled = true;

                        AMRflowLayoutPanel1.Controls.Clear();
                    }


                }
                else
                {
                    AMRlabel1.Location = new Point(50, 360);
                    AMRlabel1.Width = 230;
                    AMRlabel1.Height = 60;
                    AMRlabel1.Text = "No hay nada que cifrar";
                    AMRlabel1.Visible = true;
                    AMRlabel1.Enabled = true;

                    AMRflowLayoutPanel1.Controls.Clear();
                }

            }
            else if (this.controlador == 1)
            {

                if (this.lista.Count != 0)
                {
                    string texto1 = AMRcomboBoxA.SelectedItem.ToString();

                    int[] arregloPosiciones = lista.ToArray(); // Convierte la lista a un arreglo

                    int opcion = 1;
                    if (texto1 == "Alfanumerico")
                    {
                        opcion = 1;
                    }
                    else if (texto1 == "Mixto")
                    {
                        opcion = 0;
                    }


                    List<string> listaTextoCifrado = cipher1.DescifradorCesarMixto(arregloPosiciones, opcion);
                    AMRrichTextBox1.Text = string.Join("", listaTextoCifrado); // Muestra los elementos separados por comas
                }
                else
                {
                    AMRrichTextBox1.Text = "No hay nada que descifrar";
                }


            }
            else if (this.controlador == 2)
            {
                String textoClaro = AMRrichTextBox1.Text;
                String cifra = AMRrichTextBox2.Text;

                if (textoClaro != "" && cifra != "")
                {
                    bool ver1 = this.VerificarEnAbecedario(textoClaro, 1);
                    bool ver2 = this.VerificarEnAbecedario(cifra, 1);

                    if (ver1 == true & ver2 == true)
                    {
                        List<char> textoCifrado = cipher1.CifradoVigenere(textoClaro, cifra);


                        // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                        AMRrichTextBox3.Text = new string(textoCifrado.ToArray());
                    }
                    else
                    {
                        AMRrichTextBox3.Text = "Revisa que el texto y la clave tengas las letras del alfabeto";
                    }


                }
                else
                {
                    AMRrichTextBox3.Text = "Revisa que en el campo de texto y clave tengan datos";
                }


            }
            else if (this.controlador == 3)
            {
                String textoClaro = AMRrichTextBox1.Text;
                String cifra = AMRrichTextBox2.Text;

                if (textoClaro != "" && cifra != "")
                {
                    bool ver1 = this.VerificarEnAbecedario(textoClaro, 1);
                    bool ver2 = this.VerificarEnAbecedario(cifra, 1);

                    if (ver1 == true & ver2 == true)
                    {
                        List<char> textoCifrado = cipher1.DescifradoVigenere(textoClaro, cifra);


                        // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                        AMRrichTextBox3.Text = new string(textoCifrado.ToArray());
                    }
                    else
                    {
                        AMRrichTextBox3.Text = "Revisa que el texto y la clave tengas las letras del alfabeto";
                    }
                }
                else
                {
                    AMRrichTextBox3.Text = "Revisa que en el campo de texto y clave tengan datos";
                }
            }
            else if (this.controlador == 4)
            {
                int[,] matriz = cipher1.GenerarMatriz();
                string palabra = AMRrichTextBox1.Text;
                string clave = AMRrichTextBox2.Text;
                if (clave.Length == 4)
                {
                    bool ver1 = this.VerificarEnAbecedario(palabra, 1);
                    bool ver2 = this.VerificarEnAbecedario(clave, 1);

                    if (ver1 == true & ver2 == true)
                    {
                        List<int> textoCifrado = cipher1.CifradorHomofono(matriz, palabra, clave);
                        // Convertir la lista de enteros a una cadena de texto separada por comas
                        string textoCifradoString = string.Join(", ", textoCifrado);

                        // Asignar la cadena de texto al richTextBox1
                        AMRrichTextBox3.Text = textoCifradoString;
                    }
                    else
                    {
                        AMRrichTextBox3.Text = "Revisa que el texto y la clave tengas las letras del alfabeto";
                    }
                }
                else
                {
                    AMRrichTextBox3.Text = "La clave debe tener 4 caracteres";
                }
            }
            else if (this.controlador == 5)
            {
                int[,] matriz = cipher1.GenerarMatriz();
                string textoCifradoString = AMRrichTextBox1.Text; // Cambio: Convertir a cadena de texto
                string[] valoresTextoCifrado = textoCifradoString.Split(','); // Separar los valores por comas

                bool ver1 = this.VerificarNumerosYComasEnArreglo(valoresTextoCifrado);
                if (ver1 == true)
                {
                    string clave = AMRrichTextBox2.Text.ToUpper(); // Convertir la clave a mayúsculas

                    bool ver2 = this.VerificarEnAbecedario(clave, 1);
                    if (ver2 == true)
                    {

                        List<int> textoCifrado = new List<int>();
                        foreach (string valor in valoresTextoCifrado)
                        {
                            int numero;
                            if (int.TryParse(valor, out numero))
                            {
                                textoCifrado.Add(numero);
                            }
                            else

                            {
                                // Manejar el caso en que el valor no sea un número válido
                                // Puedes mostrar un mensaje de error o tomar otra acción
                            }
                        }


                        if (clave.Length == 4)
                        {
                            List<char> textoClaro = cipher1.DescifradorHomofono(matriz, textoCifrado, clave);

                            // Convertir la lista de caracteres a una cadena de texto
                            string textoClaroString = new string(textoClaro.ToArray());

                            // Asignar la cadena de texto al richTextBox1
                            AMRrichTextBox3.Text = textoClaroString;
                        }
                        else
                        {
                            AMRrichTextBox3.Text = "La clave debe tener 4 caracteres";
                        }
                    }
                    else
                    {
                        AMRrichTextBox3.Text = "La clave solo debe contener letras del alfabeto";
                    }

                }
                else
                {
                    AMRrichTextBox3.Text = "Revisa que el texto cifrado sean numeros que tengan maximo 2 digitos y sean separados solo por comas ','";
                }
            }
        }

        public void ModificarTamanioPictureBoxes(int nuevoAncho, int nuevoAlto)
        {
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Width = nuevoAncho;
                pictureBox.Height = nuevoAlto;
            }
        }

        private void AMRcomboBoxTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = AMRcomboBoxTC.SelectedItem.ToString();
            int tam;
            if (int.TryParse(selectedItem, out tam))
            {
                this.tamCuadros = tam;
                this.ModificarTamanioPictureBoxes(tam, tam);
            }
        }

        private void AMRpictureBox0_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(0);
            this.agregarPinturaAux(0);
        }

        private void AMRpictureBox1_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(1);
            this.agregarPinturaAux(1);
        }

        private void AMRpictureBox2_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(2);
            this.agregarPinturaAux(2);
        }

        private void AMRpictureBox3_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(3);
            this.agregarPinturaAux(3);
        }

        private void AMRpictureBox4_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(4);
            this.agregarPinturaAux(4);
        }

        private void AMRpictureBox5_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(5);
            this.agregarPinturaAux(5);
        }

        private void AMRpictureBox6_Click(object sender, EventArgs e)
        {
            int num = 6;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox7_Click(object sender, EventArgs e)
        {
            int num = 7;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox8_Click(object sender, EventArgs e)
        {
            int num = 8;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox9_Click(object sender, EventArgs e)
        {
            int num = 9;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox10_Click(object sender, EventArgs e)
        {
            int num = 10;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox11_Click(object sender, EventArgs e)
        {
            int num = 11;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox12_Click(object sender, EventArgs e)
        {
            int num = 12;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox13_Click(object sender, EventArgs e)
        {
            int num = 13;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox14_Click(object sender, EventArgs e)
        {
            int num = 14;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox15_Click(object sender, EventArgs e)
        {
            int num = 15;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox16_Click(object sender, EventArgs e)
        {
            int num = 16;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox17_Click(object sender, EventArgs e)
        {
            int num = 17;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox18_Click(object sender, EventArgs e)
        {
            int num = 18;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox19_Click(object sender, EventArgs e)
        {
            int num = 19;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox20_Click(object sender, EventArgs e)
        {
            int num = 20;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox21_Click(object sender, EventArgs e)
        {
            int num = 21;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox22_Click(object sender, EventArgs e)
        {
            int num = 22;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox23_Click(object sender, EventArgs e)
        {
            int num = 23;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox24_Click(object sender, EventArgs e)
        {
            int num = 24;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void AMRpictureBox25_Click(object sender, EventArgs e)
        {
            int num = 25;
            this.AgregarElemento(num);
            this.agregarPinturaAux(num);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int n = int.Parse(nxn.Text);
            HashSet<string> mySet = hill.Generaclave(n, 1);
            List<String> clavesValidas = new List<String>();
            foreach (string fruit in mySet)
            {
                clavesValidas.Add(fruit);
            }
            String clave = clavesValidas[0];
            qey.Text = clave;
        }

        // JOSUE
        public void roundedButton(ref Button roundedButton)
        {
            int radius = 40;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(roundedButton.Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(roundedButton.Width - radius, roundedButton.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, roundedButton.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();
            roundedButton.Region = new Region(path);
        }
    }
}