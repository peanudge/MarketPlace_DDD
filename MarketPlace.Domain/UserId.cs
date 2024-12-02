namespace MarketPlace.Domain;

public record class UserId(Guid Value)
{
    public static implicit operator Guid(UserId self) => self.Value;
}
