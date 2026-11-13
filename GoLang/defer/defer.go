package main

import "fmt"

func deferredExample() {
	fmt.Println("Start")
	defer fmt.Println("Deferred")
	fmt.Println("End")
}
