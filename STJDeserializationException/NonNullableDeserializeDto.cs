namespace STJDeserializationException.NonNullable
{
    public record struct DeserializeDto
    {
        public Start Start { get; set; }
    }

    public record struct Start
    {
        public float? Value { get; set; }
    }

}
