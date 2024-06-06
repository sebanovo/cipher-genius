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



namespace cipher_genius
{
    public partial class Form1 : Form
    {
        /**
         * Sebastian Cespedes Rodas 
        */
        bool estaCifrando = true;
        Hill hill = new Hill();
        Playfair playfair = new Playfair();

        public Form1()
        {
            InitializeComponent();


            //Adolfo Mendoza Ribera
            //-----------------------------------------
            comboBox1.Text = "Alfanumerico";
            pictureBoxes = new List<PictureBox>();
            lista = new List<int>(); // Inicializa la lista vacía
            this.MostrarUOcultar(false, 1);
            //-----------------------------------------
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
                // TODO_SEBASTIAN: , mejorar la logica
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


        /**************************** Sebastian FIN *********************************************************************/

        /*************************Adolfo INICIO ********************************************************/

        /**
         * Adolfo Mendoza Ribera 
        */
        List<int> lista = new List<int>(); // Inicializa la lista vacía
        CipherManager cipher1 = new CipherManager();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();




        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox0_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(0);
            this.agregarPinturaAux(0);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(1);
            this.agregarPinturaAux(1);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(2);
            this.agregarPinturaAux(2);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(3);
            this.agregarPinturaAux(3);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(4);
            this.agregarPinturaAux(4);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(5);
            this.agregarPinturaAux(5);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(6);
            this.agregarPinturaAux(6);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(7);
            this.agregarPinturaAux(7);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(8);
            this.agregarPinturaAux(8);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(16);
            this.agregarPinturaAux(16);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(15);
            this.agregarPinturaAux(15);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(14);
            this.agregarPinturaAux(14);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(13);
            this.agregarPinturaAux(13);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(12);
            this.agregarPinturaAux(12);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(11);
            this.agregarPinturaAux(11);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(10);
            this.agregarPinturaAux(10);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(9);
            this.agregarPinturaAux(9);
            //textBox1.Text = string.Join(", ", lista); // Muestra los elementos separados por comas
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled == false)
            {
                int[] arregloPosiciones = lista.ToArray(); // Convierte la lista a un arreglo

                string texto = comboBox1.SelectedItem.ToString();
                int opcion = 1;
                if (texto == "Alfanumerico")
                {
                    opcion = 1;
                }
                else if (texto == "Mixto")
                {
                    opcion = 0;
                }


                List<string> listaTextoCifrado = cipher1.DescifradorCesarMixto(arregloPosiciones, opcion);
                richTextBox1.Text = string.Join("", listaTextoCifrado); // Muestra los elementos separados por comas
            }
            else if (button2.Enabled == false)
            {
                // Inicializa la lista de letras
                List<string> listaLetras = new List<string>();

                string textoClaro = richTextBox2.Text; // Asegúrate de obtener el texto del control richTextBox2

                // Recorre el texto claro en orden inverso
                for (int i = textoClaro.Length - 1; i >= 0; i--)
                {
                    listaLetras.Add(textoClaro[i].ToString());
                }

                List<int> listaPosiciones = new List<int>();

                string texto = comboBox1.SelectedItem.ToString();
                int opcion = 1;
                if (texto == "Alfanumerico")
                {
                    opcion = 1;
                }
                else if (texto == "Mixto")
                {
                    opcion = 0;
                }

                // Invoca el método CifradorCesarMixto
                listaPosiciones = cipher1.CifradorCesarMixto(listaLetras, opcion);
                listaPosiciones.Reverse();
                flowLayoutPanel1.Controls.Clear();
                foreach (int num in listaPosiciones)
                {
                    this.agregarPintura(num);
                }

            }
            else if (button7.Enabled == false)
            {
                String textoClaro = richTextBox2.Text;
                String cifra = richTextBox3.Text;

                List<char> textoCifrado = cipher1.CifradoVigenere(textoClaro, cifra);


                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBox1.Text = new string(textoCifrado.ToArray());
            }
            else if (button4.Enabled == false)
            {
                String textoClaro = richTextBox2.Text;
                String cifra = richTextBox3.Text;

                List<char> textoCifrado = cipher1.DescifradoVigenere(textoClaro, cifra);


                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBox1.Text = new string(textoCifrado.ToArray());
            }
            else if (button5.Enabled == false)
            {
                int[,] matriz = cipher1.GenerarMatriz();
                string palabra = richTextBox2.Text;
                string clave = textBox2.Text;
                if (clave.Length == 4)
                {
                    List<int> textoCifrado = cipher1.CifradorHomofono(matriz, palabra, clave);
                    // Convertir la lista de enteros a una cadena de texto separada por comas
                    string textoCifradoString = string.Join(", ", textoCifrado);

                    // Asignar la cadena de texto al richTextBox1
                    richTextBox1.Text = textoCifradoString;
                }
                else
                {
                    richTextBox1.Text = "La clave debe tener 4 caracteres";
                }

            }
            else if (button11.Enabled == false)
            {
                int[,] matriz = cipher1.GenerarMatriz();
                string textoCifradoString = richTextBox2.Text; // Cambio: Convertir a cadena de texto
                string[] valoresTextoCifrado = textoCifradoString.Split(','); // Separar los valores por comas

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

                string clave = textBox2.Text.ToUpper(); // Convertir la clave a mayúsculas
                if (clave.Length == 4)
                {
                    List<char> textoClaro = cipher1.DescifradorHomofono(matriz, textoCifrado, clave);

                    // Convertir la lista de caracteres a una cadena de texto
                    string textoClaroString = new string(textoClaro.ToArray());

                    // Asignar la cadena de texto al richTextBox1
                    richTextBox1.Text = textoClaroString;
                }
                else
                {
                    richTextBox1.Text = "La clave debe tener 4 caracteres";
                }

            }

        }

        private void pictureBox17_Click_1(object sender, EventArgs e)
        {
            this.AgregarElemento(17);
            this.agregarPinturaAux(17);
        }

        private void agregarPintura(int num)
        {
            // Crear un nuevo PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = 30; // Establece el ancho deseado
            pictureBox.Height = 30; // Establece la altura deseada
            pictureBox.BackColor = System.Drawing.Color.LightGray; // Cambia el color de fondo si lo deseas
                                                                   // Construye la ruta completa al archivo de imagen
            string imageName = "img" + (num + 1);
            Type resourceType = typeof(Properties.Resources);
            PropertyInfo imageProperty = resourceType.GetProperty(imageName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            pictureBox.Image = (Image)imageProperty.GetValue(null, null);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


            // Agrega el PictureBox al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(pictureBox);

            // Opcional: Asigna una posición inicial dentro del FlowLayoutPanel
            // (El FlowLayoutPanel organizará automáticamente los controles)
            pictureBoxes.Add(pictureBox);

        }

        private void agregarPinturaAux(int num)
        {
            // Crear un nuevo PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = 20; // Establece el ancho deseado
            pictureBox.Height = 20; // Establece la altura deseada
            pictureBox.BackColor = System.Drawing.Color.LightGray; // Cambia el color de fondo si lo deseas
                                                                   // Construye la ruta completa al archivo de imagen

            string imageName = "img" + (num + 1);
            Type resourceType = typeof(Properties.Resources);
            PropertyInfo imageProperty = resourceType.GetProperty(imageName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            pictureBox.Image = (Image)imageProperty.GetValue(null, null);

            //pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


            // Agrega el PictureBox al FlowLayoutPanel
            flowLayoutPanel3.Controls.Add(pictureBox);

            // Opcional: Asigna una posición inicial dentro del FlowLayoutPanel
            // (El FlowLayoutPanel organizará automáticamente los controles)
            pictureBoxes.Add(pictureBox);

            // Agrega el controlador de eventos para el clic en el PictureBox
            pictureBox.Click += (sender, e) =>
            {
                // Elimina el PictureBox del FlowLayoutPanel
                flowLayoutPanel3.Controls.Remove(pictureBox);
                this.EliminarElemento(num);
                // También puedes eliminarlo de la lista de PictureBoxes si lo deseas
                pictureBoxes.Remove(pictureBox);
            };
        }


        private void AgregarElemento(int elemento)
        {
            lista.Insert(0, elemento); // Agrega el elemento al principio de la lista
        }
        private void EliminarElemento(int elemento)
        {
            lista.Remove(elemento); // Elimina el elemento de la lista
        }


        private void MostrarUOcultar(bool ver, int num)
        {

            // Supongamos que tienes 24 PictureBox en total
            for (int i = 0; i <= 25; i++)
            {
                PictureBox pictureBox = Controls.Find($"pictureBox{i}", true).FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.Enabled = ver;
                    pictureBox.Visible = ver;
                }
            }

            if (num == 1)
            {
                button1.Text = "Cifrar";
                button2.Enabled = false;
                button3.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button11.Enabled = true;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Escribe las letras que deseas cifrar";

                label6.Visible = false;
                label6.Enabled = false;

                label7.Visible = false;
                label7.Enabled = false;

                richTextBox2.Enabled = true;
                richTextBox2.Visible = true;
                richTextBox2.Text = "";

                richTextBox3.Enabled = false;
                richTextBox3.Visible = false;

                flowLayoutPanel1.Enabled = true;
                flowLayoutPanel1.Visible = true;
                flowLayoutPanel1.Controls.Clear();

                flowLayoutPanel3.Enabled = false;
                flowLayoutPanel3.Visible = false;
                flowLayoutPanel3.Controls.Clear();


                flowLayoutPanel2.Enabled = false;
                flowLayoutPanel2.Visible = false;

                textBox2.Enabled = false;
                textBox2.Visible = false;

                comboBox1.Enabled = true;
                comboBox1.Visible = true;





            }
            else if (num == 2)
            {
                button1.Text = "Descifrar";
                button2.Enabled = true;
                button3.Enabled = false;
                button7.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button11.Enabled = true;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Selecciona las imagenes que deseas descifrar";

                label6.Visible = false;
                label6.Enabled = false;

                label7.Visible = false;
                label7.Enabled = false;


                richTextBox1.Text = "";

                richTextBox2.Enabled = false;
                richTextBox2.Visible = false;

                richTextBox3.Enabled = false;
                richTextBox3.Visible = false;

                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;

                flowLayoutPanel2.Controls.Clear();
                flowLayoutPanel2.Enabled = true;
                flowLayoutPanel2.Visible = true;

                flowLayoutPanel3.Enabled = true;
                flowLayoutPanel3.Visible = true;
                flowLayoutPanel3.Controls.Clear();

                textBox2.Enabled = false;
                textBox2.Visible = false;

                comboBox1.Enabled = true;
                comboBox1.Visible = true;

            }
            else if (num == 3)
            {
                button1.Text = "Cifrar";
                button2.Enabled = true;
                button3.Enabled = true;
                button7.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                button11.Enabled = true;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Escribe las letras que deseas cifrar";

                label6.Visible = true;
                label6.Enabled = true;
                label6.Text = "Escribe el texto en claro";

                label7.Visible = true;
                label7.Enabled = true;
                label7.Text = "Escribe la clave";



                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                richTextBox1.Text = "";
                richTextBox2.Enabled = true;
                richTextBox2.Visible = true;
                richTextBox2.Text = "";

                richTextBox3.Enabled = true;
                richTextBox3.Visible = true;
                richTextBox3.Text = "";

                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Enabled = false;
                flowLayoutPanel2.Visible = false;

                textBox2.Enabled = false;
                textBox2.Visible = false;

                comboBox1.Enabled = false;
                comboBox1.Visible = false;


            }
            else if (num == 4)
            {
                button1.Text = "Descifrar";
                button2.Enabled = true;
                button3.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = true;
                button11.Enabled = true;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Escribe las letras que deseas descifrar";

                label6.Visible = true;
                label6.Enabled = true;
                label6.Text = "Escribe el texto cifrado";

                label7.Visible = true;
                label7.Enabled = true;
                label7.Text = "Escribe la clave";


                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                richTextBox1.Text = "";
                richTextBox2.Enabled = true;
                richTextBox2.Visible = true;
                richTextBox2.Text = "";

                richTextBox3.Enabled = true;
                richTextBox3.Visible = true;
                richTextBox3.Text = "";

                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Enabled = false;
                flowLayoutPanel2.Visible = false;

                textBox2.Enabled = false;
                textBox2.Visible = false;

                comboBox1.Enabled = false;
                comboBox1.Visible = false;
            }
            else if (num == 5)
            {
                button1.Text = "Cifrar";
                button2.Enabled = true;
                button3.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = false;
                button11.Enabled = true;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Escribe las letras que deseas cifrar";

                label6.Visible = true;
                label6.Enabled = true;
                label6.Text = "Escribe el texto en claro";

                label7.Visible = true;
                label7.Enabled = true;
                label7.Text = "Escribe la clave que tenga 4 caracteres";

                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                richTextBox1.Text = "";
                richTextBox2.Enabled = true;
                richTextBox2.Visible = true;
                richTextBox2.Text = "";
                richTextBox3.Enabled = false;
                richTextBox3.Visible = false;

                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Enabled = false;
                flowLayoutPanel2.Visible = false;

                textBox2.Enabled = true;
                textBox2.Visible = true;

                comboBox1.Enabled = false;
                comboBox1.Visible = false;
            }
            else if (num == 6)
            {
                button1.Text = "Descifrar";
                button2.Enabled = true;
                button3.Enabled = true;
                button7.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button11.Enabled = false;

                label4.Visible = true;
                label4.Enabled = true;
                label4.Text = "Escribe las letras que deseas cifrar";

                label6.Visible = true;
                label6.Enabled = true;
                label6.Text = "Escribe el texto cifrado, que son maximo dos digitos y separados por comas";


                label7.Visible = true;
                label7.Enabled = true;
                label7.Text = "Escribe la clave que tenga 4 caracteres";

                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                richTextBox1.Text = "";
                richTextBox2.Enabled = true;
                richTextBox2.Visible = true;
                richTextBox2.Text = "";
                richTextBox3.Enabled = false;
                richTextBox3.Visible = false;

                flowLayoutPanel1.Enabled = false;
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Enabled = false;
                flowLayoutPanel2.Visible = false;

                textBox2.Enabled = true;
                textBox2.Visible = true;

                comboBox1.Enabled = false;
                comboBox1.Visible = false;
            }
            button1.Enabled = true;
            button1.Visible = true;
            richTextBox1.Visible = true;
            richTextBox1.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBoxes = new List<PictureBox>();
            lista = new List<int>(); // Inicializa la lista vacía
            this.MostrarUOcultar(false, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBoxes = new List<PictureBox>();
            lista = new List<int>(); // Inicializa la lista vacía
            this.MostrarUOcultar(true, 2);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Oculta los botones de maximizar, minimizar y cerrar en la esquina superior derecha de la ventana
            this.FormBorderStyle = FormBorderStyle.None;
            tabControl1.Size = new Size(1350, 700);


            // Ajusta el tamaño del formulario para que ocupe toda la pantalla
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Muestra los botones de maximizar, minimizar y cerrar en la esquina superior derecha de la ventana
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Restaura el tamaño original del formulario (puedes ajustar esto según tus necesidades)
            this.WindowState = FormWindowState.Normal;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            textBox2.Text = "";
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            lista = new List<int>(); // Inicializa la lista vacía

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            lista = new List<int>(); // Inicializa la lista vacía
            this.MostrarUOcultar(false, 3);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            lista = new List<int>(); // Inicializa la lista vacía
            this.MostrarUOcultar(false, 4);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int[,] matriz = cipher1.GenerarMatriz();
            cipher1.RotarFilaNVeces(matriz, 1, 5);

            // Crear una cadena de texto para almacenar el contenido de la matriz
            string contenidoMatriz = "";

            for (int fila = 0; fila < matriz.GetLength(0); fila++)
            {
                for (int columna = 0; columna < matriz.GetLength(1); columna++)
                {
                    contenidoMatriz += matriz[fila, columna].ToString("D2") + "\t";
                }
                contenidoMatriz += Environment.NewLine; // Agregar salto de línea al final de cada fila
            }

            // Asignar la cadena de texto al richTextBox2
            richTextBox2.Text = contenidoMatriz;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[,] matriz = cipher1.GenerarMatriz(); // Utiliza el nombre de la función sin "cipher1."
            int num = cipher1.BuscarNumeroEnMatriz(matriz, int.Parse(textBox2.Text));
            int columna;
            if (int.TryParse(textBox2.Text, out columna))
            {
                int[] valores = cipher1.ObtenerValoresColumna(matriz, num);

                // Convierte los valores a una cadena antes de asignarlos al richTextBox2.Text
                richTextBox2.Text = string.Join(", ", valores);
            }
            else
            {
                // Maneja el caso en que la conversión de la entrada a un número entero falla
                // Por ejemplo, muestra un mensaje de error al usuario
                richTextBox2.Text = "Error: Ingresa un número válido en el cuadro de texto.";
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.MostrarUOcultar(false, 5);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.MostrarUOcultar(false, 6);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(18);
            this.agregarPinturaAux(18);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(19);
            this.agregarPinturaAux(19);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(20);
            this.agregarPinturaAux(20);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(21);
            this.agregarPinturaAux(21);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(22);
            this.agregarPinturaAux(22);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(23);
            this.agregarPinturaAux(23);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(24);
            this.agregarPinturaAux(24);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            this.AgregarElemento(25);
            this.agregarPinturaAux(25);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            ///encriptao playfair
            String clave = qey.Text;
            clave = clave.Trim();
            String texto = input.Text;
            texto = texto.Trim();
            playfair.run(clave);
            String salida = playfair.Encriptar(texto, clave);

            output.Text = salida;
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            ///descencri playfair
            String clave = qey.Text;
            clave = clave.Trim();
            String cripto = input.Text;
            cripto = cripto.Trim();
            String salida = playfair.Desencriptar(cripto, clave);
            output.Text = salida;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            qey.Clear();
            input.Clear();
            output.Clear();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            //boton `encriptar hill
            String clave = qey.Text;
            clave = clave.Trim();

            String texto = input.Text;
            texto = texto.Trim();

            int nnn = int.Parse(nxn.Text);
            hill.run(clave, nnn);
            String salida = hill.Encriptar(texto, clave, nnn);
            output.Text = salida;


        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //boton desencriptar hill
            String clave = qey.Text;
            clave = clave.Trim();
            String texto = input.Text;
            texto = texto.Trim();
            String salida = hill.Desencriptar(texto, clave);
            output.Text = salida;
        }

        private void qey_TextChanged(object sender, EventArgs e)
        {

        }
        private void buttonVigenere_Click(object sender, EventArgs e)
        {
            // vigenere cifrar
            Vigenere vigenere = new Vigenere();
            Edit3.Text = vigenere.Cifrar(Edit1.Text, Edit2.Text);
        }

        private void buttonVigenereDes_Click(object sender, EventArgs e)
        {
            Vigenere vigenere = new Vigenere();
            Edit1.Text = vigenere.Descifrar(Edit3.Text, Edit2.Text);

        }

        /*************************Adolfo Fin ********************************************************/

        private void buttonCCC_Click(object sender, EventArgs e)
        {
            string textoClaro = richTextBoxTC.Text;
            string clave = richTextBoxTCF.Text;

            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            labelAviso.Text = "";
            if (clave.Length >= textoClaro.Length)
            {
                List<char> textoCifrado = cipher1.CifradorClaveContinua(textoClaro, clave);

                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBoxResultado.Text = new string(textoCifrado.ToArray());
            }
            else
            {
                labelAviso.Text = "La longitud de la clave es menor que el texto en claro, tiene que ser igual o mayor";
            }
        }

        private void buttonDCC_Click(object sender, EventArgs e)
        {
            string textoClaro = richTextBoxTC.Text;
            string clave = richTextBoxTCF.Text;


            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            labelAviso.Text = "";
            if (clave.Length >= textoClaro.Length)
            {
                List<char> textoCifrado = cipher1.DescifradorClaveContinua(textoClaro, clave);

                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBoxResultado.Text = new string(textoCifrado.ToArray());
            }
            else
            {
                labelAviso.Text = "La longitud de la clave es menor que el texto en claro, tiene que ser igual o mayor";
            }
        }

        private void buttonCV_Click(object sender, EventArgs e)
        {
            string textoClaro = richTextBoxTC.Text;
            string clave = richTextBoxTCF.Text;


            textoClaro = textoClaro.Replace(" ", "").ToUpper();
            textoClaro = textoClaro.Replace(",", "").ToUpper();
            textoClaro = textoClaro.Replace(".", "").ToUpper();
            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            labelAviso.Text = "";
            if (textoClaro.Length <= clave.Length)
            {
                List<char> textoCifrado = cipher1.CifradorVernam(textoClaro, clave);

                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBoxResultado.Text = new string(textoCifrado.ToArray());
            }
            else if (textoClaro.Length > clave.Length)
            {
                labelAviso.Text = "La longitud de la clave tiene que ser igual o mayor que el texto en claro";
            }
        }

        public int BinarioADecimal(string input)
        {
            char[] array = input.ToCharArray(); // Invertido porque los valores incrementan de derecha a izquierda: 16-8-4-2-1
            Array.Reverse(array);
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '1')
                {
                    // Usamos la potencia de 2 según la posición
                    sum += (int)Math.Pow(2, i);
                }
            }

            return sum;
        }

        private void buttonDV_Click(object sender, EventArgs e)
        {
            string textoCifrado = richTextBoxTC.Text;
            string clave = richTextBoxTCF.Text;

            clave = clave.Replace(" ", "").ToUpper();
            clave = clave.Replace(",", "");
            clave = clave.Replace(".", "");

            List<int> listaCifrado = new List<int>();

            // Divide la cadena en subcadenas utilizando el espacio como separador
            string[] subcadenas = textoCifrado.Split(' ');

            foreach (string subcadena in subcadenas)
            {
                // Convierte cada subcadena en un número entero
                int numero;
                if (int.TryParse(subcadena, out numero))
                {
                    int k = this.BinarioADecimal(subcadena);
                    listaCifrado.Add(k);
                }
                else
                {
                    Console.WriteLine($"No se pudo convertir '{subcadena}' a un número entero.");
                }
            }

            labelAviso.Text = "";
            if (listaCifrado.Count <= clave.Length)
            {
                List<char> textoclaro = cipher1.DescifradorVernam(listaCifrado, clave);

                // Convierte la lista de caracteres cifrados a una cadena y muestra el resultado
                richTextBoxResultado.Text = new string(textoclaro.ToArray());
            }
            else if (textoCifrado.Length > clave.Length)
            {
                labelAviso.Text = "La longitud de la clave tiene que ser igual o mayor que el texto en claro";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            //AQUI EL SEGUNDO EDIT TNEGOQ UE MANDARLE UN NUMEERO PERO COMO CONVERITO DE INT A CADENA

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
                MessageBox.Show("Por favor, ingresa un número válido en el segundo campo.");
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
    }
}