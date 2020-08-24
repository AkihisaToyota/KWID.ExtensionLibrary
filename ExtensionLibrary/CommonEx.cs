using System;
using System.Collections.Generic;
using System.IO;

namespace KWID.ExtensionLibrary
{
    /// <summary>
    /// 汎用的な利便性の高いメソッドを追加するクラスです。
    /// </summary>
    public static class CommonEx
    {

        /// <summary>
        /// try 句を使用して代入式を使用できます。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="func">try 処理</param>
        /// <param name="defaultValue">例外時のデフォルト値</param>
        /// <returns></returns>
        public static T TryFunc<T>(Func<T> func, T defaultValue = default)
        {
            try
            {
                return func();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// try 句を使用した処理を行えます。
        /// </summary>
        /// <typeparam name="T">catch する例外の型</typeparam>
        /// <param name="tryAction">try 処理</param>
        /// <param name="catchAction">catch 処理</param>
        /// <param name="finallyAction">finally 処理</param>
        public static void TryAction<T>(Action tryAction, Action<T> catchAction = null, Action finallyAction = null)
            where T : Exception
        {
            try
            {
                tryAction?.Invoke();
            }
            catch (Exception ex)
            {
                catchAction?.Invoke((T)ex);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        /// <summary>
        /// try 句を使用した処理を行えます。
        /// </summary>
        /// <param name="tryAction">try 処理</param>
        /// <param name="catchAction">catch 処理</param>
        /// <param name="finallyAction">finally 処理</param>
        public static void TryAction(Action tryAction, Action<Exception> catchAction = null, Action finallyAction = null)
        {
            TryAction<Exception>(tryAction, catchAction, finallyAction);
        }
    }
}