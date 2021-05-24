using System.Data.SQLite;
using System;
using System.Linq;
// using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


using PostModel;


namespace DatabaseInterface
{
  public class Database
  {

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // => options.UseSqlite("Data Source=../sql_database/post.db");



    public SQLiteConnection connection;

    // public DbSet<Post> Posts { get; set; }
    private List<Post> posts;


    public Database()
    {
      this.posts = new List<Post>();
      try
      {
        var testconn = new SQLiteConnection("Data Source=../sql_database/post.db");
        this.connection = testconn.OpenAndReturn();
      }
      catch (SQLiteException e)
      {
        Console.WriteLine(e);
      }



    }


    // Data getters
    public Post FetchPost(string id)
    {
      var foundPost = this.posts.Where(p => p.postID == id);
      return foundPost.First();
    }

    public void CheckForNewPosts()
    {

    }


  }
}