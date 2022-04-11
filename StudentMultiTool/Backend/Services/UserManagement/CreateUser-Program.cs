using System;
using System.Data.SqlClient;

namespace CreateUser
{
    class CreateUserProgram
    {
        public static bool CreateUsers(string username, string password)
        {

            Console.WriteLine($"Do you want to create an account?");
            string confirm = Console.ReadLine();
            if (confirm.Equals("yes") || confirm.Equals("y") || confirm.Equals("Y") || confirm.Equals("Yes") || confirm.Equals("YES"))
            {
                Console.WriteLine($"Your Username must be a minimum of 5 characters, include: a-z, 0-9, and/or .,@! ");
                Console.WriteLine($"Your password must be a minimum of 8 characters, including: a-z, A-Z, and/or 0-9 ");
                Console.WriteLine($"Create a username: ");
                string confirm1 = Console.ReadLine();
                string[] username1 = { "", "", "", "", "" };

                foreach (string username2 in username1)
                {
                    bool b = ValidateUserName(username);

                }
                string[] password1 = { "", "", "", "", "", "", "", "" };
                foreach (string password2 in password1)
                {
                    bool b = ValidatePassword(password);

                }

               

                static bool ValidateUserName(string username)
                {
                    int validCondition = 0;
                    foreach (char c in username)
                    {
                        if (c >= 'a' && c <= 'z')
                        {
                            validCondition++;
                            break;
                        }
                    }
                    if (validCondition == 0)
                        return false;
                    foreach (char c in username)
                    {
                        if (c >= '0' && c <= '9')
                        {
                            validCondition++;
                            break;
                        }
                    }
                    if (validCondition == 1)
                        return false;
                    if (validCondition == 2)
                    {
                        char[] special = { '.', ',', '@', '!' };
                        if (username.IndexOfAny(special) == -1)
                            return false;
                    }
                    return true;
                }

                static bool ValidatePassword(string password)
                {
                    int validCondition = 0;
                    foreach (char c in password)
                    {
                        if (c >= 'a' && c <= 'z')
                        {
                            validCondition++;
                            break;
                        }
                    }
                    foreach (char c in password)
                    {
                        if (c >= 'A' && c <= 'Z')
                        {
                            validCondition++;
                            break;
                        }
                    }
                    if (validCondition == 0)
                        return false;
                    foreach (char c in password)
                    {
                        if (c >= '0' && c <= '9')
                        {
                            validCondition++;
                            break;
                        }
                    }
                    if (validCondition == 1)
                        return false;
                    if (validCondition == 2)
                    {
                        char[] special = { '.', ',', '@', '!' };
                        if (password.IndexOfAny(special) == -1)
                            return false;
                    }
                    return true;
                }
            }
            return true;
        }

    }

}
