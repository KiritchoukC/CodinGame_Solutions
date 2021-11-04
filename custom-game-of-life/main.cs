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
        string[] inputs = Console.ReadLine().Split(' ');
        int h = int.Parse(inputs[0]);
        int w = int.Parse(inputs[1]);
        int n = int.Parse(inputs[2]);

        var livingRules = GetRules(Console.ReadLine());
        var deathRules = GetRules(Console.ReadLine());

        var gridCells = new Cell[h][];

        for (int i = 0; i < h; i++)
        {
            string line = Console.ReadLine();
            gridCells[i] = new Cell[w];
            for(int j = 0; j < w; j++)
            {
                gridCells[i][j] = new Cell(i, j, line[j]);
            }
        }

        var grid = new Grid(gridCells, h, w, livingRules, deathRules);

        var result = Play(grid, n);

        Console.WriteLine(result.ToString());
    }

    static Grid Play(Grid grid, int turnNumber)
    {
        while(turnNumber > 0)
        {
            grid = grid.Select(cell => UpdateCell(cell, grid));
            turnNumber--;
        }

        return grid;
    }

    static Cell UpdateCell(Cell cell, Grid grid)
    {
        var neighbors = cell.GetNeighbors(grid);

        var livingNeighbors = neighbors.Count(x => x == 'O');

        if (cell.IsAlive && !grid.LivingRules.Contains(livingNeighbors))
        {
            return cell.Kill();
        }

        if (cell.IsDead && grid.DeathRules.Contains(livingNeighbors))
        {
            return cell.Revive();
        }

        return cell;
    }

    static IEnumerable<int> GetRules(string input)
    {
        return
            input
            .Select((r, i) => new {index = i, rule = r })
            .Where(t => t.rule == '1')
            .Select(t => t.index);
    }

    class Cell
    {
        public int Y { get; }
        public int X { get; }
        public char Value { get; }
        public bool IsDead => Value == '.';
        public bool IsAlive => Value == 'O';
        public Cell(int y, int x, char value)
        {
            Y = y;
            X = x;
            Value = value;
        }

        public Cell Kill()
        {
            return new Cell(Y, X, '.');
        }
        public Cell Revive()
        {
            return new Cell(Y, X, 'O');
        }

        public IEnumerable<char> GetNeighbors(Grid grid)
        {
            yield return X > 0                                     ? grid.Cells[Y    ][X - 1].Value : '.'; // left
            yield return X < grid.Width - 1                        ? grid.Cells[Y    ][X + 1].Value : '.'; // right
            yield return Y > 0                                     ? grid.Cells[Y - 1][X    ].Value : '.'; // up
            yield return X > 0 && Y > 0                            ? grid.Cells[Y - 1][X - 1].Value : '.'; // up left
            yield return Y > 0 && X < grid.Width - 1               ? grid.Cells[Y - 1][X + 1].Value : '.'; // up right
            yield return Y < grid.Height - 1                       ? grid.Cells[Y + 1][X    ].Value : '.'; // down
            yield return Y < grid.Height - 1 && X > 0              ? grid.Cells[Y + 1][X - 1].Value : '.'; // down left
            yield return Y < grid.Height - 1 && X < grid.Width - 1 ? grid.Cells[Y + 1][X + 1].Value : '.'; // down right
        }


        public override string ToString()
        {
            return Value.ToString();
        }
    }

    class Grid
    {
        public Cell[][] Cells { get; }
        public int Height { get; }
        public int Width { get; }
        public IEnumerable<int> LivingRules { get; }
        public IEnumerable<int> DeathRules { get; }
        public Grid(Cell[][] cells, int height, int width, IEnumerable<int> livingRules, IEnumerable<int> deathRules)
        {
            Cells = cells;
            Height = height;
            Width = width;
            LivingRules = livingRules;
            DeathRules = deathRules;
        }

        public Grid Select(Func<Cell, Cell> f)
        {
            var result = new Grid(
                new Cell[Height][],
                Height,
                Width,
                LivingRules,
                DeathRules);

            for (int i = 0; i < Cells.Length; i++)
            {
                result.Cells[i] = new Cell[Width];
                for(int j = 0; j < Cells[i].Length; j++)
                {
                    result.Cells[i][j] = f(Cells[i][j]);
                }
            }

            return result;
        }

        public override string ToString()
        {

            var lines = new List<string>();
            for (int i = 0; i < Height; i++)
            {
                lines.Add(string.Join("", Cells[i].ToList()));
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}
