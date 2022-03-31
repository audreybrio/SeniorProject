using System;
using Xunit;
using StudentMultiTool.Backend.Services.UserManagement;

namespace BulkOperationsTesting
{
    // All of these tests fail with the following exception:
    // System.IO.FileNotFoundException, re: C:\Users\Public\TestFolder\bulkops.txt
    // For these tests to pass, that file must exist exist on your system.
    public class BulkUnitTesting
    {
        [Fact]
        public void GroupCreateUsers()
        {
            string fileName = "bulkops.txt";
                    string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for (int i = 0; i < bulkOperLines.Length; i++)
            {
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if (newUser[0] == "CreateUser")
                {
                    bool userCreated = UserManager.CreateUsers(newUser[1], newUser[2], newUser[3], newUser[4], newUser[5]);
                    Assert.True(userCreated);
                }
            }
        }

        [Fact]
        public void GroupDeleteUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for (int i = 0; i < bulkOperLines.Length; i++)
            {
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if (newUser[0] == "DeleteUser")
                {
                    bool userDeleted = UserManager.DeleteUser(newUser[1]);
                    Assert.True(userDeleted);
                }
            }

        }

        [Fact]
        public void GroupUpdateUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for (int i = 0; i < bulkOperLines.Length; i++)
            {
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if (newUser[0] == "UpdateUser")
                {
                    bool userUpdateRole = UserManager.UpdateRoleUser(newUser[1], newUser[2]);
                    Assert.True(userUpdateRole);
                }
            }

        }

        [Fact]
        public void GroupEnableUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for (int i = 0; i < bulkOperLines.Length; i++)
            {
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if (newUser[0] == "EnableUser")
                {
                    bool enableUser = UserManager.EnableUser(newUser[1]);
                    Assert.True(enableUser);
                }
            }

        }

        [Fact]
        public void GroupDisableUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for (int i = 0; i < bulkOperLines.Length; i++)
            {
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if (newUser[0] == "DisableUser")
                {
                    bool disableUser = UserManager.EnableUser(newUser[1]);
                    Assert.True(disableUser);
                }
            }
        }

    }
}
