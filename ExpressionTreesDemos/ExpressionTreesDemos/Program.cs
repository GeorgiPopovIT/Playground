using ExpressionTreesDemos;
using System.Linq.Expressions;

var myClass = new MyClass();

Func<MyClass, string> func = c => c.MyMethod(42, "Test Expression Trees");

//Console.WriteLine(func(myClass));

Func<int, int, int> sum = (x, y) => x + y;
//Console.WriteLine(sum(2, 4));

Expression<Func<int>> add = () => 1 + 2;
var func2 = add.Compile();
//Console.WriteLine(func2());

Expression<Func<int, int>> addFive = (num) => num + 5;

if (addFive is LambdaExpression lambdaExp)
{
    var parameter = lambdaExp.Parameters[0];

    //Console.WriteLine(parameter.Name);
    //Console.WriteLine(parameter.Type);
}

Expression<Func<MyClass, string>> expr = c => c.MyMethod(42, "Test Expression Trees");

var two = Expression.Constant(2,typeof(int));
var one = Expression.Constant(1,typeof(int));

var addition = Expression.Add(two, one);

//PrintExampleMethods();

ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
ConstantExpression five = Expression.Constant(5, typeof(int));
BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);


var sum3 = ReplaceNodes(addition);
var executableFunc = Expression.Lambda(sum3);

var func3 = (Func<int>)executableFunc.Compile();
var answer = func3();
Console.WriteLine(answer);

static void AnotherMethod(Func<MyClass,string> someFunc)
{

}

 static Expression ReplaceNodes(Expression original)
{
    if (original.NodeType == ExpressionType.Constant)
    {
        return Expression.Multiply(original, Expression.Constant(10));
    }
    else if (original.NodeType == ExpressionType.Add)
    {
        var binaryExpression = (BinaryExpression)original;
        return Expression.Add(
            ReplaceNodes(binaryExpression.Left),
            ReplaceNodes(binaryExpression.Right));
    }
    return original;
}

static void PrintExampleMethods()
{
    Expression<Func<int, int, int>> addition2 = (a, b) => a + b;


    Console.WriteLine($"This expression is a {addition2.NodeType} expression type");
    Console.WriteLine($"The name of the lambda is {((addition2.Name == null) ? "<null>" : addition2.Name)}");
    Console.WriteLine($"The return type is {addition2.ReturnType.ToString()}");
    Console.WriteLine($"The expression has {addition2.Parameters.Count} arguments. They are:");
    foreach (var argumentExpression in addition2.Parameters)
    {
        Console.WriteLine($"\tParameter Type: {argumentExpression.Type.ToString()}, Name: {argumentExpression.Name}");
    }

    var additionBody = (BinaryExpression)addition2.Body;
    Console.WriteLine($"The body is a {additionBody.NodeType} expression");
    Console.WriteLine($"The left side is a {additionBody.Left.NodeType} expression");
    var left = (ParameterExpression)additionBody.Left;
    Console.WriteLine($"\tParameter Type: {left.Type.ToString()}, Name: {left.Name}");
    Console.WriteLine($"The right side is a {additionBody.Right.NodeType} expression");
    var right = (ParameterExpression)additionBody.Right;
    Console.WriteLine($"\tParameter Type: {right.Type.ToString()}, Name: {right.Name}");
}