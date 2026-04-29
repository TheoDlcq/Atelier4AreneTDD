using System;

namespace GladiatorArena
{
    public class Gladiator
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int Strength { get; private set; }
        public int Armor { get; private set; }

        public Gladiator(string name, int health, int strength, int armor)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Armor = armor;
        }

        public void Attack(Gladiator opponent, IDice dice)
        {
            var score = dice.Roll();
            
            if (score < 1 || score > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(dice), "Le jet de dé doit être compris entre 1 et 6.");
            }

            if (this == opponent)
            {
                throw new InvalidOperationException("Un gladiateur ne peut pas s'attaquer lui-même.");
            }

            // Calcul des dégâts de base (Dé + Force)
            int totalDegats = score + Strength;
            
            // Multiplicateur de coup critique si le dé fait 6
            if (score == 6) 
            {
                totalDegats *= 2;
            }

            // Soustraction de l'armure (on s'assure que ça ne devienne pas négatif)
            int degatsFinals = Math.Max(0, totalDegats - opponent.Armor);
            
            // Application des dégâts
            opponent.Health -= degatsFinals;
        }
    }
}