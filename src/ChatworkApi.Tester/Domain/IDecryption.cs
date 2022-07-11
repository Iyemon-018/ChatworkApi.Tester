namespace ChatworkApi.Tester.Domain
{
    /// <summary>
    /// 暗号化された情報を復号化するためのインターフェースです。
    /// </summary>
    public interface IDecryption
    {
        /// <summary>
        /// 指定された文字列を復号化します。
        /// </summary>
        /// <param name="value">暗号化された文字列</param>
        /// <returns>復号化した結果を返します。</returns>
        string Decrypt(string value);
    }
}