using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories.IRepository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
