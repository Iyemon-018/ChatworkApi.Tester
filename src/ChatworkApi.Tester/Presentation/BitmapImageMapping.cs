namespace ChatworkApi.Tester.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// あるクラスのプロパティに対して<see cref="BitmapImage"/> をマッピングするための機能を提供するクラスです。
    /// </summary>
    /// <seealso cref="IconCache"/>
    internal static class BitmapImageMapping
    {
        /// <summary>
        /// アカウント情報を持つクラスのシーケンスに対して <see cref="BitmapImage"/> を読み込んで設定します。
        /// </summary>
        /// <typeparam name="T">アカウント情報クラスの型</typeparam>
        /// <param name="items">アカウント情報を持つクラスのシーケンス</param>
        /// <param name="expression"><see cref="BitmapImage"/> を設定するプロパティを識別するための式木</param>
        /// <param name="getIdExpression"><see cref="BitmapImage"/> を取得するための ID を識別するための式木</param>
        /// <param name="getPathExpression"><see cref="BitmapImage"/> を取得するための パス を識別するための式木</param>
        /// <returns></returns>
        public static async Task SetAccountIconToImageProperties<T>(IEnumerable<T>                   items
                                                                  , Expression<Func<T, BitmapImage>> expression
                                                                  , Expression<Func<T, int>>         getIdExpression
                                                                  , Expression<Func<T, string>>      getPathExpression)
        {
            var propertyInfo = expression.GetPropertyInfo();

            foreach (var item in items)
            {
                await SetAccountImageProperty(item, propertyInfo, getIdExpression, getPathExpression).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// チャットルーム情報を持つクラスのシーケンスに対して <see cref="BitmapImage"/> を読み込んで設定します。
        /// </summary>
        /// <typeparam name="T">チャットルーム情報クラスの型</typeparam>
        /// <param name="items">チャットルーム情報を持つクラスのシーケンス</param>
        /// <param name="expression"><see cref="BitmapImage"/> を設定するプロパティを識別するための式木</param>
        /// <param name="getIdExpression"><see cref="BitmapImage"/> を取得するための ID を識別するための式木</param>
        /// <param name="getPathExpression"><see cref="BitmapImage"/> を取得するための パス を識別するための式木</param>
        /// <returns></returns>
        public static async Task SetRoomIconToImageProperties<T>(IEnumerable<T>                   items
                                                               , Expression<Func<T, BitmapImage>> expression
                                                               , Expression<Func<T, int>>         getIdExpression
                                                               , Expression<Func<T, string>>      getPathExpression)
        {
            var propertyInfo = expression.GetPropertyInfo();

            foreach (var item in items)
            {
                await SetRoomImageProperty(item, propertyInfo, getIdExpression, getPathExpression).ConfigureAwait(false);
            }
        }

        private static async Task SetRoomImageProperty<T>(T                           item
                                                        , PropertyInfo                propertyInfo
                                                        , Expression<Func<T, int>>    getIdExpression
                                                        , Expression<Func<T, string>> getPathExpression)
        {
            var id    = (int) getIdExpression.GetPropertyInfo().GetValue(item);
            var path  = (string) getPathExpression.GetPropertyInfo().GetValue(item);
            var image = await IconCache.ToRoomIcon(id, path).ConfigureAwait(false);
            propertyInfo.SetValue(item, image, null);
        }


        private static async Task SetAccountImageProperty<T>(T                           item
                                                           , PropertyInfo                propertyInfo
                                                           , Expression<Func<T, int>>    getIdExpression
                                                           , Expression<Func<T, string>> getPathExpression)
        {
            var id    = (int) getIdExpression.GetPropertyInfo().GetValue(item);
            var path  = (string) getPathExpression.GetPropertyInfo().GetValue(item);
            var image = await IconCache.ToAccountIcon(id, path).ConfigureAwait(false);
            propertyInfo.SetValue(item, image, null);
        }

        private static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            if (!(expression.Body is MemberExpression member)) throw new ArgumentException(nameof(expression));
            if (!(member.Member is PropertyInfo propertyInfo)) throw new ArgumentException(nameof(expression));

            return propertyInfo;
        }
    }
}