/*
 Moving Average
Average definition:

The result you get by adding two or more amounts together and dividing the total by the number of amounts.
Source: Cambridge

In this problem, we want you to implement a Moving Average algorithm.

A moving average algorithm is an algorithm that receives a positive integer X and return the average compared to the N latest numbers.

Constraints
N can be any number from 0 to 999999

0 < N < 999999

X can be any number from 0 to 9999999999

0 < X < 9999999999

Examples
MovingAverage(3)
add(3) == 3.0
add(4) == 3.5
add(5) == 4.0
add(4) == 4.333333333333333
MovingAverage(2)
add(3) == 3.0
add(4) == 3.5
add(5) == 4.5
add(6) == 5.5
MovingAverage(1)
add(3) == 3.0
add(4) == 4.0
add(5) == 5.0
add(6) == 6.0
Task & Objectives
You must implement the logic for calling the MovingAverage class and the add method, which will return the moving average of the latest N elements added.
*/ 



var average = new MovingAverage(3);

Console.WriteLine(average.Add(3));
Console.WriteLine(average.Add(4));
Console.WriteLine(average.Add(5));
Console.WriteLine(average.Add(4));

Console.WriteLine("-----------------");

var average1 = new MovingAverage(2);

Console.WriteLine(average1.Add(3));
Console.WriteLine(average1.Add(4));
Console.WriteLine(average1.Add(5));
Console.WriteLine(average1.Add(6));

Console.WriteLine("-----------------");




public class MovingAverage
{
    public double[] Numbers { get; set; }
    public int Interval { get; set; }
    public MovingAverage(int interval)
    {
        Numbers = new double[0];
        Interval = interval;
    }

    public double Add(double value)
    {
        double[] newArray = new double[Numbers.Length + 1];
        newArray[0] = value;

        double total = 0;

        for (int i = 1; i <= Numbers.Length; i++)
        {
            newArray[i] = Numbers[i - 1];
        }

        for (int i = 0; i < newArray.Length; i++)
        {
            if (i < Interval)
            {
                total += newArray[i];
            }
        }

        Numbers = newArray;

        if (Interval > newArray.Length)
        {
            return total / newArray.Length;
        }
        else
        {
            return total / Interval;
        }
    }
}