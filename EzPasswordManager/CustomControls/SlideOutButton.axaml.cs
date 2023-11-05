using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Transformation;
using System;
using System.Reactive.Linq;

namespace EzPasswordManager.CustomControls
{
    public class SlideOutButton : Button
    {
        public static readonly StyledProperty<double> MultiplierWidthProperty =
            AvaloniaProperty.Register<SlideOutButton, double>(nameof(MultiplierWidth));

        public double MultiplierWidth
        {
            get { return GetValue(MultiplierWidthProperty); }
            set { SetValue(MultiplierWidthProperty, value); }
        }

        public static readonly StyledProperty<TransformOperations> TransformProperty =
            AvaloniaProperty.Register<SlideOutButton, TransformOperations>(nameof(Transforms));

        public TransformOperations Transforms
        {
            get { return GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }

        public SlideOutButton()
        {
            var bounds = this.GetObservable(Button.BoundsProperty);

            bounds.Subscribe(change =>
            {
                UpdatePosition(change);
            });

            var enabled = this.GetObservable(Button.IsEffectivelyEnabledProperty);

            enabled.Subscribe(enabled =>
            {
                UpdatePosition(null);
            });
        }

        private void UpdatePosition(Rect? rect)
        {
            double newWidth = 0;

            if (this.IsEffectivelyEnabled && rect.HasValue)
                newWidth = rect.Value.Width;
            else if (this.IsEffectivelyEnabled && this.Bounds.Width > 0)
                newWidth = this.Bounds.Width;

            var transform = TransformOperations.CreateBuilder(1);
            transform.AppendTranslate(-newWidth * MultiplierWidth, 0);
            this.RenderTransform = transform.Build();
        }

        public Transitions CreateTransitions()
        {
            return new Transitions()
            {
                new TransformOperationsTransition()
                {
                    Property = Visual.RenderTransformProperty,
                    Easing = new CubicEaseOut()
                },
                new BrushTransition()
                {
                    Property = Button.BackgroundProperty,
                    Easing = new CubicEaseOut()
                },
                new BrushTransition()
                {
                    Property = Button.ForegroundProperty,
                    Easing = new CubicEaseOut()
                },
                new DoubleTransition()
                {
                    Property = Visual.OpacityProperty,
                    Easing = new CubicEaseOut()
                }
            };
        }

        public void SetTransitionTimes(TimeSpan transformTime, 
                                       TimeSpan backgroundTime, 
                                       TimeSpan foregroundTime, 
                                       TimeSpan opacityTime)
        {
            if (this.Transitions is not null)
            {
                if (this.Transitions[0] is TransformOperationsTransition transform)
                    transform.Duration = transformTime;

                if (this.Transitions[1] is BrushTransition background)
                    background.Duration = backgroundTime;

                if (this.Transitions[2] is BrushTransition foreground)
                    foreground.Duration = foregroundTime;

                if (this.Transitions[3] is DoubleTransition opacity)
                    opacity.Duration = opacityTime;
            }
        }

        private Grid Part_Grid;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Part_Grid = e.NameScope.Find<Grid>("PART_Grid");

            this.Transitions = CreateTransitions();

            if (this.Classes.Contains("Add"))
            {
                SetTransitionTimes(TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }

            if (this.Classes.Contains("Remove"))
            {
                SetTransitionTimes(TimeSpan.FromSeconds(0.45),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }

            if (this.Classes.Contains("Edit"))
            {
                SetTransitionTimes(TimeSpan.FromSeconds(0.55),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }

            if (this.Classes.Contains("Deselect"))
            {
                SetTransitionTimes(TimeSpan.FromSeconds(0.65),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }
        }
    }
}
