package main

import (
	"fmt"
	"time"
)

func ex2() {
	results := make(chan string)

	go func() {
		{
			time.Sleep(time.Second * 5)
			results <- "Finished"
		}
	}()

	select {
	case result := <-results:
		fmt.Println("Result:", result)

	case <-time.After(3 * time.Second):
		fmt.Println("Operation timed out")
	}
}
