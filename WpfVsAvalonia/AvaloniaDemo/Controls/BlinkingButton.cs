using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Styling;

namespace AvaloniaDemo.Controls;

public class BlinkingButton : Button
{
    public static readonly StyledProperty<bool> IsBlinkingProperty =
        AvaloniaProperty.Register<BlinkingButton, bool>(nameof(IsBlinking));

    private Animation? _blinkAnimation;

    public BlinkingButton()
    {
        // 监听属性变化
        this.GetObservable(IsBlinkingProperty).Subscribe(OnIsBlinkingChanged);
    }

    public bool IsBlinking
    {
        get => GetValue(IsBlinkingProperty);
        set => SetValue(IsBlinkingProperty, value);
    }

    private void OnIsBlinkingChanged(bool isBlinking)
    {
        if (isBlinking)
            StartBlinking();
        else
            StopBlinking();
    }

    private void StartBlinking()
    {
        _blinkAnimation ??= new Animation
        {
            Duration = TimeSpan.FromSeconds(1.2),
            IterationCount = IterationCount.Infinite,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(OpacityProperty, 1.0) }
                },
                new KeyFrame
                {
                    Cue = new Cue(0.5d),
                    Setters = { new Setter(OpacityProperty, 0.3) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(OpacityProperty, 1.0) }
                }
            }
        };

        _blinkAnimation.RunAsync(this);
    }

    private void StopBlinking()
    {
        _blinkAnimation = null;
        Opacity = 1.0;
    }
}