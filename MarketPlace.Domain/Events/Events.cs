namespace MarketPlace.Domain;

public static class Events
{
    public class ClassifiedAdCreated
    {
        public required Guid Id { get; set; }
        public required Guid OwnerId { get; set; }
    }

    public class ClassifiedAdTitleChanged
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
    }


    public class ClassifiedAdTextUpdated
    {
        public required Guid Id { get; set; }
        public required string AdText { get; set; }
    }

    public class ClassifiedAdPriceUpdated
    {
        public required Guid Id { get; set; }
        public required decimal Amount { get; set; }
        public required string CurrencyCode { get; set; }
    }

    public class ClassifiedAdSentForReview
    {
        public required Guid Id { get; set; }
    }
}
