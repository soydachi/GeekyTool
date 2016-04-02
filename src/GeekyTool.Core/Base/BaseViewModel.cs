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
}
