﻿namespace WeekDate_specs;

public class Supports_type_conversion
{
    [Test]
    public void via_TypeConverter_registered_with_attribute()
        => typeof(WeekDate).Should().HaveTypeConverterDefined();

    [Test]
    public void from_null_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.From<string>(null).To<WeekDate>().Should().Be(default);
        }
    }

    [Test]
    public void from_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.From("2017-W23-7").To<WeekDate>().Should().Be(Svo.WeekDate);
        }
    }

    [Test]
    public void to_string()
    {
        using (TestCultures.En_GB.Scoped())
        {
            Converting.ToString().From(Svo.WeekDate).Should().Be("2017-W23-7");
        }
    }

    [Test]
    public void from_Date()
        => Converting.From(new Date(2017, 06, 11)).To<WeekDate>().Should().Be(Svo.WeekDate);

    [Test]
    public void from_DateTime()
        => Converting.From(new DateTime(2017, 06, 11)).To<WeekDate>().Should().Be(Svo.WeekDate);

    [Test]
    public void from_DateTimeOffset()
        => Converting.From(new DateTimeOffset(2017, 06, 11, 00, 00, 00, TimeSpan.Zero)).To<WeekDate>().Should().Be(Svo.WeekDate);

    [Test]
    public void from_LocalDateTime()
        => Converting.From(new LocalDateTime(2017, 06, 11)).To<WeekDate>().Should().Be(Svo.WeekDate);

    [Test]
    public void to_Date()
        => Converting.To<Date>().From(Svo.WeekDate).Should().Be(new Date(2017, 06, 11));

    [Test]
    public void to_DateTime()
        => Converting.To<DateTime>().From(Svo.WeekDate).Should().Be(new DateTime(2017, 06, 11));

    [Test]
    public void to_DateTimeOffset()
        => Converting.To<DateTimeOffset>().From(Svo.WeekDate).Should().Be(new DateTimeOffset(2017, 06, 11, 00, 00, 00, TimeSpan.Zero));

    [Test]
    public void to_LocalDateTime()
        => Converting.To<LocalDateTime>().From(Svo.WeekDate).Should().Be(new LocalDateTime(2017, 06, 11));
}

public class Supports_JSON_serialization
{
    [TestCase("1997-W14-6", "1997-W14-6")]
    public void System_Text_JSON_deserialization(object json, WeekDate svo)
        => JsonTester.Read_System_Text_JSON<WeekDate>(json).Should().Be(svo);

    [TestCase("1997-W14-6", "1997-W14-6")]
    public void convention_based_deserialization(object json, WeekDate svo)
        => JsonTester.Read<WeekDate>(json).Should().Be(svo);

    [TestCase("1997-W14-6", "1997-W14-6")]
    public void System_Text_JSON_serialization(WeekDate svo, object json)
        => JsonTester.Write_System_Text_JSON(svo).Should().Be(json);

    [TestCase("1997-W14-6", "1997-W14-6")]
    public void convention_based_serialization(WeekDate svo, object json)
        => JsonTester.Write(svo).Should().Be(json);

    [TestCase("Invalid input", typeof(FormatException))]
    [TestCase("yyyy-06-11", typeof(FormatException))]
    [TestCase(true, typeof(InvalidOperationException))]
    public void throws_for_invalid_json(object json, Type exceptionType)
    {
        var exception = Assert.Catch(() => JsonTester.Read<WeekDate>(json));
        Assert.IsInstanceOf(exceptionType, exception);
    }
}
public class Is_Open_API_data_type
{
    [Test]
    public void with_info()
       => Qowaiv.OpenApi.OpenApiDataType.FromType(typeof(WeekDate))
       .Should().Be(new Qowaiv.OpenApi.OpenApiDataType(
           dataType: typeof(WeekDate),
           description: "Full-date notation as defined by ISO 8601.",
           example: "1997-W14-6",
           type: "string",
           format: "date-weekbased"));
}
