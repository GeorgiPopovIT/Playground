package main

import "fmt"

type User struct {
	Name        string
	Age         int
	IsDeveloper bool
}

func (u User) Greet() {
	fmt.Println("Hello,", u.Name)
}

func (u User) IsAdult() bool {
	return u.Age >= 18
}

func (u *User) Promote() {
	u.IsDeveloper = true
}

func (u *User) Birthday() {
	u.Age++
}

func main() {
	users := []User{
		{Name: "Ivan", Age: 31, IsDeveloper: true},
		{Name: "Maria", Age: 26, IsDeveloper: true},
		{Name: "Bob", Age: 17, IsDeveloper: false},
	}

	users = append(users, User{Name: "Alice", Age: 30, IsDeveloper: false})

	users[2].Promote()
	users[1].Birthday()

	for _, u := range users {
		u.Greet()

		fmt.Printf("Age: %d\n", u.Age)
		fmt.Printf("Adult: %t\n", u.IsAdult())
		fmt.Printf("Developer: %t\n", u.IsDeveloper)

		fmt.Println()
	}

	//If modify obj to change real data:
	//	for i := range users {
	//	users[i].Birthday()
}
