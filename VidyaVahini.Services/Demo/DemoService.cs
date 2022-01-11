using VidyaVahini.Entities.Demo;
using VidyaVahini.Infrastructure.Logger;

namespace VidyaVahini.Services.Demo
{
    public class DemoService : IDemoService
    {
        private readonly ILogger _logger;

        public DemoService(ILogger logger)
        {
            _logger = logger;
        }
        public DemoResponse Demo()
        {
            _logger.LogInformation("Log Test from DemoRepository -Test method.");
            return new DemoResponse();
        }
    }
}
