namespace GeekyTool.Base
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool isBusy;
        protected bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
    }
}
