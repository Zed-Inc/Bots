using System;
using System.Threading.Tasks;
using DSharpPlus;
namespace cs_bot
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("==> Initalizing bot");
      MainAsync().GetAwaiter().GetResult();
    }
    static async Task MainAsync()
    {
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

                   if (e.Message.Content.ToLower().StartsWith("status"))
                   {
                     var status = e.Author.Presence.Game.ToString();
                     Console.WriteLine(status);
                     await e.Message.RespondAsync("no!");
                   }

                 };




      await discord.ConnectAsync();
      await Task.Delay(-1);
    }
  }
}
