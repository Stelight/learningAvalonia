using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfDemo.control;

public class BlinkingButton : Button
{
    public static readonly DependencyProperty
        IsBlinkingProperty = DependencyProperty.Register(
            nameof(IsBlinking), typeof(bool), typeof(BlinkingButton), new PropertyMetadata(false, OnIsBlinkingChanged));


    // ================== 路由事件定义 ==================
    public static readonly RoutedEvent BlinkingStartedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(BlinkingStarted),
            RoutingStrategy.Bubble, // 事件冒泡
            typeof(RoutedEventHandler),
            typeof(BlinkingButton));

    public static readonly RoutedEvent BlinkingStoppedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(BlinkingStopped),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(BlinkingButton));

    private Storyboard? _blinkStoryboard;

    static BlinkingButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BlinkingButton),
            new FrameworkPropertyMetadata(typeof(BlinkingButton)));
    }

    public bool IsBlinking
    {
        get => (bool)GetValue(IsBlinkingProperty);
        set => SetValue(IsBlinkingProperty, value);
    }

    // CLR 封装
    public event RoutedEventHandler BlinkingStarted
    {
        add => AddHandler(BlinkingStartedEvent, value);
        remove => RemoveHandler(BlinkingStartedEvent, value);
    }

    public event RoutedEventHandler BlinkingStopped
    {
        add => AddHandler(BlinkingStoppedEvent, value);
        remove => RemoveHandler(BlinkingStoppedEvent, value);
    }

    private static void OnIsBlinkingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var btn = (BlinkingButton)d;
        if ((bool)e.NewValue)
            btn.StartBlinking();
        else
            btn.StopBlinking();
    }

    private void StartBlinking()
    {
        if (_blinkStoryboard == null)
        {
            var animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.3,
                Duration = new Duration(TimeSpan.FromSeconds(0.6)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _blinkStoryboard = new Storyboard();
            _blinkStoryboard.Children.Add(animation);
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
        }

        _blinkStoryboard.Begin();
        RaiseEvent(new RoutedEventArgs(BlinkingStartedEvent, this));
    }

    private void StopBlinking()
    {
        _blinkStoryboard?.Stop();
        Opacity = 1.0;
        // 触发路由事件
        RaiseEvent(new RoutedEventArgs(BlinkingStoppedEvent, this));
    }
}