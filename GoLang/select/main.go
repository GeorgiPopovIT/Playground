package main

import (
	"fmt"
	"time"
)

func main() {
	numbers := make(chan int)
	messages := make(chan string)

	go func() {
		for {
			numbers <- 100
			time.Sleep(time.Second)
		}
	}()

	go func() {
		for {
			messages <- "Task completed"
			time.Sleep(time.Second * 2)
		}
	}()

	for {
		select {
		case num := <-numbers:
			fmt.Println("Received number:", num)
		case msg := <-messages:
			fmt.Println("Received message:", msg)
		}
	}
}
