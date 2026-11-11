// package main

// import "sync"

// func Worker(id int, jobs <-chan int, results chan<- int, wg *sync.WaitGroup) {
// 	defer wg.Done()
// 	for job := range jobs {
// 		results <- job * 10
// 	}
// }

// func main() {
// 	var wg sync.WaitGroup

// 	jobs := make(chan int, 5)
// 	results := make(chan int, 5)

// 	for i := 1; i <= 3; i++ {
// 		wg.Add(1)
// 		go Worker(i, jobs, results, &wg)
// 	}

// 	for i := 1; i <= 5; i++ {
// 		jobs <- i
// 	}
// 	close(jobs)

// 	go func() {
//         wg.Wait()
//         close(results)
//     }()

// 	for value := range results {
// 		println(value)
// 	}
// }
