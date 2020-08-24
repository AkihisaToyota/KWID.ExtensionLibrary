# 概要
Nuget で公開している [KWID.ExtensionLibrary](https://www.nuget.org/packages/KWID.ExtensionLibrary/) のソースです。
C# のいろいろなメソッドに拡張メソッドを追加します。

# 拡張メソッド
一覧を載せますが、説明はメソッド名とサマリーなどから読み取ってください（ごめんなさい）

## string
+ bool IsNullOrEmpty(this string self)
+ bool IsNullOrWhiteSpace(this string self)
+ bool EqualsIgnoreCase(this string strA, string strB)
+ string[] Split(this string self, string separator, StringSplitOptions options = StringSplitOptions.None)
+ string DefaultIfNullOrEmpty(this string self, string defaultValue)
+ string DefaultIfNullOrWhiteSpace(this string self, string defaultValue)
+ bool ToBool(this string self, bool isConvertNumeric = false)
+ int ToInt(this string self, int defaultValue = default)
+ int ToInt(this string self, System.Globalization.NumberStyles style, IFormatProvider provider, int defaultValue = default)
+ string Left(this string self, int length)
+ string Right(this string self, int length)
+ string TrimLeft(this string self, string trimStr)
+ string TrimRight(this string self, string trimStr)
+ string Slice(this string self, int startIndex, int? endIndex = null)
+ string AddSeparator(this string self, string separator, int separateLength)
## int
+ bool ToBool(this int self)
## DateTime
+ bool IsMinValue(this DateTime self)
## IDictionary&lt;TKey, TValue&gt;
+ TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
+ TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
## IEnumerable&lt;T&gt;
+ bool IsNullOrEmpty<T>(this IEnumerable<T> self)
+ string JoinString<T>(this IEnumerable<T> self, string separator)
+ HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
+ IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> self, int size)
## IEnumerable&lt;char&gt;
+ string NewString(this IEnumerable<char> self)
## Type
+ bool IsGenericEnumerable(this Type self)
## Exception
+ string GetMessages(this Exception self)
## System.Data.SqlClient.SqlDataReader
+ T GetColumnValue<T>(this SqlDataReader self, string columnName, T defaultValue = default(T))
+ object GetColumnValue(this SqlDataReader self, string columnName)

# 汎用メソッド

## class CollectionEx
+ T[] NewArray<T>(params T[] items)
+ List<T> NewList<T>(params T[] items)
+ HashSet<T> NewHashSet<T>(params T[] items)
## class CommonEx
+ T TryFunc<T>(Func<T> func, T defaultValue = default)
+ void TryAction<T>(Action tryAction, Action<T> catchAction = null, Action finallyAction = null)
+ void TryAction(Action tryAction, Action<Exception> catchAction = null, Action finallyAction = null)

# License
[MIT License](https://github.com/kawaidainfinity/KWID.ExtensionLibrary/blob/master/LICENSE)