namespace ChatworkApi.Tester.Domain
{
    using System.Security.Cryptography;
    using ArtisanCode.SimpleAesEncryption;

    /// <summary>
    /// AES で暗号化するためのクラスです。
    /// </summary>
    public sealed class Encryption : IEncryption
    {
        private readonly string _key;

        private readonly int _keySize;

        public Encryption(string key
                        , int    keySize)
        {
            _key     = key;
            _keySize = keySize;
        }

        /// <summary>
        /// 指定された文字列を暗号化します。
        /// </summary>
        /// <param name="value">暗号化対象の文字列</param>
        /// <returns>暗号化した結果を返します。</returns>
        public string Encrypt(string value)
        {
            var config = new SimpleAesEncryptionConfiguration
                         {
                             EncryptionKey = new EncryptionKeyConfigurationElement(_keySize, _key)
                           , CipherMode    = CipherMode.CBC
                           , Padding       = PaddingMode.ISO10126
                         };

            return new RijndaelMessageEncryptor(config).Encrypt(value);
        }
    }
}