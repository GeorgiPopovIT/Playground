package main

import "fmt"

type Greeter interface {
	Greet()
}

type User struct {
	Name string
	Age  int
}

type Admin struct {
	Name string
}

// func (u User) Greet() {
// 	fmt.Println("Hello, I am", u.Name, "and I am a user.")
// }

func (u User) Greet() {
	fmt.Println("Hello, I am", u.Name, "and I am a user.")
}

func (a Admin) Greet() {
	fmt.Println("Hello, I am", a.Name, "and I am an admin.")
}

func Introduce(g Greeter) {
	g.Greet()
}

func (u *User) Birthday() {
	u.Age++
}

func (u *User) ChangeName(newName string) {
	u.Name = newName
}

func main() {
	newUser := &User{Name: "New User"}

	greeters := []Greeter{
		newUser,
		&Admin{Name: "Maria"},
		&User{Name: "John", Age: 25},
		&Admin{Name: "Alice"},
	}

	newUser.ChangeName("Updated Ivan")
	newUser.Birthday()

	fmt.Println("Updated user:")
	fmt.Println("Name:", newUser.Name)
	fmt.Println("Age:", newUser.Age)

	fmt.Println("\nAll greeters:")

	for _, g := range greeters {
		Introduce(g)
	}
}
