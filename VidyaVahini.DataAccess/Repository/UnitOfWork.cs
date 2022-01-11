using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.DataAccess.Contracts;

namespace VidyaVahini.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly vidyavahiniContext _dbContext;
		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

		public Dictionary<Type, object> Repositories
		{
			get { return _repositories; }
			set { Repositories = value; }
		}

		public UnitOfWork(vidyavahiniContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IDataAccessRepository<T> Repository<T>() where T : class
		{
			if (Repositories.Keys.Contains(typeof(T)))
			{
				return Repositories[typeof(T)] as IDataAccessRepository<T>;
			}

			IDataAccessRepository<T> repo = new DataAccessRepository<T>(_dbContext);
			Repositories.Add(typeof(T), repo);
			return repo;
		}

		public async Task<int> CommitAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public int Commit()
		{
			return _dbContext.SaveChanges();
		}

		public void Rollback()
		{
			_dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		}
	}
}
