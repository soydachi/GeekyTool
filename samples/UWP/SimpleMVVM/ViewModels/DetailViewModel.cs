using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Base;
using GeekyTool.Services;
using SimpleMVVM.Models;

namespace SimpleMVVM.ViewModels
{
    public class DetailViewModel : BaseViewModel, INavigable<NavigationEventArgs>
    {
        private readonly INavigationService navigationService;

        public DetailViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Person = new Person();
        }

        public Task OnNavigatedFrom(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Person)
            {
                Person = (Person) e.Parameter;
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
