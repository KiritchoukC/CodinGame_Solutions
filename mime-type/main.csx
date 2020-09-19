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
        int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        
        var hashTable = new Hashtable();
        
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            string EXT = inputs[0]; // file extension
            string MT = inputs[1]; // MIME type.
            
            //Console.Error.WriteLine($"EXT: {EXT}");
            //Console.Error.WriteLine($"MT: {MT}");
            
            hashTable.Add(EXT.ToLower(), MT);
        }
        for (int i = 0; i < Q; i++)
        {
            string FNAME = Console.ReadLine(); // One file name per line.
            //Console.Error.WriteLine($"FNAME: {FNAME}");
            
            if(string.IsNullOrWhiteSpace(FNAME)){
                continue;
            }
            
            if(!FNAME.Contains(".")){
                    Console.WriteLine("UNKNOWN");
                    continue;
            }
            
            var fileExtension = FNAME.Split('.').LastOrDefault().ToLower();
                        
            if(fileExtension == null || !hashTable.ContainsKey(fileExtension)){
                    Console.WriteLine("UNKNOWN");
                    continue;
            }
            
            Console.Error.WriteLine($"FileName: {FNAME} /// Extension: {fileExtension} /// result: {hashTable[fileExtension]}");
            Console.WriteLine(hashTable[fileExtension]);
        }
    }
}