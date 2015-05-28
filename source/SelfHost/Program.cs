using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Thinktecture.IdentityServer.Core.Logging;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IdentityServer3 SelfHost";
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            const string url = "https://localhost:44333/core";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("\n\nServer listening at {0}. Press enter to stop", url);
                Console.ReadLine();
            }
        }
    }
}
