[![NuGet](https://img.shields.io/nuget/v/Encrypt.Library.svg)](https://nuget.org/packages/Encrypt.Library)
[![NetStandard 2.1](https://img.shields.io/badge/NetStandard-2.1-orange.svg)](https://www.microsoft.com/net/core)
[![license](https://img.shields.io/github/license/yswenli/Encrypt.Library.svg)](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)

**中文** | **[English](README_EN.md)**

# Encrypt.Library

> 一个开箱即用的 .NET 加密工具箱——从 AES 到国密 SM2，从文件哈希到 TOTP 动态口令，一行代码搞定。

## 为什么选择 Encrypt.Library？

- **算法全覆盖**：对称加密（AES / DES / SM4）、非对称加密（RSA / SM2）、哈希（MD5 / SHA / SM3）、消息认证码（HMAC）、一次性密码（TOTP / HOTP），一个包全搞定
- **国密原生支持**：SM2 签名加密、SM3 哈希、SM4 对称加密，无需额外依赖
- **流式加密 API**：支持 `Stream` 直接读写，GB 级文件加密也不撑爆内存
- **企业微信开箱即用**：内置消息签名验证、消息体加解密，对接企业微信零门槛
- **安全密钥生成**：所有随机数均由 `RandomNumberGenerator` 生成，杜绝可预测风险
- **零外部依赖**：BouncyCastle 源码内嵌，不引入额外 NuGet 包

---

## 目录

- [快速开始](#快速开始)
- [对称加密](#对称加密)
  - [AES](#aes)
  - [DES（3DES）](#des3des)
  - [SM4（国密）](#sm4国密)
  - [流式加密（大文件）](#流式加密大文件)
- [非对称加密](#非对称加密)
  - [RSA](#rsa)
  - [SM2（国密）](#sm2国密)
- [哈希与摘要](#哈希与摘要)
  - [MD5](#md5)
  - [SHA 系列](#sha-系列)
  - [SM3（国密）](#sm3国密)
  - [HMAC 消息认证码](#hmac-消息认证码)
- [一次性密码 TOTP / HOTP](#一次性密码-totp--hotp)
- [数据脱敏](#数据脱敏)
- [数字 ID 加密](#数字-id-加密)
- [GZip 压缩](#gzip-压缩)
- [Base64 编码](#base64-编码)
- [雪花 ID 生成器](#雪花-id-生成器)
- [MachineKey 生成](#machinekey-生成)
- [微信 / 企业微信](#微信--企业微信)
- [安全建议](#安全建议)
- [许可证](#许可证)

---

## 快速开始

### 安装

```bash
dotnet add package Encrypt.Library
```

或在 NuGet 包管理器中搜索 `Encrypt.Library`。

### 最简示例

```csharp
using Encrypt.Library;

// AES 加密解密
var encrypted = AESUtil.Encrypt("你好世界", key);
var decrypted = AESUtil.Decrypt(encrypted, key);

// 计算 MD5
var hash = MD5Util.GetMD5Str("Hello");

// RSA 签名验签
var sign = RSAUtil.Sign("重要数据", privateKey);
var valid = RSAUtil.Verify("重要数据", sign, publicKey);
```

---

## 对称加密

> 对称加密 = 同一把钥匙上锁和开锁。速度快，适合大量数据加密。

### AES

AES 是目前最广泛使用的对称加密算法，被美国政府选为加密标准。支持 **ECB** 和 **CBC** 两种模式：

| 模式 | 安全性 | 特点 | 推荐场景 |
|------|--------|------|----------|
| ECB | 较低 | 相同明文产生相同密文，会泄露数据模式 | 仅用于简单场景 |
| CBC | 高 | 引入 IV 向量，相同明文加密结果不同 | **生产环境推荐** |

> 密钥要求：16 字节 = 128 位，24 字节 = 192 位，32 字节 = 256 位。IV 固定 16 字节。

```csharp
// 生成随机密钥和 IV
var key = AESUtil.Key.Key;   // 32 字节密钥
var iv  = AESUtil.Key.IV;    // 16 字节 IV

// ---- CBC 模式（推荐）----
var cipher = AESUtil.Encrypt("敏感数据", key, iv);   // 返回 Base64
var plain  = AESUtil.Decrypt(cipher, key, iv);        // "敏感数据"

// ---- ECB 模式（无需 IV）----
var cipher2 = AESUtil.Encrypt("数据", key);
var plain2  = AESUtil.Decrypt(cipher2, key);

// ---- 字节数组 ----
byte[] raw = Encoding.UTF8.GetBytes("二进制数据");
byte[] enc = AESUtil.Encrypt(raw, key, iv);
byte[] dec = AESUtil.Decrypt(enc, key, iv);

// ---- Hex 输出（而非 Base64）----
var hexCipher = AESUtil.Encrypt("数据", key, withBase64: false);
var hexPlain  = AESUtil.Decrypt(hexCipher, key, withBase64: false);
```

---

### DES（3DES）

> 本库使用的是 **Triple DES（3DES）**，密钥长度 24 字节，安全性高于原始 DES。如果你在做新项目，建议直接用 AES。

```csharp
var key = DESUtil.Key;    // 24 字节密钥
var iv  = DESUtil.IV;     // 8 字节 IV

// 字符串加密（返回 Base64）
var cipher = DESUtil.Encrypt("数据", key, iv);
var plain  = DESUtil.Decrypt(cipher, key, iv);

// 字节数组
byte[] enc = DESUtil.Encrypt(Encoding.UTF8.GetBytes("数据"), key, iv);
byte[] dec = DESUtil.Decrypt(enc, key, iv);

// ECB 模式（无需 IV）
var cipher2 = DESUtil.Encrypt("数据", key);
```

---

### SM4（国密）

SM4 是中国国家密码管理局发布的对称加密标准，安全性与 AES 相当。如果你的项目需要满足国密合规要求，SM4 是对称加密的首选。

> 密钥长度 16 字节，IV 长度 16 字节。

```csharp
var key = SM4Util.Key;    // 16 字节密钥
var iv  = SM4Util.IV;     // 16 字节 IV

// CBC 模式
var cipher = SM4Util.Encrypt(key, iv, "数据", Sm4Mode.CBC);
var plain  = SM4Util.Decrypt(key, iv, cipher, Sm4Mode.CBC);

// ECB 模式
var cipher2 = SM4Util.Encrypt(key, "数据");
var plain2  = SM4Util.Decrypt(key, cipher2);
```

---

### 流式加密（大文件）

> 需要加密一个 500MB 的文件？不必全部加载到内存，用流式 API 边读边写。

```csharp
using var input  = File.OpenRead("原始文件.dat");
using var output = File.Create("加密文件.dat");
EncryptProvider.AESEncryptStream(input, output, key);

// 解密
using var encInput  = File.OpenRead("加密文件.dat");
using var decOutput = File.Create("解密文件.dat");
EncryptProvider.AESDecryptStream(encInput, decOutput, key);
```

DES 同样支持流式：

```csharp
EncryptProvider.DESEncryptStream(input, output, key);
EncryptProvider.DESDecryptStream(input, output, key);
```

---

## 非对称加密

> 非对称加密 = 两把钥匙：公钥加密、私钥解密；私钥签名、公钥验签。

### RSA

RSA 是最经典的非对称加密算法，基于大整数分解的数学难题。

```csharp
// 生成密钥对
var rsaKey = RSAUtil.Key;
var publicKey  = rsaKey.PublicKey;
var privateKey = rsaKey.PrivateKey;

// ---- 加密解密 ----
var cipher = RSAUtil.Encrypt(publicKey, "短数据");
var plain  = RSAUtil.Decrypt(privateKey, cipher);

// ---- 签名验签 ----
var sign  = RSAUtil.Sign("重要数据", privateKey);
var valid = RSAUtil.Verify("重要数据", sign, publicKey);  // true
```

#### PEM 格式

与 OpenSSL、Java、Python 等互操作时通常需要 PEM 格式密钥：

```csharp
// 生成 PEM 格式密钥
var (pubPem, priPem) = RSAUtil.GetPem(pkcs1: true);   // PKCS#1
var (pubPem8, priPem8) = RSAUtil.GetPem(pkcs1: false); // PKCS#8（推荐）

// 使用 PEM 密钥加密
var cipher = RSAUtil.EncryptWithPem(pubPem, "数据");
var plain  = RSAUtil.DecryptWithPem(priPem, cipher);
```

#### PKCS 格式密钥

```csharp
// PKCS#1 格式
var (pub1, pri1) = RSAUtil.GetPKCS(pkcs1: true);

// PKCS#8 格式（推荐）
var (pub8, pri8) = RSAUtil.GetPKCS(pkcs1: false);
```

---

### SM2（国密）

SM2 是基于椭圆曲线的国密非对称算法，256 位密钥安全性相当于 RSA 3072 位。国密合规项目必选。

```csharp
// 生成密钥对
SM2Util.GenerateKey(out byte[] pubKey, out byte[] privKey);

// 加密
var mode = Mode.C1C3C2;  // Java 常用模式；C1C2C3 为 C++ 常用
var plain = Encoding.UTF8.GetBytes("国密加密数据");
var enc   = SM2Util.Encrypt(pubKey, privKey, mode, plain);
var dec   = SM2Util.Decrypt(pubKey, privKey, mode, enc);

// 签名验签
byte[] signature = SM2Util.Sign(pubKey, privKey, mode, plain);
bool ok = SM2Util.VerifySign(pubKey, privKey, mode, plain, signature);
```

---

## 哈希与摘要

> 哈希 = 把任意数据"浓缩"成固定长度的指纹。不可逆，常用于校验数据完整性。

### MD5

> MD5 速度快但存在碰撞风险，**不要用于密码存储或安全场景**，适合文件校验等用途。

```csharp
// 字符串
var hash = MD5Util.GetMD5Str("Hello World");  // 返回小写 32 位十六进制

// 字节数组
byte[] hashBytes = MD5Util.GetMD5(Encoding.UTF8.GetBytes("data"));

// 文件（流式读取，不会撑爆内存）
var fileHash = MD5Util.GetMD5StrForFile("大文件.zip");

// 流
using var stream = File.OpenRead("文件.txt");
var streamHash = MD5Util.GetMD5Str(stream);

// 16 位截断（取中间 8 字节）
var hash16 = MD5Util.GetMD5Str("data", MD5Length.L16);

// 异步（.NET 6+，大文件可取消）
var asyncHash = await MD5Util.GetMD5StrForFileAsync("超大文件.iso");
```

---

### SHA 系列

| 算法 | 输出长度 | 安全性 | 推荐场景 |
|------|----------|--------|----------|
| SHA1 | 160 位 | 已不安全 | 兼容旧系统 |
| SHA256 | 256 位 | 安全 | **通用推荐** |
| SHA384 | 384 位 | 安全 | 高安全需求 |
| SHA512 | 512 位 | 安全 | 最高安全等级 |

```csharp
var sha1   = SHAUtil.GetSHA1("data");
var sha256 = SHAUtil.GetSHA256("data");
var sha384 = SHAUtil.GetSHA384("data");
var sha512 = SHAUtil.GetSHA512("data");
```

---

### SM3（国密）

SM3 是国密哈希算法，输出 256 位，安全性与 SHA256 相当。国密合规场景下替代 SHA256。

```csharp
// 返回字节数组
byte[] hash = SM3Util.ToSM3byte("国密哈希");

// 返回十六进制字符串
string hexHash = SM3Util.ToSM3HexStr("国密哈希");

// 带密钥的 HMAC-SM3
byte[] hmac = SM3Util.ToSM3byte("数据", "密钥");
```

---

### HMAC 消息认证码

HMAC 在哈希的基础上加入密钥，既能校验数据完整性，又能验证发送方身份。

```csharp
var key = "my-secret-key";

var hmacMd5    = MD5Util.GetHMACMD5("data", key);
var hmacSha1   = SHAUtil.GetSHA1("data", key);
var hmacSha256 = SHAUtil.GetSHA256("data", key);
var hmacSha384 = SHAUtil.GetSHA384("data", key);
var hmacSha512 = SHAUtil.GetSHA512("data", key);
```

---

## 一次性密码 TOTP / HOTP

用于双因素认证（2FA），与 Google Authenticator 等 TOTP 应用兼容。

```csharp
// TOTP（基于时间，每 30 秒刷新一次）
string code = OptUtil.GetTotp("JBSWY3DPEHPK3PXP");

// HOTP（基于计数器）
string hotpCode = OptUtil.GetHotp("JBSWY3DPEHPK3PXP", counter: 1);

// 生成 OTP URI（用于生成二维码）
string uri = OptUtil.GetOtpUri(
    secret: "JBSWY3DPEHPK3PXP",
    otpType: OtpType.Totp,
    user: "user@example.com",
    issuer: "MyApp",
    hash: OtpHashMode.Sha1,
    digits: 6,
    period: 30,
    counter: 0
);
```

---

## 数据脱敏

在展示敏感信息时，用星号遮挡中间部分，保护用户隐私。

```csharp
// 通用脱敏：保留首尾，中间用 * 替代
DesensitizationUtil.CommonDisplay("13812345678");  // "138****5678"

// 姓名脱敏
DesensitizationUtil.GetSensitiveName("张三丰");  // "张*丰"
DesensitizationUtil.GetSensitiveName("李四");    // "李*"

// 手机号脱敏
DesensitizationUtil.GetSensitivePhoneNumber("13812345678");  // "138****5678"
```

---

## 数字 ID 加密

将整数 ID 加密成不可猜测的字符串，适合用在 URL 中隐藏自增 ID。

```csharp
// 整数 → 加密字符串
string encId = DigitalEncryptUtil.FromInt(12345);

// 加密字符串 → 整数
int decId = DigitalEncryptUtil.ToInt(encId);  // 12345

// 同样支持 long 和 decimal
string encLong = DigitalEncryptUtil.FromLong(9999999999L);
long decLong   = DigitalEncryptUtil.ToLong(encLong);

string encDec = DigitalEncryptUtil.FromDecimal(99.99m);
decimal decDec = DigitalEncryptUtil.ToDecimal(encDec);
```

---

## GZip 压缩

```csharp
// 字节数组压缩
byte[] original = Encoding.UTF8.GetBytes("需要压缩的长文本...");
byte[] compressed = GZipUtil.Compress(original);
byte[] restored   = GZipUtil.Decompress(compressed);

// 字符串压缩（返回 Base64）
string zipStr   = GZipUtil.Compress("需要压缩的文本");
string unzipStr = GZipUtil.Decompress(zipStr);
```

---

## Base64 编码

```csharp
// 字符串 → Base64
string b64 = "Hello".ConvertToBase64Str();     // "SGVsbG8="

// Base64 → 字符串
string raw = b64.ConvertToUTF8Str();           // "Hello"

// URL 安全的 Base64（替换 +/ 为 -_，去除 =）
string urlSafe = b64.EncodeForUriSafe();
string restored = urlSafe.DecodeForUriSafe();
```

---

## 雪花 ID 生成器

分布式环境下生成全局唯一、趋势递增的 64 位 ID。

```csharp
// 设置机器 ID（可选，默认为 1）
SnowflakeIDcreator.SetWorkerID(1);

// 生成 ID
long id = SnowflakeIDcreator.NextId();  // 如 1425678901234567890
```

---

## MachineKey 生成

用于 ASP.NET 的 `<machineKey>` 配置节。

```csharp
// decryptionKey（16-48 字符）
string decKey = MachineKeyUtil.CreateDecryptionKey(48);

// validationKey（48-128 字符）
string valKey = MachineKeyUtil.CreateValidationKey(128);
```

---

## 微信 / 企业微信

### 消息签名验证

验证请求是否来自微信服务器：

```csharp
string signature = SHAUtil.GetSHA1ForWeChat(token, timestamp, nonce, encrypt);
// 将 signature 与微信传来的 signature 比对
bool isValid = signature == requestSignature;
```

### 消息加密

```csharp
// key: 在微信后台配置的 EncodingAESKey
// receiveId: 企业微信为企业 ID，公众号为 AppId
string encrypted = AESUtil.EncryptForWeChat(message, key, receiveId);
```

### 消息解密

```csharp
string decrypted = AESUtil.DecryptForWeChat(encryptedData, key, receiveId);
```

---

## 安全建议

| 场景 | 推荐 | 避免 |
|------|------|------|
| 对称加密 | AES-256-CBC、SM4 | DES、ECB 模式 |
| 非对称加密 | RSA 2048+、SM2 | RSA 1024 |
| 哈希 | SHA256、SHA512、SM3 | MD5（仅用于校验）、SHA1 |
| 密码存储 | bcrypt / PBKDF2 / Argon2 | 直接 MD5/SHA |
| 随机数 | `RandomNumberGenerator` | `System.Random` |

> **本库不适用于密码存储场景。** 如需存储用户密码，请使用专门的密码哈希库（如 BCrypt.Net）。

---

## 许可证

[MIT License](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)
