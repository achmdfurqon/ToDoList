using Dapper;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Data.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ConnectionStrings connectionString;
        DynamicParameters parameters = new DynamicParameters();
        CommandType spType = CommandType.StoredProcedure;
        DataContext dataContext;
        string sp;
        public ActivityRepository(ConnectionStrings connectionStrings, DataContext context)
        {
            connectionString = connectionStrings;
            dataContext = context;
        }
        
        public async Task<DataTableView> Get(string uid, int status, string keyword, int page, int size)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_DataTableServerSide";
                var offset = (page - 1) * size;
                parameters.Add("Offset", offset);
                parameters.Add("PageSize", size);
                parameters.Add("UserId", uid);
                parameters.Add("Status", status);
                parameters.Add("Keyword", keyword);
                parameters.Add("@length", DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@filterLength", DbType.Int32, direction: ParameterDirection.Output);
                var result = new DataTableView();
                result.data = await sql.QueryAsync<ActivityVM>(sp, parameters, commandType: spType);
                result.length = parameters.Get<int>("@length");
                result.filterLength = parameters.Get<int>("@filterLength");
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_DeleteActivity";
                parameters.Add("Id", id);
                var result = await sql.ExecuteAsync(sp, parameters, commandType: spType);
                return Convert.ToBoolean(result);
            }
        }

        public async Task<IEnumerable<ActivityVM>> Get()
        {
            using(var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_GetActivities";
                var result = await sql.QueryAsync<ActivityVM>(sp);
                return result;
            }            
        }

        public async Task<ActivityVM> Get(int id)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_GetActivity";
                parameters.Add("Id", id);
                var result = await sql.QuerySingleOrDefaultAsync<ActivityVM>(sp, parameters, commandType: spType);
                return result;
            }
        }

        public async Task<int> Post(ActivityVM activity)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_AddActivity";
                parameters.Add("uId", activity.UserId);
                parameters.Add("Name", activity.Name);
                parameters.Add("Create", DateTime.Now);
                var result = await sql.ExecuteAsync(sp, parameters, commandType: spType);
                return result;
            }
        }

        public async Task<int> Put(int id, ActivityVM activity)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_EditActivity";
                parameters.Add("Name", activity.Name);
                parameters.Add("Update", DateTime.Now);
                parameters.Add("Id", id);
                var result = await sql.ExecuteAsync(sp, parameters, commandType: spType);
                return result;
            }
        }
        public async Task<bool> Complete(int id)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_CompleteActivity";
                parameters.Add("Finish", DateTime.Now);
                parameters.Add("Id", id);
                var result = await sql.ExecuteAsync(sp, parameters, commandType: spType);
                return Convert.ToBoolean(result);
            }
        }
        public async Task<bool> Remove(int id)
        {
            using (var sql = new SqlConnection(connectionString.Value))
            {
                sp = "SP_RemoveActivity";
                parameters.Add("Delete", DateTime.Now);
                parameters.Add("Id", id);
                var result = await sql.ExecuteAsync(sp, parameters, commandType: spType);
                return Convert.ToBoolean(result);
            }
        }
    }
}
