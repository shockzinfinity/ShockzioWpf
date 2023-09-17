using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shockzio.Design.Images;

internal class ImageRoot
{
  [JsonPropertyName("images")]
  public List<ImageItem> Items { get; set; }
}