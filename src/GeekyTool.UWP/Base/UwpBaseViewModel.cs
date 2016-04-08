using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;
using GeekyTool.Services;

namespace GeekyTool.Base
{
    public abstract class UwpBaseViewModel : BaseViewModel, INavigable
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

        public virtual Task OnNavigatedFrom<NavigationEventArgs>(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedTo<NavigationEventArgs>(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
