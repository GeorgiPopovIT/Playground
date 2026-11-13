// package main

// import "fmt"

// func Worker(id int, ch chan int) {
// 	ch <- id * 10
// }

// func main() {
// 	ch := make(chan int)

// 	for i := 1; i <= 5; i++ {
// 		go Worker(i, ch)
// 	}

// 	for i := 1; i <= 5; i++ {
// 		result := <-ch
// 		fmt.Println(result)
// 	}
// }
