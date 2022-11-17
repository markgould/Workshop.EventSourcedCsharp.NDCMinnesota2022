using BeerSender.Domain.Package;

namespace BeerSender.Domain.Core;

public class CommandRouter
{
    private readonly Func<Guid, IEnumerable<object>> _eventStream;
    private readonly Action<object> _publishEvent;
    private readonly DataStore _dataStore;

    public CommandRouter(Func<Guid, IEnumerable<object>> eventStream, Action<object> publishEvent, DataStore dataStore)
    {
        _eventStream = eventStream;
        _publishEvent = publishEvent;
        _dataStore = dataStore;
    }

    public void HandleCommand<T>(T command) where T : ICommand
    {
        switch (command)
        {
            case IPackageCommand packageCommand:
                if (!_dataStore.AggregateDictionary.TryGetValue(packageCommand.PackageId, out var aggregate))
                {
                    aggregate = new BeerPackage();
                    _dataStore.AggregateDictionary.Add(packageCommand.PackageId, aggregate);
                }

                foreach (var @event in _eventStream(packageCommand.PackageId))
                {
                    aggregate.Apply(@event);
                }

                var handler = FindHandler(command);
                foreach (var @event in handler.Handle(command))
                    _publishEvent(@event);
                break;
        }
    }

    private static IHandle<T> FindHandler<T>(T command) where T : ICommand
    {
        var type = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .FirstOrDefault(x => typeof(IHandle<T>).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        if (type == null)
            throw new Exception($"Handler not found for type {command.GetType()}");
        var handler = Activator.CreateInstance(type) as IHandle<T>;
        return handler;
    }
}