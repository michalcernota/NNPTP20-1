using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber firstNumber = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber secondNumber = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber result = firstNumber.Add(secondNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, result);
        }
    }
}


