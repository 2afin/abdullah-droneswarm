using System;

class HelloWorld
{
    static void Main()
    {
        int numRepeat = 100;
        int max = 5000;
        int min = 100;
        int stepsize = 100;
        int numsteps = (max - min) / stepsize;
        var watch = new System.Diagnostics.Stopwatch();
        Flock flock;

        string bubblesortcsv = "bubblesort.csv";
        using (var writer = new System.IO.StreamWriter(bubblesortcsv))
        {
            writer.WriteLine("NumDrones,Time");
            for (int i = 0; i < numsteps; i++)
            {
                int numdrones = i * stepsize + min;
                Console.WriteLine("Current num drones = " + numdrones);

                flock = new Flock(numdrones);
                flock.Init((int)(0.9 * numdrones)); // fill up 90% of drones
                watch.Restart();
                for (int rep = 0; rep < numRepeat; rep++)
                {
                    flock.bubblesort();
                }
                watch.Stop();
                
                double time = (watch.Elapsed.TotalSeconds / numRepeat) * 1_000_000;
                writer.WriteLine($"{numdrones},{time:F5}");
            }
        }
        //-------------------------------------------------------------------------------------------------
        string delfrontcsv = "delfront.csv";
        using (var writer = new System.IO.StreamWriter(delfrontcsv))
        {
            writer.WriteLine("NumDrones,Time");
            for (int i = 0; i < numsteps; i++)
            {
                int numdrones = i * stepsize + min;
                Console.WriteLine("Current num drones = " + numdrones);

                flock = new Flock(numdrones);
                flock.Init((int)(0.9 * numdrones)); // fill up 90% of drones
                watch.Restart();
                for (int rep = 0; rep < numRepeat; rep++)
                {
                    flock.DeleteFront();
                }
                watch.Stop();
                
                double time = (watch.Elapsed.TotalSeconds / numRepeat) * 1_000_000;
                writer.WriteLine($"{numdrones},{time:F5}");
            }
        }
        //-------------------------------------------------------------------------------------------------
        string mintempcsv = "mintemp.csv";
        using (var writer = new System.IO.StreamWriter(mintempcsv))
        {
            writer.WriteLine("NumDrones,Time");
            for (int i = 0; i < numsteps; i++)
            {
                int numdrones = i * stepsize + min;
                Console.WriteLine("Current num drones = " + numdrones);

                flock = new Flock(numdrones);
                flock.Init((int)(0.9 * numdrones)); // fill up 90% of drones
                watch.Restart();
                for (int rep = 0; rep < numRepeat; rep++)
                {
                    flock.min();
                }
                watch.Stop();
                
                double time = (watch.Elapsed.TotalSeconds / numRepeat) * 1_000_000;
                writer.WriteLine($"{numdrones},{time:F5}");
            }
        }
        //-------------------------------------------------------------------------------------------------
        string maxtempcsv = "maxtemp.csv";
        using (var writer = new System.IO.StreamWriter(maxtempcsv))
        {
            writer.WriteLine("NumDrones,Time");
            for (int i = 0; i < numsteps; i++)
            {
                int numdrones = i * stepsize + min;
                Console.WriteLine("Current num drones = " + numdrones);

                flock = new Flock(numdrones);
                flock.Init((int)(0.9 * numdrones)); // fill up 90% of drones
                watch.Restart();
                for (int rep = 0; rep < numRepeat; rep++)
                {
                    flock.min();
                }
                watch.Stop();
                
                double time = (watch.Elapsed.TotalSeconds / numRepeat) * 1_000_000;
                writer.WriteLine($"{numdrones},{time:F5}");
            }
        }
        //-------------------------------------------------------------------------------------------------
        string avgtempcsv = "avgtemp.csv";
        using (var writer = new System.IO.StreamWriter(avgtempcsv))
        {
            writer.WriteLine("NumDrones,Time");
            for (int i = 0; i < numsteps; i++)
            {
                int numdrones = i * stepsize + min;
                Console.WriteLine("Current num drones = " + numdrones);

                flock = new Flock(numdrones);
                flock.Init((int)(0.9 * numdrones)); // fill up 90% of drones
                watch.Restart();
                for (int rep = 0; rep < numRepeat; rep++)
                {
                    flock.min();
                }
                watch.Stop();
                
                double time = (watch.Elapsed.TotalSeconds / numRepeat) * 1_000_000;
                writer.WriteLine($"{numdrones},{time:F5}");
            }
        }
    }
}
