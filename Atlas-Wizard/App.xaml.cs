using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TrustedUninstaller.Shared;

namespace Atlas_Wizard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Playbook playbook;
        public static MainWindow mainWindow;
        public static string buildNumber;
    }
}
