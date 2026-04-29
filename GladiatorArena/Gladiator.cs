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
            if (this == opponent)
            {
                throw new InvalidOperationException("Un gladiateur ne peut pas s'attaquer lui-même.");
            }

            var score = dice.Roll();
            if (score < 1 || score > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(dice), "Le jet de dé doit être compris entre 1 et 6.");
            }

            throw new NotImplementedException("To be done");
        }
    }
}