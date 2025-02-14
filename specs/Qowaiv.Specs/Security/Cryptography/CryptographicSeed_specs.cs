﻿namespace Security.Cryptography.CryptographicSeed_specs;

public class Computed
{
    [Test]
    public void from_algorithm()
    {
        var algorithm = MD5.Create();
        var seed = algorithm.ComputeCryptographicSeed(new byte[] { 0xD4, 0x1D, 0x8C, 0xD9 });
        seed.Value().Should().Be("t4VYHkd9cKuZGqBn8j2XKw==");
    }
}

public class With_domain_logic
{
    [TestCase(false, "Qowaiv==")]
    [TestCase(true, "")]
    public void IsEmpty_returns(bool result, CryptographicSeed svo)
        => svo.IsEmpty().Should().Be(result);

    [Test]
    public void value_accessable_via_Value_method()
        => Svo.CryptographicSeed.Value().Should().Be("Qowaig==");

    [Test]
    public void value_accessable_via_ToByteArray_method()
        => Svo.CryptographicSeed.ToByteArray()
        .Should().BeEquivalentTo(new byte[] { 0x42, 0x8C, 0x1A, 0x8A });
}

public class Has_constant
{
    [Test]
    public void Empty_represent_default_value()
        => CryptographicSeed.Empty.Should().Be(default(CryptographicSeed));
}

public class equality_is_limited_to_empty
{
    [Test]
    public void not_equal_to_null()
        => Svo.CryptographicSeed.Equals(null).Should().BeFalse();

    [Test]
    public void not_equal_to_other_type()
        => Svo.CryptographicSeed.Equals(new object()).Should().BeFalse();

    [Test]
    public void not_equal_to_different_value()
        => Svo.CryptographicSeed.Equals(CryptographicSeed.Create(17, 69)).Should().BeFalse();

    [Test]
    public void not_equal_to_same_value()
        => Svo.CryptographicSeed.Equals(CryptographicSeed.Parse("Qowaiv==")).Should().BeFalse();

    [Test]
    public void equal_to_for_two_empties()
        => CryptographicSeed.Empty.Equals(CryptographicSeed.Empty).Should().BeTrue();

    [Test]
    public void hash_code_is_value_based()
    {
        Func<int> hash = () => Svo.CryptographicSeed.GetHashCode();
        hash.Should().Throw<HashingNotSupported>();
    }
}

public class Can_be_parsed
{
    [Test]
    public void from_null_string_represents_Empty()
    {
        Assert.AreEqual(CryptographicSeed.Empty, CryptographicSeed.Parse(null));
    }

    [Test]
    public void from_empty_string_represents_Empty()
    {
        Assert.AreEqual(CryptographicSeed.Empty, CryptographicSeed.Parse(string.Empty));
    }

    [Test]
    public void from_valid_input_only_otherwise_return_false_on_TryParse()
        => CryptographicSeed.TryParse("input = invalid", out _).Should().BeFalse();

    [Test]
    public void with_TryParse()
    {
        CryptographicSeed.TryParse("Qowaiv==", out var seed).Should().BeTrue();
        seed.Value().Should().Be("Qowaig==");
    }
}

public class Supports_type_conversion_from
{
    [Test]
    public void via_TypeConverter_registered_with_attribute()
        => typeof(CryptographicSeed).Should().HaveTypeConverterDefined();

    [Test]
    public void null_string()
        => Converting.From<string>(null).To<CryptographicSeed>().Should().Be(CryptographicSeed.Empty);

    [Test]
    public void empty_string()
        => Converting.From(string.Empty).To<CryptographicSeed>().Should().Be(CryptographicSeed.Empty);

    [Test]
    public void @string()
        => Converting.From("Qowaiv==").To<CryptographicSeed>().Value()
        .Should().Be("Qowaig==");
}

public class Does_not_support_type_converstion_to
{
    [Test]
    public void convertered_is_null()
        => Converting.ToString().From(Svo.CryptographicSeed).Should().BeNull();

    [TestCase(typeof(string))]
    [TestCase(typeof(byte[]))]
    public void can_convert_to_always_return_false(Type type)
        => new CryptographicSeedTypeConverter().CanConvertTo(type).Should().BeFalse();
}

public class Supports_JSON_deserialization
{
    [Test]
    public void System_Text_JSON_deserialization()
        => JsonTester.Read_System_Text_JSON<CryptographicSeed>("Qowaiv==").Value().Should().Be(Svo.CryptographicSeed.Value());

    [Test]
    public void convention_based_deserialization()
        => JsonTester.Read<CryptographicSeed>("Qowaiv==").Value().Should().Be(Svo.CryptographicSeed.Value());
}

public class Does_not_supports_JSON_serialization
{
    [Test]
    public void serializes_to_null_System_Text_JSON()
       => JsonTester.Write_System_Text_JSON(Svo.CryptographicSeed).Should().BeNull();

    [Test]
    public void serializes_to_null()
        => JsonTester.Write(Svo.CryptographicSeed).Should().BeNull();
}

public class ToString
{
    [Test]
    public void does_not_reveal_content()
        => Svo.CryptographicSeed.ToString().Should().Be("*****");

    [Test]
    public void does_not_reveal_content_for_empty()
       => CryptographicSeed.Empty.ToString().Should().Be(string.Empty);
}

public class Debugger
{
    [TestCase("{empty}", "")]
    [TestCase("Qowaig==", "Qowaiv==")]
    public void has_custom_display(object display, CryptographicSeed svo) => svo.Should().HaveDebuggerDisplay(display);
}
