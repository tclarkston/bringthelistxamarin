using System;
using System.IO;
using System.Reflection;
using BringTheList.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BringTheList.Views
{
    public partial class NoteEntryPage : ContentPage
    {
        private string imagePath;

        public NoteEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            
            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Assembly assembly = typeof(NoteEntryPage).GetTypeInfo().Assembly;

                    byte[] buffer;
                    using (Stream stream = assembly.GetManifestResourceStream(imagePath))
                    {
                        long length = stream.Length;
                        buffer = new byte[length];
                        stream.Read(buffer, 0, (int)length);

                        //Save the stream here in services.
                    }
                }
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update
                File.WriteAllText(note.Filename, note.Text);
            }

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            await Navigation.PopAsync();
        }

        async void OnUploadClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an Image"
            });

            if (result != null)
            {
                    //save the stream to a service
                    imagePath = result.FullPath;
                    resultImage.Source = imagePath;
            }

#if __IOS__
 var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "txt", "html", "md", "css" };

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    //Do something with the file

                }
            }
#endif
        }
    }
}

