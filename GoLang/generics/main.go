package main

func Contains[T comparable](items []T, target T) bool {
	for _, item := range items {
		if item == target {
			return true
		}
	}

	return false
}

// func Contains[T comparable](items []T, target T) bool {
// 	for i := 0; i < len(items); i++ {
// 		if items[i] == target {
// 			return true
// 		}
// 	}
// 	return false
// }
