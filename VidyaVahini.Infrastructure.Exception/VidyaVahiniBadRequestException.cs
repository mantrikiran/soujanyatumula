namespace VidyaVahini.Infrastructure.Exception
{
    public class VidyaVahiniBadRequestException : System.Exception
    {
        public VidyaVahiniBadRequestException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
