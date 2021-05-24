package post

var posts []PostObj

func AddPost(p PostObj) {
	posts = append(posts, p)
}

// https://stackoverflow.com/questions/37334119/how-to-delete-an-element-from-a-slice-in-golang
func DeletePost(p PostObj) {
	updated := posts
	for index, post := range updated {
		if post.ID == p.ID {
			//TODO: if performance becomes an issue than this re-slcing might be why
			// because it is quite constly with larger arrays
			updated = append(updated[:index], updated[index+1:]...)
		}
	}
	posts = updated
}
