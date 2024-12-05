namespace MarketPlace.Api;

public interface IApplicationService
{
    Task Handle(object command);
}
