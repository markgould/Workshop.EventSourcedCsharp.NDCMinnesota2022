using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package
{
    public record PackageCreated(Guid PackageId);

    public class CreatePackage
    {
        public record Command(Guid PackageId) : IPackageCommand;

        internal class Handler : IHandle<Command>
        {
            public IEnumerable<object> Handle(Command command)
            {
                yield return new PackageCreated(command.PackageId);
            }
        }
    }
}
