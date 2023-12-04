using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEnemy
    {
        public int HP { get; set; }
        public int HPThreshold { get; set; }
        public int XP { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        public bool IsStunned { get; set; }
        public virtual void BasicAttack(Hero hero) { }
        public void PrintEnemyStats();
    }
}
