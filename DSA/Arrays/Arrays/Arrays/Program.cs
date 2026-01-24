// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
 int[] numbers = { 1, 2, 3, 4, 5 };
int target = 7;
Array.Sort(numbers);


int x, y;
x = 0; y = numbers.Length;

while (x < y || numbers[x] + numbers[y] != target)
{
    int sum = numbers[x] + numbers[y];
    if(sum < target)
    {
        x++;
    }
    else if (sum > target)
    {
        y--;
    }
    else
    {
        Console.WriteLine($"Pair found: {numbers[x]}, {numbers[y]}");
        break;
    }
}

int[] two() 
{
    return new int[] { 1, 2 };
}