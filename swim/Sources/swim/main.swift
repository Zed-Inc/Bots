struct swim {
    var text = "Hello, World!"
}


import Sword
import Foundation


main() // MARK: Start program execution


func main() {
    let bot = Sword(token: "Nzc5OTY2OTE5NzMzNTQyOTUy.X7oOzA.MmmhqgeJykXvsk5F-eKNSZgN-9k")
//    bot.editStatus(to: "online", playing: "with RTP_announcer & aitr")
    bot.editStatus(to: "online", playing: "with some bytes")
    print(">>> Bot is now online")
    bot.on(.messageCreate) { data in
      let msg = data as! Message

      if msg.content == "!ping" {
        print("recived message from :: \(msg.author?.username ?? "could not get author!!")")
        msg.reply(with: "Pong!")
      }
      else if msg.content.contains("!insult") {
        insultSomeone(completion: { i in
            var mentions = ""
            if msg.mentions.count > 0 {
                for usr in msg.mentions {
                    mentions.append("<@\(usr.id)> ")
                }
            }
            print(">>> someone was just insulted")
            
            msg.reply(with: mentions + i)
        })
      }
      else if msg.content == "!advice" {
        print(">>> someone wants advice")
        fetchAdvice { (yes) in
            msg.reply(with: yes)
        }
      }
      
      else if msg.content == "!fuckyouall" {
        if msg.author?.username == "Zed" {
            insultSomeone { (insult) in
                msg.reply(with: "@everyone\n\(insult)")
            }
        } else {
            msg.reply(with: "fuck you i dont answer to anyone but Zed")
        }
      }
      
      else if msg.content == "!help" {
        msg.reply(with: "we dont do that around here")
      }
    
      else if msg.content == "!joke" {
        print(">>> someone wants a joke")
        fetchJoke(completion:{ joke in
            print(">>> joke fetched")
            msg.reply(with: joke)
            
        })
      }
        
      else if msg.content == "!trivia" {
        print(">>> someone wants a trivia question")
        fetchTriviaQuestion(completion: { (q,a) in
            msg.reply(with: "\(q)\n||\(a)||")
            
        })
      }
      

      
        
        
        
    }
    
    
    

    bot.connect()
}

