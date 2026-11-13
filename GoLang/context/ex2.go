package main

import (
	"context"
	"fmt"
	"sync"
	"time"
)

func worker2(ctx context.Context, id int, wg *sync.WaitGroup) {
	defer wg.Done()
	for {
		select {
		case <-ctx.Done():
			fmt.Printf("Worker %d stopped\n", id)
			return

		case <-time.After(time.Second):
			fmt.Printf("Worker %d is working...\n", id)
		}
	}
}

func main2() {
	var waitGroup sync.WaitGroup

	ctx, cancel := context.WithTimeout(
		context.Background(),
		4*time.Second,
	)
	defer cancel()

	for i := 0; i < 3; i++ {
		waitGroup.Add(1)
		go worker2(ctx, i, &waitGroup)
	}

	waitGroup.Wait()

	fmt.Println("All workers stopped.")
}
