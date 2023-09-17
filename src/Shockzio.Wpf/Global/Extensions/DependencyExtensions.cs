using System.Windows;

namespace Shockzio.Wpf.Extensions;

public static class DependencyExtensions
{
  public static bool SetIfDefault<T>(this DependencyObject o, DependencyProperty property, T value)
  {
    if (DependencyPropertyHelper.GetValueSource(o, property).BaseValueSource == BaseValueSource.Default)
    {
      o.SetValue(property, value);

      return true;
    }

    return false;
  }
}