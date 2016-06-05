using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Services;

namespace GeekyTool.Base
{
    public class PageBase : Page
    {
        private BaseViewModel viewModel;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            viewModel = (BaseViewModel) this.DataContext;

            if (this.DataContext is INavigable<NavigationEventArgs>)
            {
                var navigableVm = viewModel as INavigable<NavigationEventArgs>;
                await navigableVm.OnNavigatedTo(e);
            }
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (this.DataContext is INavigable<NavigationEventArgs>)
            {
                var navigableVm = viewModel as INavigable<NavigationEventArgs>;
                await navigableVm.OnNavigatedFrom(e);
            }
        }
    }
}
