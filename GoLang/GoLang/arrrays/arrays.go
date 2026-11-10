package main

import "fmt"

func main() {
	// var numbers [3]int

	// numbers[0] = 10
	// numbers[1] = 20
	// numbers[2] = 30

	//numbers := [3]int{10, 20, 30}

	numbers := []int{10, 20, 30}

	numbers = append(numbers, 40)
	numbers = append(numbers, 50)

	for index, value := range numbers {
		fmt.Println(index, value)
	}

	fmt.Println(numbers)

	user := map[string]string{
		"name": "Ivan",
		"city": "Sofia",
	}

	user["country"] = "Bulgaria"

	fmt.Println(user["name"])
	fmt.Println(user["country"])

	value, exists := user["name"]

	if exists {
		fmt.Println(value)
	}
}
