using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Tests
{
    [TestClass()]
    public class LogicTests
    {
        // Тест на правильность определения месяца превышения ежемесячного увеличения вклада (процента)
        [TestMethod()]
        public void FindPercentMonthTest()
        {
            float deposit = 1000;
            float percent = 21;

            int month = Logic.FindPercentMonth(deposit, percent);

            Assert.AreEqual(4, month);
        }

        // Тест на правильность определения месяца превышения целевого размера вклада
        [TestMethod()]
        public void FindDepositSumMonthTest()
        {
            float deposit = 1000;
            float depositSum = 1200;

            int month = Logic.FindDepositSumMonth(deposit, depositSum);

            Assert.AreEqual(10, month);
        }
    }
}