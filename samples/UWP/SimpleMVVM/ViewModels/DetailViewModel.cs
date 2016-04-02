using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;
using SimpleMVVM.Models;

namespace SimpleMVVM.ViewModels
{
    public class DetailViewModel : BaseViewModel, INavigable
    {
        private readonly INavigationService navigationService;

        public DetailViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Person = new Person();
        }

        public Task OnNavigatedFrom(object e)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedTo(object e)
        {
            var navigation = (NavigationEventArgs) e;

            if (navigation.Parameter is Person)
            {
                Person = (Person) navigation.Parameter;
            }

            return Task.CompletedTask;
        }

        private Person person;
        public Person Person
        {
            get { return person; }
            set { Set(ref person, value); }
        }
    }
}
