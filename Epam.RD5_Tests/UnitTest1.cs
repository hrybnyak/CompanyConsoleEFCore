using NUnit.Framework;
using DAL.Context;

namespace Epam.RD5_Tests
{
    public class Tests
    {
        CompanyDbContext context;
        [SetUp]
        public void Setup()
        {
            CompanyDbContext context = new CompanyDbContext();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}