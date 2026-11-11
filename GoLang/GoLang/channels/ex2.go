package main

import (
	"sync"
)

type Result struct {
	WorkerID int
	Number   int
	Square   int
}

func Worker(id int, jobs <-chan int, results chan<- Result, wg *sync.WaitGroup) {
	defer wg.Done()

	for job := range jobs {
		square := job * job

		result := Result{
			WorkerID: id,
			Number:   job,
			Square:   square,
		}

		results <- result
	}
}

func main() {
	jobs := make(chan int, 10)
	results := make(chan Result, 10)

	var wg sync.WaitGroup

	for i := 1; i <= 30; i++ {
		wg.Add(1)
		go Worker(i, jobs, results, &wg)
	}

	for i := 1; i <= 30; i++ {
		jobs <- i
	}
	close(jobs)

	closeResultsWhenWorkersFinish(&wg, results)

	for result := range results {
		println("Worker", result.WorkerID, "processed number", result.Number, "with square", result.Square)
	}
}

func closeResultsWhenWorkersFinish(wg *sync.WaitGroup, results chan Result) {
	go func() {
		wg.Wait()
		close(results)
	}()
}
