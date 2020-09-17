using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 * ---
 * Hint: You can use the debug stream to print initialTX and initialTY, if Thor seems not follow your orders.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int lightX = int.Parse(inputs[0]); // the X position of the light of power
        int lightY = int.Parse(inputs[1]); // the Y position of the light of power
        int initialTX = int.Parse(inputs[2]); // Thor's starting X position
        int initialTY = int.Parse(inputs[3]); // Thor's starting Y position
        int remainingXMoves = lightX - initialTX;
        int remainingYMoves = lightY - initialTY;

        // game loop
        while (true)
        {
            int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            
            string direction = "";
            if(remainingXMoves == 0 || remainingYMoves == 0){
                MoveStraight(remainingXMoves, "X");
                MoveStraight(remainingYMoves, "Y");
            }else{
                if(remainingXMoves > 0 && remainingYMoves > 0){ 
                    direction = "SE";
                    remainingYMoves--;
                    remainingXMoves--;
                }
                if(remainingXMoves < 0 && remainingYMoves > 0){ 
                    direction = "SW";
                    remainingYMoves--;
                    remainingXMoves++;
                }
                if(remainingXMoves > 0 && remainingYMoves < 0){ 
                    direction = "NE";
                    remainingYMoves++;
                    remainingXMoves--;
                }
                if(remainingXMoves < 0 && remainingYMoves < 0){ 
                    direction = "NW";
                    remainingYMoves++;
                    remainingXMoves++;
                }
            }
            
            // A single line providing the move to be made: N NE E SE S SW W or NW
            Console.WriteLine(direction);
        }
    }
    
    private static void MoveStraight(int remainingMoves, string axis){
        string direction = "";
        while(remainingMoves !=0){
            if(remainingMoves > 0){
                direction = axis == "X" ? "E" : "S";
                remainingMoves--;
            }
            if(remainingMoves < 0){
                direction = axis == "X" ? "W" : "N";
                remainingMoves++;
            }
            Console.WriteLine(direction);
        }
    }
}