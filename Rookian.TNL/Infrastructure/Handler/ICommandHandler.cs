namespace Rookian.TNL.Infrastructure.Handler
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}