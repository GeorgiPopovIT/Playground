package main

import (
	"fmt"
	"os"
)

func main() {
	data := []byte("Hello from Go!")

	err := os.WriteFile("files/data.txt", data, 0644)
	if err != nil {
		panic(err)
	}

	//Read the data back from the file
	dataRead, err := os.ReadFile("files/data.txt")
	if err != nil {
		panic(err)
	}

	fmt.Println(string(dataRead))

	//Check if the file exists
	_, errExist := os.Stat("files/data.txt")

	if errExist == nil {
		fmt.Println("File exists")
	} else if os.IsNotExist(errExist) {
		fmt.Println("File does not exist")
	} else {
		fmt.Println("Other error:", errExist)
	}

	//Create the storage/logs directory if it doesn't exist
	errDir := os.MkdirAll("storage/logs", 0755)
	if errDir != nil {
		panic(errDir)
	}
}
