using System;
using System.Reflection;
using System.Threading.Tasks;
using VidyaVahini.Infrastructure.Contracts;

namespace VidyaVahini.WebApi.Aspects
{
    public class ServicesDecorator<TDecorated> : DispatchProxy
    {
        private TDecorated _decorated;

        private ILogger _logger;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                _logger.LogInformation($"Logging something before invoking {typeof(TDecorated)}.{targetMethod.Name}");

                var result = targetMethod.Invoke(_decorated, args);

                if (result is Task resultTask)
                {
                    resultTask.ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            _logger.LogError($"An unhandled exception was raised during execution of {typeof(TDecorated)}.{targetMethod.Name}", task.Exception);
                        }
                        _logger.LogInformation($"Log something after {typeof(TDecorated)}.{targetMethod.Name} completed");
                    });
                }
                else
                {
                    _logger.LogInformation($"Logging something after method {typeof(TDecorated)}.{targetMethod.Name} completion.");
                }

                return result;
            }
            catch (TargetInvocationException ex)
            {
                _logger.LogError($"Error during invocation of {typeof(TDecorated)}.{targetMethod.Name}", ex.InnerException ?? ex);
                throw ex.InnerException ?? ex;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during execution of {typeof(TDecorated)}.{targetMethod.Name}", ex.InnerException ?? ex);
                throw ex.InnerException ?? ex;
            }
        }

        public static TDecorated Create(TDecorated decorated, ILogger logger)
        {
            object proxy = Create<TDecorated, ServicesDecorator<TDecorated>>();
            ((ServicesDecorator<TDecorated>)proxy).SetParameters(decorated, logger);

            return (TDecorated)proxy;
        }

        private void SetParameters(TDecorated decorated, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }
    }
}
