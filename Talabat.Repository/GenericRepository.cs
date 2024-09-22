using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly G01DbContext dbContactor;

        public GenericRepository(G01DbContext DbContactor) {
            dbContactor = DbContactor;
        }

        #region BeforeSpecification
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await dbContactor.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
            }
            return await dbContactor.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {

            return await dbContactor.Set<T>().FindAsync(id);
        }
        #endregion

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

       

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(dbContactor.Set<T>(), Spec);
        }
    }
}
