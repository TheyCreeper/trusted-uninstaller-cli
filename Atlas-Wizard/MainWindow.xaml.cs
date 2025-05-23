using System;
using System.Collections.Generic;
using System.Linq;
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
using Atlas_Wizard.ViewModels;
using Atlas_Wizard.Views;
using TrustedUninstaller.Shared;
using Wpf.Ui.Controls;

namespace Atlas_Wizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        MainVM mainVM;
        public MainWindow()
        {
            InitializeComponent();
            mainVM = new MainVM();
            this.DataContext = mainVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".apbx"; // Default file extension
            dialog.Filter = "Playbook (.apbx)|*.apbx"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
                ExtractAndCreate(filename);
            }
        }

        public void ExtractAndCreate(string path)
        {
            string tempPath = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName();
            TrustedUninstaller.CLI.CLI.ExtractArchive(path, tempPath);
            mainVM.Playbook = TrustedUninstaller.Shared.AmeliorationUtil.DeserializePlaybook(tempPath);
            mainFrame.Content = new PlaybookUC(mainVM);
        }
    }
}
