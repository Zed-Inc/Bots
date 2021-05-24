using System;

namespace TradingWallet
{

  // defines the data for a stock
  public struct Stock
  {
    public string companyName { get; }
    public string stockName { get; }
    public float value { get; }

    public Stock(string company, string s_name, float val)
    {
      this.companyName = company;
      this.stockName = s_name;
      this.value = val;
    }
  }


  class Wallet
  {

    public float amount { get; private set; }
    public float starting_amount { get; private set; }

    public Wallet(float startingAmount)
    {
      this.starting_amount = startingAmount;
      this.amount = startingAmount;
      Console.WriteLine($"Starting with ${this.amount}, this is paper money remeber");
    }



    // make a trade on a stock
    public void makeTrade(Stock stock, float amount)
    {
      Console.WriteLine($":: Making a trade on {stock.companyName}({stock.stockName}) @{amount} stocks          ");
    }


  }


}