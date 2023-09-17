using Prism.Events;

namespace Shockzio.Wpf.Event;

public class EventAggregatorBuilder
{
  public static IEventHub Get(IEventAggregator eventAggregator)
  {
    return new EventAggregatorHub(eventAggregator);
  }
}