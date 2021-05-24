using System;
using Microsoft.ML;
using Microsoft.ML.Data;



namespace Bot
{
  class Sentiment
  {


    private PredictionEngine<SentimentData, SentimentPrediction> predictionEngine;


    private MLContext context;
    public Sentiment()
    {
      this.context = new MLContext();
      // load in the data from the csv files
      var reddit = this.context.Data.LoadFromTextFile<SentimentData>(
        "data/Reddit_Data.csv", hasHeader: true, separatorChar: ','
      );

      var twitter = this.context.Data.LoadFromTextFile<SentimentData>(
              "data/Twitter_Data.csv", hasHeader: true, separatorChar: ','
      );
      var stock = this.context.Data.LoadFromTextFile<SentimentData>(
                 "data/stock_data.csv", hasHeader: true, separatorChar: ','
         );


      // do some transformations
      var pipeline = context.Transforms.Expression("Label", "(x) => x == 1 ? true : false", "category")
      .Append(context.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.clean_comment)))
      .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());

      // fit the loaded in data to the model
      var model = pipeline.Fit(reddit);
      model.Append(pipeline.Fit(twitter));
      model.Append(pipeline.Fit(stock));

      Console.WriteLine("-- Sentiment model has been fit");
      this.predictionEngine = context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

    }


    public string predictSentiment(string message)
    {
      var prediction = this.predictionEngine.Predict(new SentimentData { clean_comment = message });

      // return $"Prediction -> {prediction.Prediction}\nprobability -> {prediction.Probability}\nScore -> {prediction.Score}";
      Console.WriteLine("==> sentiment predicted " + (prediction.Probability));
      switch (prediction.Probability)
      {
        case float p when p > .7:
          return $"positive message :: {p}";

        case float p when p >= 0.5 && p <= .7:
          return $"neutral message :: {p}";

        case float p when p < 0.5:
          return $"negative message :: {p}";

        default:
          break;
      }
      return "";

    }
    public float predictSentiment(string message, int nothing)
    {
      var prediction = this.predictionEngine.Predict(new SentimentData { clean_comment = message });
      return prediction.Probability;
    }

  }

  internal class SentimentData
  {
    [LoadColumn(0)]
    public string clean_comment { get; set; }
    [LoadColumn(1)]
    public float category { get; set; }

  }

  internal class SentimentPrediction
  {
    [ColumnName("PredictionLabel")]
    public bool Prediction { get; set; }
    public float Probability { get; set; }
    public float Score { get; set; }
  }
}