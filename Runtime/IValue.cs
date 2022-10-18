using System;
using System.Collections;
using System.Collections.Generic;

namespace Zilla.Xtructs
{
    /// <summary>
    /// Interface for any class which is responsible for holding a value
    /// </summary>
    /// <typeparam name="T">Value Type</typeparam>
    public interface IValue<T>
    {
        /// <summary>
        /// Main Value
        /// </summary>
        public T Value { get; set; }
    }

    public static class IValueExt
    {
        public static bool Equals<T>(this IValue<T> @this, T value)
            => @this.Value.Equals(value);
    }
}
