﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

#define NotCultureDependent
#define NoComparisonOperators
namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;

    public partial struct InternetMediaType
    {
#if !NotField
        private InternetMediaType(string value) => m_Value = value;
        /// <summary>The inner value of the Internet media type.</summary>
        private string m_Value;
#endif
#if !NotIsEmpty
        /// <summary>Returns true if the  Internet media type is empty, otherwise false.</summary>
        [Pure]
        public bool IsEmpty() => m_Value == default;
#endif
#if !NotIsUnknown
        /// <summary>Returns true if the  Internet media type is unknown, otherwise false.</summary>
        [Pure]
        public bool IsUnknown() => m_Value == Unknown.m_Value;
#endif
#if !NotIsEmptyOrUnknown
        /// <summary>Returns true if the  Internet media type is empty or unknown, otherwise false.</summary>
        [Pure]
        public bool IsEmptyOrUnknown() => IsEmpty() || IsUnknown();
#endif
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using Qowaiv.Hashing;

    public partial struct InternetMediaType : IEquatable<InternetMediaType>
    {
        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object obj) => obj is InternetMediaType other && Equals(other);
#if !NotEqualsSvo
        /// <summary>Returns true if this instance and the other Internet media type are equal, otherwise false.</summary>
        /// <param name = "other">The <see cref = "InternetMediaType"/> to compare with.</param>
        [Pure]
        public bool Equals(InternetMediaType other) => m_Value == other.m_Value;
#if !NotGetHashCode
        /// <inheritdoc/>
        [Pure]
        public override int GetHashCode() => Hash.Code(m_Value);
#endif
#endif
        /// <summary>Returns true if the left and right operand are equal, otherwise false.</summary>
        /// <param name = "left">The left operand.</param>
        /// <param name = "right">The right operand</param>
        public static bool operator !=(InternetMediaType left, InternetMediaType right) => !(left == right);
        /// <summary>Returns true if the left and right operand are not equal, otherwise false.</summary>
        /// <param name = "left">The left operand.</param>
        /// <param name = "right">The right operand</param>
        public static bool operator ==(InternetMediaType left, InternetMediaType right) => left.Equals(right);
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public partial struct InternetMediaType : IComparable, IComparable<InternetMediaType>
    {
        /// <inheritdoc/>
        [Pure]
        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }
            else if (obj is InternetMediaType other)
            {
                return CompareTo(other);
            }
            else
            {
                throw new ArgumentException($"Argument must be {GetType().Name}.", nameof(obj));
            }
        }

#if !NotEqualsSvo
        /// <inheritdoc/>
        [Pure]
        public int CompareTo(InternetMediaType other) => Comparer<string>.Default.Compare(m_Value, other.m_Value);
#endif
#if !NoComparisonOperators
        /// <summary>Returns true if the left operator is less then the right operator, otherwise false.</summary>
        public static bool operator <(InternetMediaType l, InternetMediaType r) => l.CompareTo(r) < 0;
        /// <summary>Returns true if the left operator is greater then the right operator, otherwise false.</summary>
        public static bool operator>(InternetMediaType l, InternetMediaType r) => l.CompareTo(r) > 0;
        /// <summary>Returns true if the left operator is less then or equal the right operator, otherwise false.</summary>
        public static bool operator <=(InternetMediaType l, InternetMediaType r) => l.CompareTo(r) <= 0;
        /// <summary>Returns true if the left operator is greater then or equal the right operator, otherwise false.</summary>
        public static bool operator >=(InternetMediaType l, InternetMediaType r) => l.CompareTo(r) >= 0;
#endif
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    public partial struct InternetMediaType : ISerializable
    {
        /// <summary>Initializes a new instance of the Internet media type based on the serialization info.</summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        private InternetMediaType(SerializationInfo info, StreamingContext context)
        {
            Guard.NotNull(info, nameof(info));
            m_Value = (string)info.GetValue("Value", typeof(string));
        }

        /// <summary>Adds the underlying property of the Internet media type to the serialization info.</summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => Guard.NotNull(info, nameof(info)).AddValue("Value", m_Value);
    }
}

namespace Qowaiv.Web
{
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public partial struct InternetMediaType : IXmlSerializable
    {
        /// <summary>Gets the <see href = "XmlSchema"/> to XML (de)serialize the Internet media type.</summary>
        /// <remarks>
        /// Returns null as no schema is required.
        /// </remarks>
        [Pure]
        XmlSchema IXmlSerializable.GetSchema() => null;
        /// <summary>Reads the Internet media type from an <see href = "XmlReader"/>.</summary>
        /// <param name = "reader">An XML reader.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));
            var xml = reader.ReadElementString();
#if !NotCultureDependent
            var val = Parse(xml, CultureInfo.InvariantCulture);
#else
            var val = Parse(xml);
#endif
#if !NotField
            m_Value = val.m_Value;
#endif
            OnReadXml(val);
        }

        partial void OnReadXml(InternetMediaType other);
        /// <summary>Writes the Internet media type to an <see href = "XmlWriter"/>.</summary>
        /// <remarks>
        /// Uses <see cref = "ToXmlString()"/>.
        /// </remarks>
        /// <param name = "writer">An XML writer.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer) => Guard.NotNull(writer, nameof(writer)).WriteString(ToXmlString());
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using Qowaiv.Json;

    public partial struct InternetMediaType
    {
        /// <summary>Creates the Internet media type from a JSON string.</summary>
        /// <param name = "json">
        /// The JSON string to deserialize.
        /// </param>
        /// <returns>
        /// The deserialized Internet media type.
        /// </returns>
        
#if !NotCultureDependent
        [Pure]
        public static InternetMediaType FromJson(string json) => Parse(json, CultureInfo.InvariantCulture);
#else
        [Pure]
        public static InternetMediaType FromJson(string json) => Parse(json);
#endif
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    public partial struct InternetMediaType : IFormattable
    {
        /// <summary>Returns a <see cref = "string "/> that represents the Internet media type.</summary>
        [Pure]
        public override string ToString() => ToString((IFormatProvider)null);
        /// <summary>Returns a formatted <see cref = "string "/> that represents the Internet media type.</summary>
        /// <param name = "format">
        /// The format that describes the formatting.
        /// </param>
        [Pure]
        public string ToString(string format) => ToString(format, null);
        /// <summary>Returns a formatted <see cref = "string "/> that represents the Internet media type.</summary>
        /// <param name = "provider">
        /// The format provider.
        /// </param>
        [Pure]
        public string ToString(IFormatProvider provider) => ToString(null, provider);
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    public partial struct InternetMediaType
    {
#if !NotCultureDependent
        /// <summary>Converts the <see cref = "string "/> to <see cref = "InternetMediaType"/>.</summary>
        /// <param name = "s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <returns>
        /// The parsed Internet media type.
        /// </returns>
        /// <exception cref = "FormatException">
        /// <paramref name = "s"/> is not in the correct format.
        /// </exception>
        [Pure]
        public static InternetMediaType Parse(string s) => Parse(s, null);
        /// <summary>Converts the <see cref = "string "/> to <see cref = "InternetMediaType"/>.</summary>
        /// <param name = "s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <param name = "formatProvider">
        /// The specified format provider.
        /// </param>
        /// <returns>
        /// The parsed Internet media type.
        /// </returns>
        /// <exception cref = "FormatException">
        /// <paramref name = "s"/> is not in the correct format.
        /// </exception>
        [Pure]
        public static InternetMediaType Parse(string s, IFormatProvider formatProvider) => TryParse(s, formatProvider, out InternetMediaType val) ? val : throw new FormatException(QowaivMessages.FormatExceptionInternetMediaType);
        /// <summary>Converts the <see cref = "string "/> to <see cref = "InternetMediaType"/>.</summary>
        /// <param name = "s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <returns>
        /// The Internet media type if the string was converted successfully, otherwise default.
        /// </returns>
        [Pure]
        public static InternetMediaType TryParse(string s) => TryParse(s, null, out InternetMediaType val) ? val : default;
        /// <summary>Converts the <see cref = "string "/> to <see cref = "InternetMediaType"/>.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name = "s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <param name = "result">
        /// The result of the parsing.
        /// </param>
        /// <returns>
        /// True if the string was converted successfully, otherwise false.
        /// </returns>
        [Pure]
        public static bool TryParse(string s, out InternetMediaType result) => TryParse(s, null, out result);
#else
        /// <summary>Converts the <see cref="string"/> to <see cref="InternetMediaType"/>.</summary>
        /// <param name="s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <returns>
        /// The parsed Internet media type.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not in the correct format.
        /// </exception>
        [Pure]
        public static InternetMediaType Parse(string s)
            => TryParse(s, out InternetMediaType val)
            ? val
            : throw new FormatException(QowaivMessages.FormatExceptionInternetMediaType);

        /// <summary>Converts the <see cref="string"/> to <see cref="InternetMediaType"/>.</summary>
        /// <param name="s">
        /// A string containing the Internet media type to convert.
        /// </param>
        /// <returns>
        /// The Internet media type if the string was converted successfully, otherwise default.
        /// </returns>
        [Pure]
        public static InternetMediaType TryParse(string s) => TryParse(s, out InternetMediaType val) ? val : default;
#endif
    }
}

namespace Qowaiv.Web
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    public partial struct InternetMediaType
    {
#if !NotCultureDependent
        /// <summary>Returns true if the value represents a valid Internet media type.</summary>
        /// <param name = "val">
        /// The <see cref = "string "/> to validate.
        /// </param>
        [Pure]
        public static bool IsValid(string val) => IsValid(val, (IFormatProvider)null);
        /// <summary>Returns true if the value represents a valid Internet media type.</summary>
        /// <param name = "val">
        /// The <see cref = "string "/> to validate.
        /// </param>
        /// <param name = "formatProvider">
        /// The <see cref = "IFormatProvider"/> to interpret the <see cref = "string "/> value with.
        /// </param>
        [Pure]
        public static bool IsValid(string val, IFormatProvider formatProvider) => !string.IsNullOrWhiteSpace(val) && TryParse(val, formatProvider, out _);
#else
        /// <summary>Returns true if the value represents a valid Internet media type.</summary>
        /// <param name="val">
        /// The <see cref="string"/> to validate.
        /// </param>
        [Pure]
        public static bool IsValid(string val)
            => !string.IsNullOrWhiteSpace(val)
            && TryParse(val, out _);
#endif
    }
}