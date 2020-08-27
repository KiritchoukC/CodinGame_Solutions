using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
  static void Main(string[] args)
  {
    double longitude = double.Parse(Console.ReadLine().Replace(',', '.'));
    double latitude = double.Parse(Console.ReadLine().Replace(',', '.'));
    int N = int.Parse(Console.ReadLine());
    var defibrilators = Enumerable.Range(0, N)
        .Select(i => Console.ReadLine())
        .Select(Convert)
        .ToList();

    var nearestDefibrilator = defibrilators.Aggregate((acc, val) => val.GetDistanceFromUserPosition(longitude, latitude) > acc.GetDistanceFromUserPosition(longitude, latitude) ? val : acc);

    // Write an answer using Console.WriteLine()
    // To debug: Console.Error.WriteLine("Debug messages...");

    Console.WriteLine(nearestDefibrilator.Name);
  }

  static Defibrilator Convert(string input)
  {
    var fields = input.Split(';');
    return new Defibrilator
    {
      Id = int.Parse(fields[0]),
      Name = fields[1],
      Address = fields[2],
      PhoneNumber = fields[3],
      Longitude = double.Parse(fields[4].Replace(',', '.')),
      Latitude = double.Parse(fields[5].Replace(',', '.'))
    };
  }

}

class Defibrilator
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string PhoneNumber { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }

  public double GetDistanceFromUserPosition(double userLongitude, double userLatitude)
  {
    var x = (userLongitude - this.Longitude) * Math.Cos((this.Latitude + userLatitude) / 2);
    var y = userLatitude - this.Latitude;
    return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) * 6371;
  }
}