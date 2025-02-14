﻿using Qowaiv.Conversion.IO;
using System.IO;

namespace Qowaiv.IO;

/// <summary>Represents a stream size.</summary>
/// <remarks>
/// A stream size measures the size of a computer file or stream. Typically it is 
/// measured in bytes with an SI prefix. The actual amount of disk space consumed by
/// the file depends on the file system. The maximum stream size a file system
/// supports depends on the number of bits reserved to store size information
/// and the total size of the file system. This value type can not represent
/// stream sizes bigger than long.MaxValue.
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
[Serializable, SingleValueObject(SingleValueStaticOptions.Continuous, typeof(long))]
[OpenApiDataType(description: "Stream size notation (in byte).", example: 1024, type: "integer", format: "stream-size")]
[OpenApi.OpenApiDataType(description: "Stream size notation (in byte).", example: 1024, type: "integer", format: "stream-size")]
[TypeConverter(typeof(StreamSizeTypeConverter))]
#if NET5_0_OR_GREATER
[System.Text.Json.Serialization.JsonConverter(typeof(Json.IO.StreamSizeJsonConverter))]
#endif
public readonly partial struct StreamSize : ISerializable, IXmlSerializable, IFormattable, IEquatable<StreamSize>, IComparable, IComparable<StreamSize>
#if NET7_0_OR_GREATER
    , IIncrementOperators<StreamSize>, IDecrementOperators<StreamSize>
    , IUnaryPlusOperators<StreamSize, StreamSize>, IUnaryNegationOperators<StreamSize, StreamSize>
    , IAdditionOperators<StreamSize, StreamSize, StreamSize>, ISubtractionOperators<StreamSize, StreamSize, StreamSize>
    , IAdditionOperators<StreamSize, Percentage, StreamSize>, ISubtractionOperators<StreamSize, Percentage, StreamSize>
    , IMultiplyOperators<StreamSize, Percentage, StreamSize>, IDivisionOperators<StreamSize, Percentage, StreamSize>
    , IMultiplyOperators<StreamSize, decimal, StreamSize>, IDivisionOperators<StreamSize, decimal, StreamSize>
    , IMultiplyOperators<StreamSize, double, StreamSize>, IDivisionOperators<StreamSize, double, StreamSize>
    , IMultiplyOperators<StreamSize, long, StreamSize>, IDivisionOperators<StreamSize, long, StreamSize>
    , IMultiplyOperators<StreamSize, int, StreamSize>, IDivisionOperators<StreamSize, int, StreamSize>
    , IMultiplyOperators<StreamSize, short, StreamSize>, IDivisionOperators<StreamSize, short, StreamSize>
    , IMultiplyOperators<StreamSize, ulong, StreamSize>, IDivisionOperators<StreamSize, ulong, StreamSize>
    , IMultiplyOperators<StreamSize, uint, StreamSize>, IDivisionOperators<StreamSize, uint, StreamSize>
    , IMultiplyOperators<StreamSize, ushort, StreamSize>, IDivisionOperators<StreamSize, ushort, StreamSize>
#endif
{
    /// <summary>Represents an empty/not set stream size.</summary>
    public static readonly StreamSize Zero;

    /// <summary>Represents 1 Byte.</summary>
    public static readonly StreamSize Byte = new(1L);

    /// <summary>Represents 1 kilobyte (1,000 byte).</summary>
    public static readonly StreamSize KB = new(1_000L);

    /// <summary>Represents 1 Megabyte (1,000,000 byte).</summary>
    public static readonly StreamSize MB = new(1_000_000L);

    /// <summary>Represents 1 Gigabyte (1,000,000,000 byte).</summary>
    public static readonly StreamSize GB = new(1_000_000_000L);

    /// <summary>Represents 1 Terabyte (1,000,000,000,000 byte).</summary>
    public static readonly StreamSize TB = new(1_000_000_000_000L);

    /// <summary>Represents 1 Petabyte (1,000,000,000,000,000 byte).</summary>
    public static readonly StreamSize PB = new(1_000_000_000_000_000L);

    /// <summary>Represents 1 kibibyte (1,024 byte).</summary>
    public static readonly StreamSize KiB = new(1L << 10);

    /// <summary>Represents 1 Mebibyte (1,048,576 byte).</summary>
    public static readonly StreamSize MiB = new(1L << 20);

    /// <summary>Represents 1 Gibibyte (1,073,741,824 byte).</summary>
    public static readonly StreamSize GiB = new(1L << 30);

    /// <summary>Represents 1 Tebibyte (1,099,511,627,776 byte).</summary>
    public static readonly StreamSize TiB = new(1L << 40);

    /// <summary>Represents 1 Petabyte (1,125,899,906,842,624 byte).</summary>
    public static readonly StreamSize PiB = new(1L << 50);

    /// <summary>Represents the minimum stream size that can be represented.</summary>
    public static readonly StreamSize MinValue = new(long.MinValue);

    /// <summary>Represents the maximum stream size that can be represented.</summary>
    public static readonly StreamSize MaxValue = new(long.MaxValue);

    /// <summary>Initializes a new instance of a stream size.</summary>
    /// <param name="size">
    /// The number of bytes.
    /// </param>
    public StreamSize(long size) => m_Value = size;

    /// <summary>The inner value of the stream size.</summary>
    private readonly long m_Value;

    #region StreamSize manipulation

    /// <summary>Gets the sign of the stream size.</summary>
    [Pure]
    public int Sign() => m_Value.Sign();

    /// <summary>Returns the absolute value of stream size.</summary>
    [Pure]
    public StreamSize Abs() => m_Value.Abs();

    /// <summary>Increases the stream size with one byte.</summary>
    [Pure]
    internal StreamSize Increment() => Add(Byte);

    /// <summary>Decreases the stream size with one percent.</summary>
    [Pure]
    internal StreamSize Decrement() => Subtract(Byte);

    /// <summary>Pluses the stream size.</summary>
    [Pure]
    internal StreamSize Plus() => +m_Value;

    /// <summary>Negates the stream size.</summary>
    [Pure]
    internal StreamSize Negate() => -m_Value;

    /// <summary>Adds a stream size to the current stream size.</summary>
    /// <param name="streamSize">
    /// The stream size to add.
    /// </param>
    [Pure]
    public StreamSize Add(StreamSize streamSize) => m_Value + streamSize.m_Value;

    /// <summary>Adds the specified percentage to the stream size.</summary>
    /// <param name="p">
    /// The percentage to add.
    /// </param>
    [Pure]
    public StreamSize Add(Percentage p) => m_Value.Add(p);

    /// <summary>Subtracts a stream size from the current stream size.</summary>
    /// <param name="streamSize">
    /// The stream size to Subtract.
    /// </param>
    [Pure]
    public StreamSize Subtract(StreamSize streamSize) => m_Value - streamSize.m_Value;

    /// <summary>AddsSubtract the specified percentage from the stream size.</summary>
    /// <param name="p">
    /// The percentage to add.
    /// </param>
    [Pure]
    public StreamSize Subtract(Percentage p) => m_Value.Subtract(p);

    #region Multiply

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(decimal factor) => (StreamSize)(m_Value * factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(double factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(float factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(Percentage factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(long factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(int factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Multiply(short factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Multiply(ulong factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Multiply(uint factor) => Multiply((decimal)factor);

    /// <summary>Multiplies the stream size with a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Multiply(ushort factor) => Multiply((decimal)factor);

    #endregion

    #region Divide

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(decimal factor) => (StreamSize)(m_Value / factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(double factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(float factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(Percentage factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(long factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(int factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [Pure]
    public StreamSize Divide(short factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Divide(ulong factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Divide(uint factor) => Divide((decimal)factor);

    /// <summary>Divide the stream size by a specified factor.</summary>
    /// <param name="factor">
    /// The factor to multiply with.
    /// </param>
    [CLSCompliant(false)]
    [Pure]
    public StreamSize Divide(ushort factor) => Divide((decimal)factor);

    #endregion

    /// <summary>Increases the stream size with one byte.</summary>
    public static StreamSize operator ++(StreamSize streamSize) => streamSize.Increment();

    /// <summary>Decreases the stream size with one byte.</summary>
    public static StreamSize operator --(StreamSize streamSize) => streamSize.Decrement();

    /// <summary>Unitary plusses the stream size.</summary>
    public static StreamSize operator +(StreamSize streamSize) => streamSize.Plus();

    /// <summary>Negates the stream size.</summary>
    public static StreamSize operator -(StreamSize streamSize) => streamSize.Negate();

    /// <summary>Adds the left and the right stream size.</summary>
    public static StreamSize operator +(StreamSize l, StreamSize r) => l.Add(r);

    /// <summary>Subtracts the right from the left stream size.</summary>
    public static StreamSize operator -(StreamSize l, StreamSize r) => l.Subtract(r);

    /// <summary>Adds the percentage to the stream size.</summary>
    public static StreamSize operator +(StreamSize streamSize, Percentage p) => streamSize.Add(p);

    /// <summary>Subtracts the percentage from the stream size.</summary>
    public static StreamSize operator -(StreamSize streamSize, Percentage p) => streamSize.Subtract(p);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, decimal factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, double factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, float factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, Percentage factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, long factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, int factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    public static StreamSize operator *(StreamSize streamSize, short factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator *(StreamSize streamSize, ulong factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator *(StreamSize streamSize, uint factor) => streamSize.Multiply(factor);

    /// <summary>Multiplies the stream size with the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator *(StreamSize streamSize, ushort factor) => streamSize.Multiply(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, decimal factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, double factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, float factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, Percentage factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, long factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, int factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    public static StreamSize operator /(StreamSize streamSize, short factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator /(StreamSize streamSize, ulong factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator /(StreamSize streamSize, uint factor) => streamSize.Divide(factor);

    /// <summary>Divides the stream size by the factor.</summary>
    [CLSCompliant(false)]
    public static StreamSize operator /(StreamSize streamSize, ushort factor) => streamSize.Divide(factor);

    #endregion

    /// <summary>Deserializes the stream size from a JSON number.</summary>
    /// <param name="json">
    /// The JSON number to deserialize.
    /// </param>
    /// <returns>
    /// The deserialized stream size.
    /// </returns>
    [Pure]
    public static StreamSize FromJson(double json) => new((long)json);

    /// <summary>Deserializes the stream size from a JSON number.</summary>
    /// <param name="json">
    /// The JSON number to deserialize.
    /// </param>
    /// <returns>
    /// The deserialized stream size.
    /// </returns>
    [Pure]
    public static StreamSize FromJson(long json) => new(json);

    /// <summary>Serializes the stream size to a JSON node.</summary>
    /// <returns>
    /// The serialized JSON number.
    /// </returns>
    [Pure]
    public long ToJson() => m_Value;

    /// <summary>Returns a <see cref="string"/> that represents the current stream size for debug purposes.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => this.DebuggerDisplay("{0: F}");

    /// <summary>Returns a <see cref="string"/> that represents the current stream size.</summary>
    [Pure]
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Returns a formatted <see cref="string"/> that represents the current stream size.</summary>
    /// <param name="format">
    /// The format that describes the formatting.
    /// </param>
    [Pure]
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Returns a formatted <see cref="string"/> that represents the current stream size.</summary>
    /// <param name="provider">
    /// The format provider.
    /// </param>
    [Pure]
    public string ToString(IFormatProvider provider) => ToString("0 byte", provider);

    /// <summary>Returns a formatted <see cref="string"/> that represents the current stream size.</summary>
    /// <param name="format">
    /// The format that describes the formatting.
    /// </param>
    /// <param name="formatProvider">
    /// The format provider.
    /// </param>
    /// <remarks>
    /// There are basically two ways to format the stream size. The first one is
    /// automatic. Based on the size the extension is chosen (byte, kB, MB, GB, ect.).
    /// This can be specified by a s/S (short notation) and a f/F (full notation).
    /// 
    /// The other option is to specify the extension explicitly. So Megabyte,
    /// kB, ect. No extension is also possible.
    /// 
    /// Short notation:
    /// 8900.ToString("s") => 8900b
    /// 238900.ToString("s") => 238.9kb
    /// 238900.ToString(" S") => 238.9 kB
    /// 238900.ToString("0000.00 S") => 0238.90 kB
    ///
    /// Full notation:
    /// 8900.ToString("0.0 f") => 8900.0 byte
    /// 238900.ToString("0 f") => 234 kilobyte
    /// 1238900.ToString("0.00 F") => 1.24 Megabyte
    /// 
    /// Custom:
    /// 8900.ToString("0.0 kb") => 8.9 kb
    /// 238900.ToString("0.0 MB") => 0.2 MB
    /// 1238900.ToString("#,##0.00 Kilobyte") => 1,239.00 Kilobyte
    /// 1238900.ToString("#,##0") => 1,238,900
    /// </remarks>
    [Pure]
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (StringFormatter.TryApplyCustomFormatter(format, this, formatProvider, out string formatted))
        {
            return formatted;
        }
        else if (FormattedPattern.Match(format ?? string.Empty) is { Success: true } match)
        {
            return ToFormattedString(formatProvider, match);
        }
        else
        {
            var streamSizeMarker = GetStreamSizeMarker(format);
            var decimalFormat = GetWithoutStreamSizeMarker(format, streamSizeMarker);
            var mp = GetMultiplier(streamSizeMarker);

            decimal size = (decimal)m_Value / (decimal)mp;

            return size.ToString(decimalFormat, formatProvider) + streamSizeMarker;
        }
    }

    /// <summary>Gets an XML string representation of the stream size.</summary>
    [Pure]
    private string ToXmlString() => ToString(CultureInfo.InvariantCulture);

    [Pure]
    private string ToFormattedString(IFormatProvider? formatProvider, Match match)
    {
        var format = match.Groups["format"].Value;
        var streamSizeMarker = match.Groups["streamSizeMarker"].Value;

        var isKibi = streamSizeMarker.Contains('i');

        var sb = new StringBuilder();
        if (m_Value < 0)
        {
            sb.Append(formatProvider.NegativeSign());
        }

        decimal size = Math.Abs(m_Value);
        var order = 0;
        if (size > 9999)
        {
            if (string.IsNullOrEmpty(format)) { format = "0.0"; }

            // Rounding would potential lead to 1000.
            while (size >= 999.5m)
            {
                order++;
                size /= isKibi ? 1024 : 1000;
            }
        }

        sb.Append(size.ToString(format, formatProvider));

        if (streamSizeMarker[0] == ' ')
        {
            sb.Append(' ');
            streamSizeMarker = streamSizeMarker.Substring(1);
        }
        return AppendExtension(sb, streamSizeMarker, order).ToString();
    }

    [FluentSyntax]
    private static StringBuilder AppendExtension(StringBuilder sb, string streamSizeMarker, int order) 
        => streamSizeMarker switch
        {
            "s" => sb.Append(ShortLabels[order].ToLowerInvariant()),
            "S" => sb.Append(ShortLabels[order]),
            "f" => sb.Append(FullLabels[order].ToLowerInvariant()),
            "F" => sb.Append(FullLabels[order]),
            "si" => sb.Append(ShortLabels1024[order].ToLowerInvariant()),
            "Si" => sb.Append(ShortLabels1024[order]),
            "fi" => sb.Append(FullLabels1024[order].ToLowerInvariant()),
            "Fi" => sb.Append(FullLabels1024[order]),
            _ => sb,
        };

    private static readonly Regex FormattedPattern = new("^(?<format>.*)(?<streamSizeMarker> ?[sSfF]i?)$", RegOptions.RightToLeft, RegOptions.Timeout);
    private static readonly string[] ShortLabels = { "B", "kB", "MB", "GB", "TB", "PB", "EB" };
    private static readonly string[] FullLabels = { "byte", "kilobyte", "Megabyte", "Gigabyte", "Terabyte", "Petabyte", "Exabyte" };
    private static readonly string[] ShortLabels1024 = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
    private static readonly string[] FullLabels1024 = { "byte", "kibibyte", "Mebibyte", "Gibibyte", "Tebibyte", "Pebibyte", "Exbibyte" };

    /// <summary>Casts a stream size to a System.int.</summary>
    public static explicit operator int(StreamSize val) => (int)val.m_Value;

    /// <summary>Casts an int to a stream size.</summary>
    public static implicit operator StreamSize(int val) => new(val);

    /// <summary>Casts a stream size to a System.long.</summary>
    public static explicit operator long(StreamSize val) => val.m_Value;

    /// <summary>Casts a long to a stream size.</summary>
    public static implicit operator StreamSize(long val) => new(val);

    /// <summary>Casts a stream size to a System.Double.</summary>
    public static explicit operator double(StreamSize val) => val.m_Value;

    /// <summary>Casts a double to a stream size.</summary>
    public static explicit operator StreamSize(double val) => new((long)val);

    /// <summary>Casts a stream size to a System.Decimal.</summary>
    public static explicit operator decimal(StreamSize val) => val.m_Value;

    /// <summary>Casts a decimal to a stream size.</summary>
    public static explicit operator StreamSize(decimal val) => new((long)val);

    /// <summary>Converts the string to a stream size.
    /// A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">
    /// A string containing a stream size to convert.
    /// </param>
    /// <param name="formatProvider">
    /// The specified format provider.
    /// </param>
    /// <param name="result">
    /// The result of the parsing.
    /// </param>
    /// <returns>
    /// True if the string was converted successfully, otherwise false.
    /// </returns>
    public static bool TryParse(string? s, IFormatProvider? formatProvider, out StreamSize result)
    {
        result = default;
        if (string.IsNullOrEmpty(s)) return false;
        else
        {

            var streamSizeMarker = GetStreamSizeMarker(s);
            var size = GetWithoutStreamSizeMarker(s, streamSizeMarker);
            var factor = GetMultiplier(streamSizeMarker);

            if (long.TryParse(size, NumberStyles.Number, formatProvider, out long sizeInt64) &&
                sizeInt64 <= long.MaxValue / factor &&
                sizeInt64 >= long.MinValue / factor)
            {
                result = new StreamSize(sizeInt64 * factor);
                return true;
            }
            else if (decimal.TryParse(size, NumberStyles.Number, formatProvider, out decimal sizeDecimal) &&
                sizeDecimal <= decimal.MaxValue / factor &&
                sizeDecimal >= decimal.MinValue / factor)
            {
                sizeDecimal *= factor;

                if (sizeDecimal <= long.MaxValue && sizeDecimal >= long.MinValue)
                {
                    result = new StreamSize((long)sizeDecimal);
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>Creates a stream size based on the size in kilobytes.</summary>
    /// <param name="size">
    /// The size in kilobytes.
    /// </param>
    [Pure]
    public static StreamSize FromKilobytes(double size) => KB * size;

    /// <summary>Creates a stream size based on the size in megabytes.</summary>
    /// <param name="size">
    /// The size in megabytes.
    /// </param>
    [Pure]
    public static StreamSize FromMegabytes(double size) => MB * size;

    /// <summary>Creates a stream size based on the size in gigabytes.</summary>
    /// <param name="size">
    /// The size in gigabytes.
    /// </param>
    [Pure]
    public static StreamSize FromGigabytes(double size) => GB * size;

    /// <summary>Creates a stream size based on the size in terabytes.</summary>
    /// <param name="size">
    /// The size in terabytes.
    /// </param>
    [Pure]
    public static StreamSize FromTerabytes(double size) => TB * size;

    /// <summary>Creates a stream size based on the size in kibibytes.</summary>
    /// <param name="size">
    /// The size in kilobytes.
    /// </param>
    [Pure]
    public static StreamSize FromKibibytes(double size) => KiB * size;

    /// <summary>Creates a stream size based on the size in mebibytes.</summary>
    /// <param name="size">
    /// The size in megabytes.
    /// </param>
    [Pure]
    public static StreamSize FromMebibytes(double size) => MiB * size;

    /// <summary>Creates a stream size based on the size in gigabytes.</summary>
    /// <param name="size">
    /// The size in gigabytes.
    /// </param>
    [Pure]
    public static StreamSize FromGibibytes(double size) => GiB * size;

    /// <summary>Creates a stream size based on the size in tebibytes.</summary>
    /// <param name="size">
    /// The size in tebibytes.
    /// </param>
    [Pure]
    public static StreamSize FromTebibytes(double size) => TiB * size;

    /// <summary>Creates a stream size from a file info.</summary>
    [Pure]
    public static StreamSize FromByteArray(byte[] bytes)
        => new(Guard.NotNull(bytes, nameof(bytes)).Length);

    /// <summary>Creates a stream size from a file info.</summary>
    [Pure]
    public static StreamSize FromFileInfo(FileInfo fileInfo)
        => new(Guard.NotNull(fileInfo, nameof(fileInfo)).Length);

    /// <summary>Creates a stream size from a stream.</summary>
    [Pure]
    public static StreamSize FromStream(Stream stream)
        => new(Guard.NotNull(stream, nameof(stream)).Length);

    [Pure]
    private static string GetStreamSizeMarker(string? input)
    {
        if (input is { Length: > 0 })
        {
            var length = input.Length;

            foreach (var marker in MultiplierLookup.Keys)
            {
                if (input.ToUpperInvariant().EndsWith(' ' + marker, StringComparison.Ordinal))
                {
                    return input.Substring(length - marker.Length - 1);
                }
                else if (input.ToUpperInvariant().EndsWith(marker, StringComparison.Ordinal))
                {
                    return input.Substring(length - marker.Length);
                }
            }
            return string.Empty;
        }
        else return string.Empty;
    }

    [Pure]
    private static string GetWithoutStreamSizeMarker(string? input, string streamSizeMarker)
        => input is { Length: > 0 }
        ? input.Substring(0, input.Length - streamSizeMarker.Length)
        : string.Empty;

    [Pure]
    private static long GetMultiplier(string streamSizeMarker)
        => string.IsNullOrEmpty(streamSizeMarker)
        ? 1
        : MultiplierLookup[streamSizeMarker.ToUpperInvariant().Trim()];

    private static readonly Dictionary<string, long> MultiplierLookup = new()
    {
        { "KILOBYTE", 1000L },
        { "MEGABYTE", 1000000L },
        { "GIGABYTE", 1000000000L },
        { "TERABYTE", 1000000000000L },
        { "PETABYTE", 1000000000000000L },
        { "EXABYTE", 1000000000000000000L },

        { "KB", 1000L },
        { "MB", 1000000L },
        { "GB", 1000000000L },
        { "TB", 1000000000000L },
        { "PB", 1000000000000000L },
        { "EB", 1000000000000000000L },

        { "KIBIBYTE", 1L << 10 },
        { "MEBIBYTE", 1L << 20 },
        { "GIBIBYTE", 1L << 30 },
        { "TEBIBYTE", 1L << 40 },
        { "PEBIBYTE", 1L << 50 },
        { "EXBIBYTE", 1L << 60 },

        { "KIB", 1L << 10 },
        { "MIB", 1L << 20 },
        { "GIB", 1L << 30 },
        { "TIB", 1L << 40 },
        { "PIB", 1L << 50 },
        { "EIB", 1L << 60 },

        { "BYTE", 1 },
        { "B", 1 },
    };
}
