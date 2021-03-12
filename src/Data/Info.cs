using System.Collections.Generic;

namespace Splash.Data
{
    public class Info
    {
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public List<Link> Links { get; set; } = new();
    }
}
