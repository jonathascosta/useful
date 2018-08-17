namespace Useful
{
    using System;

    public struct Range<T> where T : struct, IComparable<T>
    {
        public T? Start { get; }
        public T? End { get; }

        public Range(T? start = null, T? end = null) : this()
        {
            Start = start;
            End = end;
        }

        public bool Contains(T obj)
        {
            var result = true;
            result &= !(Start.HasValue) || (Start.Value.CompareTo(obj) <= 0);
            result &= !(End.HasValue) || (End.Value.CompareTo(obj) >= 0);

            return result;
        }

        public Range<T> GetIntersection(Range<T> other)
        {
            if (!Intersects(other))
                throw new InvalidOperationException("There is no intersection between these range objects.");

            T? start, end;

            if (!Start.HasValue ^ !other.Start.HasValue)
                start = Start ?? other.Start.Value;
            else
                start = Nullable.Compare(Start, other.Start) >= 0 ? Start : other.Start;

            if (!End.HasValue ^ !other.End.HasValue)
                end = End ?? other.End.Value;
            else
                end = Nullable.Compare(End, other.End) < 0 ? End : other.End;

            return new Range<T>(start, end);
        }

        public bool Intersects(Range<T> other)
        {
            var hasClosedInterval = (Start.HasValue && End.HasValue && other.Start.HasValue && other.End.HasValue &&
                    Start.Value.CompareTo(other.End.Value) <= 0 &&
                    other.Start.Value.CompareTo(End.Value) <= 0);

            var hasOpenInterval =
                (!Start.HasValue && !End.HasValue) ||
                (!other.Start.HasValue && !other.End.HasValue) ||
                (!End.HasValue && !other.End.HasValue) ||
                (!Start.HasValue && !other.Start.HasValue) ||
                (!End.HasValue && Start.Value.CompareTo(other.End.Value) <= 0) ||
                (!Start.HasValue && End.Value.CompareTo(other.Start.Value) >= 0) ||
                (!other.End.HasValue && other.Start.Value.CompareTo(End.Value) <= 0) ||
                (!other.Start.HasValue && other.End.Value.CompareTo(Start.Value) >= 0);

            return hasClosedInterval || hasOpenInterval;
        }

        public override string ToString()
        {
            return $"{Start?.ToString() ?? "∞"}..{End?.ToString() ?? "∞"}";
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        #region " Operators "

        public static bool operator ==(Range<T> a, Range<T> b)
        {
            return a.Start.Equals(b.Start) && a.End.Equals(b.End);
        }

        public static bool operator !=(Range<T> a, Range<T> b)
        {
            return !(a == b);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is Range<T>))
                return false;

            return (this == (Range<T>)obj);
        }
    }
}
