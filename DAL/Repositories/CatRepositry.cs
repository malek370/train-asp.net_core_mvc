using DAL.Repositories.Irepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.BOL.Models;
using WebApplication2.DAL.data;

namespace DAL.Repositories
{
	public class CatRepositry : ICatRepository
	{
		public readonly appDbContext appdb;
		public CatRepositry(appDbContext appdb)
		{
			this.appdb = appdb;
		}

		public bool Add(Category item)
		{
			try
			{
				appdb.categories.Add(item);
				return true;
			}
			catch { return false; }
		}

		public Category Get(int id)
		{
			return appdb.categories.FirstOrDefault(c => c.Id == id);
		}

		public IEnumerable<Category> GetAll()
		{
			return appdb.categories.ToList();
		}

		public bool Remove(int id)
		{
			try
			{
				appdb.categories.Remove(Get(id));
				return true;
			}
			catch { return false; }	
		}

		public bool Save()
		{
			try
			{
				appdb.SaveChanges();
				return true;
			}
			catch { return false; }
		}

		public bool Update(Category cat)
		{
			try
			{
				appdb.categories.Update(cat);
				return true;
			}
			catch { return false; }
		}
	}
}
