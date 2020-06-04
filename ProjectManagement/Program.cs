using System;
using System.IO;

namespace ProjectManagement
{
    class Planner
    {
        static void Main()
        {
            int UWorkers= 0;
            int Workers = 0;
            int Managers = 0;
            double GivenMonths = 0;
            double Tasks = 0;
            DataSetup(ref UWorkers, ref Workers, ref Managers, ref GivenMonths, ref Tasks);
            Console.WriteLine("MANAGED WORKERS AMOUNT {0}", Workers);
            Console.WriteLine("UNMANAGED WORKERS AMOUNT {0}", UWorkers);
            Console.WriteLine("MONTHS GIVEN {0}", GivenMonths);
            Console.WriteLine("TASKS GIVEN {0}", Tasks);
            DateCalc(ref UWorkers, ref Workers, ref Managers, ref GivenMonths, ref Tasks);
        }
        public static void DataSetup(ref int UWorkers, ref int Workers, ref int Managers, ref double GivenMonths, ref double Tasks)
        {
            int ExtraMCount = 0;
            string[] resources = System.IO.File.ReadAllLines(@"C:\Lab8\ProjectManagement\resource.txt");
            string[] tasks = System.IO.File.ReadAllLines(@"C:\Lab8\ProjectManagement\tasks.txt");

            Managers = Convert.ToInt32(resources[1]);
            Workers = Convert.ToInt32(resources[3]);

            Tasks = Convert.ToDouble(tasks[1]);
            GivenMonths = Convert.ToDouble(tasks[3]);

            UWorkers = Workers - Managers * 10;
            if (UWorkers > 0)
            {
                Workers -= UWorkers;
            }
            else if (UWorkers < -10)
            {
                ExtraMCount = -(UWorkers / 10);
                UWorkers = 0;
            }
            else
            {
                UWorkers = 0;
            }
            if (ExtraMCount != 0)
                Console.WriteLine("You have {0} extra managers!", ExtraMCount);
        }
        public static void DateCalc(ref int UWorkers, ref int Workers, ref int Managers, ref double GivenMonths, ref double Tasks)
        {
            double ResMonths = 0;
            while (Convert.ToDouble(UWorkers*1.2 + Workers*2) < Tasks)
            {
                Tasks -= UWorkers * 1.2 + Workers * 2;
                ResMonths += 1;
            }
            Tasks = Math.Ceiling(Tasks);
            Console.WriteLine("TASKS LEFT {0}", Tasks);
            Console.WriteLine("MONTHS PASSED {0}", ResMonths);
            if (Workers * 2 > Tasks)
            {
                ResMonths += Tasks / (Workers * 2);
                Tasks = 0;
            } 
            else
            {
                ResMonths += Tasks / (Workers * 2 + UWorkers * 1.2);
                Tasks = 0;
            }
            Console.WriteLine("MONTHS ON FINISH {0:#.#}", ResMonths);
            string[] output = {$"Worker amount: {Workers + UWorkers}", $"Manager amount: {Managers}", $"Months given to finish the job: {GivenMonths}", $"Job finished in {ResMonths: #.#} months."};

            using (System.IO.StreamWriter outpfile =
            new System.IO.StreamWriter(@"C:\Lab8\ProjectManagement\datecalc.txt"))
            {
                foreach (string line in output)
                {
                    outpfile.WriteLine(line);
                }
                if (GivenMonths < ResMonths)
                {
                    outpfile.WriteLine("You haven't met the deadline! You should check the number of workers and managers or give your workers more time.");
                }
                else
                {
                    outpfile.WriteLine("You have met the deadline.");
                }
                if (UWorkers != 0)
                {
                    outpfile.WriteLine("You have {0} unmanaged workers!", UWorkers);
                }
            }
        }
    }
}
