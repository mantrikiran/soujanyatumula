namespace VidyaVahini.Infrastructure.Contracts
{
    public interface ILogger
    {
        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">Error Message</param>
        void LogError(string message);

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="ex">Exception</param>
        void LogError(string message, System.Exception ex);

        /// <summary>
        /// Logs an information
        /// </summary>
        /// <param name="message">Message</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs a message in debug mode
        /// </summary>
        /// <param name="message">Message</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">Message</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex">Exception</param>
        void LogException(System.Exception ex);
    }
}
