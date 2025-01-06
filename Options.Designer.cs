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
            JPGQualityLabel = new TextBox();
            optionClose = new Button();
            amplificationLabel = new TextBox();
            amplification = new TextBox();
            ((System.ComponentModel.ISupportInitialize)JPGqualitySlider).BeginInit();
            SuspendLayout();
            // 
            // JPGqualitySlider
            // 
            JPGqualitySlider.Location = new Point(12, 30);
            JPGqualitySlider.Minimum = 1;
            JPGqualitySlider.Name = "JPGqualitySlider";
            JPGqualitySlider.Size = new Size(130, 56);
            JPGqualitySlider.TabIndex = 0;
            JPGqualitySlider.Value = 1;
            // 
            // JPGQualityLabel
            // 
            JPGQualityLabel.Location = new Point(148, 30);
            JPGQualityLabel.Name = "JPGQualityLabel";
            JPGQualityLabel.Size = new Size(205, 27);
            JPGQualityLabel.TabIndex = 1;
            JPGQualityLabel.Text = "JPG Quality";
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
            // amplificationLabel
            // 
            amplificationLabel.Location = new Point(148, 30);
            amplificationLabel.Name = "amplificationLabel";
            amplificationLabel.Size = new Size(205, 27);
            amplificationLabel.TabIndex = 3;
            amplificationLabel.Text = "Amplification (in dB)";
            // 
            // amplification
            // 
            amplification.Location = new Point(12, 30);
            amplification.Name = "amplification";
            amplification.Size = new Size(125, 27);
            amplification.TabIndex = 4;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 226);
            Controls.Add(amplification);
            Controls.Add(amplificationLabel);
            Controls.Add(optionClose);
            Controls.Add(JPGQualityLabel);
            Controls.Add(JPGqualitySlider);
            Name = "Options";
            Text = "Options";
            Load += Options_Load;
            ((System.ComponentModel.ISupportInitialize)JPGqualitySlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar JPGqualitySlider;
        private TextBox JPGQualityLabel;
        private Button optionClose;
        private TextBox amplificationLabel;
        private TextBox amplification;
    }
}