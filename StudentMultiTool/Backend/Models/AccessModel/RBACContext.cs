using StudentMultiTool.Backend.Services.DataAccess;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Models.AccessModel
{
    public class RBACContext
    {
        SQLServerDAO roles = new SQLServerDAO();

        public List<Role> GetAllRole()
        {            
            List<Role> customerRole = new List<Role>();
            string qurey = "Select * From [ROLES]";

            SqlDataReader rd = (SqlDataReader) roles.ReadData(qurey);
            while (rd.Read())
            {
                customerRole.Add(new Role
                {
                    RoleName = rd["RoleName"].ToString(),
                    RoleDescription = rd["RoleDetail"].ToString(),
                    IsSysAdmin = (bool)rd["IsAdmin"]
                });
            }

            return customerRole;

        }

        public bool AddRole(Role r)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [ROLES] value(@RoleName, @RoleDetail, @IsSysAdmin)";

            cmd.Parameters.AddWithValue("@RoleName", r.RoleName);
            cmd.Parameters.AddWithValue("@RoleDetail", r.RoleDescription);
            cmd.Parameters.AddWithValue("@IsSysAdmin", r.IsSysAdmin);
            roles.WriteData(cmd.CommandText);


            return true; 
        }

        public void UpdateRole(Role r)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [ROLES] SET @RoleName, @RoleDetail, @IsSysAdmin";

            cmd.Parameters.AddWithValue("@RoleName", r.RoleName);
            cmd.Parameters.AddWithValue("@RoleDetail", r.RoleDescription);
            cmd.Parameters.AddWithValue("@IsSysAdmin", r.IsSysAdmin);
            roles.UpdateData(cmd.CommandText);

        }

        public bool DeleteRole(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE from [ROLES] where Id = @Role_Id";
            cmd.Parameters.AddWithValue("@Role_Id", Id);
            roles.DeleteData(cmd.CommandText);

            return true;

        }


    }
}
