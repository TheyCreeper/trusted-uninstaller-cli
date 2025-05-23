using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Atlas_Wizard.ViewModels
{
    public class PBOptionsVM 
    {
        private IContentDialogService contentDialogService;
        private string DialogResultText;

        public PBOptionsVM()
        {

        }

        [RelayCommand]
        private async Task OnShowDialog(object content)
        {
            ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions()
                {
                    Title = "Save your work?",
                    Content = content,
                    PrimaryButtonText = "Save",
                    SecondaryButtonText = "Don't Save",
                    CloseButtonText = "Cancel",
                }
            );
        }
    }
}
