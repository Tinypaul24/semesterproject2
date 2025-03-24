using Avalonia.Controls;
using Avalonia.Interactivity;
using FileHelper;
using System;
using System.IO;
using DataBaseContext;

namespace HeatProductionSystem.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var fileDialogHelper = new FileDialogHelper();
            var filePath = await fileDialogHelper.ShowOpenFileDialog(this); // Pass 'this' as the parent window
            if (!string.IsNullOrEmpty(filePath))
            {
                // Go to CSV opener
                CsvDataLoader.LoadCsvFile(filePath);
                FilePathText.Text = "Loaded " + Path.GetFileName(filePath);
            }
            else
            {
                Console.WriteLine("No file selected.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}