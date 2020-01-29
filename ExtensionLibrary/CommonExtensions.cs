using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace KWID.ExtensionLibrary
{
    public static class CommonExtensions
    {
        /// <summary>
        /// SqlDataReaderから指定のカラムの値を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="columnName">取得カラム名</param>
        /// <param name="defaultValue">値がNULLのときに代わりにセットする値</param>
        /// <returns>取得された値</returns>
        public static T GetColumnValue<T>(this SqlDataReader self, string columnName, T defaultValue = default(T))
        {
            object obj = self[columnName];

            if (DBNull.Value.Equals(obj))
            {
                return defaultValue;
            }

            return (T)obj;
        }

        /// <summary>
        /// SqlDataReaderから指定のカラムの値を取得する
        /// </summary>
        /// <param name="self"></param>
        /// <param name="columnName">取得カラム名</param>
        /// <returns>取得された値</returns>
        public static object GetColumnValue(this SqlDataReader self, string columnName)
        {
            object obj = self[columnName];

            if (DBNull.Value.Equals(obj))
            {
                return null;
            }

            return obj;
        }

        #region String拡張

        #region メソッド呼び出し

        /// <summary>
        /// string.IsNullOrEmpty メソッドと同等。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>Nullか空文字列ならTrue</returns>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// string.IsNullOrWhiteSpace メソッドと同等。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>NullかTrimした結果が空文字列ならTrue</returns>
        public static bool IsNullOrWhiteSpace(this string self)
        {
            if (self == null || string.IsNullOrEmpty(self.Trim()))
                return true;

            return false;
        }

        /// <summary>
        /// 文字列を string.Compare(strA, strB, True) で比較し、大文字小文字を区別せずに比較する。
        /// </summary>
        /// <param name="strA">検証する文字列</param>
        /// <param name="strB">比較する文字列</param>
        /// <returns>一致すればTure</returns>
        public static bool EqualsIgnoreCase(this string strA, string strB)
        {
            return string.Compare(strA, strB, true) == 0;
        }

        /// <summary>
        /// string.Splitの文字列指定版
        /// </summary>
        /// <param name="self"></param>
        /// <param name="sepalator">分割させる起点の文字列</param>
        /// <param name="options">分割オプション</param>
        /// <returns>sepalatorの値で分割された文字列配列</returns>
        public static string[] Split(this string self, string sepalator, StringSplitOptions options = StringSplitOptions.None)
        {
            return self.Split(new string[] { sepalator }, options);
        }

        #endregion

        #region 変換

        /// <summary>
        /// 文字列 "true" を大文字小文字を問わず、bool型のTrueに変換する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isConvertNumber">"1" 以上の数値文字列をTrueとするか</param>
        /// <returns>変換結果</returns>
        public static bool ToBool(this string self, bool isConvertNumber = false)
        {
            bool result = self.EqualsIgnoreCase("true");

            // 数値変換
            if (isConvertNumber)
            {
                result = self.ToInt(0) >= 1;
            }

            return result;
        }

        /// <summary>
        /// 文字列を数値に変換する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="defaultValue">変換できなかった場合の数値</param>
        /// <returns>変換結果</returns>
        public static int ToInt(this string self, int defaultValue = default)
        {
            if (int.TryParse(self, out int result))
                return result;

            return defaultValue;
        }

        #endregion

        #region 文字列操作

        /// <summary>
        /// 指定された文字数分、左から取得する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="length">取得文字数。対象文字列を超える文字数はすべて取得される。</param>
        /// <returns>処理結果文字列</returns>
        public static string Left(this string self, int length)
        {
            if (self == null) return null;

            length = Math.Max(length, 0);

            if (self.Length > length)
                return self.Substring(0, length);

            return self;
        }

        /// <summary>
        /// 指定された文字数分、右から取得する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="length">取得文字数。対象文字列を超える文字数はすべて取得される。</param>
        /// <returns>処理結果文字列</returns>
        public static string Right(this string self, int length)
        {
            if (self == null) return null;

            length = Math.Max(length, 0);

            if (self.Length > length)
                return self.Substring(self.Length - length, length);

            return self;
        }

        /// <summary>
        /// 指定した文字が先頭にある場合、先頭の指定した文字を削除する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="trimStr">取り除きたい文字列</param>
        /// <returns>処理結果文字列</returns>
        public static string TrimLeft(this string self, string trimStr)
        {
            if (self == null) return null;

            if (!self.StartsWith(trimStr))
                return self;

            return self.Remove(0, trimStr.Length);
        }

        /// <summary>
        /// 指定した文字が後方にある場合、後方の指定した文字を削除する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="trimStr">取り除きたい文字列</param>
        /// <returns>処理結果文字列</returns>
        public static string TrimRight(this string self, string trimStr)
        {
            if (self == null) return null;

            if (!self.EndsWith(trimStr))
                return self;

            return self.Substring(0, self.Length - trimStr.Length);
        }

        /// <summary>
        /// JavaScriptのString.sliceメソッドと同等。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startIndex">開始インデックス</param>
        /// <param name="endIndex">終了インデックス（このインデックスの文字は含まれない）</param>
        /// <returns>処理結果文字列</returns>
        public static string Slice(this string self, int startIndex, int? endIndex = null)
        {
            if (self == null) return null;

            // 空文字列はそのまま
            if (self.IsNullOrEmpty()) return self;

            int start = startIndex;
            int end = endIndex ?? self.Length;

            // マイナス計算
            start = start < 0 ? self.Length + start : start;

            // マイナス計算
            end = end < 0 ? self.Length + end : end;

            // endがstartより前なら空文字列
            if (end < start)
                return string.Empty;

            // startが最小最大値を超えている場合は対処
            start = Math.Max(start, 0);
            start = Math.Min(start, self.Length);

            // endが最小値最大値を超えている場合は対処
            end = Math.Max(end, 0);
            end = Math.Min(end, self.Length);

            int substrLen = end - start;

            return self.Substring(start, substrLen);
        }

        #endregion
        #endregion

        #region int拡張

        /// <summary>
        /// 1以上の数値をbool型のTrueに変換する。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>変換結果</returns>
        public static bool ToBool(this int self)
        {
            return self > 0 ? true : false;
        }
        #endregion

        #region DateTime拡張

        /// <summary>
        /// DateTimeオブジェクトがDateTime.MinValueと等価かを判定する。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>等価ならTrue</returns>
        public static bool IsMinValue(this DateTime self)
        {
            return self == DateTime.MinValue;
        }

        #endregion

        #region Dictionary拡張

        /// <summary>
        /// Dictionary からキーを指定して値を取得する。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TValue">値の型</typeparam>
        /// <param name="self"></param>
        /// <param name="key">指定キー</param>
        /// <returns>指定したキーに対応する値</returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            if (self == null)
                return default(TValue);

            self.TryGetValue(key, out TValue result);
            return result;

        }

        /// <summary>
        /// Dictionary からキーを指定して値を取得する。デフォルト値を指定できる。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TValue">値の型</typeparam>
        /// <param name="self"></param>
        /// <param name="key">指定キー</param>
        /// <param name="defaultValue">取得できなかった場合のデフォルトの値</param>
        /// <returns>指定したキーに対応する値</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
        {
            if (self == null)
                return defaultValue;

            if (self.TryGetValue(key, out TValue result))
                return result;

            return defaultValue;
        }

        #endregion

        #region IEnumerable拡張

        /// <summary>
        /// コレクションがnullまたは要素数が0ならTrueを返す。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="self"></param>
        /// <returns>コレクションがnullまたは要素数0ならTrue</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || !self.Any();
        }

        /// <summary>
        /// string.Joinメソッドと同等。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="self"></param>
        /// <param name="separator">区切り文字</param>
        /// <returns>連結結果</returns>
        public static string JoinString<T>(this IEnumerable<T> self, string separator)
        {
            if (self == null) return null;

            return string.Join(separator, self.Select(e => e.ToString()).ToArray());
        }

        /// <summary>
        /// HashSetに変換する。その際、値の重複は削除される。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="self"></param>
        /// <returns>変換結果</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
        {
            if (self == null) return null;

            return new HashSet<T>(self);
        }

        #endregion

        #region Type拡張

        /// <summary>
        /// 指定されたTypeが System.Collections.Generic.IEnumerable&lt;&gt; を継承するものであれば True を返す。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;&gt; を継承するものであれば True</returns>
        public static bool IsGenericEnumerable(this Type self)
        {
            if (self == null) return false;

            return self.IsGenericType && self.GetInterfaces().Any(t => t == typeof(IEnumerable<>) || t.Name == "IEnumerable");
        }

        #endregion

        #region Exception拡張

        /// <summary>
        /// 中の例外メッセージを含めて取得する。
        /// 外側の例外メッセージから表示される。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>取得結果</returns>
        public static string GetMessages(this Exception self)
        {
            IEnumerable<string> getAllMessage()
            {
                var ex = self;
                while (ex != null)
                {
                    yield return ex.Message;
                    ex = ex.InnerException;
                }
            };

            return getAllMessage().Where(e => !e.IsNullOrWhiteSpace()).JoinString(Environment.NewLine);
        }

        #endregion

        #region object拡張

        /// <summary>
        /// 指定objectのプロパティ群をRouteValueDictionaryとして返す。
        /// 主に new { } のような単純なものに使用する。
        /// new RouetValueDictionary(obj) と同等。
        /// </summary>
        /// <param name="self"></param>
        /// <returns>変換結果</returns>
        public static IDictionary<string, object> ToRouteValueDictionary(this object self)
        {
            return new RouteValueDictionary(self);
        }

        #endregion
    }
}