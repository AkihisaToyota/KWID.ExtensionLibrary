using System.Collections.Generic;

namespace KWID.ExtensionLibrary
{
    /// <summary>
    /// 主にコレクションに関する利便性の高い拡張メソッドを追加するクラスです。
    /// </summary>
    public static class CollectionEx
    {
        /// <summary>
        /// 新しく配列を作成します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="items">配列の要素</param>
        /// <returns>作成した配列</returns>
        public static T[] NewArray<T>(params T[] items)
            => items;

        /// <summary>
        /// 新しくList&lt;T&gt;を作成します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="items">List&lt;T&gt;の要素</param>
        /// <returns>作成したList&lt;T&gt;</returns>
        public static List<T> NewList<T>(params T[] items)
            => new List<T>(items);

        /// <summary>
        /// 新しくHashSet&lt;T&gt;を作成します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="items">HashSet&lt;T&gt;の要素</param>
        /// <returns>作成したHashSet&lt;T&gt;</returns>
        public static HashSet<T> NewHashSet<T>(params T[] items)
            => new HashSet<T>(items);
    }
}