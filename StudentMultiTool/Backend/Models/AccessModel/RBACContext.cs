using StudentMultiTool.Backend.Services.DataAccess;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Models.AccessModel
{
    public class RBACContext
    {
        public string RoleGet()
        {
            string value = " "; 
            SQLServerDAO roles = new SQLServerDAO();
            List<Role> r = new List<Role>();
            string qurey = "Select * From [ROLES]";

            SqlDataReader rd = (SqlDataReader) roles.ReadData(qurey);
            while (rd.Read())
            {
                r.Add(new Role
                {
                    RoleName = rd["RoleName"].ToString(),
                    RoleDescription = rd["RoleDetail"].ToString(),
                    IsSysAdmin = (bool)rd["IsAdmin"]
                });
            }

            return value;

        }

        public void upload()
        {

        }


    }
}
