using Microsoft.VisualStudio.TestTools.UnitTesting;
using KWID.ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWID.ExtensionLibrary.Test
{
    /// <summary>
    /// 自作処理はここでテストする。
    /// 内部で .NET Framework のメソッドを読んでいるだけのものはテスト外とする。
    /// </summary>
    [TestClass()]
    public class CommonExtensionsTests
    {
        #region string
        [TestMethod()]
        public void ToBoolTest()
        {
            string str;

            str = "TRUE";
            Assert.IsTrue(str.ToBool());

            str = "true";
            Assert.IsTrue(str.ToBool());

            str = "ffffff";
            Assert.IsFalse(str.ToBool());

            str = "1";
            Assert.IsTrue(str.ToBool(true));

            str = "0";
            Assert.IsFalse(str.ToBool(true));
        }

        [TestMethod()]
        public void LeftTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("abc", str.Left(3));

            str = "abcde";
            Assert.AreEqual("abcde", str.Left(6));

            str = "abcde";
            Assert.AreEqual("", str.Left(0));

            str = "abcde";
            Assert.AreEqual("", str.Left(-1));
        }

        [TestMethod()]
        public void RightTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("cde", str.Right(3));

            str = "abcde";
            Assert.AreEqual("abcde", str.Right(6));

            str = "abcde";
            Assert.AreEqual("", str.Right(0));

            str = "abcde";
            Assert.AreEqual("", str.Right(-1));
        }

        [TestMethod()]
        public void TrimLeftTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("cde", str.TrimLeft("ab"));

            str = "abcde";
            Assert.AreEqual("abcde", str.TrimLeft("ba"));
        }

        [TestMethod()]
        public void TrimRightTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("abc", str.TrimRight("de"));

            str = "abcde";
            Assert.AreEqual("abcde", str.TrimRight("ed"));
        }

        [TestMethod()]
        public void SliceTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("abcd", str.Slice(0, 4));

            str = "abcde";
            Assert.AreEqual("cd", str.Slice(-3, -1));

            str = "abcde";
            Assert.AreEqual("bc", str.Slice(-4, 3));

            str = "abcde";
            Assert.AreEqual("abcde", str.Slice(-999, 1600));
        }

        [TestMethod()]
        public void AddSeparatorTest()
        {
            string str;

            str = "AAAABBBBCCCCDDDDEEEEFFFFGGGGHHHHII";
            Assert.AreEqual("AAAA-BBBB-CCCC-DDDD-EEEE-FFFF-GGGG-HHHH-II", str.AddSeparator("-", 4));

            str = "AAAABBBBCCCCDDDDEEEEFFFFGGGGHHHHII";
            Assert.AreEqual("AAA-ABB-BBC-CCC-DDD-DEE-EEF-FFF-GGG-GHH-HHI-I", str.AddSeparator("-", 3));

            str = "AAAA";
            Assert.AreEqual("AAAA", str.AddSeparator("-", 5));

            str = "";
            Assert.AreEqual("", str.AddSeparator("-", 5));

            str = null;
            Assert.AreEqual(null, str.AddSeparator("-", 5));

            str = "";
            Assert.AreEqual("", str.AddSeparator(null, -1));

        }

        [TestMethod()]
        public void DefaultIfNullOrEmptyTest()
        {
            string str;
            string emptyStr = "This is Empty!!!";

            str = "ABC";
            Assert.AreEqual(str, str.DefaultIfNullOrEmpty(emptyStr));

            str = "\t\n";
            Assert.AreEqual(str, str.DefaultIfNullOrEmpty(emptyStr));

            str = " ";
            Assert.AreEqual(str, str.DefaultIfNullOrEmpty(emptyStr));

            str = "";
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrEmpty(emptyStr));

            str = null;
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrEmpty(emptyStr));

        }

        [TestMethod()]
        public void DefaultIfNullOrWhiteSpaceTest()
        {
            string str;
            string emptyStr = "This is Empty!!!";

            str = "ABC";
            Assert.AreEqual(str, str.DefaultIfNullOrWhiteSpace(emptyStr));

            str = "\t\n";
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrWhiteSpace(emptyStr));

            str = " ";
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrWhiteSpace(emptyStr));

            str = "";
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrWhiteSpace(emptyStr));

            str = null;
            Assert.AreEqual(emptyStr, str.DefaultIfNullOrWhiteSpace(emptyStr));

        }
        #endregion

        #region int
        [TestMethod()]
        [Description("IntToBoolのテスト")]
        public void IntToBoolTest()
        {
            int num;

            num = 1;
            Assert.IsTrue(num.ToBool());

            num = 0;
            Assert.IsFalse(num.ToBool());

            num = 999;
            Assert.IsTrue(num.ToBool());

            num = -999;
            Assert.IsFalse(num.ToBool());
        }
        #endregion

        #region Exception

        [TestMethod()]
        [Description("GetMessagesのテスト")]
        public void GetMessagesTest()
        {
            var innerinner = new Exception("不正な文字です。");
            var inner = new Exception("文字列が処理できません。", innerinner);
            var outer = new Exception("エラーが発生しました。", inner);

            string kitai = $"エラーが発生しました。{Environment.NewLine}文字列が処理できません。{Environment.NewLine}不正な文字です。";

            Assert.AreEqual(kitai, outer.GetMessages());


            Exception ex = null;

            Assert.AreEqual("", ex.GetMessages());
        }

        #endregion

        #region IEnumerable

        [TestMethod()]
        [Description("Chunkのテスト")]
        public void ChunkTest()
        {

            List<IEnumerable<char>> result = "hogefugapiyo".Chunk(4).ToList();
            List<IEnumerable<char>> baseAnswer = new List<IEnumerable<char>>() { "hoge", "fuga", "piyo" };

            Assert.AreEqual(baseAnswer.Count, result.Count);
            for (int i = 0; i < baseAnswer.Count; i++)
            {
                string a = baseAnswer[i].NewString();
                string r = result[i].NewString();
                Assert.AreEqual(a, r);
            }


            result = "hogefugapiyofoo".Chunk(4).ToList();
            baseAnswer = new List<IEnumerable<char>>() { "hoge", "fuga", "piyo", "foo" };

            Assert.AreEqual(baseAnswer.Count, result.Count);
            for (int i = 0; i < baseAnswer.Count; i++)
            {
                string a = baseAnswer[i].NewString();
                string r = result[i].NewString();
                Assert.AreEqual(a, r);
            }


            result = "hogefugapiyofoo".Chunk(30).ToList();
            baseAnswer = new List<IEnumerable<char>>() { "hogefugapiyofoo" };

            Assert.AreEqual(baseAnswer.Count, result.Count);
            for (int i = 0; i < baseAnswer.Count; i++)
            {
                string a = baseAnswer[i].NewString();
                string r = result[i].NewString();
                Assert.AreEqual(a, r);
            }
        }

        #endregion

        #region Type

        [TestMethod()]
        [Description("IsGenericEnumerableのテスト")]
        public void IsGenericEnumerableTest()
        {
            object obj = null;

            obj = new List<string>();
            Assert.IsTrue(obj.GetType().IsGenericEnumerable());

            obj = new HashSet<int>();
            Assert.IsTrue(obj.GetType().IsGenericEnumerable());

            obj = new Dictionary<string, object>();
            Assert.IsTrue(obj.GetType().IsGenericEnumerable());

            obj = new System.Collections.Specialized.StringCollection();
            Assert.IsFalse(obj.GetType().IsGenericEnumerable());

            obj = new object();
            Assert.IsFalse(obj.GetType().IsGenericEnumerable());
        }

        #endregion
    }
}