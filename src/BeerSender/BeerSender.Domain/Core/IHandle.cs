namespace BeerSender.Domain.Core;

public interface IHandle<in TCommand> where TCommand : ICommand
{
    IEnumerable<object> Handle(TCommand command);
}