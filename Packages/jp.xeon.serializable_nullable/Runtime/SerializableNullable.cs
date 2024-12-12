using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using UnityEngine;
#if XEON_CSV_SUPPORT
using Xeon.IO;
#endif

namespace Xeon.Utility
{
    [Serializable]
    public class SerializableNullable<T> : ISerializable
#if XEON_CSV_SUPPORT
        , ICsvSupport
#endif
        where T : struct
    {
        [SerializeField]
        protected T value;
        [SerializeField]
        protected bool hasValue = false;
        public T? Value
        {
            get => hasValue ? value : null;
            set
            {
                hasValue = value.HasValue;
                if (hasValue)
                    this.value = value.Value;
            }
        }
        public bool IsNull => !hasValue;
        public bool HasValue => hasValue;

        public SerializableNullable()
        {
            hasValue = false;
            value = default;
        }

        public SerializableNullable(T? value)
        {
            hasValue = value.HasValue;
            if (hasValue)
                this.value = value.Value;
        }

        public static bool operator true(SerializableNullable<T> value) => value.hasValue;
        public static bool operator false(SerializableNullable<T> value) => !value.hasValue;

        public static bool operator ==(SerializableNullable<T> a, SerializableNullable<T> b) => a.Equals(b);
        public static bool operator !=(SerializableNullable<T> a, SerializableNullable<T> b) => !a.Equals(b);
        public static bool operator ==(SerializableNullable<T> a, T? b) => a.Value.Equals(b);
        public static bool operator !=(SerializableNullable<T> a, T? b) => !a.Value.Equals(b);

        public static bool operator ==(T? a, SerializableNullable<T> b) => a.Equals(b.Value);
        public static bool operator !=(T? a, SerializableNullable<T> b) => a.Equals(b.Value);


        public override bool Equals(object obj)
        {
            // 比較対象がNull以外
            if (obj != null)
                return obj.Equals(hasValue ? value : null);
            // if (obj == null && !hasValue) と等価
            if (!hasValue)
                return true;
            // 自身はNullではないが、比較対象がNullなのでFalse
            return false;
        }

        public void SetIsNull(bool flag) => hasValue = !flag;
        public void SetHasValue(bool flag) => hasValue = flag;
        public void SetValue(T value) => this.value = value;
        public T GetValue() => value;
        public T GetValueOrDefault() => hasValue ? value : default;
        public T GetValueOrDefault(T defaultValue) => hasValue ? value : defaultValue;

        public override string ToString() => hasValue ? value.ToString() : "null";

        public void FromString(string text)
        {
            if (text == "null")
            {
                hasValue = false;
                return;
            }
            hasValue = true;
            ConvertTextToValue(text);
        }

        protected virtual void ConvertTextToValue(string text) { }

        public override int GetHashCode() => hasValue ? value.GetHashCode() : 0;

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", hasValue ? value.ToString() : null);
        }

        public string ToCsv(string sepalator = ",")
        {
            return hasValue ? value.ToString() : "null";
        }

        public void FromCsv(string csv)
        {
            if (csv == "null")
            {
                hasValue = false;
                return;
            }
            hasValue = true;
            ConvertTextToValue(csv);
        }

        public static implicit operator T(SerializableNullable<T> value)
            => value.GetValueOrDefault();

        public static explicit operator SerializableNullable<T>(T? value) => new SerializableNullable<T>(value);
    }

    [Serializable]
    public class NullableInt : SerializableNullable<int>
    {
        public NullableInt() : base(default) { }
        public NullableInt(int? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!int.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableUint : SerializableNullable<uint>
    {
        public NullableUint() : base(default) { }
        public NullableUint(uint? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!uint.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableNint : SerializableNullable<nint>
    {
        public NullableNint() : base(default) { }
        public NullableNint(nint? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!int.TryParse(text, out var result))
                Value = null;
            else
                value = result;
        }
    }

    [Serializable]
    public class NullableNuint : SerializableNullable<nuint>
    {
        public NullableNuint() : base(default) { }
        public NullableNuint(nuint? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!uint.TryParse(text, out var result))
                Value = null;
            else
                value = result;
        }
    }

    [Serializable]
    public class NullableFloat : SerializableNullable<float>
    {
        public NullableFloat() : base(default) { }
        public NullableFloat(float? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!float.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableDouble : SerializableNullable<double>
    {
        public NullableDouble() : base(default) { }
        public NullableDouble(double? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!double.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableLong : SerializableNullable<long>
    {
        public NullableLong() : base(default) { }
        public NullableLong(long? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!long.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableUlong : SerializableNullable<ulong>
    {
        public NullableUlong() : base(default) { }
        public NullableUlong(ulong? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!ulong.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableBool : SerializableNullable<bool>
    {
        public NullableBool() : base(default) { }
        public NullableBool(bool? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!bool.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableShort : SerializableNullable<short>
    {
        public NullableShort() : base(default) { }
        public NullableShort(short? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!short.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableUshort : SerializableNullable<ushort>
    {
        public NullableUshort() : base(default) { }
        public NullableUshort(ushort? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!ushort.TryParse(text, out value))
                Value = null;
        }
    }

    [Serializable]
    public class NullableDecimal : SerializableNullable<decimal>
    {
        public NullableDecimal() : base(default) { }
        public NullableDecimal(decimal? value) : base(value)
        {
        }

        protected override void ConvertTextToValue(string text)
        {
            if (!decimal.TryParse(text, out value))
                Value = null;
        }
    }
}
