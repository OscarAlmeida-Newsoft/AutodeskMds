using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class TokenResponse
    {
        public bool error { get; set; }
        public string message { get; set; }
        public TokenData data { get; set; }
        public string Url { get; set; }
    }
}
