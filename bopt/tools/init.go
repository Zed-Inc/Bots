package main

import (
	"context"
	"fmt"
	"log"
	"os"
	"time"

	"github.com/joho/godotenv"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
	"go.mongodb.org/mongo-driver/mongo/readpref"
)

/*
	initalizes the databse
*/
var db string

func init() {
	godotenv.Load("../database.env")
	db = os.Getenv("DB_URL")
}

func main() {
	client, err := mongo.NewClient(options.Client().ApplyURI(db))
	if err != nil {
		log.Fatal(err)
	}
	ctx, _ := context.WithTimeout(context.Background(), 10*time.Second)
	err = client.Connect(ctx)
	if err != nil {
		log.Fatal(err)
	}
	defer client.Disconnect(ctx)
	err = client.Ping(ctx, readpref.Primary())
	if err != nil {
		log.Fatal(err)
	}
	databases, err := client.ListDatabaseNames(ctx, bson.M{})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(databases)

	// making a new collection
	// ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	// defer cancel()
	collection := client.Database("bopt").Collection("posts")
	posts, err := collection.Find(ctx, bson.M{})
	if err != nil {
		panic(err)
	}
	// https://pkg.go.dev/go.mongodb.org/mongo-driver/mongo?utm_source=godoc#Cursor.All
	var results []bson.M
	_ = posts.All(ctx, &results)
	fmt.Println(results)
	// res, err := collection.InsertOne(ctx, bson.D{{"name", "pi"}, {"value", 3.14159}})
	// id := res.InsertedID
	// fmt.Println(id)

	// fmt.Println(client.ListDatabaseNames(ctx, bson.M{}))
}
