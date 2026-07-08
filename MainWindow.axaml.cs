using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Media;
using System.Diagnostics;

namespace Tahoe;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public async void openFiles(object sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        // File Dialog
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choose a file",
            AllowMultiple = false
        });
        // checks user if they didnt cancel it.
        if (files.Count >= 1)
        {
            await using var stream = await files[0].OpenReadAsync();
            using var fileReader = new StreamReader(stream);

            var fileContent = await fileReader.ReadToEndAsync();
            MyEditor.Text = fileContent; 
        }        
    }
    public async void saveFiles(object sender, RoutedEventArgs e)
    {
        var top1Level = TopLevel.GetTopLevel(this);

        var saveFileDialog = await top1Level.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Please save a File!",
        });

        if(saveFileDialog is not null)
        {
            
            await using var stream = await saveFileDialog.OpenWriteAsync();
            using var fileWriter = new StreamWriter(stream);
            string fileText = MyEditor.Text;
            await fileWriter.WriteLineAsync(fileText);
        }
    }

    public void darkMode(object sender, RoutedEventArgs e)
    {
        navBar.Background = Brush.Parse("#030812");
        navBar.Foreground = Brush.Parse("#EEFFFF");
        fileMenu.Background = Brush.Parse("#030812");
        fileMenu.Foreground = Brush.Parse("#EEFFFF");
        themeMenu.Background = Brush.Parse("#030812");
        themeMenu.Foreground = Brush.Parse("#EEFFFF");
        MyEditor.Background = Brush.Parse("#0D1117");
        MyEditor.Foreground = Brush.Parse("#E1E4E8");
        sideBar.Background = Brush.Parse("#0A0E17");
    }

    public void lightMode(object sender, RoutedEventArgs e)
    {
        navBar.Background = Brush.Parse("#FDF6E3");
        navBar.Foreground = Brush.Parse("#181818");
        fileMenu.Background = Brush.Parse("#FDF6E3");
        fileMenu.Foreground = Brush.Parse("#181818");
        themeMenu.Background = Brush.Parse("#FDF6E3");
        themeMenu.Foreground = Brush.Parse("#181818");
        MyEditor.Background = Brush.Parse("#FCF9F2");
        MyEditor.Foreground = Brush.Parse("#181818");
        sideBar.Background = Brush.Parse("#F4EFE1");
    }
    public void openTerminal(object sender, RoutedEventArgs e)
    {
        ProcessStartInfo terminal = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            UseShellExecute = true,
            Arguments = "Welcome to Tahoe Terminal!"
        };
        Process.Start(terminal);
    }
}    


