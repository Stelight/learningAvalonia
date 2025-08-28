using System.Windows;

namespace WpfDemo;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        MyBlinkButton.IsBlinking = !MyBlinkButton.IsBlinking;
    }

    private void BlinkingButton_OnBlinkingStarted(object sender, RoutedEventArgs e)
    {
        Title = "警告：按钮正在闪烁！";
    }

    private void BlinkingButton_OnBlinkingStopped(object sender, RoutedEventArgs e)
    {
        Title = "路由事件 Demo";
    }
}