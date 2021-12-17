using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Brewery;
using System.Collections.Generic;
using Brewery.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAlcoholSumAbv()
        {
            //Uzsetupinu fake providerius
            var s_provider = new Mock<IAlcoholProvider>(MockBehavior.Strict);
            var s_data = new BreweryRepository(s_provider.Object);

            //Mock setup
            s_provider.Setup(s => s.GetAlcoholByAbv(It.IsAny<float>(), It.IsAny<float>(), 5.3F))
                .Returns(new List<Alcohol>
                {
                    new Alcohol {abv = (decimal)5.3},
                    new Alcohol {abv = null},
                    new Alcohol {abv = (decimal)5.3}
                });

            s_provider.Setup(s => s.GetAlcoholByAbv(It.IsAny<float>(), It.IsAny<float>(), 0F))
                .Returns(new List<Alcohol> { });

            //Test if sums with NULL
            Assert.AreEqual(10.6M, s_data.AlcoholSumAbv(1F, 10F, 5.3F));
            //Test if sums with empty Get
            Assert.AreEqual(0M, s_data.AlcoholSumAbv(0F, 0F, 0F));

            //Verify calls
            s_provider.VerifyAll();
        }

        [TestMethod]
        public void TestHighestABV()
        {
            //Uzsetupinu fake providerius
            var s_provider = new Mock<IAlcoholProvider>(MockBehavior.Strict);
            var s_data = new BreweryRepository(s_provider.Object);

            //Mock setup
            s_provider.Setup(s => s.GetAlcoholByAbv(1F, 10F, 0.0F))
                .Returns(new List<Alcohol>
                {
                    new Alcohol {abv = (decimal)4.5},
                    new Alcohol {abv = (decimal)5},
                    new Alcohol {abv = null},
                    new Alcohol {abv = (decimal)10},
                    new Alcohol {abv = (decimal)10}
                });

            s_provider.Setup(s => s.GetAlcoholByAbv(1F, 10F, 1F))
                .Returns(new List<Alcohol>{});


            //Test if sums and if sums with NULL
            Assert.AreEqual(10M, s_data.HighestABV(1F, 10F, 0.0F));
            //Test if sums with empty Get
            Assert.AreEqual(0M, s_data.HighestABV(1F, 10F, 1F));

            //Verify calls
            s_provider.VerifyAll();
        }

    }
}
