using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities.Common;
using System;

namespace DomainTest
{
    [TestClass]
    public class DateRangeTests
    {
        private DateTimeOffset Now { get; set; }
        private DateTimeOffset Yesterday => Now.AddDays(-1);
        private DateTimeOffset Tomorrow => Now.AddDays(1);
        private DateTimeOffset LastHour => Now.AddHours(-1);
        private DateTimeOffset NextHour => Now.AddHours(1);
        private DateTimeOffset LastMonth => Now.AddMonths(-1);
        private DateTimeOffset NextMonth => Now.AddMonths(1);
        private DateTimeOffset LastYear => Now.AddYears(-1);
        private DateTimeOffset NextYear => Now.AddYears(+1);

        [TestInitialize]
        public void Init()
        {
            Now = DateTimeOffset.Now;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldFailWhenStartEqEnd()
        {
            new DateRange(Now, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldFailWhenStartGtEnd()
        {
            new DateRange(Tomorrow, Yesterday);
        }

        [TestMethod]
        public void DrToString()
        {
            var actualStr = new DateRange(Yesterday, Tomorrow).ToString();
            var expectedStr = $"{Yesterday.ToString(DateRange.DisplayFormatInUtc)} - {Tomorrow.ToString(DateRange.DisplayFormatInUtc)}";
            Assert.AreEqual(actualStr, expectedStr);
        }

        [TestMethod]
        public void DrCoversDateTimeOffset()
        {
            var dr = new DateRange(Yesterday, Tomorrow);

            Assert.IsTrue(dr.Covers(Yesterday));
            Assert.IsTrue(dr.Covers(Tomorrow));
            Assert.IsTrue(dr.Covers(Now));
            Assert.IsTrue(dr.Covers(LastHour));
            Assert.IsTrue(dr.Covers(NextHour));
            Assert.IsFalse(dr.Covers(LastYear));
            Assert.IsFalse(dr.Covers(NextYear));
        }

        [TestMethod]
        public void CoversDateRange()
        {
            var dr = new DateRange(Yesterday, Tomorrow);

            //ranges that share a start and/or end
            Assert.IsTrue(dr.Covers(new DateRange(Yesterday, Tomorrow)));
            Assert.IsTrue(dr.Covers(new DateRange(Now, Tomorrow)));
            Assert.IsTrue(dr.Covers(new DateRange(Yesterday, Now)));

            //ranges inside this range
            Assert.IsTrue(dr.Covers(new DateRange(LastHour, Now)));
            Assert.IsTrue(dr.Covers(new DateRange(Now, NextHour)));
            Assert.IsTrue(dr.Covers(new DateRange(LastHour, NextHour)));

            //ranges covering this range
            Assert.IsFalse(dr.Covers(new DateRange(LastYear, Now)));
            Assert.IsFalse(dr.Covers(new DateRange(Now, NextYear)));
            Assert.IsFalse(dr.Covers(new DateRange(LastYear, NextYear)));

            //ranges completely not overlapping this range
            Assert.IsFalse(dr.Covers(new DateRange(NextMonth, NextYear)));
            Assert.IsFalse(dr.Covers(new DateRange(LastYear, LastMonth)));

        }

        [TestMethod]
        public void OverlapsDateRange()
        {
            var dr = new DateRange(Yesterday, Tomorrow);

            //ranges that share a start and/or end
            Assert.IsTrue(dr.Overlaps(new DateRange(Yesterday, Tomorrow)));
            Assert.IsTrue(dr.Overlaps(new DateRange(Now, Tomorrow)));
            Assert.IsTrue(dr.Overlaps(new DateRange(Yesterday, Now)));

            //ranges inside this range
            Assert.IsTrue(dr.Overlaps(new DateRange(LastHour, Now)));
            Assert.IsTrue(dr.Overlaps(new DateRange(Now, NextHour)));
            Assert.IsTrue(dr.Overlaps(new DateRange(LastHour, NextHour)));

            //ranges covering this range
            Assert.IsTrue(dr.Overlaps(new DateRange(LastYear, Now)));
            Assert.IsTrue(dr.Overlaps(new DateRange(Now, NextYear)));
            Assert.IsTrue(dr.Overlaps(new DateRange(LastYear, NextYear)));

            //ranges completely not overlapping this range
            Assert.IsFalse(dr.Covers(new DateRange(NextMonth, NextYear)));
            Assert.IsFalse(dr.Covers(new DateRange(LastYear, LastMonth)));

        }
    }

    
}
