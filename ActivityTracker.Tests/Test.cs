using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ActivityTracker.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestMethod1()
        {


            var list = new List<TrackerLog>();
            list.Add(new TrackerLog() { LogIn = DateTime.Now });
            list.Add(new TrackerLog() { LogOut = DateTime.Now.AddMinutes(30) });
            list.Add(new TrackerLog() { LogIn = DateTime.Now.AddMinutes(90) });
            list.Add(new TrackerLog() { LogOut = DateTime.Now.AddMinutes(100) });
            var json = JsonConvert.SerializeObject(list);

            var lin = list.Max(m => m.LogIn);
            var lout = list.Max(m => m.LogOut);
            var a = lout - lin;

            var r = JsonConvert.DeserializeObject<List<TrackerLog>>(json);
        }
    }
}
