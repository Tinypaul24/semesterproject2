using Avalonia.Controls;
using System.Threading.Tasks;  // To use Task
using System.Linq;
using Avalonia.Platform.Storage;  

namespace FileHelper
{
    public class FileDialogHelper
    {
        public async Task<string?> ShowOpenFileDialog(Window parent)
        {
            // Initialize the OpenFileDialog
            var file = await parent.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open B2Img File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new Avalonia.Platform.Storage.FilePickerFileType("Comma Separated Value") { Patterns = new[] { "*.csv" } },
                    new FilePickerFileType("All Files") { Patterns = new[] { "*" }}
                }
            });

            return file.Any() ? file[0].TryGetLocalPath() : null;  // Return the path of the first selected file
        }
    }
}