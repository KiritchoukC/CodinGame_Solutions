using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
  static void Main(string[] args)
  {
    int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
    int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis    

    var grid = Enumerable.Range(0, height).Select(y => Console.ReadLine().Select(x => x).ToList()).ToList();

    // Write an action using Console.WriteLine()
    // To debug: Console.Error.WriteLine("Debug messages...");


    // Three coordinates: a node, its right neighbor, its bottom neighbor
    Console.WriteLine(Solve(grid));
  }

  static string Solve(List<List<char>> grid)
  {
    string result = "";

    for (int y = 0; y < grid.Count; y++)
    {
      for (int x = 0; x < grid[y].Count; x++)
      {
        if (grid[x][y] == '0')
        {
          result += x + " " + y;
          result += " " + GetRightNeighbor(grid, x, y);
          result += " " + GetBottomNeighbor(grid, x, y);
          return result;
        }
      }
    }

    return result;
  }

  static string GetRightNeighbor(List<List<char>> grid, int x, int y)
  {
    var nextX = x + 1;
    var cell = grid[nextX][y];
    if (cell == '.') return "-1 -1";
    return nextX + " " + y;
  }

  static string GetBottomNeighbor(List<List<char>> grid, int x, int y)
  {
    var nextY = y + 1;
    var cell = grid[x][nextY];
    if (cell == '.') return "-1 -1";
    return x + " " + nextY;
  }
}