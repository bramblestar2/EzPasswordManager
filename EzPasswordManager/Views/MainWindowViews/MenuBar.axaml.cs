using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace EzPasswordManager.Views.MainWindowViews
{
    public class MenuBar : TemplatedControl
    {
        #region Registered Properties

        public static readonly StyledProperty<string?> TitleProperty =
            AvaloniaProperty.Register<MenuBar, string?>(nameof(Title));

        public static readonly StyledProperty<bool> ShowMinimizeProperty =
            AvaloniaProperty.Register<MenuBar, bool>(nameof(ShowMinimize));

        public static readonly StyledProperty<bool> ShowMaximizeProperty =
            AvaloniaProperty.Register<MenuBar, bool>(nameof(ShowMaximize));

        public static readonly StyledProperty<bool> ShowCloseProperty =
            AvaloniaProperty.Register<MenuBar, bool>(nameof(ShowClose));

        #endregion

        #region Properties

        public string? Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public bool ShowMinimize
        {
            get => GetValue(ShowMinimizeProperty);
            set => SetValue(ShowMinimizeProperty, value);
        }

        public bool ShowMaximize
        {
            get => GetValue(ShowMaximizeProperty);
            set => SetValue(ShowMaximizeProperty, value);
        }

        public bool ShowClose
        {
            get => GetValue(ShowCloseProperty);
            set => SetValue(ShowCloseProperty, value);
        }

        #endregion

        #region Parts

        private TextBlock? TitleText;
        private Button? MinimizeButton;
        private Button? MaximizeButton;
        private Button? CloseButton;

        #endregion

        #region Commands

        public ReactiveCommand<Unit,Unit> MinimizePressed { get; }
        public ReactiveCommand<Unit,Unit> MaximizePressed { get; }
        public ReactiveCommand<Unit,Unit> ClosePressed { get; }

        #endregion

        #region Events

        public static readonly RoutedEvent<RoutedEventArgs> MinimizeEvent =
            RoutedEvent.Register<MenuBar, RoutedEventArgs>(nameof(Minimize), RoutingStrategies.Bubble);

        public static readonly RoutedEvent<RoutedEventArgs> MaximizeEvent =
            RoutedEvent.Register<MenuBar, RoutedEventArgs>(nameof(Maximize), RoutingStrategies.Bubble);

        public static readonly RoutedEvent<RoutedEventArgs> CloseEvent =
            RoutedEvent.Register<MenuBar, RoutedEventArgs>(nameof(Close), RoutingStrategies.Bubble);

        #endregion

        #region Events Handler

        public event EventHandler<RoutedEventArgs> Minimize
        {
            add => AddHandler(MinimizeEvent, value);
            remove => RemoveHandler(MinimizeEvent, value);
        }

        public event EventHandler<RoutedEventArgs> Maximize
        {
            add => AddHandler(MaximizeEvent, value);
            remove => RemoveHandler(MaximizeEvent, value);
        }

        public event EventHandler<RoutedEventArgs> Close
        {
            add => AddHandler(CloseEvent, value);
            remove => RemoveHandler(CloseEvent, value);
        }

        #endregion

        public MenuBar()
        {
            MinimizePressed = ReactiveCommand.Create(() =>
            {
                RoutedEventArgs e = new RoutedEventArgs(MinimizeEvent, this);
                this.RaiseEvent(e);
            });

            MaximizePressed = ReactiveCommand.Create(() =>
            {
                RoutedEventArgs e = new RoutedEventArgs(MaximizeEvent, this);
                this.RaiseEvent(e);
            });

            ClosePressed = ReactiveCommand.Create(() =>
            {
                RoutedEventArgs e = new RoutedEventArgs(CloseEvent, this);
                this.RaiseEvent(e);
            });
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            TitleText = e.NameScope.Find<TextBlock>("PART_TitleText");
            MinimizeButton = e.NameScope.Find<Button>("PART_MinimizeButton");
            MaximizeButton = e.NameScope.Find<Button>("PART_MaximizeButton");
            CloseButton = e.NameScope.Find<Button>("PART_CloseButton");

            MinimizeButton.Command = MinimizePressed;
            MaximizeButton.Command = MaximizePressed;
            CloseButton.Command = ClosePressed;
        }
    }
}
