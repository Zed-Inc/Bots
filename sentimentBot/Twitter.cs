using Tweetinvi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Bot;

namespace Twitter
{

  class Tweeter
  {
    private static string secret = "ovLTU32Cf93kdnlmSeXBHKIWRgQJ6XiajOqKL3opLVETMwLSW0";
    private static string key = "PvveBTxIUW2fPHijK8zpRKNUM";
    private static string token = "1110758685804945408-QQI77v31OFJDaMl8PxIKUnfwQbfpCW";
    private static string token_secret = "WsGVTXe6C3g2e4VQ9YKhYR6djR5Rf7vZ8DdA9FwHwCcip";
    // private IAuthenticatedUser user;
    private TwitterClient client = new TwitterClient(key, secret, token, token_secret);





    // Console.WriteLine($"User :: @{user}\n");

    // while (true)
    // {

    //   Console.Write("Twitter query >> ");
    //   var query = Console.ReadLine();
    //   if (query == "exit") { break; }
    //   var prod = await fetchTweets(client, query);
    //   foreach (var p in prod)
    //   {
    //     Console.WriteLine(p.Text);
    //     Console.WriteLine($"\n@{p.CreatedBy}     // {p.CreatedAt}" + "\n------------------------------\n");

    //   }
    // }


    // public async Task<bool> setup()
    // {
    //   this.user = await this.client.Users.GetAuthenticatedUserAsync();
    //   return true;
    // }

    public async Task<string> AnalyzeTweets(string query, Sentiment sent)
    {
      string response = "";
      var sentiments = new List<float> { }; // create an empty list of values
      Console.WriteLine("Fetching tweets");
      var tweets = await fetchTweets(query);
      Console.WriteLine("Fetched");

      var start = DateTime.Now;
      Console.WriteLine("analyzing tweets");
      foreach (var t in tweets)
      {
        var value = sent.predictSentiment(t.Text, 0);
        sentiments.Add(value);
        // response += $"`{t.Text}\n@{t.CreatedBy.Name} {t.CreatedAt} :: sentiment {value}`\n--\n";
        Console.WriteLine(t.Text + "\n\n");
      }
      Console.WriteLine("analyzed");
      response += $"I have analyzed {tweets.Length} tweets\n";

      float consensus = 0.0f;
      foreach (var l in sentiments)
      {
        consensus += l;
      }

      consensus = consensus / sentiments.Count;
      Console.WriteLine($"general sentiment >> {consensus}\n");
      response += AverageConsensus(consensus);
      response += $"\ngeneral sentiment consensus >> {consensus}";
      var end = DateTime.Now;
      Console.WriteLine($"== Time taken {end - start}");
      Console.WriteLine("finished");
      return response;
    }

    public async Task<string> analyzeUser(string query, Sentiment sent)
    {
      var response = "";
      Console.WriteLine($"\n\n\n\n\n\n-- Fetching @{query} tweets");
      var user = await this.client.Users.GetUserAsync(query);
      Console.WriteLine($"{user.ScreenName} -- {user.Name}");
      var tweets = await user.GetUserTimelineAsync();


      var sentiments = new List<float> { }; // create an empty list of values
      Console.WriteLine("-- Fetched");
      foreach (var t in tweets)
      {
        var value = sent.predictSentiment(t.Text, 0);
        sentiments.Add(value);
        Console.WriteLine(t.Text + $"\nsentiment @{value}\n");
      }
      Console.WriteLine("analyzed");
      response += $"I have analyzed {tweets.Length} tweets\n";

      float consensus = 0.0f;
      foreach (var i in sentiments)
      {
        consensus += i;
      }

      consensus = consensus / sentiments.Count;
      response += AverageConsensus(consensus);
      Console.WriteLine($"general sentiment >> {consensus}\n");

      response += $"\ngeneral sentiment consensus >> {consensus}";
      var end = DateTime.Now;
      Console.WriteLine("finished");
      return response;
    }

    private string AverageConsensus(float consensus)
    {
      var response = "";
      switch (consensus)
      {
        case float p when p > .7:
          response += $"this topic is generally positive";
          break;

        case float p when p >= 0.5 && p <= .7:
          response += $"this topic is generally neutral";
          break;

        case float p when p < 0.5:
          response += $"this topic is generally negative";
          break;
        default:
          break;
      }
      return response;
    }

    private async Task<ITweet[]> fetchTweets(string query)
    { // fetch the trending tweets description

      var tweets = await this.client.Search.SearchTweetsAsync(new SearchTweetsParameters(query)
      {
        Lang = LanguageFilter.English
      });
      return tweets;
    }

  }


  ///////////////////////////////////////
}
