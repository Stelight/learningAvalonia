using System;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Styling;

namespace AvaloniaDemo.Controls;

public class TriangleControl : TemplatedControl
{
    private Path? _path;

    public static readonly StyledProperty<IBrush> FillProperty =
        AvaloniaProperty.Register<TriangleControl, IBrush>(
            nameof(Fill), Brushes.Black);
    public IBrush Fill
    {
        get => GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _path = e.NameScope.Find<Path>("PART_Path");

        if (_path != null)
        {
            // 使用 Avalonia 动画系统，做“闪烁”效果
            var animation = new Animation
            {
                Duration = TimeSpan.FromSeconds(1),
                IterationCount = IterationCount.Infinite,
                Easing = new SineEaseInOut(),
                Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0),
                        Setters =
                        {
                            new Setter(Visual.OpacityProperty, 1.0)
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(0.5),
                        Setters =
                        {
                            new Setter(Visual.OpacityProperty, 0.2)
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(1),
                        Setters =
                        {
                            new Setter(Visual.OpacityProperty, 1.0)
                        }
                    }
                }
            };

            animation.RunAsync(_path, CancellationToken.None);
        }
    }
}
