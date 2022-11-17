using BeerSender.Domain;
using FluentAssertions;

namespace BeerSender.Tests
{
    public abstract class BeerSenderTestBase
    {
        private object[] _events;
        private readonly List<object> _resultingEvents = new();

        protected DataStore DataStore { get; } = new();

        protected void Given(params object[] events)
        {
            _events = events;
        }

        protected void When<TCommand>(TCommand command) where TCommand : ICommand
        {
            var router = new CommandRouter(_ => _events, _resultingEvents.Add, DataStore);

            router.HandleCommand(command);
        }

        protected void Expect(params object[] events)
        {
            _resultingEvents
                .ToArray()
                .Should().BeEquivalentTo(events);
        }
    }
}