using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable? repositories = null;
        public UnitOfWork(StoreContext storeContext)
        {
               _storeContext = storeContext;
        }
        public async Task<int> Compelete()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _storeContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity 
        {
            if(repositories is null)  repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if(!repositories.ContainsKey(type) )
            {
             var repoType = typeof(GenericRepository<>);
           var instance = Activator.CreateInstance( repoType.MakeGenericType(typeof(TEntity)) , _storeContext);
                repositories.Add(type, instance);
            }

            return (IGenericRepository<TEntity>)repositories[type]!;
          
           
        }
    }
}
