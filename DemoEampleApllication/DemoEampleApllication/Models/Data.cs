using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Summary description for Data
/// </summary>
/// 
namespace Demoexample
{

    public class Data : IDisposable
    {
        bool disposed;

        SqlConnection con = null;
        SqlCommand comm = null;

        SqlConnection sqlImportConnection = null;
        SqlCommand sqlImportCom = null;

        SqlDataAdapter adap = null;
        string strConnection = null;
        string strImportCon = null;
        public Data()
        {
            disposed = false;

            strConnection = ConfigurationManager.AppSettings["ConnectionString"] ;
            //strConnection = ConfigurationManager.AppSettings["ConnectionString"];
            strImportCon = ConfigurationManager.AppSettings["ConnectionString"] + ";async=true;";
            con = new SqlConnection(strConnection);
            sqlImportConnection = new SqlConnection(strImportCon);
            comm = new SqlCommand();
            comm.Connection = con;
            comm.Connection.Open();

            sqlImportCom = new SqlCommand();
            sqlImportCom.Connection = sqlImportConnection;
            sqlImportCom.Connection.Open();

            adap = new SqlDataAdapter();

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                            comm.Connection.Close();

                    }
                    instance = null;
                }

                disposed = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
        }

        private static Data instance = null;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }

        public DataSet GetDataset(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                comm.CommandText = query;
                //comm.Connection.Open();
                adap.SelectCommand = comm;
                adap.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }
            return ds;
        }

        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                comm.CommandText = query;
                //comm.Connection.Open();

                adap.SelectCommand = comm;
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }
            return dt;
        }

        public bool ExecuteNonQuery(string query)
        {
            bool res = false;
            try
            {
                comm.CommandText = query;

                // comm.Connection.Open();
                int x = comm.ExecuteNonQuery();
                res = (x == 1 ? true : false);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }

            return res;
        }

        public object ExecuteScalar(string query)
        {
            object x = null;
            try
            {
                comm.CommandText = query;
                //comm.Connection.Open();
                x = comm.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }
            return x;
        }

        public T ExecuteSP<T>(List<SqlParameter> parameters, string spName)
        {
            object obj = null;
            try
            {
                //lock (comm)
                //{
                //using (con = new SqlConnection(con))
                //{
                using (sqlImportCom = new SqlCommand())
                {
                    sqlImportCom.Connection = sqlImportConnection;
                    //sqlImportCom.Connection.Open();
                    sqlImportCom.CommandText = spName;
                    sqlImportCom.CommandType = CommandType.StoredProcedure;
                    sqlImportCom.Parameters.Clear();
                    foreach (SqlParameter pm in parameters)
                    {
                        sqlImportCom.Parameters.Add(pm);
                    }
                    IAsyncResult aRes = null;
                    WaitHandle wHandle;

                    aRes = sqlImportCom.BeginExecuteNonQuery();
                    wHandle = aRes.AsyncWaitHandle;
                    wHandle.WaitOne();
                    obj = sqlImportCom.EndExecuteNonQuery(aRes);
                    obj = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }

                throw new Exception(s);
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }

public bool ExecuteSP_new(List<SqlParameter> parameters, string spName)
        {
            bool obj = false;
            try
            {
                //lock (comm)
                //{
                //using (con = new SqlConnection(con))
                //{
                using (sqlImportCom = new SqlCommand())
                {
                    sqlImportCom.Connection = sqlImportConnection;
                    //sqlImportCom.Connection.Open();
                    sqlImportCom.CommandText = spName;
                    sqlImportCom.CommandType = CommandType.StoredProcedure;
                    sqlImportCom.Parameters.Clear();
                    foreach (SqlParameter pm in parameters)
                    {
                        sqlImportCom.Parameters.Add(pm);
                    }
                    IAsyncResult aRes = null;
                    WaitHandle wHandle;

                    aRes = sqlImportCom.BeginExecuteNonQuery();
                    wHandle = aRes.AsyncWaitHandle;
                    wHandle.WaitOne();
                    int a= sqlImportCom.EndExecuteNonQuery(aRes);
                    obj = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }

                //throw new Exception(s);
                return false;
            }
            //return (T)Convert.ChangeType(obj, typeof(T));
            return true;
        }
    }
}
