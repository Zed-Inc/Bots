package main

import (
	"fmt"

	"github.com/bwmarrin/discordgo"
)

// https://github.com/bwmarrin/discordgo/blob/master/examples/pingpong/main.go
var (
	clientID     = "779966919733542952"
	clientSecret = "yrre588AZavqZmdCr35GUp52jJcELZju"
)

func main() {
	discord, err := discordgo.New("Bot " + clientSecret)
	// handle discord client creation failure
	if err != nil {
		fmt.Println("Failed to initalize a discord client -> ", err)
		return
	}

	discord.AddHandler(messageCreate)

}

func messageCreate(s *discordgo.Session, m *discordgo.Message) {

}
