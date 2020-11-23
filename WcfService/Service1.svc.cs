using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private string connectionString { get; } = "workstation id=BIPIT-KR1.mssql.somee.com;packet size=4096;user id=Gena_20_SQLLogin_1;pwd=u9svrptnie;data source=BIPIT-KR1.mssql.somee.com;persist security info=False;initial catalog=BIPIT-KR1";

        public DataTable GetData(string dateFrom, string dateTo)
        {
            string query = @"SELECT dbo.register.id, dbo.register.date, dbo.clients.name AS client_name, dbo.services.name AS service_name, dbo.services.price
                               FROM dbo.register
                         INNER JOIN dbo.clients  ON dbo.register.client_id  = dbo.clients.id 
                         INNER JOIN dbo.services ON dbo.register.service_id = dbo.services.id ";
            if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
            {
                query += $"WHERE dbo.register.date >= '{dateFrom}' AND dbo.register.date <= '{dateTo}'";
            }
            using (var connection = new SqlConnection(connectionString))
            {
                var dataAdapter = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand(query)
                    {
                        Connection = connection
                    }
                };
                var table = new DataTable() { TableName = "Register" };
                dataAdapter.Fill(table);

                return table;
            }
        }

        string GetString(string data)
        {
            return data;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        string IService1.GetString(string data)
        {
            throw new NotImplementedException();
        }
    }
}
