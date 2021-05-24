package post

import "image"

// defines a post obj that we can then post to pinterest
type PostObj struct {
	ID     string      // the id of the post in the database
	Link   string      // the link that the post is about
	Image  image.Image // the posts image
	Boards []string    // the list of boards we want to post it to
}
