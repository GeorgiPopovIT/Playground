package main

import "fmt"

func runMethodsDemo() {
	user := User{Name: "Alice", Age: 30}
	user.Greet()
	user.Promote()
	fmt.Println("Age:", user.Age)
	fmt.Println("Is Developer:", user.IsDeveloper)
}
