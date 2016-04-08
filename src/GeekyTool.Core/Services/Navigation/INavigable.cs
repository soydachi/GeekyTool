using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace GeekyTool.Core.Services
{
    public interface INavigable<in TNavigationEventArgs>
    {
        Task OnNavigatedFrom(TNavigationEventArgs e);
        Task OnNavigatedTo(TNavigationEventArgs e);
    }
}
