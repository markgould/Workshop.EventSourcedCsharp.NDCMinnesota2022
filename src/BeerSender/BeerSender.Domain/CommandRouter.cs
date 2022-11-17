namespace BeerSender.Domain;

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

                if (aggregate is IHandleCommand<T> handler)
                    foreach (var @event in handler.Handle(command))
                        _publishEvent(@event);
                break;
        }
    }
}