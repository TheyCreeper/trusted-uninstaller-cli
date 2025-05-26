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
using Microsoft.Win32;
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
            App.mainWindow = this;
            MainFrame.Content = new FirstMenu(mainVM);
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            App.buildNumber = registryKey.GetValue("CurrentBuild").ToString();
        }

        public void Options()
        {
            mainVM.Playbook = App.playbook;
            
            MainFrame.Content = new OptionsMenu(mainVM);
        }

    }
}
