package main

import "fmt"

type User struct {
	Name        string
	Age         int
	IsDeveloper bool
}

func main() {
	users := []User{
		{Name: "Ivan", Age: 30, IsDeveloper: true},
		{Name: "Maria", Age: 25, IsDeveloper: false},
		{Name: "Peter", Age: 21, IsDeveloper: true},
	}
	users = append(users, User{
		Name:        "John",
		Age:         28,
		IsDeveloper: false,
	})

	usersMap := make(map[string]int)
	totalDevelopers := 0

	for _, u := range users {
		usersMap[u.Name] = u.Age

		if u.Age >= 18 {
			fmt.Printf(
				"Name: %s | Age: %d | Developer: %t\n",
				u.Name, u.Age, u.IsDeveloper,
			)
		}

		if u.IsDeveloper {
			totalDevelopers++
		}
	}

	fmt.Println("\nTotal Developers:", totalDevelopers)
	fmt.Println("Users Map:", usersMap)
}
