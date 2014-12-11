namespace Rookian.TNL.Infrastructure.Handler
{
    public interface IMediator
    {
        TResponse Request<TResponse>(IQuery<TResponse> query);
        void Send(object command);
    }
}