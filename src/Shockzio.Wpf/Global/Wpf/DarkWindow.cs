using System;
using System.Windows;
using System.Windows.Input;

namespace Shockzio.Wpf.Controls;

public class DarkWindow : ShockzWindow
{
  public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(DarkWindow), new PropertyMetadata(null));
  public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(DarkWindow), new UIPropertyMetadata(null));

  public new object Title
  {
    get => GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  public ICommand CloseCommand
  {
    get => (ICommand)GetValue(CloseCommandProperty);
    set => SetValue(CloseCommandProperty, value);
  }

  private MaximizeButton _maximizeButton;

  static DarkWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkWindow), new FrameworkPropertyMetadata(typeof(DarkWindow)));
  }

  public DarkWindow()
  {
  }

  public override void OnApplyTemplate()
  {
    if (GetTemplateChild("PART_CloseButton") is CloseButton closeButton)
    {
      closeButton.Click += (s, e) => WindowClose();
    }

    if (GetTemplateChild("PART_MinButton") is MinimizeButton minimizeButton)
    {
      minimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
    }

    if (GetTemplateChild("PART_MaxButton") is MaximizeButton maxButton)
    {
      _maximizeButton = maxButton;

      maxButton.Click += (s, e) =>
      {
        if (_maximizeButton.IsMaximize)
          WindowState = WindowState.Normal;
        else
          WindowState = WindowState.Maximized;

        maxButton.IsMaximize = !maxButton.IsMaximize;
      };
    }

    if (GetTemplateChild("PART_DragBar") is DraggableBar draggableBar)
    {
      draggableBar.MouseDown += WindowDragMove;
    }

    _maximizeButton.IsMaximize = WindowState == WindowState.Maximized;
  }

  private void WindowClose()
  {
    if (CloseCommand == null)
    {
      Close();
    }
    else
    {
      CloseCommand.Execute(this);
    }
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
  }

  private void WindowDragMove(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton == MouseButtonState.Pressed)
    {
      GetWindow(this).DragMove();
    }
  }
}