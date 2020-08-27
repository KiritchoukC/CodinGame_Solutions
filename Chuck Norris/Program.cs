using System;
using System.Collections.Generic;
using System.Linq;

namespace Chuck_Norris
{
  class Program
  {
    static void Main(string[] args)
    {
      string MESSAGE = "CC";

      var binaries = ToBinary(MESSAGE).Aggregate((acc, val) => acc + val);

      string result = "";

      var previousChar = 'N';

      for (int i = 0; i < binaries.Length; i++)
      {
        if (previousChar == 'N')
        {
          result += binaries[i] == '1' ? "0 0" : "00 0";
          previousChar = binaries[i];
          continue;
        }

        if (binaries[i] == '1')
        {
          if (previousChar == '1')
          {
            result += "0";
          }
          else
          {
            result += " 0 0";
          }
        }
        else
        {
          if (previousChar == '0')
          {
            result += "0";
          }
          else
          {
            result += " 00 0";
          }
        }
        previousChar = binaries[i];
      }

      Console.WriteLine(result);
    }

    static IEnumerable<string> ToBinary(string source)
    {
      return source.Select(c => Convert.ToString(c, 2).PadLeft(7, '0'));
    }
  }
}