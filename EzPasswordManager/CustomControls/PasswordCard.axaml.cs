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

        private TextBlock DisplayNameTextblock { get; set; }
        private TextBlock UsernameTextblock { get; set; }
        private TextBlock PasswordTextblock { get; set; }
        private TextBlock EmailTextblock { get; set; }
        private TextBlock WebsiteTextblock { get; set; }
        private TextBlock NotesTextblock { get; set; }
        private Border Border { get; set; }

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

            Border = e.NameScope.Find<Border>("PART_Border");

            DisplayNameTextblock = e.NameScope.Find<TextBlock>("PART_DisplayNameText");
            DisplayNameTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "DisplayName"
            });

            UsernameTextblock = e.NameScope.Find<TextBlock>("PART_UsernameText");
            UsernameTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Username"
            });

            PasswordTextblock = e.NameScope.Find<TextBlock>("PART_PasswordText");
            PasswordTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Password"
            });

            EmailTextblock = e.NameScope.Find<TextBlock>("PART_EmailText");
            EmailTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Email"
            });

            WebsiteTextblock = e.NameScope.Find<TextBlock>("PART_WebsiteText");
            WebsiteTextblock.Bind(TextBlock.TextProperty, new Binding
            {
                Source = PasswordInfo,
                Path = "Website"
            });

            NotesTextblock = e.NameScope.Find<TextBlock>("PART_NotesText");
            NotesTextblock.Bind(TextBlock.TextProperty, new Binding
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
