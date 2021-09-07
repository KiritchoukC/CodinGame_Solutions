using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Arg
{
  public class Val : Arg
  {
    public Val(int val) { Value = val; }
    public int Value { get; }
  }
  public class Ref : Arg
  {
    public Ref(int cell) { Cell = cell; }
    public int Cell { get; }
  }

  public static Arg New(string arg)
  {
    if (arg.StartsWith("$"))
    {
      return new Ref(int.Parse(arg[1..]));
    }

    return new Val(int.Parse(arg));
  }
}

public class Cell
{
  public class Value : Cell
  {
    public Value(string arg1)
    {
      Arg1 = Arg.New(arg1);
    }

    public Arg Arg1 { get; }
  }
  public class Add : Cell
  {
    public Add(string arg1, string arg2)
    {
      Arg1 = Arg.New(arg1);
      Arg2 = Arg.New(arg2);
    }
    public Arg Arg1 { get; }
    public Arg Arg2 { get; }
  }
  public class Sub : Cell
  {
    public Sub(string arg1, string arg2)
    {
      Arg1 = Arg.New(arg1);
      Arg2 = Arg.New(arg2);
    }
    public Arg Arg1 { get; }
    public Arg Arg2 { get; }
  }
  public class Mult : Cell
  {
    public Mult(string arg1, string arg2)
    {
      Arg1 = Arg.New(arg1);
      Arg2 = Arg.New(arg2);
    }
    public Arg Arg1 { get; }
    public Arg Arg2 { get; }
  }
  public static Cell New(string operation, string arg1, string arg2)
  {
    return operation switch
    {
      "VALUE" => new Value(arg1),
      "ADD" => new Add(arg1, arg2),
      "MULT" => new Mult(arg1, arg2),
      "SUB" => new Sub(arg1, arg2)
    };
  }
}

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
  private static readonly Dictionary<int, int> memo = new Dictionary<int, int>();
  private static readonly List<Cell> spreadsheet = new List<Cell>();
  static void Main(string[] args)
  {
    int N = int.Parse(Console.ReadLine());
    for (int i = 0; i < N; i++)
    {
      string[] inputs = Console.ReadLine().Split(' ');
      string operation = inputs[0];
      string arg1 = inputs[1];
      string arg2 = inputs[2];
      spreadsheet.Add(Cell.New(operation, arg1, arg2));
    }
    for (int i = 0; i < N; i++)
    {
      var currentCell = spreadsheet[i];

      // Write an answer using Console.WriteLine()
      // To debug: Console.Error.WriteLine("Debug messages...");

      Console.WriteLine(GetResult(currentCell));
    }

    static int GetRefCellValue(int cell)
    {
      if (memo.ContainsKey(cell)) return memo[cell];
      var result = GetResult(spreadsheet[cell]);
      memo[cell] = result;
      return result;
    }

    static int GetValue(Arg arg)
    => arg switch
    {
      Arg.Ref @ref => GetRefCellValue(@ref.Cell),
      Arg.Val val => val.Value
    };

    static int GetResult(Cell cell)
    => cell switch
    {
      Cell.Value valueCell => GetValue(valueCell.Arg1),
      Cell.Add addCell => GetValue(addCell.Arg1) + GetValue(addCell.Arg2),
      Cell.Sub subCell => GetValue(subCell.Arg1) - GetValue(subCell.Arg2),
      Cell.Mult multCell => GetValue(multCell.Arg1) * GetValue(multCell.Arg2),
    };
  }
}