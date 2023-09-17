namespace Shockzio.Wpf.Location;

public abstract class ViewModelLocationScenario
{
  internal ViewModelLocatorCollection Publish()
  {
    ViewModelLocatorCollection items = new();
    Match(items);

    return items;
  }

  public ViewModelLocationScenario()
  {
  }

  protected abstract void Match(ViewModelLocatorCollection items);
}