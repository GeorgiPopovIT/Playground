package main

import "sync"

type BankAccount struct {
	Balance          int64
	TransactionCount int64
	mu               sync.RWMutex
}

func (a *BankAccount) GetTransactionCount() int64 {
	a.mu.RLock()
	defer a.mu.RUnlock()

	return a.TransactionCount
}
