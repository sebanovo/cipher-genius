namespace cipher_genius 
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioDoble = new System.Windows.Forms.RadioButton();
            this.radioSimple = new System.Windows.Forms.RadioButton();
            this.buttonCifrar = new System.Windows.Forms.Button();
            this.buttonDescifrar = new System.Windows.Forms.Button();
            this.buttonConvertir = new System.Windows.Forms.Button();
            this.buttonReiniciar = new System.Windows.Forms.Button();
            this.radioPorGrupos = new System.Windows.Forms.RadioButton();
            this.textBoxMensaje = new System.Windows.Forms.TextBox();
            this.textBoxClave = new System.Windows.Forms.TextBox();
            this.textBoxCriptograma = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBoxPermutacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioDoble
            // 
            this.radioDoble.AutoSize = true;
            this.radioDoble.Location = new System.Drawing.Point(661, 102);
            this.radioDoble.Name = "radioDoble";
            this.radioDoble.Size = new System.Drawing.Size(132, 21);
            this.radioDoble.TabIndex = 5;
            this.radioDoble.Text = "Columnas Doble";
            this.radioDoble.UseVisualStyleBackColor = true;
            this.radioDoble.CheckedChanged += new System.EventHandler(this.radioDoble_CheckedChanged);
            // 
            // radioSimple
            // 
            this.radioSimple.AutoSize = true;
            this.radioSimple.Location = new System.Drawing.Point(661, 129);
            this.radioSimple.Name = "radioSimple";
            this.radioSimple.Size = new System.Drawing.Size(97, 21);
            this.radioSimple.TabIndex = 6;
            this.radioSimple.TabStop = true;
            this.radioSimple.Text = "Fila Simple";
            this.radioSimple.UseVisualStyleBackColor = true;
            this.radioSimple.CheckedChanged += new System.EventHandler(this.radioSimple_CheckedChanged);
            // 
            // buttonCifrar
            // 
            this.buttonCifrar.Location = new System.Drawing.Point(13, 19);
            this.buttonCifrar.Name = "buttonCifrar";
            this.buttonCifrar.Size = new System.Drawing.Size(88, 32);
            this.buttonCifrar.TabIndex = 7;
            this.buttonCifrar.Text = "Cifrar";
            this.buttonCifrar.UseVisualStyleBackColor = true;
            this.buttonCifrar.Click += new System.EventHandler(this.buttonCifrar_Click);
            // 
            // buttonDescifrar
            // 
            this.buttonDescifrar.Location = new System.Drawing.Point(107, 19);
            this.buttonDescifrar.Name = "buttonDescifrar";
            this.buttonDescifrar.Size = new System.Drawing.Size(88, 32);
            this.buttonDescifrar.TabIndex = 8;
            this.buttonDescifrar.Text = "Descifrar";
            this.buttonDescifrar.UseVisualStyleBackColor = true;
            this.buttonDescifrar.Click += new System.EventHandler(this.buttonDescifrar_Click);
            // 
            // buttonConvertir
            // 
            this.buttonConvertir.Location = new System.Drawing.Point(799, 77);
            this.buttonConvertir.Name = "buttonConvertir";
            this.buttonConvertir.Size = new System.Drawing.Size(88, 32);
            this.buttonConvertir.TabIndex = 9;
            this.buttonConvertir.Text = "Convertir";
            this.buttonConvertir.UseVisualStyleBackColor = true;
            this.buttonConvertir.Click += new System.EventHandler(this.buttonConvertir_Click);
            // 
            // buttonReiniciar
            // 
            this.buttonReiniciar.Location = new System.Drawing.Point(799, 115);
            this.buttonReiniciar.Name = "buttonReiniciar";
            this.buttonReiniciar.Size = new System.Drawing.Size(88, 32);
            this.buttonReiniciar.TabIndex = 10;
            this.buttonReiniciar.Text = "Reiniciar";
            this.buttonReiniciar.UseVisualStyleBackColor = true;
            this.buttonReiniciar.Click += new System.EventHandler(this.buttonReiniciar_Click_1);
            // 
            // radioPorGrupos
            // 
            this.radioPorGrupos.AutoSize = true;
            this.radioPorGrupos.Location = new System.Drawing.Point(661, 75);
            this.radioPorGrupos.Name = "radioPorGrupos";
            this.radioPorGrupos.Size = new System.Drawing.Size(102, 21);
            this.radioPorGrupos.TabIndex = 11;
            this.radioPorGrupos.Text = "Por Grupos";
            this.radioPorGrupos.UseVisualStyleBackColor = true;
            this.radioPorGrupos.CheckedChanged += new System.EventHandler(this.radioPorGrupos_CheckedChanged);
            // 
            // textBoxMensaje
            // 
            this.textBoxMensaje.Location = new System.Drawing.Point(142, 85);
            this.textBoxMensaje.Name = "textBoxMensaje";
            this.textBoxMensaje.Size = new System.Drawing.Size(415, 22);
            this.textBoxMensaje.TabIndex = 12;
            // 
            // textBoxClave
            // 
            this.textBoxClave.Location = new System.Drawing.Point(142, 113);
            this.textBoxClave.Name = "textBoxClave";
            this.textBoxClave.Size = new System.Drawing.Size(132, 22);
            this.textBoxClave.TabIndex = 13;
            // 
            // textBoxCriptograma
            // 
            this.textBoxCriptograma.Location = new System.Drawing.Point(142, 141);
            this.textBoxCriptograma.Name = "textBoxCriptograma";
            this.textBoxCriptograma.Size = new System.Drawing.Size(415, 22);
            this.textBoxCriptograma.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Mensaje en claro:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Clave";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Criptograma";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 185);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(451, 288);
            this.dataGridView1.TabIndex = 18;
            // 
            // textBoxPermutacion
            // 
            this.textBoxPermutacion.Location = new System.Drawing.Point(419, 47);
            this.textBoxPermutacion.Multiline = true;
            this.textBoxPermutacion.Name = "textBoxPermutacion";
            this.textBoxPermutacion.Size = new System.Drawing.Size(137, 24);
            this.textBoxPermutacion.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(416, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "permutación";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(476, 185);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(451, 288);
            this.dataGridView2.TabIndex = 23;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(933, 185);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(451, 288);
            this.dataGridView3.TabIndex = 24;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1432, 535);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1424, 506);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ADOLFO";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1424, 506);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JOSUE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1424, 506);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MEJIAS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonDescifrar);
            this.tabPage4.Controls.Add(this.dataGridView3);
            this.tabPage4.Controls.Add(this.radioDoble);
            this.tabPage4.Controls.Add(this.dataGridView2);
            this.tabPage4.Controls.Add(this.radioSimple);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.buttonCifrar);
            this.tabPage4.Controls.Add(this.textBoxPermutacion);
            this.tabPage4.Controls.Add(this.buttonConvertir);
            this.tabPage4.Controls.Add(this.dataGridView1);
            this.tabPage4.Controls.Add(this.buttonReiniciar);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.radioPorGrupos);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.textBoxMensaje);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.textBoxClave);
            this.tabPage4.Controls.Add(this.textBoxCriptograma);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1424, 506);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "SEBASTIAN";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1424, 506);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "DANIEL";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 560);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton radioDoble;
        private System.Windows.Forms.RadioButton radioSimple;
        private System.Windows.Forms.Button buttonCifrar;
        private System.Windows.Forms.Button buttonDescifrar;
        private System.Windows.Forms.Button buttonConvertir;
        private System.Windows.Forms.Button buttonReiniciar;
        private System.Windows.Forms.RadioButton radioPorGrupos;
        private System.Windows.Forms.TextBox textBoxMensaje;
        private System.Windows.Forms.TextBox textBoxClave;
        private System.Windows.Forms.TextBox textBoxCriptograma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxPermutacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
    }
}

