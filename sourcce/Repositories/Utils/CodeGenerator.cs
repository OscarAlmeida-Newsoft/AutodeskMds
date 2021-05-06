using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CodeGenerator : ICodeGenerator
    {

        private const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "0123456789";

        public string GenerateCode(string pPrefix, int pNumNumers, int pNumLetters)
        {
            Random random = new Random();
            string number = RandomNumber(pNumNumers);
            string str = RandomString(pNumNumers);

            string rand = pPrefix + new string((number + str).ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

            return rand;
        }

        /// <summary>
        /// Generate a random string of letter between minus and mayus case 
        /// Génera cadena aleatoria de letras en mayúscula y minúscula un determinado número de caracteres
        /// </summary>
        private string RandomString(int pNumLetters)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(LETTERS, pNumLetters).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Génera cadena aleatoria de números {0-9} un determinado número de caracteres
        /// </summary>
        private string RandomNumber(int pNumNumbers)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(NUMBERS, pNumNumbers).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
