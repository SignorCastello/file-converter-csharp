namespace file_converter
{
    partial class MainWIndow
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
            Button browse_button;
            listBox1 = new ListBox();
            outputType = new ComboBox();
            textBox1 = new TextBox();
            tipoFile = new TextBox();
            Convert = new Button();
            moreOptions = new Button();
            textBox2 = new TextBox();
            browse_button = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.AllowDrop = true;
            listBox1.BackColor = SystemColors.WindowText;
            listBox1.ForeColor = SystemColors.Window;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 19;
            listBox1.Location = new Point(12, 54);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(284, 346);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox1.DragEnter += listBox1_DragEnter;
            // 
            // outputType
            // 
            outputType.BackColor = SystemColors.WindowText;
            outputType.ForeColor = SystemColors.Window;
            outputType.FormattingEnabled = true;
            outputType.Location = new Point(463, 303);
            outputType.Name = "outputType";
            outputType.Size = new Size(151, 27);
            outputType.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.WindowText;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(302, 54);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(486, 20);
            textBox1.TabIndex = 2;
            textBox1.Text = "The selected file is...";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // tipoFile
            // 
            tipoFile.BackColor = SystemColors.WindowText;
            tipoFile.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tipoFile.ForeColor = SystemColors.Window;
            tipoFile.Location = new Point(478, 80);
            tipoFile.Name = "tipoFile";
            tipoFile.Size = new Size(125, 28);
            tipoFile.TabIndex = 3;
            tipoFile.TextAlign = HorizontalAlignment.Center;
            // 
            // Convert
            // 
            Convert.BackColor = SystemColors.WindowFrame;
            Convert.ForeColor = SystemColors.WindowText;
            Convert.Location = new Point(463, 336);
            Convert.Name = "Convert";
            Convert.Size = new Size(151, 77);
            Convert.TabIndex = 4;
            Convert.Text = "CONVERT!";
            Convert.UseVisualStyleBackColor = false;
            Convert.Click += Convert_Click;
            // 
            // moreOptions
            // 
            moreOptions.BackColor = SystemColors.WindowText;
            moreOptions.Location = new Point(620, 384);
            moreOptions.Name = "moreOptions";
            moreOptions.Size = new Size(94, 29);
            moreOptions.TabIndex = 5;
            moreOptions.Text = "Options...";
            moreOptions.UseVisualStyleBackColor = false;
            moreOptions.Click += moreOptions_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Black;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(12, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(776, 28);
            textBox2.TabIndex = 6;
            textBox2.Text = "FILE CONVERTER";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // browse_button
            // 
            browse_button.BackColor = SystemColors.WindowText;
            browse_button.Location = new Point(12, 409);
            browse_button.Name = "browse_button";
            browse_button.Size = new Size(284, 29);
            browse_button.TabIndex = 7;
            browse_button.Text = "Browse...";
            browse_button.UseVisualStyleBackColor = false;
            browse_button.Click += browse_button_Click;
            // 
            // MainWIndow
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(800, 450);
            Controls.Add(browse_button);
            Controls.Add(textBox2);
            Controls.Add(moreOptions);
            Controls.Add(Convert);
            Controls.Add(tipoFile);
            Controls.Add(textBox1);
            Controls.Add(outputType);
            Controls.Add(listBox1);
            ForeColor = SystemColors.Control;
            Name = "MainWIndow";
            Text = "File Converter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private ComboBox outputType;
        private TextBox textBox1;
        private TextBox tipoFile;
        private Button Convert;
        private Button moreOptions;
        private TextBox textBox2;
    }
}
