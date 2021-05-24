using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ScheduleModel
{
  public class Schedule
  {

    [Key]
    public string postID { get; set; }

    public string dateAdded { get; set; }
  }
}