using System.Collections.Generic;

namespace ScraCo
{
    public class ColorInfo {
        public string Hexadecimal { get; set; }
        public string Description { get; set; }
        public string Complementary { get; set; }

        public IEnumerable<string> Analogous { get; set; }
        public IEnumerable<string> SplitComplementary { get; set; }
        public IEnumerable<string> Triadic { get; set; }
        public IEnumerable<string> Tetradic { get; set; }
        public IEnumerable<string> Monochromatic { get; set; }
        public IEnumerable<string> Alternatives { get; set; }
        public IEnumerable<string> Shades { get; set; }
        public IEnumerable<string> Tints { get; set; }
        public IEnumerable<string> Tones { get; set; }
    }
}