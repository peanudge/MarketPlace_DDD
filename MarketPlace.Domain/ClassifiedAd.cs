using MarketPlace.Framework;

namespace MarketPlace.Domain;

public class ClassifiedAd : Entity
{
    public ClassifiedAdId Id { get; private set; }
    public UserId OwnerId { get; private set; }
    public ClassifiedAdTitle? Title { get; private set; }
    public ClassifiedAdText? Text { get; private set; }
    public Price? Price { get; private set; }
    public ClassifiedAdState State { get; private set; }
    public UserId? ApprovedBy { get; private set; }

    // INFO: Apply(Event object) will handle initializing Id, OwerId.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Apply(new Events.ClassifiedAdCreated
        {
            Id = id,
            OwnerId = ownerId
        });
    }

    public void SetTitle(ClassifiedAdTitle title)
    {
        Apply(new Events.ClassifiedAdTitleChanged
        {
            Id = Id,
            Title = title
        });
    }

    public void UpdateText(ClassifiedAdText text)
    {
        Apply(new Events.ClassifiedAdTextUpdated
        {
            Id = Id,
            AdText = text
        });
    }
    public void UpdatePrice(Price price)
    {
        Apply(new Events.ClassifiedAdPriceUpdated
        {
            Id = Id,
            Amount = price.Amount,
            CurrencyCode = price.Currency.CurrencyCode
        });
    }

    public void RequestToPublish()
    {
        Apply(new Events.ClassifiedAdSentForReview
        {
            Id = Id
        });
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.ClassifiedAdCreated e:
                Id = new ClassifiedAdId(e.Id);
                OwnerId = new UserId(e.OwnerId);
                State = ClassifiedAdState.Inactive;
                break;
            case Events.ClassifiedAdTitleChanged e:
                Title = new ClassifiedAdTitle(e.Title);
                break;
            case Events.ClassifiedAdTextUpdated e:
                Text = new ClassifiedAdText(e.AdText);
                break;
            case Events.ClassifiedAdPriceUpdated e:
                Price = new Price(e.Amount, e.CurrencyCode);
                break;
            case Events.ClassifiedAdSentForReview e:
                State = ClassifiedAdState.PendingReview;
                break;
        }
    }

    protected override void EnsureValidState()
    {

        var valid = Id != null && OwnerId != null && (State switch
        {
            ClassifiedAdState.PendingReview =>
                Title != null
                && Text != null
                && Price != null
                && Price?.Amount > 0,
            ClassifiedAdState.Active =>
                Title != null
                && Text != null
                && Price != null
                && Price.Amount > 0
                && ApprovedBy != null,
            _ => true
        });

        if (!valid)
        {
            throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
        }
    }

    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
}

public class InvalidEntityStateException : Exception
{
    public InvalidEntityStateException(object entity, string message)
        : base($"Entity {entity.GetType().Name} state change rejected, {message}")
    {
    }
}
