using Microsoft.VisualStudio.TestTools.UnitTesting;
using KWID.ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting;

namespace KWID.ExtensionLibrary.Test
{
    /// <summary>
    /// CommonEx 汎用メソッドのテスト
    /// </summary>
    [TestClass()]
    public class CommonExTests
    {
        [TestMethod()]
        [Description("TryFunc のテスト")]
        public void TryFuncTest()
        {
            string str1 = CommonEx.TryFunc(() => "hoge", "piyo");
            Assert.AreEqual("hoge", str1);

            string str2 = CommonEx.TryFunc(() => throw new Exception(), "hoge");
            Assert.AreEqual("hoge", str2);

            var finfo1 = CommonEx.TryFunc(() => new FileInfo(@"C:\Windows\System32\notepad.exe"));
            Assert.AreEqual("notepad.exe", finfo1.Name);

            var finfo2 = CommonEx.TryFunc(() => new FileInfo(@"\\\\\\\\\\\\\\\\"));
            Assert.IsNull(finfo2);
        }

        [TestMethod()]
        [Description("TryAction のテスト")]
        public void TryActionTest()
        {
            string str1 = null;
            CommonEx.TryAction(() => str1 = "hoge");
            Assert.AreEqual("hoge", str1);

            string str2 = null;
            CommonEx.TryAction(() => throw new Exception("hoge"), (ex) => str2 = ex.Message);
            Assert.AreEqual("hoge", str2);

            string str3 = null;
            CommonEx.TryAction(() => throw new Exception("hoge"), (ex) => str3 = ex.Message, () => str3 = "fuga");
            Assert.AreEqual("fuga", str3);

            string str4 = null;
            CommonEx.TryAction(() => str4 = "hoge", null, () => str4 = "fuga");
            Assert.AreEqual("fuga", str4);
        }

        [TestMethod()]
        [Description("TryAction<T> のテスト")]
        public void TryActionGenericTest()
        {
            Exception throwedException = null;
            CommonEx.TryAction<FileNotFoundException>(() => throw new FileNotFoundException("hoge"),
                (ex) => throwedException = ex);
            Assert.IsInstanceOfType(throwedException, typeof(FileNotFoundException));

            CommonEx.TryAction<Exception>(() => throw new FileNotFoundException("hoge"),
                (ex) => throwedException = ex);
            Assert.IsInstanceOfType(throwedException, typeof(FileNotFoundException));
        }
    }
}