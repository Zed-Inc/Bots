
using System;
using Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;


namespace Stocks
{
  class Stocks
  {
    private string key = "M13EMXKHGE6OEFN2";
    private AlphaVantageClient client;
    private StocksClient stocksClient;

    public Stocks()
    {
      this.client = new AlphaVantageClient(this.key);
      this.stocksClient = this.client.Stocks();
    }

    public async Task<bool> fetchStocks()
    {
      StockTimeSeries stockTs = await this.stocksClient.GetTimeSeriesAsync("AAPL", Interval.Daily, OutputSize.Compact, isAdjusted: true);

      GlobalQuote globalQuote = await this.stocksClient.GetGlobalQuoteAsync("AAPL");

      ICollection<SymbolSearchMatch> searchMatches = await this.stocksClient.SearchSymbolAsync("BA");
      Console.WriteLine($"opening: {globalQuote.OpeningPrice}\nprice: {globalQuote.Price}");
      return true;
    }


  }
}

