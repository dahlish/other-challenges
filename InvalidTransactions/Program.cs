/*
Invalid Transactions

In order to protect our customers we need to identify invalid transactions, a transaction is invalid if:

The amout exceeds $2000, or;
It occurs within (and including) 60 minutes of another transaction with the same name and the exact same price, or;
It occurs within (and including) 60 minutes of another transaction with the same name in a different city, or;
It occurs at the exact same time of another transaction with the same name.
Task
You are given an array of strings transaction where transactions[i] consists of comma-separated values representing the name, time (in minutes), amount, and city of the transaction.
Return a list of transactions that are possibly invalid. You may return the answer in any order.

Example 1
Input: transactions = ["john,20,800,stockholm","john,50,100,beijing"]
Output: ["john,20,800,stockholm","john,50,100,beijing"]
Explanation : The first transaction is invalid because the second transaction occurs within a difference of 60 minutes, has the same name and is in a different city. Similarly the second one is invalid too.

Example 2
Input: transactions = ["john,20,200,stockholm","john,50,200,stockholm"]
Output: ["john,20,200,stockholm","john,50,200,stockholm"]
Explanation : The first transaction is invalid because the second transaction occurs within a difference of 60 minutes, has the same name and the same price. Similarly the second one is invalid too.

Example 3
Input: transactions = ["john,20,150,stockholm","john,20,300,stockholm"]
Output: ["john,20,150,stockholm","john,20,300,stockholm"]
Explanation : The first transaction is invalid because the second transaction occurs at the exact same time and has the same name.

*/


string[] transactions1 = { "john,20,800,stockholm", "john,50,100,beijing", "dahlish,50,100,beijing", "dahlish,100,200,stockholm"};
string[] transactions2 = { "john,20,200,stockholm", "john,50,200,stockholm" };
string[] transactions3 = { "john,20,150,stockholm", "john,20,300,stockholm" };
string[] transactions4 = { "john,20,150,stockholm", "john,30,300,stockholm" };

string[] transactions5 = { "john5,1,2453,Sydney", "john5,36,2591,Sydney","john3,36,3404,Sydney" };

string[] transactions6 = { "john2,33,647,New York", "john2,40,1196,Stockholm","john0,53,1982,Sydney"};


string[] transactions7 = { "john1,24,2327,New York" };


Console.WriteLine("---Trans1---");
foreach(var transaction in CalculateInvalidTranscations(transactions1))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

Console.WriteLine("---Trans2---");
foreach (var transaction in CalculateInvalidTranscations(transactions2))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

Console.WriteLine("---Trans3---");
foreach (var transaction in CalculateInvalidTranscations(transactions3))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

Console.WriteLine("---Trans4---");
foreach (var transaction in CalculateInvalidTranscations(transactions4))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

foreach (var transaction in CalculateInvalidTranscations(transactions5))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

foreach (var transaction in CalculateInvalidTranscations(transactions6))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");

foreach (var transaction in CalculateInvalidTranscations(transactions7))
{
    Console.WriteLine(transaction);
}
Console.WriteLine("------");



string[] CalculateInvalidTranscations(string[] transactions)
{
    string[] invalidTransactions = new string[0];
    List<Transaction> matches = new List<Transaction>();

    for (int i = 0; i < transactions.Length; i++)
    {
        string[] splitTransaction = transactions[i].Split(',');


        Transaction currentTransaction = new Transaction
        {
            Name = splitTransaction[0],
            Minutes = int.Parse(splitTransaction[1]),
            Amount = decimal.Parse(splitTransaction[2]),
            City = splitTransaction[3]
        };

        for (int y = 0; y < transactions.Length; y++)
        {
            if (currentTransaction.Amount > 2000)
            {
                if (!matches.Any(c => c == currentTransaction))
                {
                    matches.Add(currentTransaction);
                }
            }
            if (y != i)
            {
                string[] splitOtherTransaction = transactions[y].Split(',');
               

                Transaction otherTransaction = new Transaction
                {
                    Name = splitOtherTransaction[0],
                    Minutes = int.Parse(splitOtherTransaction[1]),
                    Amount = decimal.Parse(splitOtherTransaction[2]),
                    City = splitOtherTransaction[3]
                };

                if (otherTransaction.Amount > 2000)
                {
                    if (!matches.Any(c => c == otherTransaction))
                    {
                        matches.Add(otherTransaction);
                    }
                }
                else if ( ((currentTransaction.Minutes + 60) >= otherTransaction.Minutes && otherTransaction.Name == currentTransaction.Name && currentTransaction.Amount == otherTransaction.Amount) 
                    || ((currentTransaction.Minutes - 60) <= otherTransaction.Minutes && otherTransaction.Name == currentTransaction.Name && currentTransaction.Amount == otherTransaction.Amount))
                {
                    if (!matches.Any(c => c == otherTransaction))
                    {
                        matches.Add(otherTransaction);
                    }
                }
                else if ( ((currentTransaction.Minutes + 60) >= otherTransaction.Minutes && currentTransaction.Name == otherTransaction.Name && currentTransaction.City != otherTransaction.City)
                    || ((currentTransaction.Minutes - 60) <= otherTransaction.Minutes && currentTransaction.Name == otherTransaction.Name && currentTransaction.City != otherTransaction.City))
                {
                    if (!matches.Any(c => c == otherTransaction))
                    {
                        matches.Add(otherTransaction);
                    }
                }
                else if (currentTransaction.Minutes == otherTransaction.Minutes && currentTransaction.Name == otherTransaction.Name)
                {
                    if (!matches.Any(c => c == otherTransaction))
                    {
                        matches.Add(otherTransaction);
                    }
                }
            }
        }

    }

    if (matches.Count > 0)
    {
        invalidTransactions = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            string matchString = $"{matches[i].Name},{matches[i].Minutes},{matches[i].Amount},{matches[i].City},";
            invalidTransactions[i] = matchString;
        }
    }
    else
    {
        invalidTransactions = new string[1];
        invalidTransactions[0] = String.Empty;
    }

    return invalidTransactions;
}

class Transaction
{
    public string Name { get; set; }
    public int Minutes { get; set; }
    public decimal Amount { get; set; }
    public string City { get; set; }
    public static bool operator ==(Transaction a, Transaction b)
    {
        return a.Name == b.Name && a.Amount == b.Amount && a.City == b.City && a.Minutes == b.Minutes;
    }
    public static bool operator !=(Transaction a, Transaction b)
    {
        return a.Name != b.Name || a.Amount != b.Amount || a.City != b.City || a.Minutes != b.Minutes;
    }
}