using System.Dynamic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace csharp
{
  class Program
  {
    static void Main(string[] args)
    {
      string expression = Console.ReadLine();
      Console.Error.WriteLine("Expression: " + expression);
      string types = Console.ReadLine();
      Console.Error.WriteLine("Types: " + types);
      int N = int.Parse(Console.ReadLine());

      var queries = GetQueriesFromExpression(expression);

      var firstQuery = queries.First();

      var items = Enumerable.Range(0, N)
        .Select(i => Console.ReadLine())
        .Select(itemStr =>
        {
          dynamic item = new ExpandoObject();

          var properties = itemStr.Split(',')
            .Select(prop => prop.Split(':'))
            .ToDictionary(x => x[0], x => x[1]);

          foreach (var prop in properties)
          {
            ((IDictionary<String, Object>)item).Add(prop.Key, prop.Value);
          }

          return item;
        });

      items.ToList().ForEach(Console.WriteLine);

      //   var orderedItems = items.OrderBy(firstQuery.exp);

      foreach (var query in queries.Skip(1))
      {
        // orderedItems = orderedItems.ThenBy(query.exp);
      }

      items.Select(x => x.id).ToList().ForEach(Console.WriteLine);
    }

    static List<Query> GetQueriesFromExpression(string expression)
    {
      var temp = expression
        .Replace("+", ",+")
        .Replace("-", ",-")
        .Split(',')
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(Query.FromExpression);

      temp.ToList().ForEach(q => Console.Error.WriteLine(q.isAsc.ToString() + " " + q.name));

      return temp.ToList();
    }
  }

  internal class Query
  {
    private Query(bool isAsc, string name)
    {
      this.isAsc = isAsc;
      this.name = name;
    }
    public bool isAsc { get; }
    public string type { get; set; }
    public string name { get; }
    // public Func<dynamic, string> exp
    // {
    //   get
    //   {
    //     var parameter = Expression.Parameter(typeof(dynamic), "x");
    //     var member = Expression.Property(parameter, this.name); //x.Name
    //     return Expression.Lambda<Func<dynamic, string>>(member, parameter).Compile(); //x => x.Name
    //   }
    // }

    public static Query FromExpression(string expression)
    {
      return new Query(expression[0] == '+', expression.Substring(1, expression.Length - 1));
    }
  }
}