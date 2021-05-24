using System;
using System.Threading.Tasks;
using System.Net.Http;
using TradingWallet;
namespace Network
{

  class Network
  {
    private static string finnhub_base = "";
    private static HttpClient shared = new HttpClient();

    public static async Task<string> CallFinnhub(Stock stock)
    {
      Console.WriteLine(":: Making a network call");
      try
      {
        var contents = await shared.GetStringAsync("http://'https://finnhub.io/api/v1/quote?symbol=AAPL&token=c0187k748v6sc26qu0c0");
        Console.WriteLine(contents);

      }
      catch (System.Exception)
      {
        Console.WriteLine("!! ERROR failed network call");
        return "";
      }

      return "";
    }
  }


}