namespace ConsoleApp3;

public class FizzBuzz
{
    private int n;
    private int index = 1;
    private object locker = new();


    public FizzBuzz(int n)
    {
        this.n = n;
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz)
    {
        while (true)
        {
            lock (locker)
            {
                if (index % 3 == 0 && index % 5 != 0)
                {
                    printFizz();
                    index++;
                }
            }
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz)
    {
        while (true)
        {
            lock (locker)
            {
                if (index % 3 != 0 && index % 5 == 0)
                {
                    printBuzz();
                    index++;
                }
            }
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz)
    {
        while (true)
        {
            lock (locker)
            {
                if (index % 3 == 0 && index % 5 == 0)
                {
                    printFizzBuzz();
                    index++;
                }
            }
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber)
    {
        while (true)
        {
            lock (locker)
            {
                if (index % 3 != 0 && index % 5 != 0)
                {
                    printNumber(index);
                    index++;
                }
            }
        }
    }
}
