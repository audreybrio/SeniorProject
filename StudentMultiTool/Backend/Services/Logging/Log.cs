using System;

namespace StudentMultiTool.Backend.Services.Logging
{
	public class Log
	{
		public DateTime timestamp { get; }
		public string category { get; }
		public string level { get; }
		public int user { get; }
		public string description { get; }

		// Constructor. If timestamp is null, then the assumption is made that a new log is being constructed.
		// The timestamp will be immediately set to the current time (UTC). Alternatively, a value can be
		// passed in, in the case of a log that is being read from the datastore.
		public Log(string category, string level, int user, string description, DateTime? timestamp = null)
		{
			this.timestamp = DateTime.UtcNow;
			if (timestamp.HasValue)
            {
				this.timestamp = (DateTime)timestamp;
            }
			this.category = category;
			this.level = level;
			this.user = user;
			this.description = description;
		}

		// Reference: https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring?view=net-5.0
        public override string ToString()
        {
			string delimiter = " | ";
			return this.timestamp.ToString() + delimiter + this.category + delimiter + 
				   this.level + delimiter + this.user + delimiter + this.description;
        }

		// Reference: https://docs.microsoft.com/en-us/dotnet/api/system.object.equals?view=net-5.0
        public override bool Equals(object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
				return false;
            }
			Log other = (Log) obj;
			return other.timestamp == this.timestamp &&
				   other.category == this.category &&
				   other.level == this.level &&
				   other.user == this.user &&
				   other.description == this.description;
        }
    }
}