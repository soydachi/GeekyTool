using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Windows.UI.Core;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;
using GeekyTool.Services;

namespace GeekyTool.Base
{
    public abstract class UwpBaseViewModel : BaseViewModel
    {
        protected CoreDispatcher MainDispatcher => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

        protected INavigationService NavigationService { get; set; }

        private ICommand goBackCommand;
        protected ICommand GoBackCommand
            => goBackCommand ?? (goBackCommand = new DelegateCommand(GoBackCommandDelegate));

        private void GoBackCommandDelegate()
        {
            NavigationService.GoBack();
        }

        private ICommand goHomeCommand;
        protected ICommand GoHomeCommand
            => goHomeCommand ?? (goHomeCommand = new DelegateCommand<string>(GoHomeCommandDelegate));

        protected virtual void GoHomeCommandDelegate(string pageName = "-- UNKNOWN --")
        {
            NavigationService.NavigateTo(pageName);
            ((NavigationService)NavigationService).ClearNavigationHistory();
        }
    }
}
