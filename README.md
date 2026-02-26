[![NuGet](https://img.shields.io/nuget/v/Encrypt.Library.svg)](https://nuget.org/packages/Encrypt.Library)
[![NET 6.0](https://img.shields.io/badge/.NET-6.0-brightgreen)](https://www.microsoft.com/net/core)
[![NetStandard 2.1](https://img.shields.io/badge/NetStandard-2.1-orange.svg)](https://www.microsoft.com/net/core)
[![license](https://img.shields.io/github/license/myloveCc/Encrypt.Library.svg)](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)

## 简介

`Encrypt.Library` 是一个 NETCore 加密解密工具库，支持 AES、DES、RSA、MD5、SHA1、SHA256、SHA384、SHA512、SM3、SM2、SM4 等常用加密算法。

---

## 目录索引

- [快速开始](#快速开始)
- [对称加密算法](#对称加密算法)
  - [AES](#aes)
  - [DES](#des)
  - [SM4](#sm4)
- [非对称加密算法](#非对称加密算法)
  - [RSA](#rsa)
- [哈希算法](#哈希算法)
  - [MD5](#md5)
  - [SHA](#sha)
  - [HMAC](#hmac)
  - [SM3](#sm3)
- [国密算法](#国密算法)
  - [SM2](#sm2-1)
  - [SM4](#sm4-1)
- [微信/企业微信消息加解密](#微信企业微信消息加解密)
- [许可证](#license)

---

## 快速开始

### 安装方式

#### Package Manager
```
Install-Package Encrypt.Library -Version 2.0.6.6
```

#### .NET CLI
```
dotnet add package Encrypt.Library -Version 2.0.6.6
```

#### PackageReference
```xml
<PackageReference Include="Encrypt.Library" Version="2.0.6.6" />
```

---

## 对称加密算法

### AES

> AES（Advanced Encryption Standard）高级加密标准，是一种对称加密算法，支持 ECB 和 CBC 两种模式。

#### 生成 AES 密钥

```csharp
// 生成 AES 密钥和 IV（密钥长度 256 位，IV 长度 128 位）
var aesKey = AESUtil.Key;
var key = aesKey.Key;   // 加密密钥
var iv = aesKey.IV;     // 初始化向量（CBC 模式必需）
```

#### AES 加密

**ECB 模式（无需 IV）**

```csharp
// 注意：ECB 模式安全性较低，不推荐用于敏感数据
var srcString = "需要加密的字符串";
var encrypted = AESUtil.Encrypt(srcString, key);
```

**CBC 模式（需要 IV）**

```csharp
// CBC 模式安全性更高，适用于大多数场景
var srcString = "需要加密的字符串";
var encrypted = AESUtil.Encrypt(srcString, key, iv);

// 字节数组加密（适用于二进制数据）
var srcBytes = Encoding.UTF8.GetBytes("需要加密的字符串");
var encryptedBytes = AESUtil.Encrypt(srcBytes, key, iv);
```

#### AES 解密

**ECB 模式**

```csharp
var encryptedStr = "已加密的字符串";
var decrypted = AESUtil.Decrypt(encryptedStr, key);
```

**CBC 模式**

```csharp
var encryptedStr = "已加密的字符串";
var decrypted = AESUtil.Decrypt(encryptedStr, key, iv);

// 字节数组解密
var encryptedBytes = /* 已加密的字节数组 */;
var decryptedBytes = AESUtil.Decrypt(encryptedBytes, key, iv);
```

---

### DES

> DES（Data Encryption Standard）数据加密标准，是一种对称加密算法，由于密钥长度较短（56 位），安全性相对较低，建议使用 3DES 或 AES 替代。

#### 生成 DES 密钥

```csharp
// DES 密钥长度为 64 位（8 字节）
var desKey = DESUtil.Key;
var key = desKey.Key;
```

#### 生成 DES 初始化向量

```csharp
// DES IV 长度为 64 位（8 字节），CBC 模式必需
var desIv = DESUtil.Iv;
```

#### DES 加密

**ECB 模式（无需 IV）**

```csharp
var srcString = "需要加密的字符串";
var encrypted = DESUtil.Encrypt(srcString, key);

// 字节数组加密
var srcBytes = Encoding.UTF8.GetBytes("需要加密的字符串");
var encryptedBytes = DESUtil.Encrypt(srcBytes, key);
```

**CBC 模式（需要 IV）**

```csharp
// 字节数组加密（推荐使用 CBC 模式以提高安全性）
var srcBytes = Encoding.UTF8.GetBytes("需要加密的字符串");
var encrypted = DESUtil.Encrypt(srcBytes, key, desIv);
```

#### DES 解密

**ECB 模式**

```csharp
var encryptedStr = "已加密的字符串";
var decrypted = DESUtil.Decrypt(encryptedStr, key);

// 字节数组解密
var encryptedBytes = /* 已加密的字节数组 */;
var decryptedBytes = DESUtil.Decrypt(encryptedBytes, key);
```

**CBC 模式**

```csharp
var encryptedBytes = /* 已加密的字节数组 */;
var decrypted = DESUtil.Decrypt(encryptedBytes, key, desIv);
```

---

### SM4

> SM4 是中国国家密码管理局发布的对称加密算法，是一种分组密码算法，分组长度为 128 位，密钥长度也为 128 位。其安全性与 AES 相当，已被纳入 ISO/IEC 国际标准。

#### 生成 SM4 密钥

```csharp
// SM4 密钥长度为 128 位（16 字节）
var sm4Key = SM4Util.Key;
var key = sm4Key.Key;
```

#### 生成 SM4 初始化向量

```csharp
// SM4 IV 长度为 128 位（16 字节），CBC 模式必需
var sm4Iv = SM4Util.Iv;
```

#### SM4 加密

**ECB 模式（无需 IV）**

```csharp
var srcString = "需要加密的字符串";
var encrypted = SM4Util.Encrypt(key, srcString);

// 字节数组加密
var srcBytes = Encoding.UTF8.GetBytes("需要加密的字符串");
var encryptedBytes = SM4Util.Encrypt(key, srcBytes);
```

**CBC 模式（需要 IV）**

```csharp
// 字节数组加密
var srcBytes = Encoding.UTF8.GetBytes("需要加密的字符串");
var encrypted = SM4Util.Encrypt(key, sm4Iv, srcBytes);
```

#### SM4 解密

**ECB 模式**

```csharp
var encryptedStr = "已加密的字符串";
var decrypted = SM4Util.Decrypt(key, encryptedStr);

// 字节数组解密
var encryptedBytes = /* 已加密的字节数组 */;
var decryptedBytes = SM4Util.Decrypt(key, encryptedBytes);
```

**CBC 模式**

```csharp
var encryptedBytes = /* 已加密的字节数组 */;
var decrypted = SM4Util.Decrypt(key, sm4Iv, encryptedBytes);
```

---

## 非对称加密算法

### RSA

> RSA 是一种非对称加密算法，基于大整数分解的数学难题。RSA 支持数据加密和数字签名，常用于密钥交换和身份认证。

#### 生成 RSA 密钥对

```csharp
// 生成 RSA 密钥对（默认 2048 位）
var rsaKey = RSAUtil.Key;

// 获取密钥组件
var publicKey = rsaKey.PublicKey;    // 公钥
var privateKey = rsaKey.PrivateKey;  // 私钥
var exponent = rsaKey.Exponent;      // 指数
var modulus = rsaKey.Modulus;        // 模数
```

#### RSA 签名与验签

```csharp
var rawStr = "需要签名的原始字符串";

// 使用私钥签名
var signStr = RSAUtil.Sign(rawStr, privateKey);

// 使用公钥验签
var result = RSAUtil.Verify(rawStr, signStr, publicKey);
```

#### RSA 加密

```csharp
var publicKey = rsaKey.PublicKey;
var srcString = "需要加密的字符串";

// 默认使用 OAEP 填充（推荐）
var encrypted = RSAUtil.Encrypt(publicKey, srcString);

// 如需兼容旧版 macOS/Linux 可使用 PKCS1 填充
// var encrypted = RSAUtil.Encrypt(publicKey, srcString, true);
```

#### RSA 解密

```csharp
var privateKey = rsaKey.PrivateKey;
var encryptedStr = "已加密的字符串";

// 默认使用 OAEP 填充
var decrypted = RSAUtil.Decrypt(privateKey, encryptedStr);

// 兼容 PKCS1 填充
// var decrypted = RSAUtil.Decrypt(privateKey, encryptedStr, true);
```

#### RSA PEM 格式支持

```csharp
// 生成 PEM 格式密钥

// PKCS1 格式（传统格式）
var pkcs1KeyTuple = RSAUtil.GetPKCS(true);
var publicPkcs1 = pkcs1KeyTuple.publicPkcs1;
var privatePkcs1 = pkcs1KeyTuple.privatePkcs1;

// PKCS8 格式（推荐格式）
var pkcs8KeyTuple = RSAUtil.GetPKCS(false);
publicPkcs1 = pkcs8KeyTuple.publicPkcs1;
privatePkcs1 = pkcs8KeyTuple.privatePkcs1;

// 使用 PEM 格式密钥进行加解密
var rawStr = "需要加密的字符串";
var encryptedStr = RSAUtil.EncryptWithPem(pemPublicKey, rawStr);
var decryptedStr = RSAUtil.DecryptWithPem(pemPrivateKey, encryptedStr);
```

---

## 哈希算法

### MD5

> MD5（Message-Digest Algorithm 5）是一种不可逆的哈希算法，产生 128 位（16 字节）的哈希值。由于存在碰撞风险，不推荐用于安全敏感场景，仅适用于数据完整性校验等场景。

```csharp
var srcString = "需要计算哈希的字符串";

// 计算 MD5 哈希值（返回小写十六进制字符串）
var hashed = MD5Util.GetMD5Str(srcString);

// 另一种写法（功能相同）
var hashed2 = MD5Util.GetMd5Str(srcString);
```

---

### SHA

> SHA（Secure Hash Algorithm）安全哈希算法家族，包括 SHA1、SHA256、SHA384、SHA512 等变体。SHA1 由于存在安全漏洞，已不推荐使用；SHA256 及以上版本仍被广泛使用。

#### SHA1

```csharp
var srcString = "需要计算哈希的字符串";
var hashed = SHAUtil.GetSHA1(srcString);
```

#### SHA256

```csharp
var srcString = "需要计算哈希的字符串";
var hashed = SHAUtil.GetSHA256(srcString);
```

#### SHA384

```csharp
var srcString = "需要计算哈希的字符串";
var hashed = SHAUtil.GetSHA384(srcString);
```

#### SHA512

```csharp
var srcString = "需要计算哈希的字符串";
var hashed = SHAUtil.GetSHA512(srcString);
```

---

### HMAC

> HMAC（Hash-based Message Authentication Code）基于哈希的消息认证码，通过结合密钥和哈希算法提供消息完整性和认证功能。

#### HMAC-MD5

```csharp
var key = "密钥";
var srcString = "需要计算 HMAC 的字符串";
var hashed = MD5Util.GetHMACMD5(srcString, key);
```

#### HMAC-SHA1
```csharp
var key = "密钥";
var srcString = "需要计算 HMAC 的字符串";
var hashed = SHAUtil.GetSHA1(srcString, key);
```

#### HMAC-SHA256
```csharp
var key = "密钥";
var srcString = "需要计算 HMAC 的字符串";
var hashed = SHAUtil.GetSHA256(srcString, key);
```

#### HMAC-SHA384
```csharp
var key = "密钥";
var srcString = "需要计算 HMAC 的字符串";
var hashed = SHAUtil.GetSHA384(srcString, key);
```

#### HMAC-SHA512
```csharp
var key = "密钥";
var srcString = "需要计算 HMAC 的字符串";
var hashed = SHAUtil.GetSHA512(srcString, key);
```

---

## 国密算法

> 国密算法是中国国家密码管理局发布的密码算法标准，包括 SM1、SM2、SM3、SM4 等。其中 SM1 和 SM4 为对称加密算法，SM2 为非对称加密算法，SM3 为哈希算法。

### SM3

> SM3 是一种国密哈希算法，产生 256 位（32 字节）的哈希值。安全性与 SHA256 相当，已被纳入 ISO/IEC 国际标准。

```csharp
// 计算 SM3 哈希值（返回字节数组）
byte[] hashBytes = SM3Util.ToSM3byte("需要计算 SM3 哈希的字符串");

// 如需返回十六进制字符串
// string hashString = SM3Util.ToSM3HexStr("需要计算 SM3 哈希的字符串");
```

---

### SM2

> SM2 是一种国密非对称加密算法，基于椭圆曲线密码学（ECC）。SM2 算法相较于 RSA 具有更短的密钥长度和更高的安全强度，已被纳入 ISO/IEC 国际标准。

#### 生成 SM2 密钥对

```csharp
byte[] pubKey, privKey;
SM2Util.GenerateKey(out pubKey, out privKey);
```

#### SM2 加密

```csharp
// SM2 加密模式：C1C2C3（C++ 实现常用）或 C1C3C2（Java 实现常用）
var mode = Mode.C1C2C3;

// 待加密的字节数组
var plainBytes = Encoding.UTF8.GetBytes("需要加密的字符串");

// 加密
var encryptedBytes = SM2Util.Encrypt(pubKey, privKey, mode, plainBytes);
```

#### SM2 解密

```csharp
// 使用之前加密的模式进行解密
var decryptedBytes = SM2Util.Decrypt(pubKey, privKey, mode, encryptedBytes);
var decryptedString = Encoding.UTF8.GetString(decryptedBytes);
```

---

## 微信/企业微信消息加解密

> 本库提供了微信/企业微信消息的加解密功能，包括消息签名验证和消息体加解密。

### 微信消息签名验证

> 微信服务器会发送 signature 参数用于验证请求是否来自微信服务器。通过 SHA1 算法计算签名进行验证。

```csharp
var token = "你的Token";           // 在微信后台配置的 Token
var timestamp = "时间戳";          // 微信服务器传来的 timestamp
var nonce = "随机字符串";          // 微信服务器传来的 nonce
var encrypt = "加密消息";          // 微信服务器传来的 encrypt（加密消息）

// 计算签名并验证
var signature = SHAUtil.GetSHA1ForWeChat(token, timestamp, nonce, encrypt);
// 将计算的 signature 与微信传来的 signature 比对
```

### 微信消息加密

> 用于对发送给微信服务器的消息进行加密。

```csharp
var data = "需要加密的明文消息";
var key = "AES密钥（Base64编码）";      // 在微信后台配置的 EncodingAESKey 解码得到
var receiveId = "企业ID或AppId";        // 企业微信为企业ID，公众号为AppId

// 加密消息（返回 Base64 编码的密文）
var encrypted = AESUtil.EncryptForWeChat(data, key, receiveId);
```

### 微信消息解密

> 用于解密来自微信服务器的消息。

```csharp
var encryptedData = "微信服务器传来的加密消息（Base64编码）";
var key = "AES密钥（Base64编码）";      // 在微信后台配置的 EncodingAESKey 解码得到
var receiveId = "企业ID或AppId";        // 企业微信为企业ID，公众号为 AppId

// 解密消息
var decrypted = AESUtil.DecryptForWeChat(encryptedData, key, receiveId);
```

---

## 许可证

[MIT License](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)
