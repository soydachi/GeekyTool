using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeekyTool.Core.Services
{
    public interface IStatusInformer
    {
        bool ShowStatus     { get; set; }
        InformerState State { get; }
        ObservableCollection<string> InfoMessages   { get; } 
        ObservableCollection<string> ErrorMessages  { get; }

        void RegisterError  (string errorMesage);
        void RegisterErrors (IList<string> errorMessages);
        void RegisterInfo   (string infoMessage);
        void RegisterInfos  (IList<string> infoMessages);
        void RegisterAll    (IList<IList<string>> statusLists);
        void Show();
    }
}