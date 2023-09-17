using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace Shockzio.Design.Images;

public class ImageContainer
{
  internal static ImageRoot _data;
  internal static Dictionary<string, ImageItem> _items;

  static ImageContainer()
  {
    Build();
  }

  private static void Build()
  {
    Assembly assembly = Assembly.GetExecutingAssembly();
    var resourceName = "Shockzio.Design.Properties.Resources.images.yaml";

    using var stream = assembly.GetManifestResourceStream(resourceName);
    using StreamReader reader = new(stream);
    StringReader sr = new(reader.ReadToEnd());
    Deserializer deserializer = new();
    var yamlObject = deserializer.Deserialize(sr);
    var serializer = new SerializerBuilder().JsonCompatible().Build();
    var jsonString = serializer.Serialize(yamlObject).Trim();

    _data = JsonSerializer.Deserialize<ImageRoot>(jsonString);
    _items = new();

    foreach (var item in _data.Items)
    {
      _items.Add(item.Name, item);
    }
  }
}