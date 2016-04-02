using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;
using SimpleMVVM.Models;
using SimpleMVVM.Views;

namespace SimpleMVVM.ViewModels
{
    public class MainViewModel : BaseViewModel, INavigable
    {
        private readonly INavigationService navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Person = new Person();

            NavigateToNexStep = new DelegateCommand(NavigateToNexStepDelegate);
        }

        public Task OnNavigatedFrom(object e)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedTo(object e)
        {
            return Task.CompletedTask;
        }

        private Person person;
        public Person Person
        {
            get { return person; }
            set { Set(ref person, value); }
        }

        public ICommand NavigateToNexStep { get; private set; }
        private void NavigateToNexStepDelegate()
        {
            navigationService.NavigateTo(nameof(DetailView), Person);
        }
    }
}
