﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Talabat.Core.Specifications
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery,ISpecification<T> Spec) 
        {
            var Query = InputQuery;
            if(Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }
            if(Spec.OrderBy is not null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }
            if(Spec.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDescending);
            }
            if (Spec.AllowPagination)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            Query = Spec.Includes.Aggregate(Query, (CurrentQuerry, IncludeExpression) => CurrentQuerry.Include(IncludeExpression));
            return Query;
        }
    }
}
