using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package;

public record BeerAdded(Guid PackageId, BeerBottle Beer);
public record BeerFailedToAdd(Guid PackageId, BeerBottle Beer, AddBeerFailureReason AddBeerFailureReason);

public enum AddBeerFailureReason
{
    PackageFull
}

public class AddBeer
{
    public record Command(Guid PackageId, BeerBottle Beer) : IPackageCommand;

    internal class Handler : IHandle<Command>
    {
        public IEnumerable<object> Handle(Command command)
        {
            yield return new BeerAdded(command.PackageId, command.Beer);
        }
    }
}