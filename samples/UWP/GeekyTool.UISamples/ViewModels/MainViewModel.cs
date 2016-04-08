using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using GeekyTool.Core.Base;
using GeekyTool.Core.Services;
using GeekyTool.Extensions;

namespace GeekyTool.UISamples.ViewModels
{
    public class MainViewModel : BaseViewModel, INavigable<NavigationEventArgs>
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<string>();
            for (int i = 0; i < 19; i++)
            {
                Items.Add(string.Format(
                    "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta)  {0}", i));
            }

            ShowDialogCommand = new DelegateCommand(ShowDialogCommandDelegate);
        }

        public Task OnNavigatedFrom(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedTo(NavigationEventArgs e)
        {
            return Task.CompletedTask;
        }

        private ObservableCollection<string> items;
        public ObservableCollection<string> Items
        {
            get { return items; }
            set { Set(ref items, value); }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set { Set(ref isOpen, value); }
        }

        private string item;
        public string Item
        {
            get { return item; }
            set { Set(ref item, value); }
        }

        public ICommand ShowDialogCommand { get; private set; }

        private void ShowDialogCommandDelegate()
        {
            IsOpen = true;
        }
    }
}
