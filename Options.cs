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
        public int quality;
        public bool hasQualityChanged = false;
        public Options()
        {
            InitializeComponent();
        }

        public void optionClose_Click(object sender, EventArgs e)
        {
            quality = JPGqualitySlider.Value * 10; //so 10 gets to 100
            hasQualityChanged = true;
            this.Close();
        }
    }
}
