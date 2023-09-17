using System.Windows;
using Shockzio.Wpf.Mvvm;

namespace Shockzio.Wpf.Controls;

public interface IViewable
{
  FrameworkElement View { get; init; }
  ObservableBase ViewModel { get; }
}