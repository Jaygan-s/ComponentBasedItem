using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{
    public class UsableBase
    {
        public string name { get; set; }
        public string description { get; set; }

        public void OnUse(ActorClass holder)
        {
            Console.WriteLine($" > Using: {name}");
        }
    }
}
