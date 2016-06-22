using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace extreme_carpaccio.tests
{
    public class SomethingShould
    {
        [Test]
        public void VerificationFormule()
        {
            double tax = 10;
            double reduc = 0.5;
            double entree = 15;
            double sortie = 0;

            sortie = entree*tax;
            sortie = sortie * (1 - reduc);

            Assert.IsTrue(sortie == 75);

            Assert.IsFalse(false);
        }
    }
}
