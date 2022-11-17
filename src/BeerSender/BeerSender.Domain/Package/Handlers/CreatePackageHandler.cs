using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package.Handlers
{
    internal class CreatePackageHandler : IHandle<CreatePackage>
    {
        public IEnumerable<object> Handle(CreatePackage command)
        {
            yield return new PackageCreated(command.PackageId);
        }
    }
}
