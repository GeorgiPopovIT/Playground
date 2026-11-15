package main

import (
	"fmt"
	"os"
)

func ex() {
	text := "Learning Go is fun!"

	SaveData("files/notes.txt", text)

	loadedText, _ := LoadData("files/notes.txt")

	fmt.Println("Loaded text:", loadedText)

	fmt.Println("File exists:", FileExists("files/notes.txt"))
}

func SaveData(filename string, content string) error {
	return os.WriteFile(filename, []byte(content), 0644)
}

func LoadData(filename string) (string, error) {
	data, err := os.ReadFile(filename)
	if err != nil {
		return "", err
	}
	return string(data), nil
}

func FileExists(filename string) bool {
	_, err := os.Stat(filename)
	return err == nil
}
