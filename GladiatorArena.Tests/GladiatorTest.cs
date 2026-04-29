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

        [TestMethod]
        public void Attack_AttaqueSoiMeme_ThrowInvalidOperationException()
        {
            var gladiateur = new Gladiator("Spartacus", 100, 10, 5);
            var fakeDice = new FakeDice { FixedResult = 3 };

            Assert.ThrowsException<InvalidOperationException>(() => gladiateur.Attack(gladiateur, fakeDice));
        }

        [TestMethod]
        public void Attack_Standard_CalculeDegatsCorrects()
        {
            var attacker = new Gladiator("Spartacus", 100, 10, 0);
            var defender = new Gladiator("Crixus", 100, 0, 5); // Armure de 5
            var fakeDice = new FakeDice { FixedResult = 3 }; // (3+10) - 5 = 8 dégâts

            attacker.Attack(defender, fakeDice);

            Assert.AreEqual(92, defender.Health);
        }

        [TestMethod]
        public void Attack_ArmureTropForte_AucunDegat()
        {
            var attacker = new Gladiator("Spartacus", 100, 10, 0);
            var defender = new Gladiator("Crixus", 100, 0, 20); // Armure de 20
            var fakeDice = new FakeDice { FixedResult = 1 }; // (1+10) - 20 = -9 -> 0 dégâts

            attacker.Attack(defender, fakeDice);

            Assert.AreEqual(100, defender.Health);
        }

        [TestMethod]
        public void Attack_CoupCritique_DoubleLesDegats()
        {
            var attacker = new Gladiator("Spartacus", 100, 10, 0);
            var defender = new Gladiator("Crixus", 100, 0, 5);
            var fakeDice = new FakeDice { FixedResult = 6 }; // (6+10) * 2 = 32. 32-5 = 27 dégâts.

            attacker.Attack(defender, fakeDice);

            Assert.AreEqual(73, defender.Health); // 100 - 27 = 73
        }
        
        [TestMethod]
        public void Attack_MortDeLAdversaire_SanteEgaleZero()
        {
            // Arrange : Un attaquant très fort, un défenseur très faible (10 HP)
            var attacker = new Gladiator("Spartacus", 100, 50, 0);
            var defender = new Gladiator("Crixus", 10, 0, 0); 
            var fakeDice = new FakeDice { FixedResult = 1 }; // Dégâts totaux : 1 + 50 = 51

            // Act
            attacker.Attack(defender, fakeDice);

            // Assert : Les PV de Crixus ne doivent pas être à -41, mais bloqués à 0
            Assert.AreEqual(0, defender.Health);
        }
    }
}