using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class Enemy
    {
        public int HP { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        
        public Enemy()
        {
           Type = "";
        }
        
    }
}
