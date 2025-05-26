using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static TrustedUninstaller.Shared.Playbook;
using static TrustedUninstaller.Shared.Playbook.CheckboxPage;
using static TrustedUninstaller.Shared.Playbook.FeaturePage;
using static TrustedUninstaller.Shared.Playbook.RadioPage;

namespace Atlas_Wizard.Views
{
    /// <summary>
    /// Interaction logic for OptionsMenu.xaml
    /// </summary>
    public partial class OptionsMenu : UserControl
    {
        private int Index = 0;
        private MainVM vm;
        private string CurrentGroup;
        private string CurrentGroupDefault;
        private string CurrentlySelected;
        private List<string> checkedBoxes = new List<string>();
        public OptionsMenu(MainVM vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            this.vm = vm;
            OptionsControl.Content = vm.Playbook.FeaturePages[Index];
            if (vm.Playbook.FeaturePages[Index] is RadioPage rp)
            {
                CurrentGroup = rp.DefaultOption.Split('-')[0];
                CurrentGroupDefault = rp.DefaultOption;
                Description.Text = rp.Description;
                FirstLine.Text = rp.TopLine.Text;
                HyperLinkButton.Content = rp.BottomLine.Text;
                HyperLinkButton.NavigateUri = rp.BottomLine.Link;
            }
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            bool loop = true;
            while (loop)
            {
                if (vm.Playbook.FeaturePages[Index + 1].WinVer != null)
                {
                    List<string> winvers = vm.Playbook.FeaturePages[Index + 1].WinVer.Split(',').ToList();
                    if (!winvers.Contains(App.buildNumber))
                    {
                        Index++;
                    }
                    else
                    {
                        loop = false;
                        NextPage();
                    }
                }
                else
                {
                    loop = false;
                    NextPage();
                }
            }
        }

        private void NextPage()
        {
            if (vm.Playbook.Options is null) vm.Playbook.Options = new List<string>();
            foreach (string checkedItem in checkedBoxes)
            {
                if (!vm.Playbook.Options.Contains(checkedItem))
                {
                    vm.Playbook.Options.Add(checkedItem);
                }
            }
            if (!vm.Playbook.Options.Contains(CurrentlySelected))
            {
                vm.Playbook.Options.Add(CurrentlySelected);
            }
            Index++;
            OptionsControl.Content = vm.Playbook.FeaturePages[Index];
            if (vm.Playbook.FeaturePages[Index] is RadioPage rp)
            {
                CurrentGroup = rp.DefaultOption.Split('-')[0];
                CurrentGroupDefault = rp.DefaultOption;
                Description.Text = rp.Description;
                FirstLine.Text = rp.TopLine.Text;
                HyperLinkButton.Content = rp.BottomLine.Text;
                HyperLinkButton.NavigateUri = rp.BottomLine.Link;
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RadioOption radioOption = rb.DataContext as RadioOption;
            CurrentlySelected = radioOption.Name;
        }

        private void RadioButton_Initialized(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RadioOption radioOption = rb.DataContext as RadioOption;
            rb.GroupName = CurrentGroup;
            if (radioOption.Name == CurrentGroupDefault)
            {
                rb.IsChecked = true;
            }
        }

        private void CheckBox_Initialized(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            CheckboxOption cbo = cb.DataContext as CheckboxOption;
            checkedBoxes.Add(cbo.Name);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            CheckboxOption cbo = cb.DataContext as CheckboxOption;
            if (!checkedBoxes.Contains(cbo.Name))
            {
                checkedBoxes.Add(cbo.Name);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            CheckboxOption cbo = cb.DataContext as CheckboxOption;
            if (checkedBoxes.Contains(cbo.Name))
            {
                checkedBoxes.Remove(cbo.Name);
            }
        }
    }
}
