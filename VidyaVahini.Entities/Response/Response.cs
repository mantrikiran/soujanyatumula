namespace VidyaVahini.Entities.Response
{
    public class Response<T>
        where T : class
    {
        public bool Success { get; set; }
        public ErrorDetails Error { get; set; }
        public T Body { get; set; }

        public Response()
        {
            Error = new ErrorDetails();
        }
    }
}
