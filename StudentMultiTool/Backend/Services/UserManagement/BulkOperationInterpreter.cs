using StudentMultiTool.Backend.DAL;
using UserAcc;

namespace StudentMultiTool.Backend.Services.UserManagement
{
    public class BulkOperationInterpreter
    {
        public string Success { get; } = "Success";
        public FileValidator FileValidator { get; set; }
        public string CreateUser { get; } = "create";
        public string DeleteUser { get; } = "delete";
        public string UpdateUser { get; } = "update";
        public string EnableUser { get; } = "enable";
        public string DisableUser { get; } = "disable";
        public string IgnoreField { get; } = "-";
        public int MaxOperations { get; } = 10000;
        public BulkOperationInterpreter()
        {
            FileValidator = new FileValidator();
        }

        public string Execute(string filePath)
        {
            if (!FileValidator.IsBulkOperationFile(filePath))
            {
                return "Not a valid bulk operation file";
            }
            UserAccountDAO uad = new UserAccountDAO();
            string[] commands = File.ReadAllLines(filePath);
            List<string> operations = new List<string> { this.CreateUser, this.UpdateUser, this.DeleteUser, this.EnableUser, this.DisableUser };
            List<int> arguments = new List<int> { 4, 5, 3, 2, 2 };
            //Dictionary<string, int> operations = new Dictionary<string, int> { { CreateUser, 5 },  };
            int executed = 0;
            for (int i = 0; i < commands.Length && i < MaxOperations; i++)
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(commands[i]))
                {
                    string[] current = commands[i].Split(' '); // [0] = operation
                    string operation = current[0].ToLower();
                    if (operations.Contains(operation) && current.Length >= 2)
                    {
                        string username = current[1];

                        // If there's nothing in the username argument, then this command was malformed
                        if (!string.IsNullOrEmpty(username))
                        {
                            UserAccount? user = uad.SelectSingle(username);

                            // If the user with the given username is null, then we can create it
                            if (user == null)
                            {
                                if (operation.Equals(CreateUser))
                                {
                                    user = new UserAccount();
                                    user.Email = string.Empty;
                                    try
                                    {
                                        user.Username = username; // required
                                        //user.Email = current[2]; // required
                                        //user.Role = current[3]; // required
                                        //user.Passcode = current[4]; // required
                                        //user.Name = current[5];
                                        //user.School = current[6];
                                        user.Email = !string.IsNullOrEmpty(current[2]) ? current[2] : string.Empty; // required
                                        user.Role = !string.IsNullOrEmpty(current[3]) ? current[3] : string.Empty; // required
                                        user.Passcode = !string.IsNullOrEmpty(current[4]) ? current[4] : string.Empty; // required
                                        user.Name = !string.IsNullOrEmpty(current[5]) ? current[5] : string.Empty;
                                        user.School = !string.IsNullOrEmpty(current[6]) ? current[6] : string.Empty;
                                        user.Active = "true".Equals(current[7]) ? true : false;
                                    }
                                    catch (Exception ex)
                                    {
                                        // If some of the fields are missing, that's okay due to the default values
                                        // in the constructor
                                    }
                                    // TODO: create the damn user
                                    result = uad.InsertUser(user);
                                    if (result.Equals(uad.Success))
                                    {
                                        executed++;
                                    }
                                }
                            }

                            // If there was a user with the given username, then we can update/delete/enable/disable
                            else
                            {
                                // Update the user
                                if (operation.Equals(UpdateUser))
                                {
                                    try
                                    {
                                        // required; this is the new username of the user current[1]
                                        user.Username = (!string.IsNullOrEmpty(current[2]) && !IgnoreField.Equals(current[2])) ?
                                            current[2] : user.Username;
                                        user.Email = (!string.IsNullOrEmpty(current[3]) && !IgnoreField.Equals(current[3])) ?
                                            current[3] : user.Email; // required
                                        user.Role = (!string.IsNullOrEmpty(current[4]) && !IgnoreField.Equals(current[4])) ?
                                            current[4] : user.Role; // required
                                        user.Passcode = (!string.IsNullOrEmpty(current[5]) && !IgnoreField.Equals(current[5])) ?
                                            current[5] : user.Passcode; // required
                                        user.Name = (!string.IsNullOrEmpty(current[6]) && !IgnoreField.Equals(current[6])) ?
                                            current[6] : user.Name;
                                        user.School = (!string.IsNullOrEmpty(current[7]) && !IgnoreField.Equals(current[7])) ?
                                            current[7] : user.School;
                                        // users can't be enabled or disabled through this operation, they have to explicitly be
                                        // enabled or disabled due to the check on the number of admins when disabling users
                                    }
                                    catch (Exception ex)
                                    {
                                        // If some of the fields are missing, that's okay due to the default values
                                        // in the constructor
                                    }
                                    result = uad.UpdateSingle(user);
                                    if (result.Equals(uad.Success))
                                    {
                                        executed++;
                                    }
                                }

                                // Delete the user
                                if (operation.Equals(DeleteUser))
                                {
                                    bool canDelete = false;
                                    if ("admin".Equals(user.role))
                                    {
                                        int numAdmins = uad.CountAdmins();
                                        if (numAdmins - 1 >= 1)
                                        {
                                            canDelete = true;
                                        }
                                    }
                                    else
                                    {
                                        canDelete = true;
                                    }
                                    if (canDelete)
                                    {
                                        result = uad.DeleteSingle(user);
                                        if (result.Equals(uad.Success))
                                        {
                                            executed++;
                                        }
                                    }
                                }

                                // Enable the user
                                if (operation.Equals(EnableUser))
                                {
                                    user.Active = true;
                                    result = uad.UpdateSingle(user);
                                    if (result.Equals(uad.Success))
                                    {
                                        executed++;
                                    }
                                }

                                // Disable the user
                                if (operation.Equals(DisableUser))
                                {
                                    bool canDisable = false;
                                    if ("admin".Equals(user.role))
                                    {
                                        int numAdmins = uad.CountAdmins();
                                        if (numAdmins - 1 >= 1)
                                        {
                                            canDisable = true;
                                        }
                                    }
                                    else
                                    {
                                        canDisable = true;
                                    }
                                    if (canDisable)
                                    {
                                        user.Active = false;
                                        result = uad.UpdateSingle(user);
                                        if (result.Equals(uad.Success))
                                        {
                                            executed++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // go to the next line
            }
            Console.WriteLine("Executed " + executed + " / " + commands.Length + " operations");
            return Success;
        }
    }
}
