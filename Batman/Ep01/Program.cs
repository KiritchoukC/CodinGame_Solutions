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
class Player
{
  static void Main(string[] args)
  {
    string[] inputs;
    inputs = Console.ReadLine().Split(' ');
    int W = int.Parse(inputs[0]); // width of the building.
    int H = int.Parse(inputs[1]); // height of the building.
    int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
    inputs = Console.ReadLine().Split(' ');
    int X0 = int.Parse(inputs[0]);
    int Y0 = int.Parse(inputs[1]);

    Console.Error.WriteLine(new { W, H });

    int currentX = X0;
    int currentY = Y0;

    int yMin = 0;
    int yMax = H;
    int xMin = 0;
    int xMax = W;

    // game loop
    while (true)
    {
      string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

      var isDown = bombDir.Contains('D');
      var isUp = bombDir.Contains('U');
      var isLeft = bombDir.Contains('L');
      var isRight = bombDir.Contains('R');

      if (isDown)
      {
        yMin = currentY;
        currentY += getNextLocation(yMin, yMax);
      }

      if (isUp)
      {
        yMax = currentY;
        currentY -= getNextLocation(yMin, yMax);
      }

      if (isRight)
      {
        xMin = currentX;
        currentX += getNextLocation(xMin, xMax);
      }

      if (isLeft)
      {
        xMax = currentX;
        currentX -= getNextLocation(xMin, xMax);
      }

      // Write an action using Console.WriteLine()
      // To debug: Console.Error.WriteLine("Debug messages...");

      // the location of the next window Batman should jump to.
      Console.WriteLine($"{currentX} {currentY}");
    }
  }

  static int getNextLocation(int min, int max)
  {
    if (min == max) return 0;
    if (min == 0 && max == 1) return 1;
    return Convert.ToInt32((max - min) / 2);
  }
}