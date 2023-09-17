using System.Runtime.CompilerServices;

namespace Shockzio.Design.Images;

public static class ImageConverter
{
  public static string GetData([CallerMemberName] string name = null)
  {
    return ImageContainer._items[name].Data;
  }
}