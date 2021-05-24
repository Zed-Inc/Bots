using System.Linq;
// using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace PostModel
{
  public class Post
  {
    [Key]
    public string postID { get; set; }
    public bool pulled { get; set; }
    public string link { get; set; }
    public List<string> boards { get; }

    //TODO Update this variable, image is stored as a bitmap in the database
    public string image { get; set; }
    public string desc { get; set; }
  }
}


