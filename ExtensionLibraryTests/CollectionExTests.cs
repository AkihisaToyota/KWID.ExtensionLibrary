using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWID.ExtensionLibrary.Test
{
    /// <summary>
    /// CollectionEx 汎用メソッドのテスト
    /// </summary>
    [TestClass()]
    public class CollectionExTests
    {
        [TestMethod()]
        [Description("NewArray, NewList, NewHashSet のテスト")]
        public void NewTest()
        {
            string[] ary = CollectionEx.NewArray("hoge", "fuga", "piyo");
            Assert.AreEqual(3, ary.Length);
            Assert.AreEqual("hoge", ary[0]);
            Assert.AreEqual("fuga", ary[1]);
            Assert.AreEqual("piyo", ary[2]);

            var lst = CollectionEx.NewList("hoge", "fuga", "piyo");
            Assert.AreEqual(3, lst.Count);
            Assert.AreEqual("hoge", lst[0]);
            Assert.AreEqual("fuga", lst[1]);
            Assert.AreEqual("piyo", lst[2]);

            var set = CollectionEx.NewHashSet("hoge", "fuga", "piyo");
            string[] equalAry = new string[] { "hoge", "fuga", "piyo" };
            Assert.AreEqual(equalAry.Length, set.Count);
            int cnt = 0;
            foreach (var setItem in set)
            {
                Assert.AreEqual(equalAry[cnt++], setItem);
            }
        }
    }
}
