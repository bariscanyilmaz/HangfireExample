using System;
using Hangfire;
using Hangfire.SqlServer;

namespace HangfireExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(@"Server=.\SQLEXPRESS;Database=Hangfire.Sample; Integrated Security=True;");

            

            //Fire-and-Forget
            BackgroundJob.Enqueue(()=>Console.WriteLine("Fire-and-Forget "));

            //Recurring
            RecurringJob.AddOrUpdate(()=>Console.WriteLine("Recurring "),Cron.Daily);

            //Delayed Jobs
            var jobId= BackgroundJob.Schedule(()=>Console.WriteLine("10 seconds Delayed"),TimeSpan.FromSeconds(10));

            //Continuations
            BackgroundJob.ContinueJobWith(jobId,()=>Console.WriteLine("Continuations"));

             using (var server = new BackgroundJobServer())
            {
                Console.ReadLine();
            }
            
        }


    }
}
