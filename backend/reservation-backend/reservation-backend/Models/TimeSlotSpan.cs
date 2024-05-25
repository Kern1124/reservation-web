namespace reservation_backend.Models;

public class TimeSlotSpan
{
    public int Id { get; set; }
    public int OfferedServiceId { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public TimeSlotSpan(string start, string end)
    {
        Start = start;
        End = end;
    }

    public TimeSlotSpan(TimeSlotDto dto)
    {
        Start = dto.Start;
        End = dto.End;
    }
}