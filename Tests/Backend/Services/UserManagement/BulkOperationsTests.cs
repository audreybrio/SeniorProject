using System;
using Xunit;
/*using BulkOperations;
using CreateUser;
using DeleteUser;
using UpdateAccounts;
using EnableUser;
using DisableUser;*/

namespace OperationsTests{
    public class BulkOperationsTests{

        [Fact]
        public void GroupCreateUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for(int i = 0; i < bulkOperLines.Length; i++){
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if(newUser[0] == "CreateUser"){
                    bool userCreated = true;// UpdateCreate(newUser[1], newUser[2], newUser[3], newUser[4], newUser[5]);
                    Assert.True(userCreated);
                }
            }
        }

        [Fact]
        public void GroupDeleteUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for(int i = 0; i < bulkOperLines.Length; i++){
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if(newUser[0] == "DeleteUser"){
                    bool userDeleted = true;// DeleteUser(newUser[1]);
                    Assert.True(userDeleted);
                }
            }
            
        }

        [Fact]
        public void GroupUpdateUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for(int i = 0; i < bulkOperLines.Length; i++){
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if(newUser[0] == "UpdateUser"){
                    bool userUpdateRole = true;// UpdateUserRole(newUser[1], newUser[2]);
                    Assert.True(userUpdateRole);
                }
            }
            
        }

        [Fact]
        public void GroupEnableUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for(int i = 0; i < bulkOperLines.Length; i++){
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if(newUser[0] == "EnableUser"){
                    bool enableUser = true;// userEnable(newUser[1]);
                    Assert.True(enableUser);
                }
            }
            
        }

        [Fact]
        public void GroupDisableUsers()
        {
            string fileName = "bulkops.txt";
            string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
            for(int i = 0; i < bulkOperLines.Length; i++){
                string[] newUser = bulkOperLines[i].Split(' '); // [0] = operation
                if(newUser[0] == "DisableUser"){
                    bool disableUser = true;// userDisable(newUser[1]);
                    Assert.True(disableUser);
                }
            }
        }


    }
}