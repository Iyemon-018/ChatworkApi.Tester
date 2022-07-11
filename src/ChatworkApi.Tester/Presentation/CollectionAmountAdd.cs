namespace ChatworkApi.Tester.Presentation
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public enum ItemAddOrientation
    {
        First

      , Last

       ,
    }

    /// <summary>
    /// コレクションに対して一定数量ずつ要素を追加するためのクラスです。
    /// </summary>
    /// <typeparam name="T">コレクションの要素の型</typeparam>
    public sealed class CollectionAmountAdd<T>
    {
        /// <summary>
        /// <see cref="_target" /> に対していくつ追加するか
        /// </summary>
        private readonly int _incrementCount;

        /// <summary>
        /// <see cref="_target" /> に追加する要素のシーケンス
        /// </summary>
        private readonly List<T> _originalSource;

        /// <summary>
        /// 追加対象のコレクション
        /// </summary>
        private readonly Collection<T> _target;

        private readonly ItemAddOrientation _orientation;

        /// <summary>
        /// 現在追加した数
        /// </summary>
        private int _current;

        public CollectionAmountAdd(IEnumerable<T>     originalSource
                                 , Collection<T>      target
                                 , int                incrementCount
                                 , ItemAddOrientation orientation = ItemAddOrientation.Last)
        {
            _originalSource = new List<T>(originalSource);
            _target         = target;
            _incrementCount = incrementCount;
            _orientation    = orientation;
        }

        /// <summary>
        /// すべての要素を追加したかどうかを取得します。
        /// </summary>
        public bool AllAdded { get; private set; }

        /// <summary>
        /// 指定した数量だけ、要素を追加します。
        /// </summary>
        public IEnumerable<T> Execute()
        {
            // 要素をすべて追加している場合は即時切り上げて処理効率を向上させる。
            if (AllAdded) return Enumerable.Empty<T>();

            var collection = _originalSource.Skip(_current).Take(_incrementCount).ToArray();

            if (!collection.Any())
            {
                AllAdded = true;
                return Enumerable.Empty<T>();
            }

            foreach (var item in collection)
            {
                if (_orientation == ItemAddOrientation.First)
                {
                    _target.Insert(0, item);
                }
                else if (_orientation == ItemAddOrientation.Last)
                {
                    _target.Add(item);
                }

                _current++;
            }

            return collection;
        }

        public async Task<IEnumerable<T>> ExecuteAsync() => await Task.Run(Execute);

        public void AddSource(IEnumerable<T> items)
        {
            _originalSource.AddRange(items);
            AllAdded = false;
        }
    }
}