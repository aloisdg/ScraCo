using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScraCo.Sample {
    class Program {
        static void Main() {
            var colorHexa = new ColorHexa ();
            Task.Run (async () => {
                var color = await colorHexa.ScrapeAsync ("#c715f0").ConfigureAwait (false);
                var json = JsonConvert.SerializeObject (color, Formatting.Indented);
                Console.WriteLine (json);
            }).Wait ();
            Console.ReadLine();
        }
    }
}
