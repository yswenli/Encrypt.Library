using System.Text;

namespace Encrypt.Library.TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "yswenli";
            var base64 = str.ConvertToBase64Str();
            Assert.IsNotNull(base64);
            var urlSafe = base64.EncodeForUriSafe();
            var b2 = urlSafe.DecodeForUriSafe();
            Assert.AreEqual(base64, b2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var a = DigitalEncryptUtil.FromInt(1);
            Assert.IsNotNull(a);

            var a1 = DigitalEncryptUtil.ToInt(a);
            Assert.IsNotNull(a1);

            var b = DigitalEncryptUtil.FromLong(1);
            Assert.IsNotNull(b);

            var b1 = DigitalEncryptUtil.ToLong(b);
            Assert.IsNotNull(b1);

            var c = DigitalEncryptUtil.FromDecimal(1);
            Assert.IsNotNull(c);

            var c1 = DigitalEncryptUtil.ToDecimal(c);
            Assert.IsNotNull(c1);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var str = "yswenli";
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var stream = new MemoryStream(bytes))
            {
                var md5 = MD5Util.GetMD5Str(stream);
                Assert.IsNotNull(md5);
            }
        }

        /// <summary>
        /// ≤‚ ‘aes
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            var key = "yswenli";

            var data = "aaa";

            var encrypt = AESUtil.Encrypt(key, data);
            var result = AESUtil.Decrypt(encrypt, key);

            Assert.IsTrue(data == result);

            var encrypt2 = AESUtil.Encrypt(key, data);
            var result2 = AESUtil.Decrypt(encrypt2, key);

            Assert.IsTrue(data == result2);
        }

    }
}