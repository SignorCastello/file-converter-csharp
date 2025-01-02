namespace file_converter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            outputType = new ComboBox();
            textBox1 = new TextBox();
            tipoFile = new TextBox();
            Convert = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.AllowDrop = true;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 19;
            listBox1.Location = new Point(12, 54);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(284, 384);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox1.DragEnter += listBox1_DragEnter;
            // 
            // outputType
            // 
            outputType.FormattingEnabled = true;
            outputType.Items.AddRange(new object[] { "JPG", "PNG" });
            outputType.Location = new Point(463, 303);
            outputType.Name = "outputType";
            outputType.Size = new Size(151, 27);
            outputType.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Control;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(302, 54);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(486, 20);
            textBox1.TabIndex = 2;
            textBox1.Text = "The selected file is...";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // tipoFile
            // 
            tipoFile.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tipoFile.Location = new Point(478, 80);
            tipoFile.Name = "tipoFile";
            tipoFile.Size = new Size(125, 28);
            tipoFile.TabIndex = 3;
            tipoFile.TextAlign = HorizontalAlignment.Center;
            // 
            // Convert
            // 
            Convert.Location = new Point(463, 336);
            Convert.Name = "Convert";
            Convert.Size = new Size(151, 77);
            Convert.TabIndex = 4;
            Convert.Text = "CONVERT!";
            Convert.UseVisualStyleBackColor = true;
            Convert.Click += Convert_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Convert);
            Controls.Add(tipoFile);
            Controls.Add(textBox1);
            Controls.Add(outputType);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private ComboBox outputType;
        private TextBox textBox1;
        private TextBox tipoFile;
        private Button Convert;
    }
}
