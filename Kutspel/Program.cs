using System;

namespace Kutspel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var g = new Game();
            g.Turn();
        }
    }
}
