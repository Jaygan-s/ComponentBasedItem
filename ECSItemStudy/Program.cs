using System;

namespace ComponentBaseStructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ActorClass actor = new ActorClass();

            bool printNew = true;
            while (true)
            {
                printNew = actor.ActionSelectEvent(printNew);
                if (printNew)
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}


