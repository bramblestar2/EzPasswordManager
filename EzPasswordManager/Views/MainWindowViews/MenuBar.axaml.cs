using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace EzPasswordManager.Views.MainWindowViews
{
    public class MenuBar : TemplatedControl
    {
        #region Direct Properties

        public static readonly DirectProperty<MenuBar, string?> TitleProperty =
            AvaloniaProperty.RegisterDirect<MenuBar, string?>
            (nameof(Title), o => o.Title, (o, v) => o.Title = v);

        public static readonly DirectProperty<MenuBar, bool> ShowMinimizeProperty =
            AvaloniaProperty.RegisterDirect<MenuBar, bool>
            (nameof(ShowMaximize), o => o.ShowMinimize, (o, v) => o.ShowMinimize = v);

        public static readonly DirectProperty<MenuBar, bool> ShowMaximizeProperty =
            AvaloniaProperty.RegisterDirect<MenuBar, bool>
            (nameof(ShowMaximize), o => o.ShowMaximize, (o, v) => o.ShowMaximize = v);

        public static readonly DirectProperty<MenuBar, bool> ShowCloseProperty =
            AvaloniaProperty.RegisterDirect<MenuBar, bool>
            (nameof(ShowClose), o => o.ShowClose, (o, v) => o.ShowClose = v);

        #endregion

        #region Properties

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetAndRaise(TitleProperty, ref _title, value);
        }

        private bool _showMinimize;
        public bool ShowMinimize
        {
            get => _showMinimize;
            set => SetAndRaise(ShowMinimizeProperty, ref _showMinimize, value);
        }

        private bool _showMaximize;
        public bool ShowMaximize
        {
            get => _showMaximize;
            set => SetAndRaise(ShowMaximizeProperty, ref _showMaximize, value);
        }

        private bool _showClose;
        public bool ShowClose
        {
            get => _showClose;
            set => SetAndRaise(ShowCloseProperty, ref _showClose, value);
        }

        #endregion

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

        }
    }
}
