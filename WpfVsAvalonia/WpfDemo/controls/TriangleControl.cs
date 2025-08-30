using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfDemo.controls;

    public class TriangleControl : Control
    {
        private Path? _path;

        static TriangleControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TriangleControl),
                new FrameworkPropertyMetadata(typeof(TriangleControl)));
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Brush),
                typeof(TriangleControl), new PropertyMetadata(Brushes.Black));

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_path != null)
            {
                // 避免重复应用模板导致多次动画
                _path.ClearValue(UIElement.OpacityProperty);
            }

            _path = GetTemplateChild("PART_Path") as Path;

            if (_path != null)
            {
                // 创建动画
                var blinkAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.2,
                    Duration = TimeSpan.FromSeconds(0.5),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };

                // 开启动画
                _path.BeginAnimation(UIElement.OpacityProperty, blinkAnimation);
            }
        }
    }
    
