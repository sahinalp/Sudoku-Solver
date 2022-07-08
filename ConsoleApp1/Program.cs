using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<List<int>> Sudoku= new List<List<int>>();

            List<int> arr1= new List<int>();
            List<int> arr2= new List<int>();
            List<int> arr3= new List<int>();
            List<int> arr4= new List<int>();
            List<int> arr5= new List<int>();
            List<int> arr6= new List<int>();
            List<int> arr7= new List<int>();
            List<int> arr8= new List<int>();
            List<int> arr9= new List<int>();

            arr1.Add(1);
            arr1.Add(2);
            arr1.Add(3);
            arr1.Add(4);
            arr1.Add(5);
            arr1.Add(6);
            arr1.Add(7);
            arr1.Add(8);
            arr1.Add(9);

            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Sudoku.Add(arr1);
            Console.WriteLine(Sudoku);
            Console.WriteLine(Sudoku[0][0]);
            Console.WriteLine(Sudoku[0][1]);
            Console.ReadLine();

        }
    }
}
