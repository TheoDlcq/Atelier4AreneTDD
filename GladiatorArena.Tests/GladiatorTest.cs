using Microsoft.VisualStudio.TestTools.UnitTesting;
using GladiatorArena;
using System;

namespace GladiatorArena.Tests
{
    // Notre faux dé pour forcer le résultat et rendre les tests déterministes
    public class FakeDice : IDice
    {
        public int FixedResult { get; set; }
        public int Roll() => FixedResult;
    }

    [TestClass]
    public class GladiatorTest
    {
        [TestMethod]
        public void Attack_DiceTruque_ThrowArgumentOutOfRangeException()
        {
            var attacker = new Gladiator("Spartacus", 100, 10, 5);
            var defender = new Gladiator("Crixus", 100, 10, 5);
            var fakeDice = new FakeDice { FixedResult = 7 }; // Dé truqué > 6

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => attacker.Attack(defender, fakeDice));
        }
    }
}