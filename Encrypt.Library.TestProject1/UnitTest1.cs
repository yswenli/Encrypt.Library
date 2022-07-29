namespace Encrypt.Library.TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = DigitalEncryptUtil.FromInt(1);

            var a1 = DigitalEncryptUtil.ToInt(a);

            var b = DigitalEncryptUtil.FromLong(1);

            var b1 = DigitalEncryptUtil.ToLong(b);

            var c = DigitalEncryptUtil.FromDecimal(1);

            var c1 = DigitalEncryptUtil.ToDecimal(c);
        }
    }
}