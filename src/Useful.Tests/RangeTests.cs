namespace Useful.Tests
{
    using System;
    using Xunit;

    public class RangeTests
    {
        #region " Contains "

        [Fact]
        public void ContainsShouldSuccessIfItIsAtLimit()
        {
            var range = new Range<int>(1, 3);
            Assert.True(range.Contains(1));
            Assert.True(range.Contains(2));
            Assert.True(range.Contains(3));

            range = new Range<int>(null, 3);
            Assert.True(range.Contains(int.MinValue));
            Assert.True(range.Contains(3));

            range = new Range<int>(1);
            Assert.True(range.Contains(1));
            Assert.True(range.Contains(int.MaxValue));
        }

        [Fact]
        public void ContainsShouldFailIfItIsNotInRange()
        {
            var range = new Range<int>(1, 3);
            Assert.False(range.Contains(0));
            Assert.False(range.Contains(4));

            range = new Range<int>(null, 3);
            Assert.False(range.Contains(4));

            range = new Range<int>(1);
            Assert.False(range.Contains(0));
        }

        #endregion

        #region " Intersection "

        [Fact]
        public void IntersectionShouldSuccessIfItHasIntersection()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(2, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(2, 3);
            b = new Range<int>(1, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(1, 3);
            b = new Range<int>(3, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(2, 3);
            b = new Range<int>(1, 2);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(2, 3);
            b = new Range<int>(2, 3);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(2, 2);
            b = new Range<int>(2, 2);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(1);
            b = new Range<int>(2, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(null, 3);
            b = new Range<int>(2, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(null, 3);
            b = new Range<int>(null, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(1);
            b = new Range<int>(2);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(1);
            b = new Range<int>(null, 4);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(null, 3);
            b = new Range<int>(2);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));

            a = new Range<int>(null);
            b = new Range<int>(1, 6);
            Assert.True(a.Intersects(b));
            Assert.True(b.Intersects(a));
        }

        [Fact]
        public void IntersectionShouldFailIfItHasNoIntersection()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(4, 6);
            Assert.False(a.Intersects(b));
            Assert.False(b.Intersects(a));

            a = new Range<int>(3);
            b = new Range<int>(1, 2);
            Assert.False(a.Intersects(b));
            Assert.False(b.Intersects(a));

            a = new Range<int>(null, 2);
            b = new Range<int>(3, 4);
            Assert.False(a.Intersects(b));
            Assert.False(b.Intersects(a));

            a = new Range<int>(4);
            b = new Range<int>(null, 3);
            Assert.False(a.Intersects(b));
            Assert.False(b.Intersects(a));
        }

        #endregion

        #region " GetIntersection "

        [Fact]
        public void GetIntersectionShouldReturnValidRangeIfItHasIntersection()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(2, 4);
            var c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(2, 3);
            b = new Range<int>(1, 4);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(1, 3);
            b = new Range<int>(3, 4);
            c = a.GetIntersection(b);
            Assert.Equal(3, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(3, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(2, 3);
            b = new Range<int>(1, 2);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(2, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(2, c.End);

            a = new Range<int>(2, 3);
            b = new Range<int>(2, 3);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(2, 2);
            b = new Range<int>(2, 2);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(2, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(2, c.End);

            a = new Range<int>(1);
            b = new Range<int>(2, 4);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(4, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(4, c.End);

            a = new Range<int>(null, 3);
            b = new Range<int>(2, 4);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(null, 3);
            b = new Range<int>(null, 4);
            c = a.GetIntersection(b);
            Assert.Null(c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Null(c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(1);
            b = new Range<int>(2);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Null(c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Null(c.End);

            a = new Range<int>(1);
            b = new Range<int>(null, 4);
            c = a.GetIntersection(b);
            Assert.Equal(1, c.Start);
            Assert.Equal(4, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(1, c.Start);
            Assert.Equal(4, c.End);

            a = new Range<int>(null, 3);
            b = new Range<int>(2);
            c = a.GetIntersection(b);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(2, c.Start);
            Assert.Equal(3, c.End);

            a = new Range<int>(null);
            b = new Range<int>(1, 6);
            c = a.GetIntersection(b);
            Assert.Equal(1, c.Start);
            Assert.Equal(6, c.End);
            c = b.GetIntersection(a);
            Assert.Equal(1, c.Start);
            Assert.Equal(6, c.End);
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection1()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(4, 6);
            Assert.Throws<InvalidOperationException>(() => a.GetIntersection(b));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection2()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(4, 6);
            Assert.Throws<InvalidOperationException>(() => b.GetIntersection(a));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection3()
        {
            var a = new Range<int>(3);
            var b = new Range<int>(1, 2);
            Assert.Throws<InvalidOperationException>(() => a.GetIntersection(b));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection4()
        {
            var a = new Range<int>(3);
            var b = new Range<int>(1, 2);
            Assert.Throws<InvalidOperationException>(() => b.GetIntersection(a));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection5()
        {
            var a = new Range<int>(null, 2);
            var b = new Range<int>(3, 4);
            Assert.Throws<InvalidOperationException>(() => a.GetIntersection(b));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection6()
        {
            var a = new Range<int>(null, 2);
            var b = new Range<int>(3, 4);
            Assert.Throws<InvalidOperationException>(() => b.GetIntersection(a));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection7()
        {
            var a = new Range<int>(4);
            var b = new Range<int>(null, 3);
            Assert.Throws<InvalidOperationException>(() => a.GetIntersection(b));
        }

        [Fact]
        public void GetIntersectionShouldThrowExceptionIfItHasNoIntersection8()
        {
            var a = new Range<int>(4);
            var b = new Range<int>(null, 3);
            Assert.Throws<InvalidOperationException>(() => b.GetIntersection(a));
        }

        #endregion

        [Fact]
        public void ToStringTest()
        {
            const string format = "{0}..{1}";

            var range = new Range<int>(1, 2);
            var actual = range.ToString();
            var expected = string.Format(format, range.Start, range.End);
            Assert.Equal(expected, actual);

            range = new Range<int>(1);
            actual = range.ToString();
            expected = string.Format(format, range.Start, "∞");
            Assert.Equal(expected, actual);

            range = new Range<int>(null, 2);
            actual = range.ToString();
            expected = string.Format(format, "∞", 2);
            Assert.Equal(expected, actual);

            range = new Range<int>(null);
            actual = range.ToString();
            expected = string.Format(format, "∞", "∞");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OperatorsTest()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(1, 3);

            Assert.True(a == b);

            a = new Range<int>(1);
            b = new Range<int>(1);

            Assert.True(a == b);

            a = new Range<int>(null, 1);
            b = new Range<int>(null, 1);

            Assert.True(a == b);

            a = new Range<int>(null);
            b = new Range<int>(null);

            Assert.True(a == b);

            a = new Range<int>(1, 3);
            b = new Range<int>(1);

            Assert.True(a != b);

            a = new Range<int>(1);
            b = new Range<int>(null, 1);

            Assert.True(a != b);

            a = new Range<int>(null, 1);
            b = new Range<int>(null);

            Assert.True(a != b);
        }

        [Fact]
        public void GetHashCodeTest()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(1, 3);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());

            a = new Range<int>(1);
            b = new Range<int>(1);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());

            a = new Range<int>(null, 1);
            b = new Range<int>(null, 1);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());

            a = new Range<int>(null);
            b = new Range<int>(null);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void EqualsTest()
        {
            var a = new Range<int>(1, 3);
            var b = new Range<int>(1, 3);

            Assert.True(a.Equals(b));

            a = new Range<int>(1);
            b = new Range<int>(1);

            Assert.True(a.Equals(b));

            a = new Range<int>(null, 1);
            b = new Range<int>(null, 1);

            Assert.True(a.Equals(b));

            a = new Range<int>(null);
            b = new Range<int>(null);

            Assert.True(a.Equals(b));
        }

        [Fact]
        public void EqualsShouldFailIfObjectIsNotRange()
        {
            var a = new Range<int>(1, 3);
            const int b = 1;

            Assert.False(a.Equals(b));
        }
    }
}
