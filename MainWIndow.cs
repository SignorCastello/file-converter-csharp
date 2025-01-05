using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace file_converter
{
    public partial class MainWIndow : Form
    {
        public string? filePath;
        public int quality;
        public bool hasQualityChanged;
        public MainWIndow()
        {
            InitializeComponent();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[]? files = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                files = e.Data.GetData(DataFormats.FileDrop) as string[];
            }
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                filePath = Path.GetFullPath(file);
                listBox1.Items.Add(fileName);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedFileName = listBox1.SelectedItem as string;
            if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".jpg")
            {
                //you selected an image
                tipoFile.Text = "IMAGE";
                if(Path.GetExtension(selectedFileName)?.ToLower() == ".png")
                {
                    outputType.Text = "JPG";
                }
                if(Path.GetExtension(selectedFileName)?.ToLower() == ".jpg")
                {
                    outputType.Text = "PNG";
                }
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if(hasQualityChanged == false) //default value for quality
            {
                quality = 90;
            }
            string fileFormat = outputType.Text;
            string ? outputPath = Path.ChangeExtension(filePath, fileFormat);
            if (fileFormat == "JPG" && outputPath != null && filePath != null)
            {
                using (var img = SixLabors.ImageSharp.Image.Load(filePath))
                {
                    img.Save(outputPath, new JpegEncoder { Quality = quality });
                }
            }
            if (fileFormat == "PNG" && outputPath != null && filePath != null)
            {
                using (var img = SixLabors.ImageSharp.Image.Load(filePath))
                {
                    img.Save(outputPath, new PngEncoder());
                }
            }
            MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
        }

        private void moreOptions_Click(object sender, EventArgs e)
        {
            using(Options options = new Options())
            {
                options.ShowDialog();
                quality = options.quality;
            }
        }
    }
}
