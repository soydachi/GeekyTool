using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// ReSharper disable once CheckNamespace
namespace GeekyTool.Services
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        ///     The key that is returned by the <see cref="CurrentPageKey" /> property
        ///     when the current Page is the root page.
        /// </summary>
        public const string RootPageKey = "-- ROOT --";

        /// <summary>
        ///     The key that is returned by the <see cref="CurrentPageKey" /> property
        ///     when the current Page is not found.
        ///     This can be the case when the navigation wasn't managed by this NavigationService,
        ///     for example when it is directly triggered in the code behind, and the
        ///     NavigationService was not configured for this page type.
        /// </summary>
        public const string UnknownPageKey = "-- UNKNOWN --";

        private readonly Dictionary<string, Type> pagesByKey = new Dictionary<string, Type>();

        private readonly Frame navigationFrame;

        public NavigationService()
        {
            
        }

        public NavigationService(Frame navigationFrame)
        {
            this.navigationFrame = navigationFrame;
        }


        public void GoBack()
        {
            if (navigationFrame != null)
                navigationFrame.GoBack();
            else
                ((Frame) Window.Current.Content).GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (pagesByKey)
            {
                if (!pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                        nameof(pageKey));
                }
                
                var frame = navigationFrame ?? (Frame)Window.Current.Content;
                frame.Navigate(pagesByKey[pageKey], parameter);
            }
        }

        public string CurrentPageKey
        {
            get
            {
                lock (pagesByKey)
                {
                    var frame = navigationFrame ?? (Frame)Window.Current.Content;

                    if (frame.BackStackDepth == 0)
                    {
                        return RootPageKey;
                    }

                    if (frame.Content == null)
                    {
                        return UnknownPageKey;
                    }

                    var currentType = frame.Content.GetType();

                    if (pagesByKey.All(p => p.Value != currentType))
                    {
                        return UnknownPageKey;
                    }

                    var item = pagesByKey.FirstOrDefault(
                        i => i.Value == currentType);

                    return item.Key;
                }
            }
        }

        public void Configure(string key, Type pageType)
        {
            lock (pagesByKey)
            {
                if (pagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (pagesByKey.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException(
                        "This type is already configured with key " + pagesByKey.First(p => p.Value == pageType).Key);
                }

                pagesByKey.Add(
                    key,
                    pageType);
            }
        }

        /// <summary>
        ///     Removes the BackStack history of navigation.
        /// </summary>
        public void ClearNavigationHistory(bool appViewBackButtonVisibility = false)
        {
            var frame = navigationFrame ?? (Frame)Window.Current.Content;
            frame.BackStack.Clear();

            if (appViewBackButtonVisibility)
                UpdateAppViewBackButtonVisibility(frame);
        }

        /// <summary>
        ///     Go back navigation.
        /// </summary>
        /// <summary>
        ///     Remove the last page of the BackStack from the navigation history
        /// </summary>
        public void RemoveLastPageFromNavigationHistory(bool appViewBackButtonVisibility = false)
        {
            var frame = navigationFrame ?? (Frame)Window.Current.Content;
            frame.BackStack.Remove(frame.BackStack.Last());

            if (appViewBackButtonVisibility)
                UpdateAppViewBackButtonVisibility(frame);
        }

        private void UpdateAppViewBackButtonVisibility(Frame frame)
        {
            // Each time a navigation event occurs, update the Back button's visibility
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                frame.CanGoBack
                    ? AppViewBackButtonVisibility.Visible
                    : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
