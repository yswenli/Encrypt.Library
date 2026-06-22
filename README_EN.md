[![NuGet](https://img.shields.io/nuget/v/Encrypt.Library.svg)](https://nuget.org/packages/Encrypt.Library)
[![NetStandard 2.1](https://img.shields.io/badge/NetStandard-2.1-orange.svg)](https://www.microsoft.com/net/core)
[![license](https://img.shields.io/github/license/yswenli/Encrypt.Library.svg)](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)

**[中文](README.md)** | **English**

# Encrypt.Library

> A ready-to-use .NET cryptography toolbox — from AES to SM2, from file hashing to TOTP one-time passwords, all in one line of code.

## Why Encrypt.Library?

- **Full algorithm coverage** — Symmetric (AES / DES / SM4), Asymmetric (RSA / SM2), Hashing (MD5 / SHA / SM3), HMAC, One-Time Passwords (TOTP / HOTP), all in one package
- **Native Chinese cryptography (SM)** — SM2 signing & encryption, SM3 hashing, SM4 symmetric encryption, no extra dependencies
- **Streaming encryption API** — Read and write directly via `Stream`, encrypt GB-sized files without blowing up memory
- **WeChat / WeCom ready** — Built-in message signature verification and message body encrypt/decrypt
- **Secure key generation** — All random numbers generated via `RandomNumberGenerator`, no predictable `System.Random`
- **Zero external dependencies** — BouncyCastle source embedded, no extra NuGet packages pulled in

---

## Table of Contents

- [Quick Start](#quick-start)
- [Symmetric Encryption](#symmetric-encryption)
  - [AES](#aes)
  - [DES (3DES)](#des-3des)
  - [SM4 (Chinese Standard)](#sm4-chinese-standard)
  - [Streaming Encryption (Large Files)](#streaming-encryption-large-files)
- [Asymmetric Encryption](#asymmetric-encryption)
  - [RSA](#rsa)
  - [SM2 (Chinese Standard)](#sm2-chinese-standard)
- [Hashing & Digests](#hashing--digests)
  - [MD5](#md5)
  - [SHA Family](#sha-family)
  - [SM3 (Chinese Standard)](#sm3-chinese-standard)
  - [HMAC Message Authentication](#hmac-message-authentication)
- [One-Time Passwords: TOTP / HOTP](#one-time-passwords-totp--hotp)
- [Data Masking](#data-masking)
- [Numeric ID Encryption](#numeric-id-encryption)
- [GZip Compression](#gzip-compression)
- [Base64 Encoding](#base64-encoding)
- [Snowflake ID Generator](#snowflake-id-generator)
- [MachineKey Generation](#machinekey-generation)
- [WeChat / WeCom](#wechat--wecom)
- [Security Recommendations](#security-recommendations)
- [License](#license)

---

## Quick Start

### Install

```bash
dotnet add package Encrypt.Library
```

Or search for `Encrypt.Library` in the NuGet Package Manager.

### Minimal Example

```csharp
using Encrypt.Library;

// AES encrypt / decrypt
var encrypted = AESUtil.Encrypt("Hello World", key);
var decrypted = AESUtil.Decrypt(encrypted, key);

// Compute MD5
var hash = MD5Util.GetMD5Str("Hello");

// RSA sign / verify
var sign = RSAUtil.Sign("important data", privateKey);
var valid = RSAUtil.Verify("important data", sign, publicKey);
```

---

## Symmetric Encryption

> Symmetric encryption = one key to lock and unlock. Fast, ideal for encrypting large amounts of data.

### AES

AES (Advanced Encryption Standard) is the most widely used symmetric encryption algorithm, adopted by the U.S. government as the encryption standard. It supports two modes: **ECB** and **CBC**.

| Mode | Security | Behavior | Recommended For |
|------|----------|----------|-----------------|
| ECB | Low | Identical plaintext produces identical ciphertext, leaking data patterns | Simple, non-sensitive scenarios only |
| CBC | High | Uses an IV vector, so identical plaintext encrypts differently each time | **Production use** |

> Key requirements: 16 bytes = 128-bit, 24 bytes = 192-bit, 32 bytes = 256-bit. IV is always 16 bytes.

```csharp
// Generate a random key and IV
var key = AESUtil.Key.Key;   // 32-byte key
var iv  = AESUtil.Key.IV;    // 16-byte IV

// ---- CBC Mode (Recommended) ----
var cipher = AESUtil.Encrypt("sensitive data", key, iv);   // Returns Base64
var plain  = AESUtil.Decrypt(cipher, key, iv);             // "sensitive data"

// ---- ECB Mode (No IV needed) ----
var cipher2 = AESUtil.Encrypt("data", key);
var plain2  = AESUtil.Decrypt(cipher2, key);

// ---- Byte arrays ----
byte[] raw = Encoding.UTF8.GetBytes("binary data");
byte[] enc = AESUtil.Encrypt(raw, key, iv);
byte[] dec = AESUtil.Decrypt(enc, key, iv);

// ---- Hex output (instead of Base64) ----
var hexCipher = AESUtil.Encrypt("data", key, withBase64: false);
var hexPlain  = AESUtil.Decrypt(hexCipher, key, withBase64: false);
```

---

### DES (3DES)

> This library uses **Triple DES (3DES)** with a 24-byte key, which is more secure than original DES. For new projects, use AES instead.

```csharp
var key = DESUtil.Key;    // 24-byte key
var iv  = DESUtil.IV;     // 8-byte IV

// String encryption (returns Base64)
var cipher = DESUtil.Encrypt("data", key, iv);
var plain  = DESUtil.Decrypt(cipher, key, iv);

// Byte arrays
byte[] enc = DESUtil.Encrypt(Encoding.UTF8.GetBytes("data"), key, iv);
byte[] dec = DESUtil.Decrypt(enc, key, iv);

// ECB mode (no IV needed)
var cipher2 = DESUtil.Encrypt("data", key);
```

---

### SM4 (Chinese Standard)

SM4 is a symmetric encryption standard published by China's State Cryptography Administration. Its security is comparable to AES. If your project requires Chinese cryptography compliance (GM/T), SM4 is the go-to choice for symmetric encryption.

> Key length: 16 bytes. IV length: 16 bytes.

```csharp
var key = SM4Util.Key;    // 16-byte key
var iv  = SM4Util.IV;     // 16-byte IV

// CBC mode
var cipher = SM4Util.Encrypt(key, iv, "data", Sm4Mode.CBC);
var plain  = SM4Util.Decrypt(key, iv, cipher, Sm4Mode.CBC);

// ECB mode
var cipher2 = SM4Util.Encrypt(key, "data");
var plain2  = SM4Util.Decrypt(key, cipher2);
```

---

### Streaming Encryption (Large Files)

> Need to encrypt a 500 MB file? No need to load it all into memory — use the streaming API to read and write on the fly.

```csharp
using var input  = File.OpenRead("original.dat");
using var output = File.Create("encrypted.dat");
EncryptProvider.AESEncryptStream(input, output, key);

// Decrypt
using var encInput  = File.OpenRead("encrypted.dat");
using var decOutput = File.Create("decrypted.dat");
EncryptProvider.AESDecryptStream(encInput, decOutput, key);
```

DES also supports streaming:

```csharp
EncryptProvider.DESEncryptStream(input, output, key);
EncryptProvider.DESDecryptStream(input, output, key);
```

---

## Asymmetric Encryption

> Asymmetric encryption = two keys: the public key encrypts, the private key decrypts; the private key signs, the public key verifies.

### RSA

RSA is the most classic asymmetric encryption algorithm, based on the mathematical difficulty of factoring large integers.

```csharp
// Generate a key pair
var rsaKey = RSAUtil.Key;
var publicKey  = rsaKey.PublicKey;
var privateKey = rsaKey.PrivateKey;

// ---- Encrypt / Decrypt ----
var cipher = RSAUtil.Encrypt(publicKey, "short data");
var plain  = RSAUtil.Decrypt(privateKey, cipher);

// ---- Sign / Verify ----
var sign  = RSAUtil.Sign("important data", privateKey);
var valid = RSAUtil.Verify("important data", sign, publicKey);  // true
```

#### PEM Format

When interoperating with OpenSSL, Java, Python, etc., you'll typically need PEM-format keys:

```csharp
// Generate PEM-format keys
var (pubPem, priPem) = RSAUtil.GetPem(pkcs1: true);   // PKCS#1
var (pubPem8, priPem8) = RSAUtil.GetPem(pkcs1: false); // PKCS#8 (Recommended)

// Encrypt with PEM key
var cipher = RSAUtil.EncryptWithPem(pubPem, "data");
var plain  = RSAUtil.DecryptWithPem(priPem, cipher);
```

#### PKCS Format Keys

```csharp
// PKCS#1 format
var (pub1, pri1) = RSAUtil.GetPKCS(pkcs1: true);

// PKCS#8 format (Recommended)
var (pub8, pri8) = RSAUtil.GetPKCS(pkcs1: false);
```

---

### SM2 (Chinese Standard)

SM2 is a Chinese national asymmetric algorithm based on elliptic curve cryptography (ECC). A 256-bit SM2 key provides security equivalent to a 3072-bit RSA key. Required for Chinese cryptography compliance projects.

```csharp
// Generate a key pair
SM2Util.GenerateKey(out byte[] pubKey, out byte[] privKey);

// Encrypt
var mode = Mode.C1C3C2;  // Common in Java; C1C2C3 is common in C++
var plain = Encoding.UTF8.GetBytes("SM2 encrypted data");
var enc   = SM2Util.Encrypt(pubKey, privKey, mode, plain);
var dec   = SM2Util.Decrypt(pubKey, privKey, mode, enc);

// Sign / Verify
byte[] signature = SM2Util.Sign(pubKey, privKey, mode, plain);
bool ok = SM2Util.VerifySign(pubKey, privKey, mode, plain, signature);
```

---

## Hashing & Digests

> Hashing = condensing arbitrary data into a fixed-length fingerprint. One-way (irreversible), commonly used for data integrity verification.

### MD5

> MD5 is fast but has known collision risks. **Do not use for password storage or security-sensitive scenarios.** Suitable for file checksums and similar use cases.

```csharp
// String
var hash = MD5Util.GetMD5Str("Hello World");  // Returns lowercase 32-char hex

// Byte array
byte[] hashBytes = MD5Util.GetMD5(Encoding.UTF8.GetBytes("data"));

// File (streaming read, won't blow up memory)
var fileHash = MD5Util.GetMD5StrForFile("large-file.zip");

// Stream
using var stream = File.OpenRead("file.txt");
var streamHash = MD5Util.GetMD5Str(stream);

// 16-bit truncation (middle 8 bytes)
var hash16 = MD5Util.GetMD5Str("data", MD5Length.L16);

// Async (.NET 6+, supports cancellation for large files)
var asyncHash = await MD5Util.GetMD5StrForFileAsync("huge-file.iso");
```

---

### SHA Family

| Algorithm | Output Length | Security | Recommended For |
|-----------|--------------|----------|-----------------|
| SHA1 | 160 bits | Broken | Legacy compatibility only |
| SHA256 | 256 bits | Secure | **General purpose** |
| SHA384 | 384 bits | Secure | High security needs |
| SHA512 | 512 bits | Secure | Maximum security |

```csharp
var sha1   = SHAUtil.GetSHA1("data");
var sha256 = SHAUtil.GetSHA256("data");
var sha384 = SHAUtil.GetSHA384("data");
var sha512 = SHAUtil.GetSHA512("data");
```

---

### SM3 (Chinese Standard)

SM3 is a Chinese national hash algorithm producing a 256-bit digest, with security comparable to SHA256. Use it as a SHA256 replacement in Chinese cryptography compliance scenarios.

```csharp
// Returns byte array
byte[] hash = SM3Util.ToSM3byte("SM3 hash");

// Returns hex string
string hexHash = SM3Util.ToSM3HexStr("SM3 hash");

// HMAC-SM3 with a key
byte[] hmac = SM3Util.ToSM3byte("data", "key");
```

---

### HMAC Message Authentication

HMAC adds a secret key on top of a hash, allowing you to verify both data integrity and the sender's identity.

```csharp
var key = "my-secret-key";

var hmacMd5    = MD5Util.GetHMACMD5("data", key);
var hmacSha1   = SHAUtil.GetSHA1("data", key);
var hmacSha256 = SHAUtil.GetSHA256("data", key);
var hmacSha384 = SHAUtil.GetSHA384("data", key);
var hmacSha512 = SHAUtil.GetSHA512("data", key);
```

---

## One-Time Passwords: TOTP / HOTP

Used for two-factor authentication (2FA). Compatible with apps like Google Authenticator.

```csharp
// TOTP (time-based, refreshes every 30 seconds)
string code = OptUtil.GetTotp("JBSWY3DPEHPK3PXP");

// HOTP (counter-based)
string hotpCode = OptUtil.GetHotp("JBSWY3DPEHPK3PXP", counter: 1);

// Generate OTP URI (for QR code generation)
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

## Data Masking

Mask sensitive information with asterisks to protect user privacy when displaying data.

```csharp
// Generic masking: keep first and last characters, replace middle with *
DesensitizationUtil.CommonDisplay("13812345678");  // "138****5678"

// Name masking
DesensitizationUtil.GetSensitiveName("Zhang Sanfeng");  // "Z*ng"

// Phone number masking
DesensitizationUtil.GetSensitivePhoneNumber("13812345678");  // "138****5678"
```

---

## Numeric ID Encryption

Encrypt integer IDs into unguessable strings — perfect for hiding auto-increment IDs in URLs.

```csharp
// Integer → encrypted string
string encId = DigitalEncryptUtil.FromInt(12345);

// Encrypted string → integer
int decId = DigitalEncryptUtil.ToInt(encId);  // 12345

// Also supports long and decimal
string encLong = DigitalEncryptUtil.FromLong(9999999999L);
long decLong   = DigitalEncryptUtil.ToLong(encLong);

string encDec = DigitalEncryptUtil.FromDecimal(99.99m);
decimal decDec = DigitalEncryptUtil.ToDecimal(encDec);
```

---

## GZip Compression

```csharp
// Byte array compression
byte[] original = Encoding.UTF8.GetBytes("long text to compress...");
byte[] compressed = GZipUtil.Compress(original);
byte[] restored   = GZipUtil.Decompress(compressed);

// String compression (returns Base64)
string zipStr   = GZipUtil.Compress("text to compress");
string unzipStr = GZipUtil.Decompress(zipStr);
```

---

## Base64 Encoding

```csharp
// String → Base64
string b64 = "Hello".ConvertToBase64Str();     // "SGVsbG8="

// Base64 → String
string raw = b64.ConvertToUTF8Str();           // "Hello"

// URL-safe Base64 (replaces +/ with -_, removes =)
string urlSafe = b64.EncodeForUriSafe();
string restored = urlSafe.DecodeForUriSafe();
```

---

## Snowflake ID Generator

Generate globally unique, trend-increasing 64-bit IDs for distributed systems.

```csharp
// Set worker ID (optional, defaults to 1)
SnowflakeIDcreator.SetWorkerID(1);

// Generate ID
long id = SnowflakeIDcreator.NextId();  // e.g. 1425678901234567890
```

---

## MachineKey Generation

For ASP.NET `<machineKey>` configuration section.

```csharp
// decryptionKey (16-48 characters)
string decKey = MachineKeyUtil.CreateDecryptionKey(48);

// validationKey (48-128 characters)
string valKey = MachineKeyUtil.CreateValidationKey(128);
```

---

## WeChat / WeCom

### Message Signature Verification

Verify that a request actually comes from the WeChat server:

```csharp
string signature = SHAUtil.GetSHA1ForWeChat(token, timestamp, nonce, encrypt);
// Compare signature with the one sent by WeChat
bool isValid = signature == requestSignature;
```

### Message Encryption

```csharp
// key: EncodingAESKey configured in WeChat admin console
// receiveId: Corp ID for WeCom, AppId for Official Accounts
string encrypted = AESUtil.EncryptForWeChat(message, key, receiveId);
```

### Message Decryption

```csharp
string decrypted = AESUtil.DecryptForWeChat(encryptedData, key, receiveId);
```

---

## Security Recommendations

| Scenario | Recommended | Avoid |
|----------|-------------|-------|
| Symmetric encryption | AES-256-CBC, SM4 | DES, ECB mode |
| Asymmetric encryption | RSA 2048+, SM2 | RSA 1024 |
| Hashing | SHA256, SHA512, SM3 | MD5 (checksums only), SHA1 |
| Password storage | bcrypt / PBKDF2 / Argon2 | Plain MD5/SHA |
| Random numbers | `RandomNumberGenerator` | `System.Random` |

> **This library is not intended for password storage.** For storing user passwords, use a dedicated password hashing library like BCrypt.Net.

---

## License

[MIT License](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)
