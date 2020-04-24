using Parichay.Bussiness.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Parichay.Data.Common
{
    public interface IContext
    {
        IDbSet<Customer> Customer { get; set; }
        IDbSet<UserMst> UserMst { get; set; }
        IDbSet<Address> Address { get; set; }

        IDbSet<Sales> Sales { get; set; }
        IDbSet<SalesItem> SalesItem { get; set; }
        IDbSet<SecurePassword> SecurePassword { get; set; }
        IDbSet<Customerpayment> Customerpayment { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbRawSqlQuery<TEntity> executeSP<TEntity>(string sqlCmd, params object[] parameters) where TEntity : class;
    }
    [Serializable]
    public class ParichayContext : DbContext, IContext
    {
        static ParichayContext()
        {
            //Database.SetInitializer<ParichayContext>(null);
        }
        static string valueChainContext = System.Configuration.ConfigurationManager.ConnectionStrings["ValueChainContext"].ToString();
        public ParichayContext()
       : base(valueChainContext)
        {
            //this.Configuration.LazyLoadingEnabled = false; 
            //this.Configuration.ProxyCreationEnabled = false;
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 1000000; // value in seconds
            var context = new SqlConnection(valueChainContext);
        }
        
        public override int SaveChanges()
        {
            using (ParichayContext context = new ParichayContext())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));
                        foreach (var entry in modifiedEntries)
                        {
                            IAuditableEntity entity = entry.Entity as IAuditableEntity;
                            if (entity != null)
                            {
                                // string identityName = Thread.CurrentPrincipal.Identity.Name;
                                DateTime now = DateTime.Now;
                                if (entry.State == System.Data.Entity.EntityState.Added)
                                {
                                    //  entity.CreatedBy = identityName;
                                    entity.CreatedDate = now;
                                }
                                else
                                {
                                    // base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                                    entity.ModifiedDate = now;
                                }
                                // entity.UpdatedBy = identityName;
                            }
                        }
                        int EntityId = base.SaveChanges();
                        dbTran.Commit();
                        return EntityId;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        dbTran.Rollback();
                        //Console.WriteLine("OPTIMISTIC CONCURRENCY EXCEPTION OCCURED");
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        dbTran.Dispose();
                    }
                }
            }
        }
        public IDbSet<Customer> Customer { get; set; }
        public IDbSet<UserMst> UserMst { get; set; }
        public IDbSet<Address> Address { get; set; }

        public IDbSet<Sales> Sales { get; set; }
        public IDbSet<SalesItem> SalesItem { get; set; }
        public IDbSet<SecurePassword> SecurePassword { get; set; }

        public IDbSet<Customerpayment> Customerpayment { get; set; }

        public DbRawSqlQuery<TEntity> executeSP<TEntity>(string sqlCmd, params object[] parameters) where TEntity : class
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            var queryResult = this.Database.SqlQuery<TEntity>(sqlCmd, parameters);
            this.Configuration.AutoDetectChangesEnabled = true;
            return queryResult;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    [Serializable]
    public class ParichaySpContext : DataContext, ISPContext
    {
        static string vaultSpContext = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["ValueChainContext"]);
        public ParichaySpContext()
            : base(vaultSpContext)
        {
            this.CommandTimeout = 3000000;
        }
    
        public IEnumerable<T> getAllSP<T>(string query, params object[] parameters)
        {
            return this.ExecuteQuery<T>(query, parameters);
        }
        public IEnumerable getAllSP(string query, params object[] parameters)
        {
            return this.ExecuteQuery(null, query, parameters);
        }
        public T getScalarSP<T>(string query, params object[] parameters)
        {
            return this.ExecuteQuery<T>(query, parameters).FirstOrDefault();
        }

        //[Function(Name = "_SP_AddCustomerWithAddress")]
        //public ISingleResult<SpScalar> AddCustomerWithAddress(
        //                                      [Parameter(Name = "Id", DbType = "Int")] int Id,
        //                                [Parameter(Name = "FirstName", DbType = "varchar(200)")] string FirstName,
        //                                [Parameter(Name = "MiddleName", DbType = "varchar(200)")] string MiddleName,
        //                                [Parameter(Name = "LastName", DbType = "varchar(200)")] string LastName,
        //                                [Parameter(Name = "Gender", DbType = "varchar(200)")] string Gender,
        //                                [Parameter(Name = "EmailId", DbType = "varchar(200)")] string EmailId,
        //                                [Parameter(Name = "MobileNumberOne", DbType = "varchar(200)")] string MobileNumberOne,
        //                                [Parameter(Name = "MobileNumbertwo", DbType = "varchar(200)")] string MobileNumbertwo,
        //                                [Parameter(Name = "Remark", DbType = "varchar(500)")] string Remark,
        //                                [Parameter(Name = "AddressGridSerialize", DbType = "nvarchar(max)")] string AddressGridSerialize
        //                              )
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),
        //         FirstName
        //        );
        //    return (ISingleResult<SpScalar>)(result.ReturnValue);
        //}

    }
    public interface ISPContext
    {
        IEnumerable<T> getAllSP<T>(string query, params object[] parameters);
        T getScalarSP<T>(string query, params object[] parameters);
        IEnumerable getAllSP(string query, params object[] parameters);

        //ISingleResult<SpScalar> AddCustomerWithAddress(
        //                                [Parameter(Name = "Id", DbType = "Int")] int Id,
        //                                [Parameter(Name = "FirstName", DbType = "varchar(200)")] string FirstName,
        //                                [Parameter(Name = "MiddleName", DbType = "varchar(200)")] string MiddleName,
        //                                [Parameter(Name = "LastName", DbType = "varchar(200)")] string LastName,
        //                                [Parameter(Name = "Gender", DbType = "varchar(200)")] string Gender,
        //                                [Parameter(Name = "EmailId", DbType = "varchar(200)")] string EmailId,
        //                                [Parameter(Name = "MobileNumberOne", DbType = "varchar(200)")] string MobileNumberOne,
        //                                [Parameter(Name = "MobileNumbertwo", DbType = "varchar(200)")] string MobileNumbertwo,
        //                                [Parameter(Name = "Remark", DbType = "varchar(500)")] string Remark,
        //                                [Parameter(Name = "AddressGridSerialize", DbType = "nvarchar(max)")] string AddressGridSerialize
        //                              );
    }
}
