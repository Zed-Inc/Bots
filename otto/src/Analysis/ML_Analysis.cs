using Microsoft.ML;



namespace SentimentAnalysis
{
  class Sentiment
  {
    private MLContext context;
    public Sentiment()
    {
      this.context = new MLContext();
    }


    public int analyze(string text)
    {


      return 1;
    }
  }
}