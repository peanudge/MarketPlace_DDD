namespace MarketPlace.Domain;

public class ClassifiedAd
{
    public ClassifiedAdId Id { get; }

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
    {
        Id = id;
        OwnerId = ownerId;
        State = ClassifiedAdState.Inactive;
    }

    public void SetTitle(ClassifiedAdTitle title)
    {
        Title = title;
        EnsureValidState();
    }
    public void UpdateText(ClassifiedAdText text)
    {
        Text = text;
        EnsureValidState();
    }
    public void UpdatePrice(Price price)
    {
        Price = price;
        EnsureValidState();
    }

    public UserId OwnerId;
    public ClassifiedAdTitle? Title { get; private set; }
    public ClassifiedAdText? Text { get; private set; }
    public Price? Price { get; private set; }
    public ClassifiedAdState State { get; private set; }
    public UserId? ApprovedBy { get; private set; }

    public void RequestToPublish()
    {
        State = ClassifiedAdState.PendingReview;
        EnsureValidState();
    }

    private void EnsureValidState()
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
