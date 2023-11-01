using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.Styling;
using Avalonia.VisualTree;
using System;
using System.Diagnostics;
using System.Linq;

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

            string type;
            type = this.Classes.ToList().DefaultIfEmpty("").FirstOrDefault("Add");
            if (!string.IsNullOrEmpty(type))
            {
                this.Transitions = CreateTransitions();
                SetTransitionTimes(TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }
            type = "";

            type = this.Classes.ToList().DefaultIfEmpty("").FirstOrDefault("Remove");
            if (!string.IsNullOrEmpty(type))
            {
                this.Transitions = CreateTransitions();
                SetTransitionTimes(TimeSpan.FromSeconds(0.45),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }
            type = "";

            type = this.Classes.ToList().DefaultIfEmpty("").FirstOrDefault("Edit");
            if (!string.IsNullOrEmpty(type))
            {
                this.Transitions = CreateTransitions();
                SetTransitionTimes(TimeSpan.FromSeconds(0.55),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }
            type = "";

            type = this.Classes.ToList().DefaultIfEmpty("").FirstOrDefault("Deselect");
            if (!string.IsNullOrEmpty(type))
            {
                this.Transitions = CreateTransitions();
                SetTransitionTimes(TimeSpan.FromSeconds(0.65),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35),
                                   TimeSpan.FromSeconds(0.35));
            }
        }
    }
}
