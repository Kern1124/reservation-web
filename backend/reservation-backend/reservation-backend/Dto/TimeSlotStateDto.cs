namespace reservation_backend.Models;

public class TimeSlotStateDto
{
  public string TimeSlot { get; set; }
  public string Start { get; set; }
  public string End { get; set; }
  public bool Available { get; set; }
  public int? ReservedById { get; set; }

  public TimeSlotStateDto(string start, string end, bool available, int? reservedById)
  {
    ReservedById = reservedById;
    TimeSlot = start + " - " + end;
    Start = start;
    End = end;
    Available = available;
  }
}