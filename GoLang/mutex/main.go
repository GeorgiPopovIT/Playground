package main

import (
	"fmt"
	"sync"
)

func main() {
	balance := 0

	var wg sync.WaitGroup
	var mu sync.Mutex

	for i := 0; i < 500; i++ {
		wg.Add(1)

		go func() {
			defer wg.Done()

			mu.Lock()
			defer mu.Unlock()

			balance += 1000

		}()
	}

	wg.Wait()
	fmt.Println("Final balance:", balance)
}
