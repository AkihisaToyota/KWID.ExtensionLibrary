using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Routing;
#pragma warning disable CS1573 // パラメーターには XML コメント内に対応する param タグがありませんが、他のパラメーターにはあります

namespace KWID.ExtensionLibrary
{
    /// <summary>
    /// 様々な拡張メソッドを追加するクラスです。
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// SqlDataReaderから指定のカラムの値を取得します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        /// SqlDataReaderから指定のカラムの値を取得します。
        /// </summary>
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
        /// string.IsNullOrEmpty メソッドと同等です。
        /// </summary>
        /// <returns>Nullか空文字列ならTrue</returns>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// string.IsNullOrWhiteSpace メソッドと同等です。
        /// </summary>
        /// <returns>NullかTrimした結果が空文字列ならTrue</returns>
        public static bool IsNullOrWhiteSpace(this string self)
        {
            if (self == null || string.IsNullOrEmpty(self.Trim()))
                return true;

            return false;
        }

        /// <summary>
        /// 文字列を string.Compare(strA, strB, True) で比較し、大文字小文字を区別せずに比較します。
        /// </summary>
        /// <param name="strA">検証する文字列</param>
        /// <param name="strB">比較する文字列</param>
        /// <returns>一致すればTure</returns>
        public static bool EqualsIgnoreCase(this string strA, string strB)
        {
            return string.Compare(strA, strB, true) == 0;
        }

        /// <summary>
        /// string.Splitのセパレータに文字列を指定できます。
        /// </summary>
        /// <param name="separator">分割させる起点の文字列</param>
        /// <param name="options">分割オプション</param>
        /// <returns>sepalatorの値で分割された文字列配列</returns>
        public static string[] Split(this string self, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return self.Split(new string[] { separator }, options);
        }

        #endregion

        #region 代入

        /// <summary>
        /// 文字列が null または空の場合、指定した文字列を返却します。
        /// 当てはまらない場合は変換せずそのまま返却します。
        /// </summary>
        /// <param name="defaultValue">null または空の場合に返却する値</param>
        /// <returns>変換結果</returns>
        public static string DefaultIfNullOrEmpty(this string self, string defaultValue)
        {
            if (self.IsNullOrEmpty())
                return defaultValue;
            return self;
        }

        /// <summary>
        /// 文字列が null または空、空白の場合、指定した文字列を返却します。
        /// 当てはまらない場合は変換せずそのまま返却します。
        /// </summary>
        /// <param name="defaultValue">null または空、空白の場合に返却する値</param>
        /// <returns>変換結果</returns>
        public static string DefaultIfNullOrWhiteSpace(this string self, string defaultValue)
        {
            if (self.IsNullOrWhiteSpace())
                return defaultValue;
            return self;
        }

        #endregion

        #region 変換

        /// <summary>
        /// 文字列 "true" をbool型のTrueに変換します。（IgnoreCase）
        /// </summary>
        /// <param name="isConvertNumeric">1以上の数値文字列をTrueとする場合はTrue</param>
        /// <returns>変換結果</returns>
        public static bool ToBool(this string self, bool isConvertNumeric = false)
        {
            bool result = self.EqualsIgnoreCase("true");

            // 数値変換
            if (isConvertNumeric)
            {
                result = self.ToInt(0) >= 1;
            }

            return result;
        }

        /// <summary>
        /// 文字列を数値に変換します。
        /// </summary>
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
        /// 指定された文字数分、左から取得します。
        /// </summary>
        /// <param name="length">取得文字数（文字数超過、0未満の場合は丸められます）</param>
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
        /// 指定された文字数分、右から取得します。
        /// </summary>
        /// <param name="length">取得文字数（文字数超過、0未満の場合は丸められます）</param>
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
        /// 指定した文字が先頭にある場合、先頭の指定した文字を削除します。
        /// </summary>
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
        /// 指定した文字が後方にある場合、後方の指定した文字を削除します。
        /// </summary>
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
        /// JavaScriptのString.sliceメソッドと同等です。
        /// </summary>
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

        /// <summary>
        /// セパレータに指定した文字列を指定した文字数区切りに追加します。
        /// </summary>
        /// <param name="separator">セパレーター</param>
        /// <param name="separateLength">区切りたい文字数</param>
        /// <returns>セパレーター追加結果</returns>
        public static string AddSeparator(this string self, string separator, int separateLength)
        {
            if (self == null) return null;

            return self.Chunk(separateLength).Select(e => e.NewString()).JoinString(separator);
        }

        #endregion
        #endregion

        #region int拡張

        /// <summary>
        /// 1以上の数値をbool型のTrueに変換します。
        /// </summary>
        /// <returns>変換結果</returns>
        public static bool ToBool(this int self)
        {
            return self > 0 ? true : false;
        }
        #endregion

        #region DateTime拡張

        /// <summary>
        /// DateTimeオブジェクトがDateTime.MinValueと等価かを判定します。
        /// </summary>
        /// <returns>等価ならTrue</returns>
        public static bool IsMinValue(this DateTime self)
        {
            return self == DateTime.MinValue;
        }

        #endregion

        #region Dictionary拡張

        /// <summary>
        /// Dictionaryからキーを指定して値を取得します。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TValue">値の型</typeparam>
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
        /// Dictionaryからキーを指定して値を取得します。取得できなかった場合はデフォルト値を返します。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TValue">値の型</typeparam>
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
        /// コレクションがnullまたは要素数が0ならTrueを返します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <returns>コレクションがnullまたは要素数0ならTrue</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || !self.Any();
        }

        /// <summary>
        /// string.Joinメソッドと同等です。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="separator">区切り文字</param>
        /// <returns>連結結果</returns>
        public static string JoinString<T>(this IEnumerable<T> self, string separator)
        {
            if (self == null) return null;

            return string.Join(separator, self.Select(e => e.ToString()).ToArray());
        }

        /// <summary>
        /// HashSetに変換します。その際、値の重複は削除れます。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <returns>変換結果</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
        {
            if (self == null) return null;

            return new HashSet<T>(self);
        }

        /// <summary>
        /// コレクションを指定したサイズに分割します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="size">分割結果</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> self, int size)
        {
            if (self == null) yield return null;

            while (self.Any())
            {
                yield return self.Take(size);
                self = self.Skip(size);
            }
        }

        #endregion

        #region IEnumerable<char>

        /// <summary>
        /// new string(charArray) と同等です。
        /// </summary>
        /// <returns>変換結果</returns>
        public static string NewString(this IEnumerable<char> self)
        {
            if (self == null) return null;

            return new string(self.ToArray());
        }

        #endregion

        #region Type拡張

        /// <summary>
        /// 指定されたTypeが System.Collections.Generic.IEnumerable&lt;&gt; を継承するものかどうかを判定します。
        /// </summary>
        /// <returns>System.Collections.Generic.IEnumerable&lt;&gt; を継承するものであれば True</returns>
        public static bool IsGenericEnumerable(this Type self)
        {
            if (self == null) return false;

            return self.IsGenericType && self.GetInterfaces().Any(t => t == typeof(IEnumerable<>) || t.Name == nameof(IEnumerable<object>));
        }

        #endregion

        #region Exception拡張

        /// <summary>
        /// 中の例外メッセージを含めて取得します。
        /// 外側の例外メッセージから表示される。
        /// </summary>
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

        ///// <summary>
        ///// 指定objectのプロパティ群をRouteValueDictionaryとして返す。
        ///// 主に new { } のような単純なものに使用します。
        ///// new RouetValueDictionary(obj) と同等です。
        ///// </summary>
        ///// <returns>変換結果</returns>
        //public static IDictionary<string, object> ToRouteValueDictionary(this object self)
        //{
        //    return new RouteValueDictionary(self);
        //}

        #endregion
    }
}