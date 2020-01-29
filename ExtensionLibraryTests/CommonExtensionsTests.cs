using Microsoft.VisualStudio.TestTools.UnitTesting;
using KWID.ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWID.ExtensionLibrary.Tests
{
    /// <summary>
    /// 自作処理はここでテストする。
    /// 内部で .NET Framework のメソッドを読んでいるだけのものはテスト外とする。
    /// </summary>
    [TestClass()]
    public class CommonExtensionsTests
    {
        #region string.ToBool
        [TestMethod()]
        public void ToBoolTest()
        {
            string str;

            str = "TRUE";
            Assert.AreEqual(true, str.ToBool());

            str = "true";
            Assert.AreEqual(true, str.ToBool());

            str = "ffffff";
            Assert.AreEqual(false, str.ToBool());

            str = "1";
            Assert.AreEqual(true, str.ToBool(true));

            str = "0";
            Assert.AreEqual(false, str.ToBool(true));
        }
        #endregion

        #region string.Left
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
        #endregion

        #region string.Right
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
        #endregion

        #region string.TrimLeft
        [TestMethod()]
        public void TrimLeftTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("cde", str.TrimLeft("ab"));

            str = "abcde";
            Assert.AreEqual("abcde", str.TrimLeft("ba"));
        }
        #endregion

        #region string.TrimRight
        [TestMethod()]
        public void TrimRightTest()
        {
            string str;

            str = "abcde";
            Assert.AreEqual("abc", str.TrimRight("de"));

            str = "abcde";
            Assert.AreEqual("abcde", str.TrimRight("ed"));
        }
        #endregion

        #region string.Slice
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
        #endregion

        #region int.ToBool
        [TestMethod()]
        [Description("IntToBoolのテスト")]
        public void IntToBoolTest()
        {
            int num;

            num = 1;
            Assert.AreEqual(true, num.ToBool());

            num = 0;
            Assert.AreEqual(false, num.ToBool());

            num = 999;
            Assert.AreEqual(true, num.ToBool());

            num = -999;
            Assert.AreEqual(false, num.ToBool());
        }
        #endregion

        #region Exception.GetMessages

        [TestMethod()]
        public void GetMessagesTest()
        {
            var innerinner = new Exception("不正な文字です。");
            var inner = new Exception("文字列が処理できません。", innerinner);
            var outer = new Exception("エラーが発生しました。", inner);

            string kitai = $"エラーが発生しました。{Environment.NewLine}文字列が処理できません。{Environment.NewLine}不正な文字です。";

            Assert.AreEqual(kitai, outer.GetMessages());
        }

        [TestMethod()]
        public void GetMessagesTest2()
        {
            Exception ex = null;

            Assert.AreEqual("", ex.GetMessages());
        }

        #endregion
    }
}