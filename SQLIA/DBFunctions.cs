using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SQLIA
{
    class DBFunctions
    {
        SqlConnection objSqlConnection;
        SqlCommand objSQLCommand;
        SqlDataAdapter objSQLDataAdapter;

        public DBFunctions()
        {}

        private string GetDBConnectionString()
        {
            return "Data Source=.;Initial Catalog=Sample;Integrated Security=True";
        }


        public DataTable GetValidSQLSmt()
        {
            DataTable dtValidSQL = null;
            try
            {
                dtValidSQL = new DataTable();
                objSqlConnection = new SqlConnection(GetDBConnectionString());
                objSQLCommand = new SqlCommand("GetValidSQLSmt", objSqlConnection);
                objSQLCommand.CommandType = CommandType.StoredProcedure;

                objSQLDataAdapter = new SqlDataAdapter(objSQLCommand);
                objSQLDataAdapter.Fill(dtValidSQL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dtValidSQL;
        }
    }
}
