package main

import (
	"fmt"
	"os"
	"os/signal"
	"syscall"

	"github.com/bwmarrin/discordgo"
	"github.com/joho/godotenv"
)

func main() {
	godotenv.Load("tokens.env")
	token := os.Getenv("TOKEN")
	// Create a new Discord session using the provided bot token.
	dg, err := discordgo.New("Bot " + token)
	if err != nil {
		fmt.Println("error creating Discord session,", err)
		return
	}

	// Register the messageCreate func as a callback for MessageCreate events.
	dg.AddHandler(messageCreate)

	// In this example, we only care about receiving message events.
	dg.Identify.Intents = discordgo.MakeIntent(discordgo.IntentsGuildMessages)

	// Open a websocket connection to Discord and begin listening.
	err = dg.Open()
	if err != nil {
		fmt.Println("error opening connection,", err)
		return
	}

	// Wait here until CTRL-C or other term signal is received.
	fmt.Println("Bot is now running.  Press CTRL-C to exit.")
	sc := make(chan os.Signal, 1)
	signal.Notify(sc, syscall.SIGINT, syscall.SIGTERM, os.Interrupt, os.Kill)
	<-sc

	// Cleanly close down the Discord session.
	dg.Close()
}

// This function will be called (due to AddHandler above) every time a new
// message is created on any channel that the authenticated bot has access to.
func messageCreate(s *discordgo.Session, m *discordgo.MessageCreate) {

	// Ignore all messages created by the bot itself
	// This isn't required in this specific example but it's a good practice.
	// if m.Author.ID == s.State.User.ID {
	// 	return
	// }

	if m.Content == "!pic" {
		dog := MakeRequest()
		fmt.Print("|	url -> ", dog.Url, "\n\n")
		// if the url is an mp4 then we just want to return
		dataType := dog.Url[len(dog.Url)-4:]
		validTypes := []string{".jpg", ".png"}
		if mayContain(dataType, validTypes) {
			imageName := FetchImage(dog.Url)
			file, err := os.Open(imageName)
			defer file.Close()
			if err != nil {
				fmt.Println("failed to open file for reading -> ", err)
				s.ChannelMessageSend(m.ChannelID, "failed to send image :(")
				s.ChannelMessageSend(m.ChannelID, "!pic")
				return
			}
			s.ChannelFileSend(m.ChannelID, dog.Url, file)
			s.ChannelMessageSend(m.ChannelID, "!pic")
		} else {
			s.ChannelMessageSend(m.ChannelID, "could not load an image, try sending the command again")
			s.ChannelMessageSend(m.ChannelID, "!pic")
		}
	}

}

func mayContain(d string, s []string) bool {
	for i := 0; i < len(s); i++ {
		if d == s[i] {
			return true
		}
	}
	return false
}
