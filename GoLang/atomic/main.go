package main

import (
	"fmt"
	"sync"
	"sync/atomic"
)

func main() {
	counter := int64(0)

	var wg sync.WaitGroup

	for i := 0; i < 10; i++ {
		wg.Add(1)
		go func() {
			defer wg.Done()
			for j := 0; j < 1000; j++ {
				atomic.AddInt64(&counter, 1)
			}
		}()
	}

	wg.Wait()
	fmt.Println("Final counter:", counter)
}

type BankAccount struct {
	Balance          int64
	TransactionCount int64
	mu               sync.Mutex
}

func Deposit(balance *int64, transactionCount *int64, amount *int64, mu *sync.Mutex) {
	mu.Lock()
	defer mu.Unlock()

	*transactionCount++
	*balance += *amount
}

func (a *BankAccount) Deposit(amount int64) {
	a.mu.Lock()
	defer a.mu.Unlock()

	a.TransactionCount++
	a.Balance += amount
}

func (a *BankAccount) Withdraw(amount int64) error {
	a.mu.Lock()
	defer a.mu.Unlock()

	if amount < 50 {
		return fmt.Errorf("withdrawal amount must be at least 50")
	} else if a.Balance < amount {
		return fmt.Errorf("insufficient balance")
	}

	a.Balance -= amount
	a.TransactionCount++
	return nil
}
