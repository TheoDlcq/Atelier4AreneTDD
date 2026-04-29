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

        // On injecte l'interface IDice ici, au lieu de la classe concrète Dice
        public void Attack(Gladiator opponent, IDice dice)
        {
            var score = dice.Roll();
            throw new NotImplementedException("To be done");
        }
    }
}