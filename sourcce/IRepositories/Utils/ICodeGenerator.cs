using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICodeGenerator
    {
        string GenerateCode(string pPrefix, int pNumNumers, int pNumLetters);
    }
}
