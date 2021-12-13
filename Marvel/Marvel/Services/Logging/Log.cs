using System;

namespace Marvel.Services.Logging
{
	public class Log
	{
		public DateTime timestamp { get; }
		public string category { get; }
		public string level { get; }
		public string user { get; }
		public string description { get; }

		// Constructor. If timestamp is null, then the assumption is made that a new log is being constructed.
		// The timestamp will be immediately set to the current time (UTC). Alternatively, a value can be
		// passed in, in the case of a log that is being read from the datastore.
		public Log(string category, string level, string user, string description, DateTime? timestamp = null)
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
        public override string ToString()
        {
			string delimiter = " | ";
			return this.timestamp.ToString() + delimiter + this.category + delimiter + 
				   this.level + delimiter + this.user + delimiter + this.description;
        }
	}
}