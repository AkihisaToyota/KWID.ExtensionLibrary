# 概要
Nuget で公開している [KWID.ExtensionLibrary](https://www.nuget.org/packages/KWID.ExtensionLibrary/) のソースです。
C# のいろいろなメソッドに拡張メソッドを追加します。

# 拡張クラス

## string
+ IsNullOrEmpty()
	+ return bool
+ IsNullOrWhiteSpace()
	+ return bool
+ EqualsIgnoreCase(string strB)
	+ return bool
+ Split(string sepalator, StringSplitOptions options)
	+ return string[]
+ ToBool(bool isConvertNumeric)
	+ return bool
+ ToInt(int defaultValue)
	+ return int
+ Left(int length)
	+ return string
+ Right(int length)
	+ return string
+ TrimLeft(string trimStr)
	+ return string
+ TrimRight(string trimStr)
	+ return string
+ Slice(int startIndex, int endIndex)
	+ return string
+ AddSeparator(string separator, int separateLength)
	+ return string
+ DefaultNullOrEmpty(string defaultValue)
	+ return string
+ DefaultNullOrWhiteSpace(string defaultValue)
	+ return string

## int
+ ToBool()
	+ return bool

## DateTime
+ IsMinValue()
	+ return bool

## Dictionary
+ GetValue<TKey, TValue>(TKey key)
	+ return TValue

+ GetValueOrDefault<TKey, TValue>(TKey key)
	+ return TValue

## IEnumerable<T>
+ IsNullOrEmpty()
	+ return bool
+ JoinString(string separator)
	+ return string
+ ToHashSet()
	+ return HashSet<T>
+ Chunk(int size)
	+ return IEnumerable<IEnumerable<T>>

## IEnumerable<char>
+ NewString()
	+ return string

## Type
+ IsGenericEnumerable
	+ return bool

## Exception
+ GetMessages()
	+ return string

## System.Data.SqlClient.SqlDataReader
+ GetColumnValue<T>(string columnName, T defaultValue = default(T))
	+ return T
+ GetColumnValue(string columnName)
	+ return object

# License
[MIT License](https://github.com/kawaidainfinity/KWID.ExtensionLibrary/blob/master/LICENSE)