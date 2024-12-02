using System.Text.RegularExpressions;

namespace MarketPlace.Domain;

public record class ClassifiedAdTitle
{
    private readonly string _value;
    public string Value => _value;

    public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);

    public static ClassifiedAdTitle FromHtml(string htmlTitle)
    {
        var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "**")
            .Replace("</b>", "**");
        return new ClassifiedAdTitle(Regex.Replace(
          supportedTagsReplaced, "<.*?>", string.Empty));
    }

    private ClassifiedAdTitle(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Title cannot be longer than 100 characters");
        }
        _value = value;
    }
}
