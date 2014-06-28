using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AsanaSharp
{
    [ComVisible(true)]
    public static class Remoteable
    {
        public static int Compare<T>(Remoteable<T> n1, Remoteable<T> n2) where T : struct
        {
            if (n1.has_value)
            {
                if (!n2.has_value)
                    return 1;

                return Comparer<T>.Default.Compare(n1.Value, n2.Value);
            }

            return n2.has_value ? -1 : 0;
        }

        public static bool Equals<T>(Remoteable<T> n1, Remoteable<T> n2) where T : struct
        {
            if (n1.has_value != n2.has_value)
                return false;

            if (!n1.has_value)
                return true;

            return EqualityComparer<T>.Default.Equals(n1.Value, n2.Value);
        }

        public static Type GetUnderlyingType(Type nullableType)
        {
            if (nullableType == null)
                throw new ArgumentNullException("nullableType");

            return nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && nullableType.GetGenericTypeDefinition() == typeof(Remoteable<>) ?
                nullableType.GetGenericArguments()[0] : null;
        }
    }

    [Serializable]
    [DebuggerStepThrough]
    public struct Remoteable<T> where T : struct
    {
        #region Sync with runtime code
        internal T? remote_value;
        internal T? local_value;
        internal bool is_local;
        internal bool has_value
        {
            get { return local_value.HasValue || remote_value.HasValue; }
        }

        #endregion

        public Remoteable(T value)
        {
            this.is_local = false;
            this.remote_value = value;
            this.local_value = null;
        }

        public Remoteable(T? value)
        {
            this.is_local = false;
            this.remote_value = value;
            this.local_value = null;
        }

        public bool HasValue
        {
            get { return has_value; }
        }
        public bool IsLocal
        {
            get { return is_local; }
        }

        public T? NullableValue
        {
            get
            {
                if (local_value.HasValue)
                    return local_value.Value;
                return remote_value;
            }
        }

        public T Value
        {
            get
            {
                if (local_value.HasValue)
                    return local_value.Value;

                return remote_value.Value;
//                if (!has_value)
//                    throw new InvalidOperationException("Nullable object must have a value.");
            }
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return has_value == false;
            if (!(other is Remoteable<T>))
                return false; // TODO ?
            
            return Equals((Remoteable<T>)other);
        }

        bool Equals(Remoteable<T> other)
        {
            if (other.has_value != has_value)
                return false;

            if (has_value == false)
                return true;

            if (local_value.HasValue)
                return other.Value.Equals(local_value);

            return other.Value.Equals(remote_value);
        }

        public override int GetHashCode()
        {
            if (!has_value)
                return 0;

            if (local_value.HasValue)
                return local_value.Value.GetHashCode();

            return remote_value.GetHashCode();
        }

        public T GetValueOrDefault()
        {
            if (local_value.HasValue)
                return local_value.Value;
            return remote_value.GetValueOrDefault();
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (local_value.HasValue)
                return local_value.Value;
            return remote_value.GetValueOrDefault(defaultValue);
        }

        public override string ToString()
        {
            if (local_value.HasValue)
                return local_value.ToString();
            else if (remote_value.HasValue)
                return remote_value.ToString();
            else
                return String.Empty;
        }

        public static implicit operator Remoteable<T>(T value)
        {
            return new Remoteable<T>(value);
        }

        public static implicit operator Remoteable<T>(T? value)
        {
            if (value.HasValue)
                return new Remoteable<T>(value.Value);
            return new Remoteable<T>(null);
        }
        
        public static explicit operator T(Remoteable<T> value)
        {
            return value.Value; //.Value;
        }
//        public static implicit operator T(Remoteable<T> value)
//        {
//            return value.Value; //.Value;
//        }

        public static explicit operator T?(Remoteable<T> value)
        {
            return value.NullableValue;
        }
        /*
        //
        // These are called by the JIT
        //
#pragma warning disable 169
        //
        // JIT implementation of box valuetype System.Nullable`1<T>
        //
        static object Box(Remoteable<T> o)
        {
            if (!o.has_value)
                return null;

            return o.remote_value;
        }

        static Remoteable<T> Unbox(object o)
        {
            if (o == null)
                return default(T);
            return (T)o;
        }
#pragma warning restore 169
        */
    }
}