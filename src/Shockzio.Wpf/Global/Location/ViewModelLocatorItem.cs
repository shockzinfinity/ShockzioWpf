﻿using System;

namespace Shockzio.Wpf.Location;

public class ViewModelLocatorItem
{
  public ViewModelLocatorItem(Type viewType, Type dataContextType)
  {
    ViewType = viewType;
    DataContextType = dataContextType;
  }

  public Type ViewType { get; set; }
  public Type DataContextType { get; set; }
}