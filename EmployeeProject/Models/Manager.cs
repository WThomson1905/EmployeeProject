using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeProject
{
    class Manager : EmployeeBase
    {
        public Manager() : base()
        {

        }

        public override void DoWork()
        {
            Console.WriteLine("Manager Class does work");
        }
    }
}
