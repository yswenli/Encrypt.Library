using System;
using Encrypt.Library;

namespace TestWeChatCrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            // 测试SHA1签名
            Console.WriteLine("测试SHA1签名:");
            string token = "test_token";
            string timestamp = "1409659813";
            string nonce = "xxx";
            string encrypt = "test_encrypt";
            string signature = SHAUtil.GetSHA1ForWeChat(token, timestamp, nonce, encrypt);
            Console.WriteLine($"签名结果: {signature}");
            Console.WriteLine();

            // 测试AES加密解密
            Console.WriteLine("测试AES加密解密:");
            string key = "4QSPUMqIe0Uer9g5uS98eIuIcB9kKkM9Qz5L5z5z5z5";
            string receiveId = "test_receive_id";
            string originalData = "{\"Content\":\"test content\",\"ToUserName\":\"test_to\",\"FromUserName\":\"test_from\",\"CreateTime\":1409659813,\"MsgType\":\"text\"}";
            
            Console.WriteLine($"原始数据: {originalData}");
            
            // 加密
            string encrypted = AESUtil.EncryptForWeChat(originalData, key, receiveId);
            Console.WriteLine($"加密结果: {encrypted}");
            
            // 解密
            string decrypted = AESUtil.DecryptForWeChat(encrypted, key, receiveId);
            Console.WriteLine($"解密结果: {decrypted}");
            
            // 验证解密结果是否与原始数据一致
            Console.WriteLine($"解密结果是否与原始数据一致: {decrypted == originalData}");
            
            Console.WriteLine();
            Console.WriteLine("测试完成，按任意键退出...");
            Console.ReadKey();
        }
    }
}