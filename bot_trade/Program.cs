using System;
using Wallet;
using Stocks;
using System.Threading.Tasks;

namespace bot_trade
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("==> Trading bot");
      Start().GetAwaiter().GetResult(); // start the program
    }

    static async Task Start()
    {
      var wallet = new Wallet.Wallet(100.0);
      var s = new Stocks.Stocks();
      await s.fetchStocks();

    }

  }
}
