namespace ChatworkApi.Tester.Domain
{
    using System.Security.Cryptography;
    using ArtisanCode.SimpleAesEncryption;

    /// <summary>
    /// AES を使用した復号化を行うためのクラスです。
    /// </summary>
    public sealed class Decryption : IDecryption
    {
        private readonly string _key;

        private readonly int _keySize;

        public Decryption(string key
                        , int    keySize)
        {
            _key     = key;
            _keySize = keySize;
        }

        /// <summary>
        /// 指定された文字列を復号化します。
        /// </summary>
        /// <param name="value">暗号化された文字列</param>
        /// <returns>復号化した結果を返します。</returns>
        public string Decrypt(string value)
        {
            var config = new SimpleAesEncryptionConfiguration
                         {
                             EncryptionKey = new EncryptionKeyConfigurationElement(_keySize, _key)
                           , CipherMode    = CipherMode.CBC
                           , Padding       = PaddingMode.ISO10126
                         };

            return new RijndaelMessageDecryptor(config).Decrypt(value);
        }
    }
}