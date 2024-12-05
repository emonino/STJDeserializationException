using STJDeserializationException;
using System.Text.Json;
using System.Text.Json.Serialization;

JsonSerializerOptions options = new ()
{
    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
    ReferenceHandler = ReferenceHandler.Preserve,
    DefaultBufferSize = 1_024, // intentionally made smaller than default to make bug more likely
};

// List<Interval> originalData = Interval.GenerateRandomData(1000, DateTime.Today);
// string dataStr = JsonSerializer.Serialize(originalData);
string dataStr = Interval.GetStaticData();

using MemoryStream stream = new();
using StreamWriter streamWriter = new(stream);
await streamWriter.WriteAsync(dataStr);
await streamWriter.FlushAsync();
stream.Position = 0;

// the following work
var syncDeserialization = JsonSerializer.Deserialize<List<DeserializeDto>>(dataStr);
var nonNullableDeserialization = await JsonSerializer.DeserializeAsync<List<STJDeserializationException.NonNullable.DeserializeDto>>(stream, options);

// but this fails
stream.Position = 0;
var actual = await JsonSerializer.DeserializeAsync<List<DeserializeDto>>(stream, options);