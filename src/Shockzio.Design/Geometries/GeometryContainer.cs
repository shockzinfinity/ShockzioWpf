using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace Shockzio.Design.Geometries;

public class GeometryContainer
{
  internal static GeometryRoot _data;
  internal static Dictionary<string, GeometryItem> _items;

  static GeometryContainer()
  {
    Build();
  }

  private static void Build()
  {
    Assembly assembly = Assembly.GetExecutingAssembly();
    var resourceName = "Shockzio.Design.Properties.Resources.geometries.yaml";

    using Stream stream = assembly.GetManifestResourceStream(resourceName);
    using StreamReader reader = new(stream);
    StringReader sr = new(reader.ReadToEnd());
    Deserializer deserializer = new();
    var yamlObject = deserializer.Deserialize(sr);
    var serialzer = new SerializerBuilder().JsonCompatible().Build();
    var jsonString = serialzer.Serialize(yamlObject).Trim();

    _data = JsonSerializer.Deserialize<GeometryRoot>(jsonString);
    _items = new();

    foreach (var item in _data.Items)
    {
      _items.Add(item.Name, item);
    }
  }
}