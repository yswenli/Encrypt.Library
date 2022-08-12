[![NuGet](https://img.shields.io/nuget/v/Encrypt.Library.svg)](https://nuget.org/packages/Encrypt.Library)
[![NET 6.0](https://img.shields.io/badge/.NET-6.0-brightgreen)](https://www.microsoft.com/net/core)
[![NetStandard 2.1](https://img.shields.io/badge/NetStandard-2.1-orange.svg)](https://www.microsoft.com/net/core)
[![license](https://img.shields.io/github/license/myloveCc/Encrypt.Library.svg)](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)


NETCore encrypt and decrypt tool，Include AES，RSA，MD5，SAH1，SAH256，SHA384，SHA512 and more

To install NETCore.Encrypt, run the following command in the [Package Manager Console](https://docs.microsoft.com/zh-cn/nuget/tools/package-manager-console)



## Package Manager
```
Install-Package Encrypt.Library -Version 1.0.1.3
```
## .NET CLI
```
dotnet add package Encrypt.Library -Version 1.0.1.3
```

## PackageReference
```
<PackageReference Include="Encrypt.Library" Version="1.0.0.3" />
```


***

# Easy to use with `Encrypt.Library`

## AES

#### Create AES Key

  ```csharp
  var aesKey = AESUtil.Key;
  
  var key = aesKey.Key;
  var iv = aesKey.IV;
  ```

#### AES encrypt
  - AES encrypt without iv (ECB mode)

    ```csharp
    var srcString = "aes encrypt";
    var encrypted = AESUtil.Encrypt(srcString, key);

    ```
  - AES encrypt with iv (CBC mode)

    ```csharp
    var srcString = "aes encrypt";
    var encrypted = AESUtil.Encrypt(srcString, key, iv);

    ```
  - AES encrypt bytes with iv (CBC mode)

    ```csharp
    var srcBytes = new byte[]{xxx};
    var encryptedBytes = AESUtil.Encrypt(srcBytes, key, iv);

    ```
#### ASE decrypt

  - AES decrypt without iv (ECB mode)
    
    ```csharp
    var encryptedStr = "xxxx";
    var decrypted = AESUtil.Decrypt(encryptedStr, key);
    ```
  
  - AES decrypt with iv (CBC mode)
   
    ```csharp
    var encryptedStr = "xxxx";
    var decrypted = AESUtil.Decrypt(encryptedStr, key, iv);
    ```

  - AES decrypt bytes with iv (CBC mode)
   
    ```csharp
    var encryptedBytes =  new byte[]{xxx};
    var decryptedBytes = AESUtil.Decrypt(encryptedBytes, key, iv);
    ```

## DES

- #### Create DES Key

  ```csharp
  
  //des key length is 24 bit
  var desKey = DESUtil.Key;
  
  ```
- #### Create DES Iv 【NEW】

  ```csharp
  
  //des iv length is 8 bit
  var desIv = DESUtil.Iv;
  
  ```

- #### DES encrypt (ECB mode)

    ```csharp
    var srcString = "des encrypt";
    var encrypted = DESUtil.Encrypt(srcString, key);
    ```
- #### DES encrypt bytes (ECB mode)
   
    ```csharp
    var srcBytes =  new byte[]{xxx};
    var decryptedBytes = DESUtil.Encrypt(srcBytes, key);
    ```
- #### DES decrypt (ECB mode)

    ```csharp
    var encryptedStr = "xxxx";
    var decrypted = DESUtil.Decrypt(encryptedStr, key);
    ```

- #### DES decrypt bytes  (ECB mode)

    ```csharp
    var encryptedBytes =  new byte[]{xxx};
    var decryptedBytes = DESUtil.Decrypt(encryptedBytes, key);
    ```

- #### DES encrypt bytes with iv (CBC mode)【NEW】

    ```csharp
    var srcBytes =  new byte[]{xxx};
    var encrypted = DESUtil.Encrypt(srcBytes, key, iv);
    ```

- #### DES decrypt bytes with iv (CBC mode)【NEW】

    ```csharp
    var encryptedBytes =  new byte[]{xxx};
    var encrypted = DESUtil.Decrypt(encryptedBytes, key, iv);
    ```

## RSA
  
  - #### Create RSA Key with RsaSize

    ```csharp
    var rsaKey = RSAUtil.Key;    //default is 2048

	// var rsaKey = EncryptProvider.CreateRsaKey(RsaSize.R3072);

    var publicKey = rsaKey.PublicKey;
    var privateKey = rsaKey.PrivateKey;
    var exponent = rsaKey.Exponent;
    var modulus = rsaKey.Modulus;
    ```
	  
  - #### Rsa Sign and Verify method

    ```csharp
	string rawStr = "xxx";
    string signStr = RSAUtil.Sign(rawStr, privateKey);
    bool   result = RSAUtil.Verify(rawStr, signStr, publicKey);
    ```

  - #### RSA encrypt
  
    ```csharp
    var publicKey = rsaKey.PublicKey;
    var srcString = "rsa encrypt";

    
    var encrypted = RSAUtil.Encrypt(publicKey, srcString);

    // On mac/linux at version 2.0.5
    var encrypted = RSAUtil.Encrypt(publicKey, srcString, RSAEncryptionPadding.Pkcs1);

    ```
  
  - #### RSA decrypt

    ```csharp
    var privateKey = rsaKey.PrivateKey;
    var encryptedStr = "xxxx";

    var decrypted = RSAUtil.Decrypt(privateKey, encryptedStr);

    // On mac/linux at version 2.0.5
    var decrypted = RSAUtil.Decrypt(privateKey, encryptedStr, RSAEncryptionPadding.Pkcs1);
    ```

  - #### RSA from string 

    ```csharp
    var privateKey = rsaKey.PrivateKey;
    RSA rsa = RSAUtil.FromString(privateKey);
    ```

   - #### RSA with PEM

     ```csharp

	 //Rsa to pem format key

	 //PKCS1 pem
	 var pkcs1KeyTuple = RSAUtil.GetPem(false);
	 var publicPem = pkcs1KeyTuple.publicPem;
	 var privatePem = pkcs1KeyTuple.privatePem;

	 //PKCS8 pem
	 var pkcs8KeyTuple = RSAUtil.GetPem(true);
	 publicPem = pkcs8KeyTuple.publicPem;
	 privatePem = pkcs8KeyTuple.privatePem;

	 //Rsa encrypt and decrypt with pem key

	 var rawStr = "xxx";
	 var enctypedStr = RSAUtil.EncryptWithPem(pemPublicKey, rawStr);
	 var decryptedStr = RSAUtil.DecryptWithPem(pemPrivateKey, enctypedStr);

	 ```

  ## MD5
  
  ```csharp
  
  var srcString = "Md5 hash";
  var hashed = MD5Util.GetMD5Str(srcString);
  
  ```
  
  ```csharp
  
  var srcString = "Md5 hash";
  var hashed = MD5Util.GetMd5Str(srcString);
  
  ```
  
  ## SHA
  
  - #### SHA1
    ```csharp
    var srcString = "sha hash";    
    var hashed = SHAUtil.GetSHA1(srcString); 
    ```
  - #### SHA256
    ```csharp  
    var srcString = "sha hash";    
    var hashed = SHAUtil.GetSHA256(srcString); 
    ```  
  - #### SHA384
    ```csharp  
    var srcString = "sha hash";    
    var hashed = SHAUtil.GetSHA384(srcString); 
    ```
  - #### SHA512
    ```csharp
    var srcString = "sha hash";    
    var hashed = SHAUtil.GetSHA512(srcString);
    ```
  
  ## HMAC
  
  - #### HMAC-MD5
    ```csharp
    var key="xxx";
    var srcString = "hmac md5 hash";     
    var hashed = MD5Util.GetHMACMD5(srcString,key);
    ```
  - #### HMAC-SHA1
    ```csharp
    var key="xxx";
    var srcString = "hmac sha hash";    
    var hashed = SHAUtil.GetSHA1(srcString,key);
    ```
  - #### HMAC-SHA256
    ```csharp
    var key="xxx";
    var srcString = "hmac sha hash";    
    var hashed = SHAUtil.GetSHA256(srcString,key);
    ```
  - #### HMAC-SHA384
    ```csharp
    var key="xxx";
    var srcString = "hmac sha hash";    
    var hashed = SHAUtil.GetSHA384(srcString,key);
    ```
  - #### HMAC-SHA512
    ```csharp
    var key="xxx";
    var srcString = "hmac sha hash";    
    var hashed = SHAUtil.GetSHA512(srcString，key);
    ```
 

  - #### SM3
    ```csharp
    byte[] bytes=SM3Util.ToSM3byte("Encrypt.Library");
    ```
## SM2
  - #### SM2
    ```csharp
    byte[] pubkey,privkey;
    SM2Util.GenerateKey(out pubkey, out privkey);
    var bytes = Encoding.UTF8.GetByte("Encrypt.Library");    
    var encrypt = SM2Util.Encrypt(pubkey,privkey,Mode.C1C2C3,bytes);
    var decrypt = SM2Util.Decrypt(pubkey,privkey,Mode.C1C2C3,encrypt);
    ```

## SM4

- #### Create SM4 Key

  ```csharp
  
  //des key length is 24 bit
  var desKey = SM4Util.Key;
  
  ```
- #### Create SM4 Iv 【NEW】

  ```csharp
  
  //des iv length is 8 bit
  var desIv = SM4Util.Iv;
  
  ```

- #### SM4 encrypt (ECB mode)

    ```csharp
    var srcString = "sm4 encrypt";
    var encrypted = SM4Util.Encrypt(key, srcString);
    ```
- #### SM4 encrypt bytes (ECB mode)
   
    ```csharp
    var srcBytes =  new byte[]{xxx};
    var decryptedBytes = SM4Util.Encrypt(key, srcBytes);
    ```
- #### SM4 decrypt (ECB mode)

    ```csharp
    var encryptedStr = "xxxx";
    var decrypted = SM4Util.Decrypt(key, encryptedStr);
    ```

- #### SM4 decrypt bytes  (ECB mode)

    ```csharp
    var encryptedBytes =  new byte[]{xxx};
    var decryptedBytes = SM4Util.Decrypt(key, encryptedBytes);
    ```

- #### SM4 encrypt bytes with iv (CBC mode)【NEW】

    ```csharp
    var srcBytes =  new byte[]{xxx};
    var encrypted = SM4Util.Encrypt(key, iv, srcBytes);
    ```

- #### SM4 decrypt bytes with iv (CBC mode)【NEW】

    ```csharp
    var encryptedBytes =  new byte[]{xxx};
    var encrypted = SM4Util.Decrypt(key, iv, encryptedBytes);
    ```


# LICENSE

[MIT License](https://github.com/yswenli/Encrypt.Library/blob/master/LICENSE.txt)

