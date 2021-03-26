using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using GammaLibrary.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GammaLibrary.Tests
{
    [TestClass]
    public class JsonTest
    {
        readonly Lazy<string> _internalLazy = new Lazy<string>(() => "sb");
        public string LazyObject => _internalLazy.Value;

        [TestMethod]
        public void TestJson()
        {
            var obj = new List<int> {2, 5};
            var jsonString = obj.ToJsonString();
            var obj2 = jsonString.JsonDeserialize<List<int>>();
            Assert.IsTrue(obj.SequenceEqual(obj2!));
        }
        
    }
}
