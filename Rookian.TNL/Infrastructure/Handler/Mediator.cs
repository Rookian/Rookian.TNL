namespace Rookian.TNL.Infrastructure.Handler
{
    public class Mediator : IMediator
    {
        public TResponse Request<TResponse>(IQuery<TResponse> query)
        {
            throw new System.NotImplementedException();
        }

        public void Send(object command)
        {
            throw new System.NotImplementedException();
        }
    }
}