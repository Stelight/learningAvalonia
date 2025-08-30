using System;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace AvaloniaDemo.Controls;

public class BlinkingButton : Button
{
    public static readonly StyledProperty<bool> IsBlinkingProperty =
        AvaloniaProperty.Register<BlinkingButton, bool>(nameof(IsBlinking));

    // 路由事件定义
    public static readonly RoutedEvent<RoutedEventArgs> BlinkingStartedEvent =
        RoutedEvent.Register<BlinkingButton, RoutedEventArgs>(
            nameof(BlinkingStarted),
            RoutingStrategies.Bubble);

    public static readonly RoutedEvent<RoutedEventArgs> BlinkingStoppedEvent =
        RoutedEvent.Register<BlinkingButton, RoutedEventArgs>(
            nameof(BlinkingStopped),
            RoutingStrategies.Bubble);

    private Animation? _blinkAnimation;
    private CancellationTokenSource? _cts;
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

    // CLR 包装
    public event EventHandler<RoutedEventArgs>? BlinkingStarted
    {
        add => AddHandler(BlinkingStartedEvent, value);
        remove => RemoveHandler(BlinkingStartedEvent, value);
    }

    public event EventHandler<RoutedEventArgs>? BlinkingStopped
    {
        add => AddHandler(BlinkingStoppedEvent, value);
        remove => RemoveHandler(BlinkingStoppedEvent, value);
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
        // 取消上一次动画
        _cts?.Cancel();

        _cts = new CancellationTokenSource();
        _blinkAnimation.RunAsync(this, _cts.Token);
        // 触发路由事件
        RaiseEvent(new RoutedEventArgs(BlinkingStartedEvent));
    }

    private void StopBlinking()
    {
        if (_blinkAnimation != null)
        {
            _cts?.Cancel(); // 立即停止动画
            _cts = null;
            Opacity = 1.0;
        }

        // 触发路由事件
        RaiseEvent(new RoutedEventArgs(BlinkingStoppedEvent));
    }
}