using System.Threading.Tasks;

namespace GeekyTool.Core.Base
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    //public abstract class NavigableViewModel : BaseViewModel, INavigable
    //{
    //    public Task OnNavigatedFrom(object e)
    //    {
    //        return Task.FromResult(true);
    //    }

    //    public Task OnNavigatedTo(object e)
    //    {
    //        return Task.FromResult(true);
    //    }
    //}
}
