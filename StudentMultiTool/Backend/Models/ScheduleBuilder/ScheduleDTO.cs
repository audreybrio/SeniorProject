namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public List<ScheduleItemDTO> Items { get; set; }
        public DateTime Modified { get; set; }

        public ScheduleDTO(
            int ScheduleId,
            List<ScheduleItemDTO> Items,
            DateTime Modified
            )
        {
            this.ScheduleId = ScheduleId;
            this.Items = Items;
            this.Modified = Modified;
        }
    }
}
