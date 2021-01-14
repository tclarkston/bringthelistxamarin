using System;
using System.IO;
using BringTheList.Models;
using Xamarin.Forms;

namespace BringTheList.Views
{
    public partial class NoteEntryPage : ContentPage
    {
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

        void OnUploadClicked(object sender, EventArgs e)
        {




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

