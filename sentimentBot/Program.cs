using System.Threading.Tasks;
using System;
using DSharpPlus;
using Bot;
using Twitter;
using Tweetinvi;

namespace sentimentBot
{
  class Program
  {
    private static Tweeter tweets = new Tweeter();
    static void Main(string[] args)
    {
      Console.WriteLine("==> Initalizing bot");
      MainAsync().GetAwaiter().GetResult();
    }
    static async Task MainAsync()
    {
      Sentiment sentiment = new Sentiment();



      var discord = new DiscordClient(new DiscordConfiguration()
      {
        Token = "Nzc5OTY2OTE5NzMzNTQyOTUy.X7oOzA.MmmhqgeJykXvsk5F-eKNSZgN-9k",
        TokenType = TokenType.Bot
      });
      Console.WriteLine("==> Bot is running");
      discord.MessageCreated += async (e) =>
                 {
                   if (e.Message.Content.ToLower().StartsWith("ping"))
                     await e.Message.RespondAsync("pong!");
                   // checks wether the user who sent the message is a bot
                   if (e.Message.Content.ToLower().StartsWith("!s"))
                   {
                     string msg = sentiment.predictSentiment(e.Message.Content.Replace("!s", ""));

                     await e.Message.RespondAsync(msg);
                   }
                   if (e.Message.Content.ToLower().StartsWith("!analyze"))
                   {
                     string msg = e.Message.Content.Replace("!analyze", ""); // remove the bang from the query
                     Console.WriteLine($"{msg}");
                     var resp = await tweets.AnalyzeTweets(msg, sentiment);
                     Console.WriteLine("===============================\n\n");
                     Console.WriteLine(resp);

                     await e.Message.RespondAsync($"{resp}");

                   }



                   if (e.Message.Content.ToLower().StartsWith("!user"))
                   {
                     string msg = e.Message.Content.Replace("!user", ""); // remove the bang from the query
                     msg = msg.Replace("@", ""); // removing the "@" symbol if that was passed in
                     Console.WriteLine($"{msg}");
                     var resp = await tweets.analyzeUser(msg, sentiment);
                     Console.WriteLine("===============================\n\n");
                     Console.WriteLine(resp);

                     await e.Message.RespondAsync($"{resp}");

                   }





                   if (e.Message.Content.ToLower().StartsWith("!help"))
                   {
                     var resp = "`!help -- brings you to this\n!analyze {topic} -- will search twitter and return the general sentiment on that topic\n!user @{username} -- will analyze tweets a user has posted and return a sentiment about them\n!s {sentence} -- will analyze the sentence passed in`";
                     await e.Message.RespondAsync($"{resp}");
                   }

                 };

      await discord.ConnectAsync();
      await Task.Delay(-1);
    }



  }
}
