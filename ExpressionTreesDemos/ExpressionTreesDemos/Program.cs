using ExpressionTreesDemos;
using System.Collections.Generic;
using System.Linq.Expressions;

var myClass = new MyClass();

Func<MyClass, string> func = c => c.MyMethod(42, "Test Expression Trees");

Console.WriteLine(func(myClass));

Func<int, int, int> sum = (x, y) => x + y;
Console.WriteLine(sum(2, 4));

Expression<Func<int>> add = () => 1 + 2;
var func2 = add.Compile();
Console.WriteLine(func2());

Expression<Func<int, int>> addFive = (num) => num + 5;

if (addFive is LambdaExpression lambdaExp)
{
    var parameter = lambdaExp.Parameters[1];

    Console.WriteLine(parameter.Name);
    Console.WriteLine(parameter.Type);
}

Expression<Func<MyClass, string>> expr = c => c.MyMethod(42, "Test Expression Trees");

static void AnotherMethod(Func<MyClass,string> someFunc)
{

}