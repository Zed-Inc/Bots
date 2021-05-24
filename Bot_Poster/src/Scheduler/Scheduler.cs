using System.Collections.Generic;

namespace Scheduler
{
  class Scheduler
  {
    List<Job.Job> jobs;

    // adds a new job to the array of jobs
    public void AddJob(string time, string id)
    {
      var newJob = new Job.Job(time, id);
      this.jobs.Add(newJob);
    }

  }
}