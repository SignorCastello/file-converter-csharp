using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using FFMpegCore;
using file_converter.Properties;
using SixLabors.ImageSharp.Formats;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Reflection;


namespace file_converter
{
    public partial class MainWIndow : Form
    {
        ResourceManager ResourceManager;
        CultureInfo? CultureInfo;
        public string? filePath;
        public string? file;
        public int quality;
        public double dBLevel;
        public bool hasQualityChanged;
        public bool hasAmplificationChanged;
        public bool IsCurrentFileImage;
        public bool IsCurrentFileVideo;
        public bool IsCurrentFileAudio;

        public MainWIndow()
        {
            InitializeComponent();
            ResourceManager = new ResourceManager("file_converter.Properties.Resources", typeof(MainWIndow).Assembly);
        }

        private void MainWIndow_Load(object sender, EventArgs e)
        {
            moreOptions.Visible = false;
            string locale = CultureInfo.InstalledUICulture.TwoLetterISOLanguageName;
            if (locale == "it")
            {
                CultureInfo = CultureInfo.CreateSpecificCulture("it");
                LanguageSet(CultureInfo);
            }
            else
            {
                CultureInfo = CultureInfo.CreateSpecificCulture("en");
                LanguageSet(CultureInfo);
            }
        }

        private void LanguageSet(CultureInfo CultureInfo)
        {
            textBox1.Text = ResourceManager.GetString("SelectedFileType", CultureInfo);
            textBox2.Text = ResourceManager.GetString("WindowTitle", CultureInfo);
            browse_button.Text = ResourceManager.GetString("Browse", CultureInfo);
            Convert.Text = ResourceManager.GetString("ConvertButtonLabel", CultureInfo);
            moreOptions.Text = ResourceManager.GetString("MoreOptionsButton", CultureInfo);
        }

        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data != null)
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
        }

        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[]? files = null;
            if (e.Data != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    files = e.Data.GetData(DataFormats.FileDrop) as string[];
                }
                if(files != null)
                {
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        filePath = Path.GetFullPath(file);
                        listBox1.Items.Add(fileName);
                    }
                }
            }
        }

        private static void OpenExceptionInNotepad(string content)
        {
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "converterlog.txt"), content);
            Process.Start("notepad.exe", Path.Combine(Path.GetTempPath(), "converterlog.txt"));
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsCurrentFileImage = false;
            IsCurrentFileVideo = false;
            IsCurrentFileAudio = false;
            string? selectedFileName = listBox1.SelectedItem as string;
            if (selectedFileName == null)
            {
                moreOptions.Visible = false;
                //the options button is now invisible
            }
            if (selectedFileName != null)
            {
                moreOptions.Visible = true;
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".jpg" || Path.GetExtension(selectedFileName)?.ToLower() == ".webp")
                {
                    //you selected an image
                    FileTypeLabel.Text = ResourceManager.GetString("IMAGE", CultureInfo);
                    outputType.Items.AddRange(["JPG", "PNG", "WEBP"]);
                    if (Path.GetExtension(selectedFileName)?.ToLower() == ".png" || Path.GetExtension(selectedFileName)?.ToLower() == ".webp")
                    {
                        outputType.Text = ResourceManager.GetString("JPG", CultureInfo);
                    }
                    if (Path.GetExtension(selectedFileName)?.ToLower() == ".jpg")
                    {
                        outputType.Text = ResourceManager.GetString("PNG", CultureInfo);
                    }
                    IsCurrentFileImage = true; //this will be needed for the Options window. I don't know how to make this easier
                }
                if (Path.GetExtension(selectedFileName)?.ToLower() == ".mp4" || Path.GetExtension(selectedFileName)?.ToLower() == ".mkv" || Path.GetExtension(selectedFileName)?.ToLower() == ".webm")
                {
                    FileTypeLabel.Text = ResourceManager.GetString("VIDEO", CultureInfo);
                    outputType.Items.AddRange(["MP4", "MKV", "AVI"]);
                    if (Path.GetExtension(selectedFileName)?.ToLower() == ".mkv" || Path.GetExtension(selectedFileName)?.ToLower() == ".webm" || Path.GetExtension(selectedFileName)?.ToLower() == ".avi")
                    {
                        outputType.Text = ResourceManager.GetString("MP4", CultureInfo);
                    }
                    IsCurrentFileVideo = true;
                }
                if (MimeTypes.GetMimeType(selectedFileName).StartsWith("audio/"))
                {
                    //you selected an audio file
                    FileTypeLabel.Text = ResourceManager.GetString("AUDIO", CultureInfo);
                    outputType.Items.AddRange(["MP3", "WAV"]);
                    if (Path.GetExtension(selectedFileName).ToLower() != ".mp3")
                    {
                        outputType.Text = ResourceManager.GetString("MP3", CultureInfo);
                    }
                    if (Path.GetExtension(selectedFileName).ToLower() == ".mp3")
                    {
                        outputType.Text = ResourceManager.GetString("WAV", CultureInfo);
                    }
                    IsCurrentFileAudio = true;
                }
            }
        }

        private static bool IsImage(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("image/"))
            {
                return true;
            }
            return false;
        }

        private static bool IsVideo(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("video/"))
            {
                return true;
            }
            return false;
        }

        private static bool IsAudio(string filePath)
        {
            if (MimeTypes.GetMimeType(filePath).StartsWith("audio/"))
            {
                return true;
            }
            return false;
        }

        private void ConvertImage(string filePath, string outputPath, string fileFormat)
        {
            if(filePath != null)
            {
                using (var img = SixLabors.ImageSharp.Image.Load(filePath))
                {
                    IImageEncoder encoder = fileFormat switch
                    {
                        "jpg" => new JpegEncoder { Quality = quality },
                        "png" => new PngEncoder(),
                        "webp" => new WebpEncoder(),
                        _ => throw new InvalidOperationException("Unsupported format")
                    };
                    img.Save(outputPath, encoder);
                    MessageBox.Show(ResourceManager.GetString("Done", CultureInfo));
                }
            }
        }

        private void FFmpegConvert(string filePath, string outputPath)
        {
            if (hasAmplificationChanged)
            {
                try
                {
                    FFMpegArguments
                        .FromFileInput(filePath)
                        .OutputToFile(outputPath, overwrite: true, options => options
                            .WithFastStart()
                            .WithVideoCodec("libx264")
                            .WithAudioCodec("aac")
                            .WithCustomArgument($"-af volume={dBLevel}")
                        )
                        .ProcessSynchronously();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ResourceManager.GetString("Failed", CultureInfo));
                    OpenExceptionInNotepad(ex.Message);
                }
                MessageBox.Show(ResourceManager.GetString("Done", CultureInfo));
            }
            else
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
                catch (Exception ex)
                {
                    MessageBox.Show(ResourceManager.GetString("Failed", CultureInfo));
                    OpenExceptionInNotepad(ex.Message);
                }
                MessageBox.Show(ResourceManager.GetString("Done", CultureInfo));
            }
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
                    MessageBox.Show(ResourceManager.GetString("Failed", CultureInfo));
                    OpenExceptionInNotepad(ex.Message);
                }
                MessageBox.Show(ResourceManager.GetString("Done", CultureInfo));
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
                    MessageBox.Show(ResourceManager.GetString("Failed", CultureInfo));
                    OpenExceptionInNotepad(ex.Message);
                }
                MessageBox.Show(ResourceManager.GetString("Done", CultureInfo));
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
                    ConvertImage(filePath, outputPath, fileFormat);
                }
                //now for the videos
                if (IsVideo(filePath))
                {
                    switch (fileFormat)
                    {
                        case "mp4":
                            MessageBox.Show(Resources.ExportInProgress);
                            FFmpegConvert(filePath, outputPath);
                            break;
                        case "mkv":
                            MessageBox.Show(Resources.ExportInProgress);
                            FFmpegConvert(filePath, outputPath);
                            break;
                        case "avi":
                            MessageBox.Show(Resources.ExportInProgress);
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
                        case "wav":
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