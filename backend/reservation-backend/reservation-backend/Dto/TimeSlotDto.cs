namespace reservation_backend.Models;

public class TimeSlotDto
{
    public string Start { get; set; }
    public string End { get; set; }
    public string FullSpan { get; set; }

    public TimeSlotDto(string start, string end)
    {
        Start = start;
        End = end;
        FullSpan = start + " - " + end;
    }

    public TimeSlotDto(TimeSlotSpan t)
    {
        Start = t.Start;
        End = t.End;
        FullSpan = t.Start + " - " + t.End;
    }
}