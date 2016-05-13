using System;
using Windows.Networking.Connectivity;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GeekyTool.UI
{
    public class AdaptiveImage : ContentControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
           "Source", typeof(AdaptiveImageSource), typeof(AdaptiveImage), new PropertyMetadata(default(AdaptiveImageSource), OnSourceChanged));

        public static readonly DependencyProperty ImageTemplateProperty = DependencyProperty.Register(
            "ImageTemplate", typeof(DataTemplate), typeof(AdaptiveImage), null);

        public static readonly DependencyProperty ImageStrechProperty = DependencyProperty.Register(
            "ImageStrech", typeof(Stretch), typeof(AdaptiveImage), new PropertyMetadata(default(Stretch)));


        public AdaptiveImage()
        {
            LayoutRoot = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Content = LayoutRoot;

            ProgressIndicator = new ProgressRing
            {
                Foreground = new SolidColorBrush(Colors.White),
                Width = 50,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
        }

        /// <summary>
        /// You need to bind an <see cref="AdaptiveImageSource"/> property. Not use this control for local images, only for http images.
        /// </summary>
        public AdaptiveImageSource Source
        {
            get { return (AdaptiveImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public DataTemplate ImageTemplate
        {
            get { return (DataTemplate)GetValue(ImageTemplateProperty); }
            set { SetValue(ImageTemplateProperty, value); }
        }

        public Stretch ImageStrech
        {
            get { return (Stretch)GetValue(ImageStrechProperty); }
            set { SetValue(ImageStrechProperty, value); }
        }


        protected Grid LayoutRoot { get; }

        private ProgressRing ProgressIndicator { get; set; }

        private FrameworkElement HiResImage { get; set; }

        private FrameworkElement LowResImage { get; set; }


        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AdaptiveImage)?.StartLoading();
        }

        private void StartLoading()
        {
            LayoutRoot.Children.Clear();
            HiResImage = null;
            LowResImage = null;

            var source = Source;

            if (source?.LowResSource == null) return;

            var lowResSource = new BitmapImage {UriSource = new Uri(source.LowResSource)};
            LowResImage = CreateImage();
            LowResImage.DataContext = lowResSource;


            LayoutRoot.Children.Add(LowResImage);


            if (source.HiResSource != null && ShouldLoadHiResolucionPicture())
            {
                ProgressIndicator.IsActive = true;
                LayoutRoot.Children.Add(ProgressIndicator);

                var hiResSource = new BitmapImage();

                hiResSource.ImageOpened += this.ImageOpened;
                hiResSource.ImageFailed += this.ImageFailed;

                hiResSource.UriSource = new Uri(source.HiResSource);

                HiResImage = CreateImage();
                HiResImage.DataContext = hiResSource;

                LayoutRoot.Children.Add(HiResImage);

                HiResImage.Visibility = Visibility.Collapsed;
            }
        }

        private void ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ProgressIndicator.IsActive = false;
        }

        private void ImageOpened(object sender, RoutedEventArgs e)
        {
            if (HiResImage != null)
            {
                LayoutRoot.Children.Remove(LowResImage);
                HiResImage.Visibility = Visibility.Visible;
                ProgressIndicator.IsActive = false;
            }
        }

        private FrameworkElement CreateImage()
        {
            var customElement = ImageTemplate?.LoadContent() as FrameworkElement;

            if (customElement != null)
                return customElement;

            var image = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Stretch = ImageStrech
            };
            image.SetBinding(Image.SourceProperty, new Binding());
            return image;
        }

        private bool ShouldLoadHiResolucionPicture()
        {
            var connectionCost = NetworkInformation.GetInternetConnectionProfile()?.GetConnectionCost();

            if (connectionCost == null)
                return false;

            switch (connectionCost.NetworkCostType)
            {
                case NetworkCostType.Unrestricted:
                    return true;
                case NetworkCostType.Fixed:
                case NetworkCostType.Variable:
                    return !connectionCost.ApproachingDataLimit && !connectionCost.OverDataLimit &&
                           !connectionCost.Roaming;
                default:
                    return false;
            }
        }
    }
}