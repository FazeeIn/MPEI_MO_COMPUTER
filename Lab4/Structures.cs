using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct
{
    public class Structures
    {
        public int p, q, r;
        public string name;
        public override string ToString() => $"{name} (p: {p}, q: {q}, r: {r})";
    } 
}
