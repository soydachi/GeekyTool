using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace GeekyTool.Core.Services
{
    public interface INavigable
    {
        Task OnNavigatedFrom(object e);
        Task OnNavigatedTo(object e);
    }
}
