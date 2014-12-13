using System;
using System.Linq;
using Rookian.TNL.Infrastructure.Bootstrapping;
using SimpleInjector;

namespace Rookian.TNL.Infrastructure.Handler
{
    public class Mediator : IMediator
    {
        private readonly Container _container;

        public Mediator(Container container)
        {
            _container = container;
        }

        public TResponse Request<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var resultType = GetQueryResultType(queryType);
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

            dynamic queryHandler = _container.GetInstance(queryHandlerType);
            return queryHandler.Handle((dynamic)query);
        }


        public void Send(object command)
        {
            throw new System.NotImplementedException();
        }


        private static Type GetQueryResultType(Type queryType)
        {
            return GetQueryInterface(queryType).GetGenericArguments()[0];
        }

        private static Type GetQueryInterface(Type type)
        {
            return (type.GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .Where(x => typeof(IQuery<>).IsAssignableFrom(x.GetGenericTypeDefinition())))
                .SingleOrDefault();
        }
    }
}