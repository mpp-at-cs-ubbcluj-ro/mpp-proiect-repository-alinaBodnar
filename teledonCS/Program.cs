using System;
using teledonCS.model;
using teledonCS.repository;
namespace teledonCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CharitableCase charitable = new CharitableCase(1, "skdjgdj", 400.0);
            Console.WriteLine(charitable);
        }
    }
}
