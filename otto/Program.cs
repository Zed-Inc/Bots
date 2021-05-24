using System;
using Tweetinvi;
using System.Threading.Tasks;


namespace otto
{


  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("<== OTTO ==>");
      var tweet = new Tweeter();
      await tweet.Tweet();



    }
  }
}

