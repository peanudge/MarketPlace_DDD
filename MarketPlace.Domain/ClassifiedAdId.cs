namespace MarketPlace.Domain;

public record class ClassifiedAdId(Guid Value)
{
    public static implicit operator Guid(ClassifiedAdId self) => self.Value;
}
