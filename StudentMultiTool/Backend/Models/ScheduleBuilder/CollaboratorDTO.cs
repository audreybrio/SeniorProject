namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    public class CollaboratorDTO
    {
        public int Schedule { get; set; }
        public string Username { get; set; }
        public bool CanWrite { get; set; }
        public bool IsOwner { get; set; }
        public CollaboratorDTO()
        {
        }
        public CollaboratorDTO(int Schedule, string Username, bool CanWrite, bool IsOwner)
        {
            this.Schedule = Schedule;
            this.Username = Username;
            this.CanWrite = CanWrite;
            this.IsOwner = IsOwner;
        }
    }
}
