using System.Threading.Tasks;

namespace GeekyTool.Core.Services
{
    public interface INavigable
    {
        Task OnNavigatedFrom(object e);
        Task OnNavigatedTo(object e);
    }
}
