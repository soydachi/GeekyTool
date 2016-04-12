using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeekyTool.Core.Services
{
    public class SimpleStatusInformer : INotifyPropertyChanged, IStatusInformer
    {
        private bool showStatus;
        private InformerState state;
        private ObservableCollection<string> infoMessages;
        private ObservableCollection<string> errorMessages;

        public SimpleStatusInformer()
        {
            state = InformerState.Fine;
            infoMessages = new ObservableCollection<string>();
            errorMessages = new ObservableCollection<string>();
        }

        public bool ShowStatus
        {
            get { return showStatus; }
            set
            {
                if (value.Equals(showStatus)) return;
                showStatus = value;
                OnPropertyChanged();
                if (value.Equals(false)) CleanStatus();
            }
        }
        
        public InformerState State
        {
            get
            {
                state = InformerState.Fine;

                if (InfoMessages.Any())
                    state = InformerState.Info;

                if (ErrorMessages.Any())
                    state = InformerState.Error;

                return state;
            }
        }

        public ObservableCollection<string> InfoMessages
        {
            get { return infoMessages; }
            private set
            {
                if (value.Equals(infoMessages)) return;
                infoMessages = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ErrorMessages
        {
            get { return errorMessages; }
            private set
            {
                if (value.Equals(errorMessages)) return;
                errorMessages = value;
                OnPropertyChanged();
            }
        }


        public void RegisterError(string errorMesage)
        {
            if (string.IsNullOrEmpty(errorMesage)) return;
            ErrorMessages.Add(errorMesage);
        }

        public void RegisterErrors(IList<string> errorMessages)
        {
            if (errorMessages == null || errorMessages.Count == 0) return;

            foreach (var errorMessage in errorMessages)
            {
                ErrorMessages.Add(errorMessage);
            }
        }

        public void RegisterInfo(string infoMessage)
        {
            if (string.IsNullOrEmpty(infoMessage)) return;
            InfoMessages.Add(infoMessage);
        }

        public void RegisterInfos(IList<string> infoMessages)
        {
            if (infoMessages == null || infoMessages.Count == 0) return;

            foreach (var infoMessage in infoMessages)
            {
                InfoMessages.Add(infoMessage);
            }
        }

        public void RegisterAll(IList<IList<string>> statusLists)
        {
            if (statusLists == null || statusLists.Count == 0) return;

            var infoList = statusLists[0];
            var errorList = statusLists[1];

            if (infoList != null && infoList.Count > 0)
            {
                foreach (var info in infoList)
                {
                    InfoMessages.Add(info);
                }
            }

            if (errorList != null && errorList.Count > 0)
            {
                foreach (var error in errorList)
                {
                    ErrorMessages.Add(error);
                }
            }
        }

        public void Show()
        {
            OnPropertyChanged(nameof(State));

            ShowStatus = true;
        }

        private void CleanStatus()
        {
            InfoMessages.Clear();
            ErrorMessages.Clear();
            OnPropertyChanged(nameof(State));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}