using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using FFMpegCore;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;


namespace file_converter
{
    public partial class MainWIndow : Form
    {
        public string? filePath;
        public string file;
        public int quality;
        public double dBLevel = 0;
        public bool hasQualityChanged;
        public bool hasAmplificationChanged;
        public bool IsCurrentFileImage;
        public bool IsCurrentFileVideo;
        public bool IsCurrentFileAudio;
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
            IsCurrentFileImage = false;
            IsCurrentFileVideo = false;
            IsCurrentFileAudio = false;
            string? selectedFileName = listBox1.SelectedItem as string;
            if (selectedFileName != null)
            {
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".jpg" || Path.GetExtension(selectedFileName)?.ToLower() == ".webp")
                {
                    //you selected an image
                    FileTypeLabel.Text = "IMAGE";
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
                    FileTypeLabel.Text = "VIDEO";
                    outputType.Items.AddRange(["MP4", "MKV", "AVI"]);
                    if (Path.GetExtension(selectedFileName)?.ToLower() == ".mkv" || Path.GetExtension(selectedFileName)?.ToLower() == ".webm" || Path.GetExtension(selectedFileName)?.ToLower() == ".avi")
                    {
                        outputType.Text = "MP4";
                    }
                    IsCurrentFileVideo = true;
                }
                if(MimeTypes.GetMimeType(selectedFileName).StartsWith("audio/"))
                {
                    //you selected an audio file
                    FileTypeLabel.Text = "AUDIO";
                    outputType.Items.AddRange(["MP3", "OPUS"]);
                    if(Path.GetExtension(selectedFileName).ToLower() != ".mp3")
                    {
                        outputType.Text = "MP3";
                    }
                    IsCurrentFileAudio = true;
                }
            }
        }
        private bool IsImage(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("image/"))
            {
                return true;
            }
            return false;
        }
        private bool IsVideo(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("video/"))
            {
                return true;
            }
            return false;
        }
        private bool IsAudio(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("audio/"))
            {
                return true;
            }
            return false;
        }

        private void FFmpegConvert(string filePath, string outputPath)
        {
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
            catch
            {
                MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
            }
            MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
        }

        private void FFmpegAudioConvert(string filePath, string outputPath, double dBLevel)
        {
            if (hasAmplificationChanged)
            {
                try
                {
                    FFMpegArguments
                        .FromFileInput(filePath)
                        .OutputToFile(outputPath, overwrite: true, options => options
                            .WithFastStart()
                            .WithAudioCodec("mp3")
                            .WithCustomArgument($"-af volume={dBLevel}")
                        )
                        .ProcessSynchronously();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
            }
            else
            {
                //this has been completely guessed. the library I am using is NOT documented well.
                try
                {
                    FFMpegArguments
                        .FromFileInput(filePath)
                        .OutputToFile(outputPath, overwrite: true, options => options
                            .WithFastStart()
                            .WithAudioCodec("mp3")
                        )
                        .ProcessSynchronously();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to convert. Is ffmpeg in PATH?");
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Your converted file has been saved in the same directory as the file you brought here.");
            }
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
                            FFmpegConvert(filePath, outputPath);
                            break;

                        case "mkv":
                            MessageBox.Show("Your file is now exporting. Please wait...");
                            FFmpegConvert(filePath, outputPath);
                            break;
                        case "avi":
                            MessageBox.Show("Your file is now exporting. Please wait...");
                            FFmpegConvert(filePath, outputPath);
                            break;
                    }
                }
                if (IsAudio(filePath))
                {
                    switch (fileFormat)
                    {
                        case "mp3":
                            FFmpegAudioConvert(filePath, outputPath, dBLevel);
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
                options.IsCurrentFileAudio = IsCurrentFileAudio;
                options.ShowDialog();
                hasAmplificationChanged = options.hasAmplificationChanged;
                quality = options.quality;
                dBLevel = options.dBLevel;
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
