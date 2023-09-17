using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shockzio.Design.Geometries;

internal class GeometryRoot
{
  [JsonPropertyName("geometries")]
  public List<GeometryItem> Items { get; set; }
}