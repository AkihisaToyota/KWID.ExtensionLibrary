# 概要
Nuget で公開している KWID.ExtensionLibrary のソースです。
C# のいろいろなメソッドに拡張メソッドを追加します。

# 拡張クラス
## System.Data.SqlClient.SqlDataReader
+ GetColumnValue<T>(string columnName, T defaultValue = default(T))
	+ return T
+ GetColumnValue(string columnName)
	+ return object

## string
+ IsNullOrEmpty()
	+ return bool
+ IsNullOrWhiteSpace()
	+ return bool
+ EqualsIgnoreCase(string strB)
	+ return bool
+ ToBool()
	+ return bool
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

## IEnumerable
+ IsNullOrEmpty()
	+ return bool

## Type
+ IsGenericEnumerable
	+ return bool

# License
[MIT License](/License)