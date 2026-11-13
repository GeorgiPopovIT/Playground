package main

import (
	"errors"
	"fmt"
	"io"
	"os"
)

func main() {

	content, err := ReadFile("defer/sample.txt")
	if err != nil {
		if errors.Is(err, os.ErrNotExist) {
			fmt.Println("File does not exist")
			return
		}
		fmt.Println("Error reading file:", err)
		return
	}
	fmt.Println("File content:", content)

}

func ReadFile(filename string) (string, error) {
	file, err := os.Open(filename)
	if err != nil {
		return "", fmt.Errorf("failed to open file %q: %w", filename, err)
	}

	defer file.Close()

	content, err := io.ReadAll(file)
	if err != nil {
		return "", fmt.Errorf("failed to read file %q: %w", filename, err)
	}
	return string(content), nil
}
