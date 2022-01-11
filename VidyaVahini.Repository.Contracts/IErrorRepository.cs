using VidyaVahini.Entities.Response;

namespace VidyaVahini.Repository.Contracts
{
    public interface IErrorRepository
    {
        /// <summary>
        /// Gets the error message from the database
        /// </summary>
        /// <param name="errorId">Id</param>
        /// <returns>Error details</returns>
        ErrorDetails GetError(int errorId);
    }
}
