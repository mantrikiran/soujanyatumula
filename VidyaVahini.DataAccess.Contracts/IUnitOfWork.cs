namespace VidyaVahini.DataAccess.Contracts
{
	public interface IUnitOfWork
	{
		/// <summary>
		/// Gets the data access repository
		/// </summary>
		/// <typeparam name="T">Repository Type</typeparam>
		/// <returns>Repository instance</returns>
		IDataAccessRepository<T> Repository<T>() where T : class;
		
		/// <summary>
		/// Commits the changes to the database
		/// </summary>
		/// <returns>Number of records updated</returns>
		int Commit();

		/// <summary>
		/// Rolls back the changes made to a database
		/// </summary>
		void Rollback();
	}
}
