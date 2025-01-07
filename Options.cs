using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace file_converter
{
    public partial class Options : Form
    {
        public bool IsCurrentFileImage = false;
        public bool IsCurrentFileVideo = false;
        public int quality;
        public int dBLevel;
        public bool hasQualityChanged = false;
        public bool hasAmplificationChanged = false;
        public Options()
        {
            InitializeComponent();
        }

        public void optionClose_Click(object sender, EventArgs e)
        {
            quality = JPGqualitySlider.Value * 10; //so 10 gets to 100
            if (int.TryParse(amplification.Text, out int a))
            {
                dBLevel = a;
            }
            hasQualityChanged = true;
            this.Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            //when the options field is loaded, hide the elements
            JPGqualitySlider.Visible = false;
            JPGQualityLabel.Visible = false;
            amplification.Visible = false;
            amplificationLabel.Visible = false;
            if (IsCurrentFileImage == true)
            {
                JPGqualitySlider.Visible = true;
                JPGQualityLabel.Visible = true;
            }
            if (IsCurrentFileVideo == true)
            {
                amplification.Visible = true;
                amplificationLabel.Visible = true;
            }
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            JPGqualitySlider.Visible = false;
            JPGQualityLabel.Visible = false;
            amplification.Visible = false;
            amplificationLabel.Visible = false;
        }
    }
}
