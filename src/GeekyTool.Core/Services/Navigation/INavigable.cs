using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace GeekyTool.Core.Services
{
    public interface INavigable
    {
        Task OnNavigatedFrom<TNavigationEventArgs>(TNavigationEventArgs e);
        Task OnNavigatedTo<TNavigationEventArgs>(TNavigationEventArgs e);
    }
}
