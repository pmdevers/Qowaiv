﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

#nullable enable

namespace Qowaiv;

public partial struct Uuid
{
    private Uuid(Guid value) => m_Value = value;

    /// <summary>The inner value of the UUID.</summary>
    private readonly Guid m_Value;

    /// <summary>Returns true if the UUID is empty, otherwise false.</summary>
    [Pure]
    public bool IsEmpty() => m_Value == default;
}

public partial struct Uuid : IEquatable<Uuid>
#if NET7_0_OR_GREATER
    , IEqualityOperators<Uuid, Uuid, bool>
#endif
{
    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Uuid other && Equals(other);

    /// <summary>Returns true if this instance and the other UUID are equal, otherwise false.</summary>
    /// <param name="other">The <see cref="Uuid" /> to compare with.</param>
    [Pure]
    public bool Equals(Uuid other) => m_Value == other.m_Value;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => Hash.Code(m_Value);

    /// <summary>Returns true if the left and right operand are equal, otherwise false.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand</param>
    public static bool operator ==(Uuid left, Uuid right) => left.Equals(right);

    /// <summary>Returns true if the left and right operand are not equal, otherwise false.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand</param>
    public static bool operator !=(Uuid left, Uuid right) => !(left == right);
}

public partial struct Uuid : IComparable, IComparable<Uuid>
{
    /// <inheritdoc />
    [Pure]
    public int CompareTo(object? obj)
    {
        if (obj is null) { return 1; }
        else if (obj is Uuid other) { return CompareTo(other); }
        else { throw new ArgumentException($"Argument must be {GetType().Name}.", nameof(obj)); }
    }
    /// <inheritdoc />
    [Pure]
#nullable disable
    public int CompareTo(Uuid other) => Comparer<Guid>.Default.Compare(m_Value, other.m_Value);
#nullable enable
}

public partial struct Uuid : IFormattable
{
    /// <summary>Returns a <see cref="string"/> that represents the UUID.</summary>
    [Pure]
    public override string ToString() => ToString(provider: null);

    /// <summary>Returns a formatted <see cref="string"/> that represents the UUID.</summary>
    /// <param name="format">
    /// The format that describes the formatting.
    /// </param>
    [Pure]
    public string ToString(string? format) => ToString(format, formatProvider: null);

    /// <summary>Returns a formatted <see cref="string"/> that represents the UUID.</summary>
    /// <param name="provider">
    /// The format provider.
    /// </param>
    [Pure]
    public string ToString(IFormatProvider? provider) => ToString(format: null, provider);
}

public partial struct Uuid : ISerializable
{
    /// <summary>Initializes a new instance of the UUID based on the serialization info.</summary>
    /// <param name="info">The serialization info.</param>
    /// <param name="context">The streaming context.</param>
    private Uuid(SerializationInfo info, StreamingContext context)
    {
        Guard.NotNull(info, nameof(info));
        m_Value = info.GetValue("Value", typeof(Guid)) is Guid val ? val : default(Guid);
    }

    /// <summary>Adds the underlying property of the UUID to the serialization info.</summary>
    /// <param name="info">The serialization info.</param>
    /// <param name="context">The streaming context.</param>
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        => Guard.NotNull(info, nameof(info)).AddValue("Value", m_Value);
}

public partial struct Uuid
{
    /// <summary>Creates the UUID from a JSON string.</summary>
    /// <param name="json">
    /// The JSON string to deserialize.
    /// </param>
    /// <returns>
    /// The deserialized UUID.
    /// </returns>
    [Pure]
    public static Uuid FromJson(string? json) => Parse(json, CultureInfo.InvariantCulture);
}

public partial struct Uuid : IXmlSerializable
{
    /// <summary>Gets the <see href="XmlSchema" /> to XML (de)serialize the UUID.</summary>
    /// <remarks>
    /// Returns null as no schema is required.
    /// </remarks>
    [Pure]
    XmlSchema? IXmlSerializable.GetSchema() => (XmlSchema?)null;

    /// <summary>Reads the UUID from an <see href="XmlReader" />.</summary>
    /// <param name="reader">An XML reader.</param>
    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        Guard.NotNull(reader, nameof(reader));
        var xml = reader.ReadElementString();
        System.Runtime.CompilerServices.Unsafe.AsRef(this) = Parse(xml, CultureInfo.InvariantCulture);
    }

    /// <summary>Writes the UUID to an <see href="XmlWriter" />.</summary>
    /// <remarks>
    /// Uses <see cref="ToXmlString()"/>.
    /// </remarks>
    /// <param name="writer">An XML writer.</param>
    void IXmlSerializable.WriteXml(XmlWriter writer)
        => Guard.NotNull(writer, nameof(writer)).WriteString(ToXmlString());
}

public partial struct Uuid
#if NET7_0_OR_GREATER
    : IParsable<Uuid>
#endif
{
    /// <summary>Converts the <see cref="string"/> to <see cref="Uuid"/>.</summary>
    /// <param name="s">
    /// A string containing the UUID to convert.
    /// </param>
    /// <returns>
    /// The parsed UUID.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not in the correct format.
    /// </exception>
    [Pure]
    public static Uuid Parse(string? s) => Parse(s, null);

    /// <summary>Converts the <see cref="string"/> to <see cref="Uuid"/>.</summary>
    /// <param name="s">
    /// A string containing the UUID to convert.
    /// </param>
    /// <param name="formatProvider">
    /// The specified format provider.
    /// </param>
    /// <returns>
    /// The parsed UUID.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not in the correct format.
    /// </exception>
    [Pure]
    public static Uuid Parse(string? s, IFormatProvider? formatProvider) => TryParse(s, formatProvider) ?? throw new FormatException(QowaivMessages.FormatExceptionUuid);

    /// <summary>Converts the <see cref="string"/> to <see cref="Uuid"/>.</summary>
    /// <param name="s">
    /// A string containing the UUID to convert.
    /// </param>
    /// <returns>
    /// The UUID if the string was converted successfully, otherwise default.
    /// </returns>
    [Pure]
    public static Uuid? TryParse(string? s) => TryParse(s, null);

    /// <summary>Converts the <see cref="string"/> to <see cref="Uuid"/>.</summary>
    /// <param name="s">
    /// A string containing the UUID to convert.
    /// </param>
    /// <param name="formatProvider">
    /// The specified format provider.
    /// </param>
    /// <returns>
    /// The UUID if the string was converted successfully, otherwise default.
    /// </returns>
    [Pure]
    public static Uuid? TryParse(string? s, IFormatProvider? formatProvider) => TryParse(s, formatProvider, out var val) ? val : default(Uuid?);

    /// <summary>Converts the <see cref="string"/> to <see cref="Uuid"/>.
    /// A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">
    /// A string containing the UUID to convert.
    /// </param>
    /// <param name="result">
    /// The result of the parsing.
    /// </param>
    /// <returns>
    /// True if the string was converted successfully, otherwise false.
    /// </returns>
    [Pure]
    public static bool TryParse(string? s, out Uuid result) => TryParse(s, null, out result);
}

public partial struct Uuid
{
    /// <summary>Returns true if the value represents a valid UUID.</summary>
    /// <param name="val">
    /// The <see cref="string"/> to validate.
    /// </param>
    [Pure]
    public static bool IsValid(string? val) => IsValid(val, (IFormatProvider?)null);

    /// <summary>Returns true if the value represents a valid UUID.</summary>
    /// <param name="val">
    /// The <see cref="string"/> to validate.
    /// </param>
    /// <param name="formatProvider">
    /// The <see cref="IFormatProvider"/> to interpret the <see cref="string"/> value with.
    /// </param>
    [Pure]
    public static bool IsValid(string? val, IFormatProvider? formatProvider)
        => !string.IsNullOrWhiteSpace(val)
        && TryParse(val, formatProvider, out _);
}
