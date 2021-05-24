package main

import (
	"fmt"
	"time"

	"github.com/go-co-op/gocron"
)

func init() {
	//TODO connect with databse
}

func main() {
	fmt.Print("\nBopt\n\n")

	cron := gocron.NewScheduler(time.Local)
	//every 15 minutes we want to query the db for new items
	cron.Every(30).Seconds().Do(test)
	// cron.Every(15).Minutes().Do(test)
	cron.StartBlocking()

}
