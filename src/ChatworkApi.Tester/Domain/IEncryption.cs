namespace ChatworkApi.Tester.Domain
{
    /// <summary>
    /// 情報を暗号化するためのインターフェースです。
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// 指定された文字列を暗号化します。
        /// </summary>
        /// <param name="value">暗号化対象の文字列</param>
        /// <returns>暗号化した結果を返します。</returns>
        string Encrypt(string value);
    }
}