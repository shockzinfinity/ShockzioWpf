using System.Collections.Generic;
using Prism.Mvvm;

namespace Shockzio.Wpf.Location;

public class ViewModelLocatorCollection : List<ViewModelLocatorItem>
{
  public void Register<T, U>()
  {
    ViewModelLocationProvider.Register<T, U>();
    this.Add(new ViewModelLocatorItem(typeof(T), typeof(U)));
  }
}