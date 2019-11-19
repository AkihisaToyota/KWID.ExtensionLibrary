using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Routing;

namespace KWID.ExtensionLibrary
{
    public static class CommonExtensions
    {
        /// <summary>
        /// SqlDataReaderから指定のカラムの値を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rdr"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetColumnValue<T>(this SqlDataReader rdr, string columnName, T defaultValue = default(T))
        {
            object obj = rdr[columnName];

            if (DBNull.Value.Equals(obj))
            {
                return defaultValue;
            }

            return (T)obj;
        }

        /// <summary>
        /// SqlDataReaderから指定のカラムの値を取得する
        /// </summary>
        /// <param name="rdr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static object GetColumnValue(this SqlDataReader rdr, string columnName)
        {
            object obj = rdr[columnName];

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
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// string.IsNullOrWhiteSpace メソッドと同等。
        /// </summary>
        /// <param name="self">対象文字列</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string self)
        {
            if (self == null || string.IsNullOrEmpty(self.Trim()))
                return true;

            return false;
        }

        /// <summary>
        /// 文字列を string.Compare(strA, strB, true) で比較し、大文字小文字を区別せずに比較する。
        /// </summary>
        /// <param name="strA">対象文字列A</param>
        /// <param name="strB">対象文字列B</param>
        /// <returns>一致すればtrue</returns>
        public static bool EqualsIgnoreCase(this string strA, string strB)
        {
            return string.Compare(strA, strB, true) == 0;
        }

        /// <summary>
        /// string.Splitの文字列指定版
        /// </summary>
        /// <param name="self"></param>
        /// <param name="sepalator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] Split(this string self, string sepalator, StringSplitOptions options = StringSplitOptions.None)
        {
            return self.Split(new string[] { sepalator }, options);
        }

        #endregion

        #region 変換

        /// <summary>
        /// 大文字小文字を問わず、文字列をbool型に変換する。
        /// 「true」をtrueとする。
        /// またオプションで「1」以上をtrueとすることも可能。
        /// </summary>
        /// <returns></returns>
        public static bool ToBool(this string self, bool isConvertNumber = false)
        {
            bool result = self.EqualsIgnoreCase("true");

            if (result)
                return result;

            // 数値変換しない場合はそのまま結果を返す
            if (!isConvertNumber)
                return result;

            if (int.TryParse(self, out int num))
                return num > 0;

            return false;
        }

        /// <summary>
        /// 文字列を数値に変換する。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string self, int defaultValue = default(int))
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
        /// <param name="self">対象文字列</param>
        /// <param name="length">文字数。対象文字列の長さを超えていても可</param>
        /// <returns></returns>
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
        /// <param name="self">対象文字列</param>
        /// <param name="length">文字数。対象文字列の長さを超えていても可</param>
        /// <returns></returns>
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
        /// <param name="trimStr"></param>
        /// <returns></returns>
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
        /// <param name="trimStr"></param>
        /// <returns></returns>
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
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
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
        /// intをbool型に変換する。
        /// 1以上をtrueとする。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ToBool(this int self)
        {
            return self > 0 ? true : false;
        }
        #endregion

        #region DateTime拡張
        /// <summary>
        /// DateTimeオブジェクトがDateTime.MinValueと等価かを判定する。
        /// </summary>
        /// <param name="self">対象DateTimeオブジェクト</param>
        /// <returns></returns>
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
        /// <param name="self">指定 Dictionary</param>
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
        /// <param name="self">指定 Dictionary</param>
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
        /// コレクションがnullまたは要素数が0ならtrueを返す。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || !self.Any();
        }

        /// <summary>
        /// string.Joinメソッドと同等。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> self, string separator)
        {
            if (self == null) return null;

            return string.Join(separator, self.Select(e => e.ToString()).ToArray());
        }

        /// <summary>
        /// HashSetに変換する。その際、値の重複は削除される。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
        {
            if (self == null) return null;

            return new HashSet<T>(self);
        }

        #endregion

        #region Type拡張

        /// <summary>
        /// 指定されたTypeが System.Collections.Generic.IEnumerable<> を継承するものであれば True を返す。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericEnumerable(this Type type)
        {
            if (type == null) return false;

            return type.IsGenericType && type.GetInterfaces().Any(t => t == typeof(IEnumerable<>) || t.Name == "IEnumerable");
        }

        #endregion

        #region object拡張

        /// <summary>
        /// 指定objectのプロパティ群をRouteValueDictionaryとして返す。
        /// 主に new { } のような単純なものに使用する。
        /// new RouetValueDictionary(obj) と同等。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToRouteValueDictionary(this object self)
        {
            return new RouteValueDictionary(self);
        }

        #endregion
    }
}