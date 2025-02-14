﻿namespace Year_specs;

public class With_domain_logic
{
    [TestCase(false, 1979)]
    [TestCase(false, "?")]
    [TestCase(true, "")]
    public void IsEmpty_returns(bool result, Year svo)
    {
        Assert.AreEqual(result, svo.IsEmpty());
    }

    [TestCase(false, 1979)]
    [TestCase(true, "?")]
    [TestCase(true, "")]
    public void IsEmptyOrUnknown_returns(bool result, Year svo)
    {
        Assert.AreEqual(result, svo.IsEmptyOrUnknown());
    }

    [TestCase(false, 1979)]
    [TestCase(true, "?")]
    [TestCase(false, "")]
    public void IsUnknown_returns(bool result, Year svo)
    {
        Assert.AreEqual(result, svo.IsUnknown());
    }
}

public class Is_not_leap_year_when
{
    [TestCase("")]
    [TestCase("?")]
    public void empty_or_unknown(Year year)
    {
        Assert.IsFalse(year.IsLeapYear);
    }

    [TestCase(1979)]
    [TestCase(2017)]
    public void not_divisable_by_4(Year year)
    {
        Assert.IsFalse(year.IsLeapYear);
    }

    [TestCase(1800)]
    [TestCase(1900)]
    public void divisiable_by_100_not_by_400(Year year)
    {
        Assert.IsFalse(year.IsLeapYear);
    }
}

public class Is_leap_year
{
    [TestCase(1988)]
    [TestCase(2004)]
    public void divisable_by_4_not_by_100(Year year)
    {
        Assert.IsTrue(year.IsLeapYear);
    }

    [TestCase(1600)]
    [TestCase(2000)]
    public void divisable_by_400(Year year)
    {
        Assert.IsTrue(year.IsLeapYear);
    }
}

public class Is_valid_for
{
    [TestCase("?")]
    [TestCase("unknown")]
    public void strings_representing_unknown(string input)
    {
        Assert.IsTrue(Year.IsValid(input));
    }

    [TestCase("1979", "nl")]
    [TestCase("1979", "nl")]
    public void strings_representing_SVO(string input, CultureInfo culture)
    {
        Assert.IsTrue(Year.IsValid(input, culture));
    }
}

public class Is_not_valid_for
{
    [Test]
    public void string_empty()
    {
        Assert.IsFalse(Year.IsValid(string.Empty));
    }

    [Test]
    public void string_null()
    {
        Assert.IsFalse(Year.IsValid((string)null));
    }

    [Test]
    public void whitespace()
    {
        Assert.IsFalse(Year.IsValid(" "));
    }

    [Test]
    public void garbage()
    {
        Assert.IsFalse(Year.IsValid("garbage"));
    }
}

public class Has_constant
{
    [Test]
    public void Empty_represent_default_value()
    {
        Assert.AreEqual(default(Year), Year.Empty);
    }

        [Test]
        public void MinValue_represents_1()
        {
            Year min = 1.CE();
            Assert.AreEqual(min, Year.MinValue);
        }

        [Test]
        public void MaxValue_represents_9999()
        {
            Year max = 9999.CE();
            Assert.AreEqual(max, Year.MaxValue);
        }
    }

public class Is_equal_by_value
{
    [Test]
    public void not_equal_to_null()
    {
        Assert.IsFalse(Svo.Year.Equals(null));
    }

    [Test]
    public void not_equal_to_other_type()
    {
        Assert.IsFalse(Svo.Year.Equals(new object()));
    }

        [Test]
        public void not_equal_to_different_value()
        {
            Year other = 2017.CE();
            Assert.IsFalse(Svo.Year.Equals(other));
        }

        [Test]
        public void equal_to_same_value()
        {
            Assert.IsTrue(Svo.Year.Equals(1979.CE()));
        }

        [Test]
        public void equal_operator_returns_true_for_same_values()
        {
            Assert.IsTrue(Svo.Year == 1979.CE());
        }

        [Test]
        public void equal_operator_returns_false_for_different_values()
        {
            Assert.IsFalse(Svo.Year == 2017.CE());
        }

        [Test]
        public void not_equal_operator_returns_false_for_same_values()
        {
            Assert.IsFalse(Svo.Year != 1979.CE());
        }

        [Test]
        public void not_equal_operator_returns_true_for_different_values()
        {
            Assert.IsTrue(Svo.Year != 2017.CE());
        }

    [TestCase("", 0)]
    [TestCase("1979", 665629288)]
    public void hash_code_is_value_based(Year svo, int hash)
    {
        using (Hash.WithoutRandomizer())
        {
            svo.GetHashCode().Should().Be(hash);
        }
    }
}

public class Can_be_parsed
{
    [Test]
    public void from_null_string_represents_Empty()
    {
        Assert.AreEqual(Year.Empty, Year.Parse(null));
    }

    [Test]
    public void from_empty_string_represents_Empty()
    {
        Assert.AreEqual(Year.Empty, Year.Parse(string.Empty));
    }

    [Test]
    public void from_question_mark_represents_Unknown()
    {
        Assert.AreEqual(Year.Unknown, Year.Parse("?"));
    }

    [Test]
    public void from_string()
    {
        var parsed = Year.Parse("1979");
        Assert.AreEqual(Svo.Year, parsed);
    }

    [Test]
    public void from_valid_input_only_otherwise_throws_on_Parse()
    {
        using (TestCultures.En_GB.Scoped())
        {
            var exception = Assert.Throws<FormatException>(() => Year.Parse("invalid input"));
            Assert.AreEqual("Not a valid year", exception.Message);
        }
    }

    [Test]
    public void from_valid_input_only_otherwise_return_false_on_TryParse()
    {
        Assert.IsFalse(Year.TryParse("invalid input", out _));
    }

    [Test]
    public void from_invalid_as_null_with_TryParse()
        => Year.TryParse("invalid input").Should().BeNull();

    [Test]
    public void with_TryParse_returns_SVO()
    {
        Assert.AreEqual(Svo.Year, Year.TryParse("1979"));
    }
}

public class Can_be_created_from_int
{
    [Test]
    public void empty_for_not_set_int()
    {
        Assert.AreEqual(Year.Empty, Year.TryCreate(default));
    }

    [Test]
    public void empty_for_not_invalid_int()
    {
        Assert.AreEqual(Year.Empty, Year.TryCreate(-10));
    }

    [Test]
    public void within_range()
    {
        Assert.AreEqual(Svo.Year, Year.TryCreate(1979));
    }

    [TestCase(0)]
    [TestCase(10000)]
    public void but_not_outside_1_to_9999(int year)
    {
        Assert.IsFalse(Year.TryCreate(year, out _));
    }
}

public class Has_custom_formatting
{
    [Test]
    public void default_value_is_represented_as_string_empty()
    {
        Assert.AreEqual(string.Empty, default(Year).ToString());
    }

    [Test]
    public void unknown_value_is_represented_as_unknown()
    {
        Assert.AreEqual("?", Year.Unknown.ToString());
    }

    [Test]
    public void custom_format_provider_is_applied()
    {
        var formatted = Svo.Year.ToString("#,##0", FormatProvider.CustomFormatter);
        Assert.AreEqual("Unit Test Formatter, value: '1,979', format: '#,##0'", formatted);
    }

    [TestCase("en-GB", null, 1979, "1979")]
    [TestCase("nl-BE", "#,##0", 1979, "1.979")]
    [TestCase("en-US", "00000", 1979, "01979")]
    public void culture_dependent(CultureInfo culture, string format, Year svo, string expected)
    {
        using (culture.Scoped())
        {
            Assert.AreEqual(expected, svo.ToString(format));
        }
    }

    [Test]
    public void with_current_thread_culture_as_default()
    {
        using (new CultureInfoScope(culture: TestCultures.Nl_NL, cultureUI: TestCultures.En_GB))
        {
            Assert.AreEqual("1979", Svo.Year.ToString(provider: null));
        }
    }
}

public class Is_comparable
{
    [Test]
    public void to_null()
    {
        Assert.AreEqual(1, Svo.Year.CompareTo(null));
    }

    [Test]
    public void to_Year_as_object()
    {
        object obj = Svo.Year;
        Assert.AreEqual(0, Svo.Year.CompareTo(obj));
    }

    [Test]
    public void to_Year_only()
    {
        Assert.Throws<ArgumentException>(() => Svo.Year.CompareTo(new object()));
    }

    [Test]
    public void can_be_sorted_using_compare()
    {
        var sorted = new[] {
                default,
                default,
                1970.CE(),
                1971.CE(),
                1972.CE(),
                Year.Unknown,
            };
            var list = new List<Year> { sorted[3], sorted[5], sorted[4], sorted[2], sorted[0], sorted[1] };
            list.Sort();
            Assert.AreEqual(sorted, list);
        }
    
        [Test]
        public void by_operators_for_different_values()
        {
            Year smaller = 1979.CE();
            Year bigger = 2017.CE();

        Assert.IsTrue(smaller < bigger);
        Assert.IsTrue(smaller <= bigger);
        Assert.IsFalse(smaller > bigger);
        Assert.IsFalse(smaller >= bigger);
    }

        [Test]
        public void by_operators_for_equal_values()
        {
            Year left = 2071.CE();
            Year right = 2071.CE();

        Assert.IsFalse(left < right);
        Assert.IsTrue(left <= right);
        Assert.IsFalse(left > right);
        Assert.IsTrue(left >= right);
    }

    [TestCase("", 1979)]
    [TestCase("?", 1979)]
    [TestCase(1979, "")]
    [TestCase(1979, "?")]
    public void by_operators_for_empty_or_unknown_always_false(Year l, Year r)
    {
        Assert.IsFalse(l <= r);
        Assert.IsFalse(l < r);
        Assert.IsFalse(l > r);
        Assert.IsFalse(l >= r);
    }
}

public class Casts
{
    [Test]
    public void explicitly_from_short()
    {
        var casted = (Year)1979;
        Assert.AreEqual(Svo.Year, casted);
    }

    [Test]
    public void explicitly_to_short()
    {
        var casted = (short)Svo.Year;
        Assert.AreEqual((short)1979, casted);
    }
}

public class Supports_type_conversion
{
    [Test]
    public void via_TypeConverter_registered_with_attribute()
        => typeof(Year).Should().HaveTypeConverterDefined();

    [Test]
    public void from_null_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.From<string>(null).To<Year>().Should().Be(Year.Empty);
        }
    }

    [Test]
    public void from_empty_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.From(string.Empty).To<Year>().Should().Be(Year.Empty);
        }
    }

    [Test]
    public void from_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.From("1979").To<Year>().Should().Be(Svo.Year);
        }
    }

    [Test]
    public void to_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.ToString().From(Svo.Year).Should().Be("1979");
        }
    }

    [Test]
    public void from_int()
        => Converting.From(1979).To<Year>().Should().Be(Svo.Year);

    [Test]
    public void to_int()
        => Converting.To<int>().From(Svo.Year).Should().Be(1979);
}

public class Supports_JSON_serialization
{
    [TestCase("?", "?")]
    [TestCase(null, null)]
    [TestCase(2017d, 2017)]
    [TestCase(2017L, 2017)]
    [TestCase("2017", 2017)]
    public void System_Text_JSON_deserialization(object json, Year svo)
        => JsonTester.Read_System_Text_JSON<Year>(json).Should().Be(svo);

    [TestCase("?", "?")]
    [TestCase(2017d, 2017)]
    [TestCase(2017L, 2017)]
    [TestCase("2017", 2017)]
    public void convention_based_deserialization(object json, Year svo)
        => JsonTester.Read<Year>(json).Should().Be(svo);

    [TestCase(null, null)]
    [TestCase(2017, 2017L)]
    public void System_Text_JSON_serialization(Year svo, object json)
        => JsonTester.Write_System_Text_JSON(svo).Should().Be(json);

    [TestCase(null, null)]
    [TestCase(2017, 2017L)]
    public void convention_based_serialization(Year svo, object json)
        => JsonTester.Write(svo).Should().Be(json);

    [TestCase("Invalid input", typeof(FormatException))]
    [TestCase("2017-06-11", typeof(FormatException))]
    [TestCase(-5L, typeof(ArgumentOutOfRangeException))]
    [TestCase(-2.3, typeof(ArgumentOutOfRangeException))]
    public void throws_for_invalid_json(object json, Type exceptionType)
    {
        var exception = Assert.Catch(() => JsonTester.Read<Year>(json));
        Assert.IsInstanceOf(exceptionType, exception);
    }
}

public class Supports_XML_serialization
{
    [Test]
    public void using_XmlSerializer_to_serialize()
    {
        var xml = Serialize.Xml(Svo.Year);
        Assert.AreEqual("1979", xml);
    }

    [Test]
    public void using_XmlSerializer_to_deserialize()
    {
        var svo = Deserialize.Xml<Year>("1979");
        Assert.AreEqual(Svo.Year, svo);
    }

    [Test]
    public void using_DataContractSerializer()
    {
        var round_tripped = SerializeDeserialize.DataContract(Svo.Year);
        Assert.AreEqual(Svo.Year, round_tripped);
    }

    [Test]
    public void as_part_of_a_structure()
    {
        var structure = XmlStructure.New(Svo.Year);
        var round_tripped = SerializeDeserialize.Xml(structure);
        Assert.AreEqual(structure, round_tripped);
    }

    [Test]
    public void has_no_custom_XML_schema()
    {
        IXmlSerializable obj = Svo.Year;
        Assert.IsNull(obj.GetSchema());
    }
}

public class Is_Open_API_data_type
{
    [Test]
    public void with_info()
      => Qowaiv.OpenApi.OpenApiDataType.FromType(typeof(Year))
      .Should().Be(new Qowaiv.OpenApi.OpenApiDataType(
          dataType: typeof(Year),
          description: "Year(-only) notation.",
          example: 1983, 
          type: "integer",
          format: "year",
          nullable: true));
}

public class Supports_binary_serialization
{
    [Test]
    [Obsolete("Usage of the binary formatter is considered harmful.")]
    public void using_BinaryFormatter()
    {
        var round_tripped = SerializeDeserialize.Binary(Svo.Year);
        Assert.AreEqual(Svo.Year, round_tripped);
    }

    [Test]
    public void storing_short_in_SerializationInfo()
    {
        var info = Serialize.GetInfo(Svo.Year);
        Assert.AreEqual((short)1979, info.GetInt16("Value"));
    }
}

public class Debugger
{
    [TestCase("{empty}", "")]
    [TestCase("{unknown}", "?")]
    [TestCase("1979", (short)1979)]
    public void has_custom_display(object display, Year svo)
        => svo.Should().HaveDebuggerDisplay(display);
}

