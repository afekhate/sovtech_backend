using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sovtech.Infrastructure.ApiResponse
{
    public class ChuknorResponse
    {
        public ChuknorResponse()
        {
            Categories = new List<Category>();
        }
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public string category { get; set; }
        public int Id { get; set; }


    }


    public class JokesDTO
    {
        public string Joke { get; set; }

        public List<string> Jokes { get; set; }

    }
   

}
