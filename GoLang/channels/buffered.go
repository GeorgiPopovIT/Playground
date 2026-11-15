package main

import "fmt"

func buffered() {
	ch := make(chan int, 2)

	ch <- 10
	ch <- 20

	for value := range ch {
		fmt.Println(value)
	}
}
