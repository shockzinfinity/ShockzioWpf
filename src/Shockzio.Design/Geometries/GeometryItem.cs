﻿using System.Text.Json.Serialization;

namespace Shockzio.Design.Geometries;

internal class GeometryItem
{
  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("data")]
  public string Data { get; set; }
}