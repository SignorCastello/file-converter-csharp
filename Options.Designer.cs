namespace file_converter
{
    partial class Options
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
            JPGqualitySlider = new TrackBar();
            textBox1 = new TextBox();
            optionClose = new Button();
            ((System.ComponentModel.ISupportInitialize)JPGqualitySlider).BeginInit();
            SuspendLayout();
            // 
            // JPGqualitySlider
            // 
            JPGqualitySlider.Location = new Point(12, 30);
            JPGqualitySlider.Name = "JPGqualitySlider";
            JPGqualitySlider.Size = new Size(130, 56);
            JPGqualitySlider.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(148, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(205, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "JPG Quality";
            // 
            // optionClose
            // 
            optionClose.Location = new Point(129, 185);
            optionClose.Name = "optionClose";
            optionClose.Size = new Size(94, 29);
            optionClose.TabIndex = 2;
            optionClose.Text = "OK.";
            optionClose.UseVisualStyleBackColor = true;
            optionClose.Click += optionClose_Click;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 226);
            Controls.Add(optionClose);
            Controls.Add(textBox1);
            Controls.Add(JPGqualitySlider);
            Name = "Options";
            Text = "Options";
            ((System.ComponentModel.ISupportInitialize)JPGqualitySlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar JPGqualitySlider;
        private TextBox textBox1;
        private Button optionClose;
    }
}