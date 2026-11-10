package main

import (
	"fmt"
	"sync"
	"time"
)

func Worker(id int, wg *sync.WaitGroup) {
	// Simulate work
	fmt.Printf("Worker %d started\n", id)
	time.Sleep(time.Millisecond * 1000)
	fmt.Printf("Worker %d finished\n", id)

	defer wg.Done()

}

func main() {
	var wg sync.WaitGroup

	for i := 1; i <= 5; i++ {
		wg.Add(1)
		go Worker(i, &wg)
	}

	wg.Wait()
	fmt.Println("All workers finished")
}
