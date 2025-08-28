using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AIClassifier classifier = new AIClassifier();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Desktop_Click(object sender, RoutedEventArgs e)
        {
            LoadingOverlay.Visibility = Visibility.Visible;
            try
            {
                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                await CreateDesktopFolders(DesktopPath);
            }
            finally
            {
                LoadingOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async Task CreateDesktopFolders(string path)
        {

            var files = new DirectoryInfo(path).GetFiles();

            foreach (var file in files)
            {

                string fileName = file.Name;

                string category = await classifier.ClassifyFile(fileName);

                string targetCategory = null;

                if (category == "Documents" || category == "Doc")
                    targetCategory = "Documents";
                else if (category == "Pictures")
                    targetCategory = "Pictures";
                else if (category == "Videos")
                    targetCategory = "Videos";
                else if (category == "Games")
                    targetCategory = "Games";
                else if (category == "Software")
                    targetCategory = "Software";
                else if (category == "Media")
                    targetCategory = "Media";
                else continue;

                string targetFolder = System.IO.Path.Combine(path, targetCategory);
                Directory.CreateDirectory(targetFolder);

                string targetPath = System.IO.Path.Combine(targetFolder, fileName);

                try
                {
                    file.MoveTo(targetPath);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Could not move file {fileName}: {ex.Message}");
                }

               
            }




            MessageBox.Show("Done organizing files.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void Downloads_Button(object sender, RoutedEventArgs e)
        {
           

            LoadingOverlay.Visibility = Visibility.Visible;
            try
            {
                string dowloandsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                await CreateDesktopFolders(dowloandsPath);
            }
            finally
            {
                LoadingOverlay.Visibility = Visibility.Collapsed;
            }

           
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
