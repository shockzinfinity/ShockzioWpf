﻿using System;
using System.Diagnostics;
using Prism.Events;

namespace Shockzio.Wpf.Event;

public interface IEventHub
{
  void Publish<T1, T2>(T2 value) where T1 : PubSubEvent<T2>, new();

  void Subscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new();

  Action<StackTrace> Publishing { get; set; }
}