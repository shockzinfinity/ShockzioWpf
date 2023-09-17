using System.Windows;
using Prism.Mvvm;
using Shockzio.Wpf.Mvvm;

namespace Shockzio.Wpf.Controls;

public class ShockzWindow : Window, IViewable
{
  public FrameworkElement View { get; init; }

  public ObservableBase ViewModel => View.DataContext is ObservableBase vm ? vm : null;

  public ShockzWindow()
  {
    View = this;
    ViewModelLocationProvider.AutoWireViewModelChanged(this, AutoWireViewModelChanged);
  }

  private void AutoWireViewModelChanged(object view, object dataContext)
  {
    DataContext = dataContext;

    if (dataContext is IViewIntializable viewModel)
    {
      viewModel.OnViewWired(view as IViewable);
    }

    if (dataContext is IViewLoadable && view is FrameworkElement frameworkElement)
    {
      frameworkElement.Loaded += ShockzWindow_Loaded;
    }
  }

  private void ShockzWindow_Loaded(object sender, RoutedEventArgs e)
  {
    if (sender is FrameworkElement fe && fe.DataContext is IViewLoadable vm)
    {
      fe.Loaded -= ShockzWindow_Loaded;
      vm.OnLoaded(fe as IViewable);
    }
  }
}