using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfDemo.control;

public class BlinkingButton : Button
{
    public static readonly DependencyProperty
        IsBlinkingProperty = DependencyProperty.Register(
            nameof(IsBlinking), typeof(bool), typeof(BlinkingButton), new PropertyMetadata(false, OnIsBlinkingChanged));

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
    }

    private void StopBlinking()
    {
        _blinkStoryboard?.Stop();
        Opacity = 1.0;
    }
}