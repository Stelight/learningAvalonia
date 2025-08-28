using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        BlinkBtn.IsBlinking = !BlinkBtn.IsBlinking;
    }

    private void BlinkingButton_OnBlinkingStarted(object? sender, RoutedEventArgs e)
    {
        Title = "⚠ 警告：按钮正在闪烁！";
    }

    private void BlinkingButton_OnBlinkingStopped(object? sender, RoutedEventArgs e)
    {
        Title = "路由事件 Demo";
    }
}