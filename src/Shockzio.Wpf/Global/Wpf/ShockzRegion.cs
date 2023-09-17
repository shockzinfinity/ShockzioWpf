using System.Windows;
using System.Windows.Controls;
using Prism.Regions;

namespace Shockzio.Wpf.Controls;

public class ShockzRegion : ContentControl
{
  public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached("RegionName", typeof(string), typeof(ShockzRegion), new PropertyMetadata(ContentNamePropertyChanged));

  public string RegionName
  {
    get => (string)GetValue(RegionNameProperty);
    set => SetValue(RegionNameProperty, value);
  }

  private static void ContentNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (e.NewValue is string str && str != "")
    {
      IRegionManager regionManager = RegionManager.GetRegionManager(Application.Current.MainWindow);
      RegionManager.SetRegionName((ShockzRegion)d, str);
      RegionManager.SetRegionManager(d, regionManager);
    }
  }

  static ShockzRegion()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ShockzRegion), new FrameworkPropertyMetadata(typeof(ShockzRegion)));
  }

  public ShockzRegion()
  {
  }
}