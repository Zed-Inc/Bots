namespace Job
{
  public struct Job
  {
    public string time; // the time we want to post it at
    public string id; // the id of the post in the database

    public Job(string time, string id)
    {
      this.time = time;
      this.id = id;
    }
  }
}