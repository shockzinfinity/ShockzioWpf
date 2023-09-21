using System.Windows;
using System.Windows.Controls;

namespace Shockzio.Wpf.Controls;

public class MaximizeButton : Button
{
  static MaximizeButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MaximizeButton), new FrameworkPropertyMetadata(typeof(MaximizeButton)));
  }

  public static readonly DependencyProperty IsMaximizeProperty = DependencyProperty.RegisterAttached("IsMaximize", typeof(bool), typeof(MaximizeButton), new PropertyMetadata(false, MaximizePropertyChanged));

  public bool IsMaximize
  {
    get => (bool)GetValue(IsMaximizeProperty);
    set => SetValue(IsMaximizeProperty, value);
  }

  private ShockzIcon _image;

  private static void MaximizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var button = (MaximizeButton)d;
    if (button.IsMaximize)
      button._image.Icon = button.IsMaximize ? IconType.Restore : IconType.Maximize;
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    if(GetTemplateChild("PART_IMG") is ShockzIcon maxButton)
    {
      _image = maxButton;
    }
  }
}