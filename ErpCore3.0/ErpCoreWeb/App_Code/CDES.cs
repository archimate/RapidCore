using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

    public class CDES
    {
        /// <summary>   
        /// 对字节进行DES加密   
        /// </summary>   
        /// <param name="p_Value">字节数组</param>   
        /// <param name="p_Key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>   
        /// <param name="p_IV">向量 如果为NULL 向量和钥匙是一个</param>   
        /// <returns>加密后的BYTE</returns>   
        public static byte[] DESEncoder(byte[] p_Value, byte[] p_Key, byte[] p_IV)
        {
            byte[] _RgbKey = p_Key;
            byte[] _RgbIV = p_IV;

            if (_RgbKey == null || _RgbKey.Length != 8) _RgbKey = new byte[] { 0x7A, 0x67, 0x6B, 0x65, 0x40, 0x73, 0x69, 0x6E };
            if (_RgbIV == null) _RgbIV = _RgbKey;

            DESCryptoServiceProvider _Desc = new DESCryptoServiceProvider();
            ICryptoTransform _ICrypto = _Desc.CreateEncryptor(_RgbKey, _RgbIV);

            return _ICrypto.TransformFinalBlock(p_Value, 0, p_Value.Length);
        }
        /// <summary>   
        /// 对字节进行DES解密   
        /// </summary>   
        /// <param name="p_Value">字节数组</param>   
        /// <param name="p_Key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>   
        /// <param name="p_IV">向量 如果为NULL 向量和钥匙是一个</param>   
        /// <returns>解密后的BYTE</returns>   
        public static byte[] DESDecoder(byte[] p_Value, byte[] p_Key, byte[] p_IV)
        {
            byte[] _RgbKey = p_Key;
            byte[] _RgbIV = p_IV;

            if (_RgbKey == null || _RgbKey.Length != 8) _RgbKey = new byte[] { 0x7A, 0x67, 0x6B, 0x65, 0x40, 0x73, 0x69, 0x6E };
            if (_RgbIV == null) _RgbIV = _RgbKey;

            DESCryptoServiceProvider _Desc = new DESCryptoServiceProvider();
            ICryptoTransform _ICrypto = _Desc.CreateDecryptor(_RgbKey, _RgbIV);

            return _ICrypto.TransformFinalBlock(p_Value, 0, p_Value.Length);
        }

        /// <summary>   
        /// DES加密   
        /// </summary>   
        /// <param name="enStr">原始数据</param>   
        /// <param name="p_TextEncoding">数据编码</param>   
        /// <param name="p_Key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>   
        /// <param name="p_IV">向量 如果为NULL 向量和钥匙是一个</param>   
        /// <returns>加密后的字符穿 00-00-00</returns>   
        public static string DESEncoder(string p_TextValue, System.Text.Encoding p_TextEncoding, byte[] p_Key, byte[] p_IV)
        {
            byte[] _DataByte = p_TextEncoding.GetBytes(p_TextValue);
            return BitConverter.ToString(DESEncoder(_DataByte, p_Key, p_IV));
        }

        /// <summary>   
        /// DES解密   
        /// </summary>   
        /// <param name="p_TextValue">经过加密数据</param>   
        /// <param name="p_TextEncoding">数据编码</param>   
        /// <param name="p_Key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>   
        /// <param name="p_IV">向量 如果为NULL 向量和钥匙是一个</param>   
        /// <returns>解密后的字符穿</returns>   
        public static string DESDecoder(string p_TextValue, System.Text.Encoding p_TextEncoding, byte[] p_Key, byte[] p_IV)
        {

            string[] _ByteText = p_TextValue.Split('-');
            byte[] _DataByte = new byte[_ByteText.Length];
            for (int i = 0; i != _ByteText.Length; i++)
            {
                _DataByte[i] = Convert.ToByte(_ByteText[i], 16);
            }
            return p_TextEncoding.GetString(DESDecoder(_DataByte, p_Key, p_IV));
        }
    }
