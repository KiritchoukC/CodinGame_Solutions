using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string[] inputs = Console.ReadLine().Split(' ');
        int? result = null;
        bool isPositive = true;
        for (int i = 0; i < n; i++)
        {
            int t = int.Parse(inputs[i]); // a temperature expressed as an integer ranging from -273 to 5526
            isPositive = t >= 0;
            var absolute = Math.Abs(t);
            if(!result.HasValue){
                result = absolute;
            }else{
                if(absolute < result){
                    result = absolute;
                }
            }
        }
        
        result = result ?? 0;

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(isPositive ? result : -result);
    }
}