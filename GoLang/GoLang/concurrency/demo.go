// package main

// import (
// 	"fmt"
// 	"sync"
// 	"time"
// )

// func sayHello(wg *sync.WaitGroup) {
// 	defer wg.Done()

// 	for i := 0; i < 5; i++ {
// 		fmt.Println("Hello", i)
// 		time.Sleep(time.Millisecond * 500)
// 	}
// }

// func main() {
// 	var wg sync.WaitGroup

// 	wg.Add(1)
// 	go sayHello(&wg)

// 	wg.Wait()

// 	fmt.Println("Main function finished")
// }
