using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Managers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager();
            manager.Seed();
           var user= manager.User.Find(1);

        }
    }
}
