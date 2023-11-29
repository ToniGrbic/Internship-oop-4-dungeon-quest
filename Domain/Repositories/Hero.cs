using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public class Hero
{
    public string Name { get; set; }
    public int XP { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }
    public string? Trait { get; set; }
    public Hero(string name, int XP)
    {
        this.Name = name;
        this.XP = XP;
    
    }
}

