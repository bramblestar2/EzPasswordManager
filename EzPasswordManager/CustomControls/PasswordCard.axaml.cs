using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;
using EzPasswordManager.DataTypes;
using System.Diagnostics;
using System.Globalization;

namespace EzPasswordManager.CustomControls
{
    public class PasswordCard : ListBoxItem
    {
        public static readonly StyledProperty<PasswordInfoStructure?> PasswordInfoProperty =
            AvaloniaProperty.Register<PasswordCard, PasswordInfoStructure?>(nameof(Background));

        public PasswordInfoStructure? PasswordInfo
        {
            get { return GetValue(PasswordInfoProperty); }
            set { SetValue(PasswordInfoProperty, value); }
        }


        public static readonly DirectProperty<PasswordCard, double?> OpenHeightProperty =
            AvaloniaProperty.RegisterDirect<PasswordCard, double?>(
                nameof(OpenHeight),
                o => o.OpenHeight
                );


        private string? something { get; set; }

        private TextBlock PartDisplayNameTextblock { get; set; }
        private TextBlock PartUsernameTextblock { get; set; }
        private TextBlock PartPasswordTextblock { get; set; }
        private TextBlock PartEmailTextblock { get; set; }
        private TextBlock PartWebsiteTextblock { get; set; }
        private TextBlock PartNotesTextblock { get; set; }
        private Border PartBorder { get; set; }
        private Grid PartGrid { get; set; }

        private double? _openHeight;
        public double? OpenHeight
        {
            get { return _openHeight; }
            private set { SetAndRaise(OpenHeightProperty, ref _openHeight, value); }
        }

        public PasswordCard()
        {
            
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            PartBorder = e.NameScope.Find<Border>("PART_Border");
            PartGrid = e.NameScope.Find<Grid>("PART_Grid");
            PartGrid.SizeChanged += (_s, _e) =>
            {
                Debug.WriteLine(_e.NewSize);
            };

            PartDisplayNameTextblock = e.NameScope.Find<TextBlock>("PART_DisplayNameText");
            PartDisplayNameTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "DisplayName"
            });

            PartUsernameTextblock = e.NameScope.Find<TextBlock>("PART_UsernameText");
            PartUsernameTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Username"
            });

            PartPasswordTextblock = e.NameScope.Find<TextBlock>("PART_PasswordText");
            PartPasswordTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Password"
            });

            PartEmailTextblock = e.NameScope.Find<TextBlock>("PART_EmailText");
            PartEmailTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Email"
            });

            PartWebsiteTextblock = e.NameScope.Find<TextBlock>("PART_WebsiteText");
            PartWebsiteTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Website"
            });

            PartNotesTextblock = e.NameScope.Find<TextBlock>("PART_NotesText");
            PartNotesTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Notes"
            });
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }
    }
}
