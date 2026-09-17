using onine.core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace onine.core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>?Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPaginationEnabled { get; set ; }

        //Get all
        public BaseSpecifications()
        {
           // Includes = new List<Expression<Func<T, object>>>();
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpress)
        {
            Criteria = criteriaExpress;
            //Includes = new List<Expression<Func<T, object>>>();
        }
        public void SetOrderBy(Expression<Func<T,object>> OrderByExpression)
        {  
            OrderBy = OrderByExpression;    
        }
        public void SetOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
        public void ApplyPagination (int skip , int take)
        {
            Skip = skip;    
            Take = take;
            IsPaginationEnabled = true;
        }

    }
}
