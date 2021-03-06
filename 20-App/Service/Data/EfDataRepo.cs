﻿// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using myCoreMvc.Domain;

// namespace myCoreMvc.App.Services
// {
//     public class EfDataRepo : IDataRepo
//     {
//         /*==================================  Fields =================================*/

//         private readonly EfCtx Ctx;

//         /*==================================  Methods =================================*/

//         public EfDataRepo(EfCtx ctx)
//         {
//             Ctx = ctx;
//         }

//         /*==================================  Methods of IDataRepo =================================*/

//         public TransactionResult Add<T>(T obj) where T : class, IThing
//         {
//             Ctx.Set<T>().Add(obj); //Task: We could simply say Ctx.Add(obj) and let it figure type and stuff
//             return Ctx.SaveChanges() == 0 ? TransactionResult.Failed : TransactionResult.Added;
//         }

//         public T Get<T>(Guid id) where T : class, IThing => Ctx.Set<T>().Find(id); //Find() is more performant than GetList etc.

//         public T Get<T>(Predicate<T> func) where T : class, IThing => Ctx.Set<T>().SingleOrDefault(t => func(t)); //Single() is a Linq to Entity execution method

//         public List<T> GetList<T>() where T : class, IThing => Ctx.Set<T>().ToList(); //ToList() is a Linq to Entity execution method

//         public List<T> GetList<T>(Predicate<T> func) where T : class, IThing => Ctx.Set<T>().Where(t => func(t)).ToList();

//         public List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing => GetQueryableIncluding<T>(includeProperties).ToList();

//         public List<T> GetListIncluding<T>(Predicate<T> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
//             => GetQueryableIncluding<T>(includeProperties).Where(predicate).ToList();

//         private IQueryable<T> GetQueryableIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
//         {
//             IQueryable<T> queryable = Ctx.Set<T>();
//             return includeProperties.Aggregate(queryable, (current, nextInclude) => current.Include(nextInclude));
//         }

//         public TransactionResult Update<T>(T obj) where T : class, IThing
//         {
//             Ctx.Set<T>().Update(obj);
//             return Ctx.SaveChanges() == 0 ? TransactionResult.NotFound : TransactionResult.Updated;
//         }

//         public TransactionResult Delete<T>(Guid id) where T : class, IThing
//         {
//             var target = Get<T>(id); // To delete using a single DB trip, we can use Ctx.Database.ExecuteSqlCommand("exec DeleteById {0}", id) or DbSet.FromSql()
//             Ctx.Set<T>().Remove(target);
//             return Ctx.SaveChanges() == 0 ? TransactionResult.NotFound : TransactionResult.Deleted;
//         }

//         TransactionResult IDataRepo.Save<T>(T obj)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
