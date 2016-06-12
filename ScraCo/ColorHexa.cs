using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace ScraCo {
    public class ColorHexa {
        private const string BaseAddress = "http://www.colorhexa.com/";
        private readonly IConfiguration _configuration = Configuration.Default.WithDefaultLoader();

        private static readonly Dictionary<string, string> Selectors = new Dictionary<string, string>
        {
            { "description", "#information > div.color-description > p > strong" }, // Vivid magenta /!\ not a collection
            { "complementary", "#complementary > ul > li:nth-child(2) > a[href]" }, // #3ef015 /!\ not a collection
            { "analogous", "#analogous > ul > li > a[href]" },
            { "split complementary", "#split-complementary > ul > li > a[href]" },
            { "triadic", "#triadic > ul > li > a[href]" },
            { "tetradic", "#tetradic > ul > li > a[href]" },
            { "monochromatic", "#monochromatic > ul > li > a[href]" },
            { "alternatives", "#alternatives > div > div > ul > li > a[href]" },
            { "shades", "#shades-tints > div > div:nth-child(1) > ul > li > a[href]"},
            { "tints", "#shades-tints > div > div:nth-child(2) > ul > li > a[href]"},
            { "tones", "#tones > div > div > ul > li > a[href]" }
        };

        public ColorHexa() { }

        public async Task<ColorInfo> ScrapeAsync(string hexastring) {
            string color;
            if (!TryParse (hexastring, out color))
                throw new ArgumentException (nameof (hexastring));

            var uri = BaseAddress + color;
            var document = await BrowsingContext.New (_configuration).OpenAsync (uri).ConfigureAwait (false);
            return new ColorInfo {
                Hexadecimal = color,
                Description = document.QuerySelector (Selectors.ElementAt (0).Value).TextContent,
                Complementary = document.QuerySelector (Selectors.ElementAt (1).Value).TextContent,
                Analogous = document.QuerySelectorAll (Selectors.ElementAt (2).Value).Select (x => x.TextContent),
                SplitComplementary = document.QuerySelectorAll (Selectors.ElementAt (3).Value).Select (x => x.TextContent),
                Triadic = document.QuerySelectorAll (Selectors.ElementAt (4).Value).Select (x => x.TextContent),
                Tetradic = document.QuerySelectorAll (Selectors.ElementAt (5).Value).Select (x => x.TextContent),
                Monochromatic = document.QuerySelectorAll (Selectors.ElementAt (6).Value).Select (x => x.TextContent),
                Alternatives = document.QuerySelectorAll (Selectors.ElementAt (7).Value).Select (x => x.TextContent),
                Shades = document.QuerySelectorAll (Selectors.ElementAt (8).Value).Select (x => x.TextContent),
                Tints = document.QuerySelectorAll (Selectors.ElementAt (9).Value).Select (x => x.TextContent),
                Tones = document.QuerySelectorAll (Selectors.ElementAt (10).Value).Select (x => x.TextContent)
            };
        }

        private static bool TryParse(string hexastring, out string color) {
            Func<string, string> unsharp = s => s.StartsWith ("#") ? s.Substring (1) : s;
            Func<char, bool> isHex = c => (c < '0' || c > '9') && (c < 'a' || c > 'f');

            if (string.IsNullOrEmpty (hexastring)) {
                color = null;
                return false;
            }
            var chars = unsharp (hexastring).ToLowerInvariant ();
            if (chars.Length != 6 || chars.ToCharArray ().Any (isHex)) {
                color = null;
                return false;
            }
            color = chars;
            return true;
        }
    }
}
