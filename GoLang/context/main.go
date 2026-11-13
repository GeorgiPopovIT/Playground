package main

import (
	"context"
	"fmt"
	"time"
)

func worker(ctx context.Context) {
	fmt.Println("Worker started...")
	select {
	case <-time.After(5 * time.Second):
		println("Finished")
	case <-ctx.Done():
		fmt.Println("Worker stopped:", ctx.Err())
		return
	}
}

func main() {
	ctx, cancel := context.WithTimeout(
		context.Background(),
		3*time.Second,
	)

	defer cancel()

	go worker(ctx)

	<-ctx.Done()

	fmt.Println("Main: cancelling worker...")
	cancel()

	time.Sleep(100 * time.Millisecond)
}
