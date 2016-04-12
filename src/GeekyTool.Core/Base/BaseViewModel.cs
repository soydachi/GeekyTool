namespace GeekyTool.Base
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
    }
}
