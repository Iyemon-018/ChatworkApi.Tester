namespace ChatworkApi.Tester.Presentation.ComponentModels
{
    using System.Windows.Input;

    /// <summary>
    /// View の Loaded 実行時に実行可能なコマンドを保有しているインターフェースです。
    /// </summary>
    public interface IViewLoadedHolder
    {
        /// <summary>
        /// Loaded イベントの実行時に呼ばれるコマンドを取得します。
        /// </summary>
        ICommand LoadedCommand { get; }
    }
}