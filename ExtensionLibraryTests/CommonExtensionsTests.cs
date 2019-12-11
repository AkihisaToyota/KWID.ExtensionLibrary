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
            string str = "TRUE";
            Assert.AreEqual(true, str.ToBool());
        }

        [TestMethod()]
        public void ToBoolTest2()
        {
            string str = "true";
            Assert.AreEqual(true, str.ToBool());
        }

        [TestMethod()]
        public void ToBoolTest3()
        {
            string str = "ffffff";
            Assert.AreEqual(false, str.ToBool());
        }

        [TestMethod()]
        public void ToBoolTest4()
        {
            string str = "1";
            Assert.AreEqual(true, str.ToBool(true));
        }

        [TestMethod()]
        public void ToBoolTest5()
        {
            string str = "0";
            Assert.AreEqual(false, str.ToBool(true));
        }
        #endregion

        #region string.Left
        [TestMethod()]
        public void LeftTest()
        {
            string str = "abcde";
            Assert.AreEqual("abc", str.Left(3));
        }

        [TestMethod()]
        public void LeftTest2()
        {
            string str = "abcde";
            Assert.AreEqual("abcde", str.Left(6));
        }

        [TestMethod()]
        public void LeftTest3()
        {
            string str = "abcde";
            Assert.AreEqual("", str.Left(0));
        }

        [TestMethod()]
        public void LeftTest4()
        {
            string str = "abcde";
            Assert.AreEqual("", str.Left(-1));
        }
        #endregion

        #region string.Right
        [TestMethod()]
        public void RightTest()
        {
            string str = "abcde";
            Assert.AreEqual("cde", str.Right(3));
        }

        [TestMethod()]
        public void RightTest2()
        {
            string str = "abcde";
            Assert.AreEqual("abcde", str.Right(6));
        }

        [TestMethod()]
        public void RightTest3()
        {
            string str = "abcde";
            Assert.AreEqual("", str.Right(0));
        }

        [TestMethod()]
        public void RightTest4()
        {
            string str = "abcde";
            Assert.AreEqual("", str.Right(-1));
        }
        #endregion

        #region string.TrimLeft
        [TestMethod()]
        public void TrimLeftTest()
        {
            string str = "abcde";
            Assert.AreEqual("cde", str.TrimLeft("ab"));
        }

        [TestMethod()]
        public void TrimLeftTest2()
        {
            string str = "abcde";
            Assert.AreEqual("abcde", str.TrimLeft("ba"));
        }
        #endregion

        #region string.TrimRight
        [TestMethod()]
        public void TrimRightTest()
        {
            string str = "abcde";
            Assert.AreEqual("abc", str.TrimRight("de"));
        }

        [TestMethod()]
        public void TrimRightTest2()
        {
            string str = "abcde";
            Assert.AreEqual("abcde", str.TrimRight("ed"));
        }
        #endregion

        #region string.Slice
        [TestMethod()]
        public void SliceTest()
        {
            string str = "abcde";
            Assert.AreEqual("abcd", str.Slice(0, 4));
        }

        [TestMethod()]
        public void SliceTest2()
        {
            string str = "abcde";
            Assert.AreEqual("cd", str.Slice(-3, -1));
        }

        [TestMethod()]
        public void SliceTest3()
        {
            string str = "abcde";
            Assert.AreEqual("bc", str.Slice(-4, 3));
        }

        [TestMethod()]
        public void SliceTest4()
        {
            string str = "abcde";
            Assert.AreEqual("abcde", str.Slice(-999, 1600));
        }
        #endregion

        #region int.ToBool
        [TestMethod()]
        public void IntToBoolTest1()
        {
            int num = 1;
            Assert.AreEqual(true, num.ToBool());
        }

        [TestMethod()]
        public void IntToBoolTest2()
        {
            int num = 0;
            Assert.AreEqual(false, num.ToBool());
        }

        [TestMethod()]
        public void IntToBoolTest3()
        {
            int num = 999;
            Assert.AreEqual(true, num.ToBool());
        }

        [TestMethod()]
        public void IntToBoolTest4()
        {
            int num = -999;
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