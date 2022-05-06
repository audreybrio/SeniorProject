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
    }
}
