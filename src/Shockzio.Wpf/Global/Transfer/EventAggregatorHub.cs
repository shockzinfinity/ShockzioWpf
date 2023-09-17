using System;
using System.Diagnostics;
using Prism.Events;

namespace Shockzio.Wpf.Event;

public class EventAggregatorHub : IEventHub
{
  private readonly IEventAggregator _eventAggregator;

  public EventAggregatorHub(IEventAggregator eventAggregator)
  {
    Debug.WriteLine("NEW EventAggregator");
    _eventAggregator = eventAggregator;
  }

  public Action<StackTrace> Publishing { get; set; }

  public void Publish<T1, T2>(T2 value) where T1 : PubSubEvent<T2>, new()
  {
    StackTrace stackTrace = new();
    // Get calling method name
    Debug.WriteLine(stackTrace.GetFrame(1).GetMethod().Name);

    Publishing?.Invoke(stackTrace);
    _eventAggregator.GetEvent<T1>().Publish(value);
  }

  public void Subscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new()
  {
    _eventAggregator.GetEvent<T1>().Subscribe(action);
  }
}