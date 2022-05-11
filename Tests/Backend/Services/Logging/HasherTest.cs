using System;
using System.Data.SqlClient;
using Xunit;
using Marvel.Services;
using Marvel.Services.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Services.Logging;

namespace Tests.Backend.Services.Logging
{
    public class HasherTest
    {
        [Fact]
        public void HashUsername()
        {
            List<string> usernames = new List<string> { "bnickle", "abrio", "dpatel", "atoscano", "stang", "jdelgado", "jcutri" };
            Hasher hasher = new Hasher();
            List<string> results = new List<string>();
            foreach (string username in usernames)
            {
                results.Add(hasher.HashUsername(username));
            }
            Assert.Equal(usernames.Count, results.Count);
        }
    }
}
