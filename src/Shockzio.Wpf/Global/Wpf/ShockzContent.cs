using System.Windows;
using System.Windows.Controls;
using Prism.Mvvm;
using Shockzio.Wpf.Mvvm;

namespace Shockzio.Wpf.Controls;

public class ShockzContent : ContentControl, IViewable
{
  public FrameworkElement View { get; init; }

  public ObservableBase ViewModel => View.DataContext is ObservableBase vm ? vm : null;

  public ShockzContent()
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
      frameworkElement.Loaded += ShockzContent_Loaded;
    }
  }

  private void ShockzContent_Loaded(object sender, RoutedEventArgs e)
  {
    if (sender is FrameworkElement fe && fe.DataContext is IViewLoadable vm)
    {
      fe.Loaded -= ShockzContent_Loaded;
      vm.OnLoaded(fe as IViewable);
    }
  }
}