package scheduler

import (
	"bopt/src/deployer"
	"time"

	"github.com/go-co-op/gocron"
)

var cron = gocron.NewScheduler(time.Local)

// adds a new post upload time
func AddNewPostSchedule(postID string, time string) {
	cron.At(time).Do(deployer.DeployPost, postID)
}
