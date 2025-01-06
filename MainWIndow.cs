using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using FFMpegCore;


namespace file_converter
{
    public partial class MainWIndow : Form
    {
        public string? filePath;
        public string file;
        public int quality;
        public bool hasQualityChanged;
        public bool IsCurrentFileImage;
        public bool IsCurrentFileVideo;
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
            if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".jpg" || Path.GetExtension(selectedFileName)?.ToLower() == ".webp")
            {
                //you selected an image
                tipoFile.Text = "IMAGE";
                outputType.Items.AddRange(["JPG", "PNG", "WEBP"]);
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".webp")
                {
                    outputType.Text = "JPG";
                }
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".jpg")
                {
                    outputType.Text = "PNG";
                }
                IsCurrentFileImage = true; //this will be needed for the Options window. I don't know how to make this easier
            }
            if (Path.GetExtension(selectedFileName)?.ToLower() == ".mp4" || Path.GetExtension(selectedFileName)?.ToLower() == ".mkv" || Path.GetExtension(selectedFileName)?.ToLower() == ".webm")
            {
                tipoFile.Text = "VIDEO";
                outputType.Items.AddRange(["MP4", "MKV", "AVI"]);
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".mkv" || Path.GetExtension(selectedFileName)?.ToLower() == ".webm" || Path.GetExtension(selectedFileName)?.ToLower() == ".avi")
                {
                    outputType.Text = "MP4";
                }
                IsCurrentFileVideo = true;
            }
        }
        private bool IsImage(string filePath)
        {
            if (Path.GetExtension(filePath)?.ToLower() == ".png" || Path.GetExtension(filePath)?.ToLower() == ".jpg" || Path.GetExtension(filePath)?.ToLower() == ".webp")
            {
                return true;
            }
            return false;
        }
        private bool IsVideo(string filePath)
        {
            if (Path.GetExtension(filePath)?.ToLower() == ".mp4" || Path.GetExtension(filePath)?.ToLower() == ".mkv" || Path.GetExtension(filePath)?.ToLower() == ".webm")
            {
                return true;
            }
            return false;
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if (hasQualityChanged == false) //default value for quality
            {
                quality = 90;
            }
            string fileFormat = outputType.Text.ToLower(); //so the extension is .jpg and not .JPG
            string? outputPath = Path.ChangeExtension(filePath, fileFormat);
            if (filePath != null && outputPath != null)
            {
                //images
                if (IsImage(filePath))
                {
                    using (var img = SixLabors.ImageSharp.Image.Load(filePath))
                    {
                        switch (fileFormat) //i don't know why but i'm proud of this.
                        {
                            case "jpg":
                                img.Save(outputPath, new JpegEncoder() { Quality = quality });
                                if (File.Exists(outputPath))
                                {
                                    MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to convert.");
                                }
                                break;
                            case "png":
                                img.Save(outputPath, new PngEncoder());
                                if (File.Exists(outputPath))
                                {
                                    MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to convert.");
                                }
                                break;
                            case "webp":
                                img.Save(outputPath, new WebpEncoder());
                                if (File.Exists(outputPath))
                                {
                                    MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to convert.");
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid output format");
                                return;
                        }
                    }
                }
                //now for the videos
                if (IsVideo(filePath))
                {
                    switch (fileFormat)
                    {
                        case "mp4":
                            MessageBox.Show("Your file is now exporting. Please wait...");
                            try //my first try with error catching.
                            {
                                FFMpegArguments
                                    .FromFileInput(filePath)
                                    .OutputToFile(outputPath, overwrite: true, options => options
                                        .WithFastStart()
                                        .WithVideoCodec("libx264")
                                        .WithAudioCodec("aac")
                                    )
                                    .ProcessSynchronously();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
                            }
                            break;
                        case "mkv":
                            MessageBox.Show("Your file is now exporting. Please wait...");
                            try
                            {
                                FFMpegArguments
                                    .FromFileInput(filePath)
                                    .OutputToFile(outputPath, overwrite: true, options => options
                                        .WithFastStart()
                                        .WithVideoCodec("libx264")
                                        .WithAudioCodec("aac")
                                    )
                                    .ProcessSynchronously();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
                            }
                            break;
                        case "avi":
                            MessageBox.Show("Your file is now exporting. Please wait...");
                            try
                            {
                                FFMpegArguments
                                    .FromFileInput(filePath)
                                    .OutputToFile(outputPath, overwrite: true, options => options
                                        .WithFastStart()
                                        .WithVideoCodec("libx264")
                                        .WithAudioCodec("aac")
                                    )
                                    .ProcessSynchronously();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
                            }
                            break;
                    }
                }
            }
        }
        private void moreOptions_Click(object sender, EventArgs e)
        {
            using (Options options = new Options())
            {
                options.IsCurrentFileImage = IsCurrentFileImage;
                options.IsCurrentFileVideo = IsCurrentFileVideo;
                options.ShowDialog();
                quality = options.quality;
            }
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        file = reader.ReadToEnd();
                        string fileName = Path.GetFileName(filePath);
                        listBox1.Items.Add(fileName);
                    }
                }
            }
        }
    }
}
