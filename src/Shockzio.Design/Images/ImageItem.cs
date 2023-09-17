using System.Text.Json.Serialization;

namespace Shockzio.Design.Images;

internal class ImageItem
{
  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("data")]
  public string Data { get; set; }
}