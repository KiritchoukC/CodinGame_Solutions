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
        var length         = int.Parse(Console.ReadLine());
        var numberOfRobots = int.Parse(Console.ReadLine());
        var robotPositions = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

        var furthestRobot = 
            Enumerable.Range(0, numberOfRobots)
            .Select(i => new Robot(robotPositions[i], length))
            .Aggregate((acc, val) => val.DistanceFromTheEnd > acc.DistanceFromTheEnd ? val : acc);

        Console.WriteLine(furthestRobot.DistanceFromTheEnd);
    }

    class Robot
    {
        public int Position { get; }
        public int DuctLength { get; }

        public int MiddlePoint        { get => (int)Math.Round((double)(DuctLength / 2)); }
        public char Direction         { get => Position <= MiddlePoint ? 'R' : 'L' ; }
        public int DistanceFromTheEnd { get => Direction == 'R' ? DuctLength - Position : Position; }

        public Robot(int position, int ductLength)
        {
            Position   = position;
            DuctLength = ductLength;
        }
    }
}
