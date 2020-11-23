package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
)

type DogPic struct {
	FileSizeBytes int64
	Url           string
}

var apiUrl = "https://random.dog/woof.json"

// make a request to the api and return a dogpic struct
func MakeRequest() DogPic {
	// query the api address
	resp, err := http.Get(apiUrl)
	if err != nil {
		log.Fatalln("error occured while making request -> ", err)

	}
	defer resp.Body.Close()
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		log.Fatalln("error occured while reading http response -> ", err)
	}
	jsonData := string(body)
	var data DogPic
	// json.Unmarshal([]byte(json), &data)
	fmt.Println("json returned -> ", jsonData)
	json.Unmarshal([]byte(jsonData), &data)
	fmt.Println(data)
	return data
}

// func FetchImage(url string) {
// 	// query the api address
// 	fmt.Println("url -> ", url)
// 	resp, err := http.Get(url)
// 	if err != nil {
// 		log.Fatalln("error occured while making request -> ", err)

// 	}
// 	defer resp.Body.Close()
// 	body, err := ioutil.ReadAll(resp.Body)
// 	if err != nil {
// 		log.Fatalln("error occured while reading http response -> ", err)
// 	}
// 	fmt.Println(string(body))
// }
