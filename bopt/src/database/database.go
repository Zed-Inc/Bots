package database

import "bopt/src/post"

// returns the post associated with the passed in ID param
func GetPost(id string) post.PostObj {
	return post.PostObj{}
}

// checks the database for any new items
func CheckDB() {
	// add a column to the recoprd called pulled it is a bool
	// defaults to false, when it has been pulled into the system then we
	// update the variable to true

}

// removes a post from the db that has been posted to pinterest
func removePostedPostFromDB(id string) {
}
