using System.Collections.Generic;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Shockzio.Wpf.Event;
using Shockzio.Wpf.Location;
using Unity;

namespace Shockzio.Wpf.Controls;

public abstract class ShockzApplication : PrismApplication
{
  private List<IModule> _modules = new();

  public ShockzApplication AddInversionModule<T>() where T : IModule, new()
  {
    IModule module = new T();
    _modules.Add(module);

    return this;
  }

  public ShockzApplication AddWireDataContext<T>() where T : ViewModelLocationScenario, new()
  {
    ViewModelLocationScenario scenario = new T();
    _ = scenario.Publish();

    return this;
  }

  protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
  {
    foreach (var module in _modules)
    {
      moduleCatalog.AddModule(module.GetType());
    }
  }

  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    containerRegistry.RegisterInstance(typeof(IEventHub), EventAggregatorBuilder.Get(containerRegistry.GetContainer().Resolve<IEventAggregator>()));
  }

  public static T GetService<T>()
  {
    if (Current is ShockzApplication app)
    {
      return app.Container.Resolve<T>();
    }

    return default;
  }
}