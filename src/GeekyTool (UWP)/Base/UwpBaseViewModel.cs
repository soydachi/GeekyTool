using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Services;

namespace GeekyTool.Base
{
    public abstract class UwpBaseViewModel : BaseViewModel, INavigable<NavigationEventArgs>
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

        public virtual Task OnNavigatedFrom(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedTo(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
