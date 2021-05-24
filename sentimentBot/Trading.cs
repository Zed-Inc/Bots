using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;


namespace Trading
{

  struct Stock
  {
    public string companyname { get; set; }
    public string stockName { get; set; }
    public float value { get; set; }
    public float owned { get; set; } // the amont of this stock we own!
  }


  class Wallet
  {
    public float starting_amount { get; private set; } // this is a reference to the amount we started with
    public List<Stock> holdingStocks { get; private set; }
    public float amount { get; private set; } // this is the wallets actual amount
    public Wallet(float startAmount)
    {
      this.starting_amount = startAmount;
      this.amount = startAmount;
      Console.WriteLine("==> Initalized wallet");
    }


    // subtracts the cost from the wallets amount
    public void tradeMade(float cost)
    {
      this.amount -= cost;
    }


  }




  // this class will handle the networking for any trades
  class Trader
  {
    // returns true when the trade was successful
    public async Task<bool> Trade(Stock stock, float amount)
    {
      return true;
    }
  }
}