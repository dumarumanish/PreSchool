using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using PreSchool.Application.Models;
using PreSchool.Data.Entities;

namespace PreSchool.Application
{
    public static  class IQueryableExtension
    {

        public static IQueryable<TEntity> IgnoreDeletedNavigationProperties<TEntity>(this IQueryable<TEntity> source) where TEntity : class, IBaseEntity
        {
            source = source.IgnoreQueryFilters();
            source = source.Where(s => !s.IsDeleted);
            return source;
        }


        public async static Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
                                          int pageNumber = 1, int? pageSize = null) where T : class
        {
            // Check if page number is less than 1
            if (pageNumber < 1)
                pageNumber = 1;


            var result = new PagedResult<T>();
            result.CurrentPage = pageNumber;
            result.RowCount = await query.CountAsync();


            // If the page size is null, then all the rows are return
            result.PageSize = pageSize ?? result.RowCount;

            // Check if page size is less than 1, Atleast one page should be displayed 
            if (result.PageSize < 1)
                result.PageSize = 1;

            var pageCount = (double)result.RowCount / result.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (pageNumber - 1) * result.PageSize;
            result.Results = await query.Skip(skip).Take(result.PageSize).ToListAsync();

            return result;
        }



        /// <summary>
        /// Sort the given query
        /// var thingsQuery = _context.Things
        ///                 .Include(t => t.Other)
        ///                 .Where(t => t.Deleted == false)
        ///                 .OrderBy(sortModels);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortModels"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortModel> sortModels)
        {

            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.Name);
                var method = item.Desc ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");

                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;


        }


        /// <summary>
        /// Return true if the entity have any relations else return false
        /// Must include all the Collections. If not must enable Proxy lazy loading
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityObj"></param>
        /// <returns></returns>
        public static bool HasAnyRelation<T>(this T entityObj) where T : class
        {

            var collectionProps = GetManyRelatedEntityNavigatorProperties(entityObj);


            foreach (var item in collectionProps)
            {
                var collectionValue = GetEntityFieldValue(entityObj, item.Name);
                if (collectionValue != null && collectionValue is IEnumerable)
                {
                    var col = collectionValue as IEnumerable;
                    if (col.GetEnumerator().MoveNext())
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        #region Privete Helper Method
        private static object GetEntityFieldValue(object entityObj, string propertyName)
        {
            var pro = entityObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).First(x => x.Name == propertyName);
            return pro.GetValue(entityObj, null);

        }

        private static IEnumerable<PropertyInfo> GetManyRelatedEntityNavigatorProperties(object entityObj)
        {
            var props = entityObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite && x.GetGetMethod().IsVirtual && x.PropertyType.IsGenericType == true);
            return props;
        }


        #endregion
    }
}
