using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMultiTool.Backend.Services.DataAccess
{
    public class DataAccessType
    {
        // Mehthod to create a specific data source suitable to the need
        public DataSource<string> getDataSource(string name, string info)
        {
            try
            {
                if (String.Equals(name, "SQL", StringComparison.OrdinalIgnoreCase))
                {
                    return new SQLServerDAO(info);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RelationDataFactory: Data Access object creation failed!");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
