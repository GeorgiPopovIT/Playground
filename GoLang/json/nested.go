package main

import (
	"encoding/json"
	"fmt"
)

type Address struct {
	City    string `json:"city"`
	Country string `json:"country"`
}

type User struct {
	ID      int     `json:"id"`
	Name    string  `json:"name"`
	Email   string  `json:"email"`
	Address Address `json:"address"`
}

func main() {

	user := User{
		ID:    1,
		Name:  "Ivan",
		Email: "ivan@example.com",
		Address: Address{
			City:    "Plovdiv",
			Country: "Bulgaria",
		},
	}

	jsonData, err := json.MarshalIndent(user, "", "  ")
	if err != nil {
		fmt.Println("Error:", err)
		return
	}
	fmt.Println(string(jsonData))

	user2 := `{
    	"id": 2,
    	"name": "Maria",
    	"email": "maria@example.com",
    	"address": {
    	    "city": "Sofia",
    	    "country": "Bulgaria"
    	}
	}`

	var userModel User

	err = json.Unmarshal([]byte(user2), &userModel)
	if err != nil {
		fmt.Println("Error:", err)
		return
	}
	fmt.Println(userModel.Name)
	fmt.Println(userModel.Address.City)
}
