package main

import (
	"encoding/json"
	"fmt"
)

// type User struct {
// 	Name     string `json:"name"`
// 	Age      int    `json:"age"`
// 	Email    string `json:"email"`
// 	Nickname string `json:"nickname,omitempty"`
// }

func main() {
	user := User{
		Name:  "Ivan",
		Age:   25,
		Email: "ivan@example.com",
	}

	jsonData, err := json.Marshal(user)
	if err != nil {
		fmt.Println("Error:", err)
		return
	}
	fmt.Println(string(jsonData))
}
