using System;
using Marvel.Services.Logging;

namespace Marvel.Services.Logging.Demo
{
    public class Demo
    {
        public void demo()
        {
            Console.WriteLine("Logging Demo");
            DbLogWriter logWriter = new DbLogWriter();
            _ = logWriter.AddLog("Demo", "Demo", 2, "Logging demo!");
        }
    }
}