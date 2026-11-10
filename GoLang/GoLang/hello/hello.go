package main

import "fmt"

func main() {
	name := "Ivan"
	age := 30
	isDeveloper := true

	getUserInfo(name, age, isDeveloper)
}

func getUserInfo(name string, age int, isDeveloper bool) {

	status := ternary(age >= 18, "Adult", "Minor")
	fmt.Println("Name:", name)
	fmt.Println("Age:", age)
	fmt.Println("Status:", status)
	fmt.Println("Developer:", isDeveloper)
}

func ternary[T any](condition bool, trueVal, falseVal T) T {
	if condition {
		return trueVal
	}
	return falseVal
}
