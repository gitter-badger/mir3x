using Autofac;
using Library;
using Server.Envir;
using System;
using System.Reflection;
using System.Runtime;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetAssembly(typeof(Config));
            ConfigReader.Load(assembly);

            Config.LoadVersion();

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;

            SEnvir.UseLogConsole = true;
            SEnvir.StartServer();

            Console.CancelKeyPress += Console_CancelKeyPress;

            // We check EnvirThread why when SEnvir is full stoped, set this to null...
            while (SEnvir.EnvirThread != null)
            {
                var command = Console.ReadLine();

            }

            // TODO fix that, not executing as expected. Server.ini hasn't been generated.
            ConfigReader.Save(typeof(Config).Assembly);
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            SEnvir.Started = false;
        }
    }
}
