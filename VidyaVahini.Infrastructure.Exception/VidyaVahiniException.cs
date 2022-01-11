using Newtonsoft.Json;
using VidyaVahini.Entities.Response;

namespace VidyaVahini.Infrastructure.Exception
{
    public class VidyaVahiniException : System.Exception
    {
        public VidyaVahiniException(ErrorDetails errorDetails)
            : base(Serialize(errorDetails))
        {

        }

        private static string Serialize(ErrorDetails errorDetails)
            => errorDetails == null ? string.Empty : JsonConvert.SerializeObject(errorDetails);
    }
}
