using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Irepositories
{
	public interface Irepository<T> where T : class
	{
		public T Get(int id);
		public IEnumerable<T> GetAll();
		public bool Remove(int id);
		public bool Add(T item);
		public bool Update(T item);
		public bool Save();
	}
}
