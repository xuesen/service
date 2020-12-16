using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IIMes.Infrastructure.Hibernate.Data;

namespace IIMes.Infrastructure.Hibernate.MSSQL
{
    public static class RepositoryExtension
    {
        private const int NativeCommandTimeout = 180;

        public static DataTable CallSP<T>(this Repository<T> repository, string spName, params SqlParameter[] commandParameters)
            where T : class
        {
            var dbConnection = repository.CurrentSession.Connection;
            var cmd = dbConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.CommandTimeout = NativeCommandTimeout;
            if (commandParameters != null)
            {
                foreach (SqlParameter t in commandParameters)
                    cmd.Parameters.Add(t);
            }

            var trans = repository.CurrentSession.Transaction;
            if (trans?.IsActive == true)
                trans.Enlist(cmd);
            var sda = new SqlDataAdapter((SqlCommand)cmd);
            var dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DateTime GetDate<T>(this Repository<T> repository)
            where T : class
        {
            const String sql = "SELECT getdate()";
            var query = repository.CurrentSession.CreateSQLQuery(sql);
            var dt = query.List<DateTime>().FirstOrDefault();
            return dt;
        }
    }
}
