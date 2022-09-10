namespace CQRS.Infras.Interfaces
{
    public interface ICommandHandler<in TCommand, TCommandResult>
    {
        Task<TCommandResult> Handle(TCommand query, CancellationToken cancellation);
    }
}
