using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.Core.BLL;
using GateWay.Core.DAL;

namespace GateWay.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleBll.Current.StartCrawler();
            Console.WriteLine("Input Q to exit.");
            while (string.Compare(Console.ReadLine(), ConsoleKey.Q.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
            {
            }
        }
    }
}
