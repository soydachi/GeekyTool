using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using GeekyTool.Extensions;

namespace GeekyTool
{
    public class Loading : ContentControl
    {
        public static readonly DependencyProperty LoadingVerticalAlignmentProperty = DependencyProperty.Register(
            "LoadingVerticalAlignment", typeof(VerticalAlignment), typeof(Loading), new PropertyMetadata(default(VerticalAlignment)));

        public static readonly DependencyProperty LoadingHorizontalAlignmentProperty = DependencyProperty.Register(
            "LoadingHorizontalAlignment", typeof(HorizontalAlignment), typeof(Loading),
            new PropertyMetadata(default(HorizontalAlignment)));

        public static readonly DependencyProperty LoadingContentProperty = DependencyProperty.Register(
            "LoadingContent", typeof(DataTemplate), typeof(Loading), new PropertyMetadata(default(DataTemplate), LoadingContentPropertyChanged));

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(
            "IsLoading", typeof(bool), typeof(Loading), new PropertyMetadata(default(bool), IsLoadingPropertyChanged));

        public static readonly DependencyProperty LoadingOpacityProperty = DependencyProperty.Register(
            "LoadingOpacity", typeof(double), typeof(Loading), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty LoadingBackgroundProperty = DependencyProperty.Register(
            "LoadingBackground", typeof(SolidColorBrush), typeof(Loading), new PropertyMetadata(default(SolidColorBrush)));

        public Loading()
        {
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;

            LoadingVerticalAlignment = VerticalAlignment.Stretch;
            LoadingHorizontalAlignment = HorizontalAlignment.Stretch;

            RootGrid = new Grid();
            BackgroundGrid = new Grid();
            ContentGrid = new Grid();
            RootGrid.Children.Add(BackgroundGrid);
            RootGrid.Children.Add(ContentGrid);
            Content = RootGrid;
        }

        protected Grid RootGrid { get; }
        protected Grid BackgroundGrid { get; set; }
        protected Grid ContentGrid { get; }
        protected Storyboard Animation { get; set; }

        public VerticalAlignment LoadingVerticalAlignment
        {
            get { return (VerticalAlignment) GetValue(LoadingVerticalAlignmentProperty); }
            set { SetValue(LoadingVerticalAlignmentProperty, value); }
        }

        public HorizontalAlignment LoadingHorizontalAlignment
        {
            get { return (HorizontalAlignment) GetValue(LoadingHorizontalAlignmentProperty); }
            set { SetValue(LoadingHorizontalAlignmentProperty, value); }
        }

        public DataTemplate LoadingContent
        {
            get { return (DataTemplate) GetValue(LoadingContentProperty); }
            set { SetValue(LoadingContentProperty, value); }
        }

        public bool IsLoading
        {
            get { return (bool) GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public double LoadingOpacity
        {
            get { return (double) GetValue(LoadingOpacityProperty); }
            set { SetValue(LoadingOpacityProperty, value); }
        }

        public SolidColorBrush LoadingBackground
        {
            get { return (SolidColorBrush) GetValue(LoadingBackgroundProperty); }
            set { SetValue(LoadingBackgroundProperty, value); }
        }

        private static void IsLoadingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Loading)?.CreateLoadingControl();
        }

        private static void LoadingContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Loading)?.CreateLoadingControl();
        }

        private void CreateLoadingControl()
        {
            if (IsLoading)
            {
                ContentGrid.Children.Clear();
                if (LoadingBackground == null && LoadingOpacity == 0d)
                    BackgroundGrid = null;
                else
                {
                    BackgroundGrid.Background = LoadingBackground;
                    BackgroundGrid.Opacity = LoadingOpacity;
                }

                CreateStoryboard(translateBegin: 40d, translateEnd: 0d, opacityBegin: 0d, opacityEnd: 1d);
                Animation.Begin();

                var contentControl = LoadingContent?.LoadContent() as FrameworkElement;
                if (contentControl == null) return;

                contentControl.HorizontalAlignment = LoadingHorizontalAlignment;
                contentControl.VerticalAlignment = LoadingVerticalAlignment;

                ContentGrid.Children.Add(contentControl);
            }
            else
            {
                CreateStoryboard(translateBegin: 0d, translateEnd: 40d, opacityBegin: 1d, opacityEnd: 0d);
                Animation.Begin();
            }
        }

        private void CreateStoryboard(double translateBegin, double translateEnd, double opacityBegin, double opacityEnd)
        {
            Animation = new Storyboard();
            ContentGrid.RenderTransform = new CompositeTransform();
            var scaleYAnimation = new DoubleAnimationUsingKeyFrames();
            var opacityAnimation = new DoubleAnimationUsingKeyFrames();
            var visibilityAnimation = new ObjectAnimationUsingKeyFrames();

            var scaleFrame1 = new EasingDoubleKeyFrame
            {
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                Value = translateBegin
            };

            var scaleFrame2 = new EasingDoubleKeyFrame
            {
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3)),
                Value = translateEnd
            };

            var opacityFrame1 = new EasingDoubleKeyFrame
            {
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                Value = opacityBegin
            };

            var opacityFrame2 = new EasingDoubleKeyFrame
            {
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2)),
                Value = opacityEnd
            };

            var visibilityFrame = new DiscreteObjectKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                Value = Visibility.Visible
            };

            var visibilityFrameEnd = new DiscreteObjectKeyFrame();
            if (!IsLoading)
            {
                visibilityFrameEnd = new DiscreteObjectKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3)),
                    Value = Visibility.Collapsed
                };
            }

            scaleYAnimation.KeyFrames.Add(scaleFrame1);
            scaleYAnimation.KeyFrames.Add(scaleFrame2);

            opacityAnimation.KeyFrames.Add(opacityFrame1);
            opacityAnimation.KeyFrames.Add(opacityFrame2);

            visibilityAnimation.KeyFrames.Add(visibilityFrame);
            if (!IsLoading) visibilityAnimation.KeyFrames.Add(visibilityFrameEnd);

            Storyboard.SetTargetProperty(scaleYAnimation, "(ContentGrid.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTargetProperty(opacityAnimation, "(RootGrid.Opacity)");
            Storyboard.SetTargetProperty(visibilityAnimation, "(this.Visibility)");

            Storyboard.SetTarget(scaleYAnimation, ContentGrid);
            Storyboard.SetTarget(opacityAnimation, RootGrid);
            Storyboard.SetTarget(visibilityAnimation, this);

            Animation.Children.Add(scaleYAnimation);
            Animation.Children.Add(opacityAnimation);
            Animation.Children.Add(visibilityAnimation);
        }
    }
}
