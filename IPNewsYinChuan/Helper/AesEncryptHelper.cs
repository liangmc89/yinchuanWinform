using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;


/// <summary>
///AesEncryptHelper 的摘要说明
/// </summary>
public class AesEncryptHelper
{
    public AesEncryptHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    //默认密钥向量 
    //private static byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    /// <summary>
    /// AES加密算法
    /// </summary>
    /// <param name="plainText">明文字符串</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回加密后的密文字节数组</returns>
    public static byte[] AESEncrypt(string plainText, string strKey, string iv)
    {
        //分组加密算法
        SymmetricAlgorithm des = Rijndael.Create();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组	
        //设置密钥及密钥向量
        des.Key = Encoding.UTF8.GetBytes(strKey);
        des.IV = Encoding.UTF8.GetBytes(iv);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
        cs.Close();
        ms.Close();
        return cipherBytes;
    }

    /// <summary>
    /// AES加密算法
    /// </summary>
    /// <param name="plainText">明文字符串</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回加密后的密文字符串</returns>
    public static string AESEncryptToString(string plainText, string strKey, string iv)
    {
        //分组加密算法
        SymmetricAlgorithm des = Rijndael.Create();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组	
        //设置密钥及密钥向量
        des.Key = Encoding.UTF8.GetBytes(strKey);
        des.IV = Encoding.UTF8.GetBytes(iv);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
        cs.Close();
        ms.Close();
        //return cipherBytes;
        return Convert.ToBase64String(cipherBytes);
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="cipherText">密文字节数组</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回解密后的字符串</returns>
    public static byte[] AESDecrypt(byte[] cipherText, string strKey, string iv)
    {
        SymmetricAlgorithm des = Rijndael.Create();
        des.Key = Encoding.UTF8.GetBytes(strKey);
        des.IV = Encoding.UTF8.GetBytes(iv);
        byte[] decryptBytes = new byte[cipherText.Length];
        MemoryStream ms = new MemoryStream(cipherText);
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
        cs.Read(decryptBytes, 0, decryptBytes.Length);
        cs.Close();
        ms.Close();
        return decryptBytes;
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="cipherText">密文字节串</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回解密后的字符串</returns>
    public static string AESDecryptToString(string cipherTextStr, string strKey, string iv)
    {
        byte[] cipherText = Convert.FromBase64String(cipherTextStr);
        SymmetricAlgorithm des = Rijndael.Create();
        des.Key = Encoding.UTF8.GetBytes(strKey);
        des.IV = Encoding.UTF8.GetBytes(iv);
        byte[] decryptBytes = new byte[cipherText.Length];
        MemoryStream ms = new MemoryStream(cipherText);
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
        cs.Read(decryptBytes, 0, decryptBytes.Length);
        cs.Close();
        ms.Close();
        return Encoding.UTF8.GetString(decryptBytes).TrimEnd('\0');
    }
}