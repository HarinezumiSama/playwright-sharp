using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlaywrightSharp.Transport.Channels
{
    internal class ChannelToGuidConverter : JsonConverter<ChannelBase>
    {
        private readonly PlaywrightConnection _connection;

        public ChannelToGuidConverter(PlaywrightConnection connection)
        {
            _connection = connection;
        }

        public override bool CanConvert(Type type) => typeof(ChannelBase).IsAssignableFrom(type);

        public override ChannelBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            string guid = document.RootElement.GetProperty("guid").ToString();
            return _connection.GetObject(guid).Channel;
        }

        public override void Write(Utf8JsonWriter writer, ChannelBase value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.Guid);
    }
}
