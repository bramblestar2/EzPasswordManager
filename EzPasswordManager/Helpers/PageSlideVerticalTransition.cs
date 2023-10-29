using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzPasswordManager.Helpers
{
    public class PageSlideVerticalTransition : IPageTransition
    {
        public TimeSpan Duration {  get; set; }

        public PageSlideVerticalTransition()
        {

        }

        public async Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            var tasks = new List<Task>();
            var parent = GetVisualParent(from, to);
            StyledProperty<double> translateYProperty = TranslateTransform.YProperty;
            StyledProperty<double> opacityProperty = Visual.OpacityProperty;
            StyledProperty<double> scaleXProperty = ScaleTransform.ScaleXProperty;
            StyledProperty<double> scaleYProperty = ScaleTransform.ScaleYProperty;

            double transitionYOut = parent.Bounds.Height+100;


            if (from is not null)
            {
                var animation = new Animation
                {
                    FillMode = FillMode.Forward,
                    Children =
                {
                    new KeyFrame
                    {
                        Setters = { new Setter { Property = translateYProperty, Value = 0d },
                                    new Setter { Property = opacityProperty, Value = 1d },
                                    new Setter { Property = scaleXProperty, Value = 1d },
                                    new Setter { Property = scaleYProperty, Value = 1d },
                        },
                        Cue = new Cue(0d)
                    },
                    new KeyFrame
                    {
                        Setters = { new Setter { Property = translateYProperty, Value = transitionYOut },
                                    new Setter { Property = opacityProperty, Value = 0d },
                                    new Setter { Property = scaleXProperty, Value = 1.5d },
                                    new Setter { Property = scaleYProperty, Value = 1.5d },
                        },
                        Cue = new Cue(1d)
                    },
                },
                    Duration = Duration,
                    Easing = new CubicEaseInOut()
                };

                tasks.Add(animation.RunAsync(from, cancellationToken));
            }

            if (to is not null)
            {
                var animation = new Animation
                {
                    FillMode = FillMode.Forward,
                    Children =
                {
                    new KeyFrame
                    {
                        Setters = { new Setter { Property = translateYProperty, Value = -transitionYOut },
                                    new Setter { Property = opacityProperty, Value = 0d },
                                    new Setter { Property = scaleXProperty, Value = 1.5d },
                                    new Setter { Property = scaleYProperty, Value = 1.5d },
                        },
                        Cue = new Cue(0d)
                    },
                    new KeyFrame
                    {
                        Setters = { new Setter { Property = translateYProperty, Value = 0d },
                                    new Setter { Property = opacityProperty, Value = 1d },
                                    new Setter { Property = scaleXProperty, Value = 1d },
                                    new Setter { Property = scaleYProperty, Value = 1d },
                        },
                        Cue = new Cue(1d)
                    },
                },
                    Duration = Duration,
                    Easing = new CubicEaseInOut()
                };

                tasks.Add(animation.RunAsync(to, cancellationToken));
            }

            await Task.WhenAll(tasks);

            if (from is not null && !cancellationToken.IsCancellationRequested)
            {
                from.IsVisible = false;
            }

        }

        private static Visual GetVisualParent(Visual? from, Visual? to)
        {
            var p1 = (from ?? to)!.GetVisualParent();
            var p2 = (to ?? from)!.GetVisualParent();

            if (p1 != null && p2 != null && p1 != p2)
            {
                throw new ArgumentException(
                                    "Controls for PageSlide must have same parent.");
            }

            return p1 ?? throw new InvalidOperationException(
                                                    "Cannot determine visual parent.");
        }

    }
}
