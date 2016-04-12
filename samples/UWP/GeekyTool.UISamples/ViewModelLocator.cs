using Autofac;
using GeekyTool.Services;
using GeekyTool.UISamples.ViewModels;
using GeekyTool.UISamples.Views;

namespace GeekyTool.UISamples
{
    public class ViewModelLocator
    {
        readonly IContainer container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // Interfaces
            var navigationService = ConfigureNavigationService();
            builder.Register(INavigationService => navigationService);

            // ViewModels
            builder.RegisterType<MainViewModel>();

            container = builder.Build();
        }

        public MainViewModel MainViewModel => container.Resolve<MainViewModel>();

        private INavigationService ConfigureNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("MainView", typeof(MainView));

            return navigationService;
        }
    }
}
