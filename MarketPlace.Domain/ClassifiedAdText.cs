namespace MarketPlace.Domain;

public record class ClassifiedAdText
{
    private readonly string _value;

    public string Value => _value;

    public static ClassifiedAdText FromString(string title) => new ClassifiedAdText(title);

    private ClassifiedAdText(string value)
    {
        if (value.Length > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Text cannot be longer than 1000 characters");
        }
        _value = value;
    }
}
