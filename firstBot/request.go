package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"image"
	"image/jpeg"
	"image/png"
	"io/ioutil"
	"log"
	"net/http"
	"os"
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
	json.Unmarshal([]byte(jsonData), &data)
	fmt.Println("|	", data)
	return data
}

// fetch the image from the url parsed from the json
func FetchImage(url string) string {
	// make the http call to the url passed in
	resp, err := http.Get(url)
	if err != nil {
		log.Fatalln("error occured while making request -> ", err)

	}

	defer resp.Body.Close()
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		log.Fatalln("error occured while reading http response -> ", err)
	}
	// Http response end
	// decode the bytes from the api into an image struct
	img, _, err := image.Decode(bytes.NewReader(body))
	if err != nil {
		fmt.Println("error while decoding image bytes -> ", err)
	}

	dataType := url[len(url)-4:]
	// create a jpg file
	imageName := "dog" + dataType
	f, err := os.Create(imageName)
	if err != nil {
		fmt.Println("error occured-> ", err)
	}
	defer f.Close()

	// do the jpeg encoding
	if dataType == ".jpg" {
		// Specify the quality, between 0-100
		// Higher is better
		opt := jpeg.Options{
			Quality: 90,
		}
		// encode the jpeg data into our file
		err = jpeg.Encode(f, img, &opt)
		if err != nil {
			fmt.Println("an error occured in jpg encoding -> ", err)
		}
	} else if dataType == ".png" { // do  gif encoding
		// Encode to `PNG` with `DefaultCompression` level
		// then save to file
		err = png.Encode(f, img)
		if err != nil {
			fmt.Println("error occured in gif encoding -> ", err)
		}
	}

	return imageName
}
