using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GeekyTool.Core.Base;
using GeekyTool.Core.Messaging;
using GeekyTool.Core.Services;
using SimpleMVVM.Models;
using SimpleMVVM.Views;

namespace SimpleMVVM.ViewModels
{
    public class MainViewModel : BaseViewModel, INavigable
    {
        private readonly INavigationService navigationService;
        private readonly IMessenger messenger;

        public MainViewModel(INavigationService navigationService, IMessenger messenger)
        {
            this.navigationService = navigationService;
            this.messenger = messenger;

            Person = new Person();

            messenger.Register<string>(DoSomeStuff);

            NavigateToNexStep = new DelegateCommand(NavigateToNexStepDelegate);
            SendMessengerObject = new DelegateCommand(SendMessengerObjectDelegate);
        }

        public Task OnNavigatedFrom(object e)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedTo(object e)
        {
            return Task.CompletedTask;
        }

        private async void DoSomeStuff(string text)
        {
            var msg = new MessageDialog(text);
            await msg.ShowAsync();
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

        public ICommand SendMessengerObject { get; private set; }
        private void SendMessengerObjectDelegate()
        {
            messenger.Publish("Hey! This is comming from an Messenger call! Good one!");
        }
    }
}
