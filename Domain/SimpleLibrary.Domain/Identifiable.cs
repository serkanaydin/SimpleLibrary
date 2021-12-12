using System;

namespace SimpleLibrary.Domain
{
    public interface IDentifiable<T> where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T Id { get; set; }
    }
}