using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace GeekyTool.UI
{
    [TemplatePart(Name = PART_ROOT_BORDER, Type = typeof(Border))]
    [TemplatePart(Name = PART_ROOT_GRID, Type = typeof(Grid))]
    [TemplatePart(Name = PART_BACK_BUTTON, Type = typeof(Button))]
    [TemplatePart(Name = PART_TITLE, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_CONTENT, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PART_DIALOGGRID, Type = typeof(Grid))]
    [TemplatePart(Name = PART_POSITIVE_COMMAND, Type = typeof(Button))]
    [TemplatePart(Name = PART_NEUTRAL_COMMAND, Type = typeof(Button))]
    [TemplatePart(Name = PART_NEGATIVE_COMMAND, Type = typeof(Button))]
    public sealed class Dialog : ContentControl
    {
        private const string PART_ROOT_BORDER = "PART_RootBorder";
        private const string PART_ROOT_GRID = "PART_RootGrid";
        private const string PART_BACK_BUTTON = "PART_BackButton";
        private const string PART_TITLE = "PART_Title";
        private const string PART_CONTENT = "PART_Content";
        private const string PART_DIALOGGRID = "PART_DialogGrid";
        private const string PART_NEGATIVE_COMMAND = "PART_NegativeCommand";
        private const string PART_NEUTRAL_COMMAND = "PART_NeutralCommand";
        private const string PART_POSITIVE_COMMAND = "PART_PositiveCommand";

        private Grid rootGrid;
        private Border rootBorder;
        private Button backButton;
        private Grid dialogGrid;
        private ContentPresenter contentPresenter;
        private TextBlock title;
        private Button positiveButton;
        private Button neutralButton;
        private Button negativeButton;

        public event RoutedEventHandler BackButtonClicked;

        public Dialog()
        {
            this.DefaultStyleKey = typeof(Dialog);
        }

        protected override void OnApplyTemplate()
        {
            rootBorder = (Border)GetTemplateChild(PART_ROOT_BORDER);
            rootGrid = (Grid)GetTemplateChild(PART_ROOT_GRID);
            backButton = (Button)GetTemplateChild(PART_BACK_BUTTON);
            dialogGrid = (Grid)GetTemplateChild(PART_DIALOGGRID);
            contentPresenter = (ContentPresenter)GetTemplateChild(PART_CONTENT);
            title = (TextBlock)GetTemplateChild(PART_TITLE);
            positiveButton = (Button)GetTemplateChild(PART_POSITIVE_COMMAND);
            neutralButton = (Button)GetTemplateChild(PART_NEUTRAL_COMMAND);
            negativeButton = (Button)GetTemplateChild(PART_NEGATIVE_COMMAND);

            // Hide buttons
            positiveButton.Command = null;
            positiveButton.Visibility = Visibility.Collapsed;
            neutralButton.Command = null;
            neutralButton.Visibility = Visibility.Collapsed;
            negativeButton.Command = null;
            negativeButton.Visibility = Visibility.Collapsed;

            BackButtonVisibility = Visibility.Visible;

            ResizeContainers();

            if (backButton != null)
                backButton.Click += BackButton_Click;

            if (TitleForeground != null)
                title.Foreground = TitleForeground;

            if (TitleFontFamily != null)
                title.FontFamily = TitleFontFamily;


            if (PositiveCommand != null)
            {
                positiveButton.Command = PositiveCommand;
                positiveButton.Visibility = Visibility.Visible;
            }

            if (NeutralCommand != null)
            {
                neutralButton.Command = NeutralCommand;
                neutralButton.Visibility = Visibility.Visible;
            }

            if (NegativeCommand != null)
            {
                negativeButton.Command = NegativeCommand;
                negativeButton.Visibility = Visibility.Visible;
            }

            Window.Current.SizeChanged += OnWindowSizeChanged;
            Unloaded += OnUnloaded;

            base.OnApplyTemplate();
        }

        private void ResizeContainers()
        {
            if (rootGrid != null)
            {
                rootGrid.Width = Window.Current.Bounds.Width;
                rootGrid.Height = Window.Current.Bounds.Height;
                dialogGrid.Margin = new Thickness(0, 80, 0, 80);
                contentPresenter.MaxWidth = Window.Current.Bounds.Width - 160;
            }

            if (rootBorder != null)
            {
                rootBorder.Width = Window.Current.Bounds.Width;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (BackButtonClicked != null)
                BackButtonClicked(sender, e);
            else
                IsOpen = false;
        }

        private void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            ResizeContainers();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= OnUnloaded;
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        public static readonly DependencyProperty BackButtonVisibilityProperty = DependencyProperty.Register(
            "BackButtonVisibility", typeof(Visibility), typeof(Dialog), new PropertyMetadata(Visibility.Visible));

        public Visibility BackButtonVisibility
        {
            get { return (Visibility)GetValue(BackButtonVisibilityProperty); }
            set { SetValue(BackButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof(bool), typeof(Dialog), new PropertyMetadata(false, OnIsOpenPropertyChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;
            var dlg = d as Dialog;
            dlg?.ApplyTemplate();
        }


        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(Dialog), null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly DependencyProperty BackButtonCommandProperty = DependencyProperty.Register(
            "BackButtonCommand", typeof(ICommand), typeof(Dialog), new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand BackButtonCommand
        {
            get { return (ICommand)GetValue(BackButtonCommandProperty); }
            set { SetValue(BackButtonCommandProperty, value); }
        }


        public static readonly DependencyProperty BackButtonCommandParameterProperty = DependencyProperty.Register(
            "BackButtonCommandParameter", typeof(object), typeof(Dialog), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object BackButtonCommandParameter
        {
            get { return (object)GetValue(BackButtonCommandParameterProperty); }
            set { SetValue(BackButtonCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register(
            "TitleForeground", typeof(SolidColorBrush), typeof(Dialog), new PropertyMetadata(default(SolidColorBrush), OnTitleForegroundChanged));

        private static void OnTitleForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dlg = d as Dialog;
            dlg?.ApplyTemplate();
        }

        public SolidColorBrush TitleForeground
        {
            get { return (SolidColorBrush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
            "TitleFontFamily", typeof(FontFamily), typeof(Dialog), new PropertyMetadata(default(FontFamily), OnTitleFontFamilyChanged));

        private static void OnTitleFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dlg = d as Dialog;
            dlg?.ApplyTemplate();
        }

        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }

        public static readonly DependencyProperty PositiveCommandProperty = DependencyProperty.Register(
            "PositiveCommand", typeof(ICommand), typeof(Dialog), new PropertyMetadata(null, OnCommandChanged));

        public ICommand PositiveCommand
        {
            get { return (ICommand)GetValue(PositiveCommandProperty); }
            set { SetValue(PositiveCommandProperty, value); }
        }

        public static readonly DependencyProperty NeutralCommandProperty = DependencyProperty.Register(
            "NeutralCommand", typeof(ICommand), typeof(Dialog), new PropertyMetadata(null, OnCommandChanged));

        public ICommand NeutralCommand
        {
            get { return (ICommand)GetValue(NeutralCommandProperty); }
            set { SetValue(NeutralCommandProperty, value); }
        }

        public static readonly DependencyProperty NegativeCommandProperty = DependencyProperty.Register(
            "NegativeCommand", typeof(ICommand), typeof(Dialog), new PropertyMetadata(null, OnCommandChanged));

        public ICommand NegativeCommand
        {
            get { return (ICommand)GetValue(NegativeCommandProperty); }
            set { SetValue(NegativeCommandProperty, value); }
        }


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dlg = d as Dialog;
            if (dlg == null) return;

            dlg.IsOpen = false;
            dlg.OnApplyTemplate();
            dlg.ApplyTemplate();
        }
    }
}
