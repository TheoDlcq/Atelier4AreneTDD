using System;

namespace GladiatorArena
{
    public class Dice : IDice
    {
        private readonly int faces;
        private readonly Random rnd = new();

        public Dice(int faces)
        {
            this.faces = faces;
        }

        public int Roll() => rnd.Next(1, faces + 1);
    }
}