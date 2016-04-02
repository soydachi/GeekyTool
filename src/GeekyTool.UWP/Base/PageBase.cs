using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;

namespace GeekyTool.Base
{
    public class PageBase : Page
    {
        private BaseViewModel viewModel;

        public PageBase()
        {
            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            viewModel = (BaseViewModel) this.DataContext;

            if (this.DataContext is INavigable)
            {
                var navigableVm = viewModel as INavigable;
                await navigableVm.OnNavigatedTo(e);
            }
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (this.DataContext is INavigable)
            {
                var navigableVm = viewModel as INavigable;
                await navigableVm.OnNavigatedFrom(e);
            }
        }
    }
}
