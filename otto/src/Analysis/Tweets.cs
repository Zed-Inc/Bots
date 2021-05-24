using Tweetinvi;
using System;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Network;
using TradingWallet;
using Tweetinvi.Parameters;

namespace otto
{

  class Tweeter
  {
    private static string secret = "ovLTU32Cf93kdnlmSeXBHKIWRgQJ6XiajOqKL3opLVETMwLSW0";
    private static string key = "PvveBTxIUW2fPHijK8zpRKNUM";
    private static string token = "1110758685804945408-QQI77v31OFJDaMl8PxIKUnfwQbfpCW";
    private static string token_secret = "WsGVTXe6C3g2e4VQ9YKhYR6djR5Rf7vZ8DdA9FwHwCcip";
    public async Task<bool> Tweet()
    {


      var client = new TwitterClient(key, secret, token, token_secret);
      var user = await client.Users.GetAuthenticatedUserAsync();
      Network.Network.CallFinnhub(new Stock());
      Console.WriteLine($"User :: @{user}\n");

      while (true)
      {

        Console.Write("Twitter query >> ");
        var query = Console.ReadLine();
        if (query == "exit") { break; }
        var prod = await fetchTrending(client, query);
        foreach (var p in prod)
        {
          Console.WriteLine(p.Text);
          Console.WriteLine($"\n@{p.CreatedBy}     // {p.CreatedAt}" + "\n------------------------------\n");

        }
      }
      return true;
    }

    private async Task<ITweet[]> fetchTrending(TwitterClient client, string query)
    { // fetch the trending tweets description

      var tweets = await client.Search.SearchTweetsAsync(new SearchTweetsParameters(query)
      {
        Lang = LanguageFilter.English
      });
      return tweets;
    }

  }


  ///////////////////////////////////////




}