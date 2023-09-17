﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shockzio.Wpf.Controls;

public enum JustifyEnum
{
  None,
  SpaceAround,
  SpaceBetween,
  SpaceEvenly,
}

public class ShockzPanel : StackPanel
{
  public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register("Spacing", typeof(double), typeof(ShockzPanel), new PropertyMetadata(0.0));

  public static readonly DependencyProperty JustifyProperty = DependencyProperty.Register("Justify", typeof(JustifyEnum), typeof(ShockzPanel), new PropertyMetadata(JustifyEnum.None));

  public double Spacing
  {
    get => (double)GetValue(SpacingProperty);
    set => SetValue(SpacingProperty, value);
  }

  public JustifyEnum Justify
  {
    get => (JustifyEnum)GetValue(JustifyProperty);
    set => SetValue(JustifyProperty, value);
  }

  public ShockzPanel()
  {
    Loaded += ShockzPanel_Loaded;
  }

  private void ShockzPanel_Loaded(object sender, RoutedEventArgs e)
  {
    var parent = VisualTreeHelper.GetParent(this) as FrameworkElement;
    Width = parent.ActualWidth;
    Height = parent.ActualHeight;
  }

  protected override Size MeasureOverride(Size constraint)
  {
    if (IsJustifyUseOption())
    {
      JustifyLayout();
      return base.MeasureOverride(constraint);
    }

    PerformLayout();
    return base.MeasureOverride(constraint);
  }

  protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
  {
    base.OnPropertyChanged(e);

    string propertyName = e.Property.Name;

    if (propertyName == nameof(Spacing) || propertyName == nameof(Justify))
    {
      if (IsJustifyUseOption())
      {
        JustifyLayout();
        return;
      }

      PerformLayout();
    }
  }

  private void JustifyLayout()
  {
    double allChildSize = 0;
    foreach (UIElement child in Children)
    {
      allChildSize += (Orientation == Orientation.Horizontal ? (double)child.GetValue(WidthProperty) : (double)child.GetValue(HeightProperty));
    }

    if (allChildSize == 0) return;
    if (Width == 0 && Orientation == Orientation.Horizontal) return;
    if (Height == 0 && Orientation == Orientation.Vertical) return;

    var parent = VisualTreeHelper.GetParent(this) as FrameworkElement;
    var parentSize = (Orientation == Orientation.Horizontal ? (double)parent.ActualWidth : (double)parent.ActualHeight);

    if (Justify == JustifyEnum.SpaceAround)
    {
      double aroundMargin = (parentSize - (double)allChildSize) / (Children.Count * 2);
      Debug.WriteLine($"Width {this.Width}");
      Debug.WriteLine($"allChildSize {allChildSize}");
      Debug.WriteLine($"Width - allChilSize {aroundMargin}");

      foreach (UIElement child in Children)
      {
        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(Math.Truncate(aroundMargin), 0, Math.Truncate(aroundMargin), 0)
          : new Thickness(0, Math.Truncate(aroundMargin), 0, Math.Truncate(aroundMargin));
        child.SetValue(MarginProperty, thick);
      }
    }
    else if (Justify == JustifyEnum.SpaceBetween)
    {
      double aroundMargin = (parentSize - (double)allChildSize) / (Children.Count - 1);

      int lastIdx = Children.Count - 1;
      int idx = 0;

      foreach (UIElement child in Children)
      {
        child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0));

        if (lastIdx == idx) break;

        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(0, 0, aroundMargin, 0)
          : new Thickness(0, 0, 0, aroundMargin);
        child.SetValue(MarginProperty, thick);

        idx++;
      }
    }
    else if (Justify == JustifyEnum.SpaceEvenly)
    {
      double aroundMargin = (parentSize - (double)allChildSize) / (Children.Count + 1);

      int lastIdx = Children.Count - 1;
      int idx = 0;

      foreach (UIElement child in Children)
      {
        child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0));
        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(aroundMargin, 0, 0, 0)
          : new Thickness(0, aroundMargin, 0, 0);

        child.SetValue(MarginProperty, thick);

        idx++;
      }
    }
  }

  private void PerformLayout()
  {
    if (Spacing < 0) return;
    if (Children.Count <= 1) return;

    Thickness thick = Orientation == Orientation.Horizontal
      ? new Thickness(0, 0, Spacing, 0)
      : new Thickness(0, 0, 0, Spacing);

    int lastIdx = Children.Count - 1;
    int idx = 0;

    foreach (UIElement child in Children)
    {
      child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0));

      if (lastIdx == idx) break;

      child.SetValue(MarginProperty, thick);

      idx++;
    }
  }

  private bool IsJustifyUseOption()
  {
    if (IsNan(ActualWidth) || IsNan(ActualHeight))
    {
      return false;
    }

    if (Justify == JustifyEnum.None)
    {
      return false;
    }

    return true;
  }

  private bool IsNan(double value)
  {
    if (double.IsInfinity(value)) return true;

    if (double.IsNaN(value)) return true;

    return false;
  }
}