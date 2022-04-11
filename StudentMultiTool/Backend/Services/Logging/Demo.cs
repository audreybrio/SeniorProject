using System;
using StudentMultiTool.Backend.Services.Logging;

namespace StudentMultiTool.Backend.Services.Logging
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