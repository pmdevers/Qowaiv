﻿using FluentAssertions;
using NUnit.Framework;
using Qowaiv.TestTools;
using Qowaiv.TestTools.Globalization;
using Qowaiv.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Qowaiv.UnitTests.Web
{
    /// <summary>Tests the Internet media type SVO.</summary>
    public class InternetMediaTypeTest
    {
        /// <summary>The test instance for most tests.</summary>
        public static readonly InternetMediaType TestStruct = InternetMediaType.Parse("application/x-chess-pgn");

        /// <summary>Represents text/html.</summary>
        public static readonly InternetMediaType TextHtml = InternetMediaType.Parse("text/html");

        /// <summary>Represents cooltalk (x-conference/x-cooltalk).</summary>
        public static readonly InternetMediaType XConferenceXCooltalk = InternetMediaType.Parse("x-conference/x-cooltalk");

        #region internet media type const tests

        /// <summary>InternetMediaType.Empty should be equal to the default of internet media type.</summary>
        [Test]
        public void Empty_None_EqualsDefault()
        {
            Assert.AreEqual(default(InternetMediaType), InternetMediaType.Empty);
        }

        #endregion

        #region internet media type IsEmpty tests

        /// <summary>InternetMediaType.IsEmpty() should be true for the default of internet media type.</summary>
        [Test]
        public void IsEmpty_Default_IsTrue()
        {
            Assert.IsTrue(default(InternetMediaType).IsEmpty());
        }
        /// <summary>InternetMediaType.IsEmpty() should be false for InternetMediaType.Unknown.</summary>
        [Test]
        public void IsEmpty_Unknown_IsFalse()
        {
            Assert.IsFalse(InternetMediaType.Unknown.IsEmpty());
        }
        /// <summary>InternetMediaType.IsEmpty() should be false for the TestStruct.</summary>
        [Test]
        public void IsEmpty_TestStruct_IsFalse()
        {
            Assert.IsFalse(TestStruct.IsEmpty());
        }

        /// <summary>InternetMediaType.IsUnknown() should be false for the default of internet media type.</summary>
        [Test]
        public void IsUnknown_Default_IsFalse()
        {
            Assert.IsFalse(default(InternetMediaType).IsUnknown());
        }
        /// <summary>InternetMediaType.IsUnknown() should be true for InternetMediaType.Unknown.</summary>
        [Test]
        public void IsUnknown_Unknown_IsTrue()
        {
            Assert.IsTrue(InternetMediaType.Unknown.IsUnknown());
        }
        /// <summary>InternetMediaType.IsUnknown() should be false for the TestStruct.</summary>
        [Test]
        public void IsUnknown_TestStruct_IsFalse()
        {
            Assert.IsFalse(TestStruct.IsUnknown());
        }

        /// <summary>InternetMediaType.IsEmptyOrUnknown() should be true for the default of internet media type.</summary>
        [Test]
        public void IsEmptyOrUnknown_Default_IsFalse()
        {
            Assert.IsTrue(default(InternetMediaType).IsEmptyOrUnknown());
        }
        /// <summary>InternetMediaType.IsEmptyOrUnknown() should be true for InternetMediaType.Unknown.</summary>
        [Test]
        public void IsEmptyOrUnknown_Unknown_IsTrue()
        {
            Assert.IsTrue(InternetMediaType.Unknown.IsEmptyOrUnknown());
        }
        /// <summary>InternetMediaType.IsEmptyOrUnknown() should be false for the TestStruct.</summary>
        [Test]
        public void IsEmptyOrUnknown_TestStruct_IsFalse()
        {
            Assert.IsFalse(TestStruct.IsEmptyOrUnknown());
        }

        #endregion

        #region TryParse tests

        /// <summary>TryParse null should be valid.</summary>
        [Test]
        public void TryParse_Null_IsValid()
        {
            string str = null;

            Assert.IsTrue(InternetMediaType.TryParse(str, out var val), "Valid");
            Assert.AreEqual(string.Empty, val.ToString(), "Value");
        }

        /// <summary>TryParse string.Empty should be valid.</summary>
        [Test]
        public void TryParse_StringEmpty_IsValid()
        {

            string str = string.Empty;

            Assert.IsTrue(InternetMediaType.TryParse(str, out var val), "Valid");
            Assert.AreEqual(string.Empty, val.ToString(), "Value");
        }

        /// <summary>TryParse "?" should be valid and the result should be InternetMediaType.Unknown.</summary>
        [Test]
        public void TryParse_Questionmark_IsValid()
        {
            string str = "?";

            Assert.IsTrue(InternetMediaType.TryParse(str, out var val), "Valid");
            Assert.IsTrue(val.IsUnknown(), "Value");
        }

        /// <summary>TryParse with specified string value should be valid.</summary>
        [Test]
        public void TryParse_StringValue_IsValid()
        {
            string str = "application/atom+xml";

            Assert.IsTrue(InternetMediaType.TryParse(str, out var val), "Valid");
            Assert.AreEqual(str, val.ToString(), "Value");
        }

        /// <summary>TryParse with specified string value should be invalid.</summary>
        [Test]
        public void TryParse_StringValue_IsNotValid()
        {
            string str = "string";

            Assert.IsFalse(InternetMediaType.TryParse(str, out var val), "Valid");
            Assert.AreEqual(string.Empty, val.ToString(), "Value");
        }

        [Test]
        public void Parse_Unknown_AreEqual()
        {
            using (TestCultures.En_GB.Scoped())
            {
                var act = InternetMediaType.Parse("?");
                var exp = InternetMediaType.Unknown;
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void Parse_InvalidInput_ThrowsFormatException()
        {
            using (TestCultures.En_GB.Scoped())
            {
                Assert.Catch<FormatException>
                (() =>
                {
                    InternetMediaType.Parse("InvalidInput");
                },
                "Not a valid internet media type");
            }
        }

        [Test]
        public void TryParse_TestStructInput_AreEqual()
        {
            using (TestCultures.En_GB.Scoped())
            {
                var exp = TestStruct;
                var act = InternetMediaType.TryParse(exp.ToString());

                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void from_invalid_as_null_with_TryParse()
            => InternetMediaType.TryParse("invalid input").Should().BeNull();

        #endregion

        #region FromFile tests

        [Test]
        public void FromFile_NullFileInfo_Empty()
        {
            var act = InternetMediaType.FromFile((FileInfo)null);
            var exp = InternetMediaType.Empty;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void FromFile_NullString_Empty()
        {
            var act = InternetMediaType.FromFile((String)null);
            var exp = InternetMediaType.Empty;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void FromFile__StringEmpty_Empty()
        {
            var act = InternetMediaType.FromFile(string.Empty);
            var exp = InternetMediaType.Empty;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void FromFile__GamesDotPgn_ApplicationXChessPgn()
        {
            var act = InternetMediaType.FromFile(new FileInfo("games.pgn"));
            var exp = TestStruct;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void FromFile__TestDotUnknown_Unknown()
        {
            var act = InternetMediaType.FromFile(new FileInfo("test.unknown"));
            var exp = InternetMediaType.Unknown;

            Assert.AreEqual(exp, act);
        }

        #endregion

        #region (XML) (De)serialization tests

        [Test]
        public void GetObjectData_SerializationInfo_AreEqual()
        {
            ISerializable obj = TestStruct;
            var info = new SerializationInfo(typeof(InternetMediaType), new System.Runtime.Serialization.FormatterConverter());
            obj.GetObjectData(info, default);

            Assert.AreEqual("application/x-chess-pgn", info.GetString("Value"));
        }

        [Test]
        [Obsolete("Usage of the binary formatter is considered harmful.")]
        public void SerializeDeserialize_TestStruct_AreEqual()
        {
            var input = TestStruct;
            var exp = TestStruct;
            var act = SerializeDeserialize.Binary(input);
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void DataContractSerializeDeserialize_TestStruct_AreEqual()
        {
            var input = TestStruct;
            var exp = TestStruct;
            var act = SerializeDeserialize.DataContract(input);
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void XmlSerialize_TestStruct_AreEqual()
        {
            var act = Serialize.Xml(TestStruct);
            var exp = "application/x-chess-pgn";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void XmlDeserialize_XmlString_AreEqual()
        {
            var act =Deserialize.Xml<InternetMediaType>("application/x-chess-pgn");
            Assert.AreEqual(TestStruct, act);
        }

        [Test]
        [Obsolete("Usage of the binary formatter is considered harmful.")]
        public void SerializeDeserialize_InternetMediaTypeSerializeObject_AreEqual()
        {
            var input = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var act = SerializeDeserialize.Binary(input);
            Assert.AreEqual(exp.Id, act.Id, "Id");
            Assert.AreEqual(exp.Obj, act.Obj, "Obj");
            Assert.AreEqual(exp.Date, act.Date, "Date");
        }
        [Test]
        public void XmlSerializeDeserialize_InternetMediaTypeSerializeObject_AreEqual()
        {
            var input = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var act = SerializeDeserialize.Xml(input);
            Assert.AreEqual(exp.Id, act.Id, "Id");
            Assert.AreEqual(exp.Obj, act.Obj, "Obj");
            Assert.AreEqual(exp.Date, act.Date, "Date");
        }
        [Test]
        public void DataContractSerializeDeserialize_InternetMediaTypeSerializeObject_AreEqual()
        {
            var input = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var act = SerializeDeserialize.DataContract(input);
            Assert.AreEqual(exp.Id, act.Id, "Id");
            Assert.AreEqual(exp.Obj, act.Obj, "Obj");
            Assert.AreEqual(exp.Date, act.Date, "Date");
        }

        [Test]
        [Obsolete("Usage of the binary formatter is considered harmful.")]
        public void SerializeDeserialize_Empty_AreEqual()
        {
            var input = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = InternetMediaType.Empty,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = InternetMediaType.Empty,
                Date = new DateTime(1970, 02, 14),
            };
            var act = SerializeDeserialize.Binary(input);
            Assert.AreEqual(exp.Id, act.Id, "Id");
            Assert.AreEqual(exp.Obj, act.Obj, "Obj");
            Assert.AreEqual(exp.Date, act.Date, "Date");
        }
        [Test]
        public void XmlSerializeDeserialize_Empty_AreEqual()
        {
            var input = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = InternetMediaType.Empty,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new InternetMediaTypeSerializeObject
            {
                Id = 17,
                Obj = InternetMediaType.Empty,
                Date = new DateTime(1970, 02, 14),
            };
            var act = SerializeDeserialize.Xml(input);
            Assert.AreEqual(exp.Id, act.Id, "Id");
            Assert.AreEqual(exp.Obj, act.Obj, "Obj");
            Assert.AreEqual(exp.Date, act.Date, "Date");
        }

        [Test]
        public void GetSchema_None_IsNull()
        {
            IXmlSerializable obj = TestStruct;
            Assert.IsNull(obj.GetSchema());
        }

        #endregion

        #region IFormattable / ToString tests

        [Test]
        public void ToString_Empty_StringEmpty()
        {
            var act = InternetMediaType.Empty.ToString();
            var exp = "";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_Unknown_QuestionMark()
        {
            var act = InternetMediaType.Unknown.ToString();
            var exp = "application/octet-stream";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_CustomFormatter_SupportsCustomFormatting()
        {
            var act = TestStruct.ToString("Unit Test Format", FormatProvider.CustomFormatter);
            var exp = "Unit Test Formatter, value: 'application/x-chess-pgn', format: 'Unit Test Format'";

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void ToString_TestStruct_ComplexPattern()
        {
            var act = TestStruct.ToString("");
            var exp = "application/x-chess-pgn";
            Assert.AreEqual(exp, act);
        }

        #endregion

        #region IEquatable tests

        /// <summary>GetHash should not fail for InternetMediaType.Empty.</summary>
        [Test]
        public void GetHash_Empty_0()
        {
            Assert.AreEqual(0, InternetMediaType.Empty.GetHashCode());
        }

        /// <summary>GetHash should not fail for the test struct.</summary>
        [Test]
        public void GetHash_TestStruct_NotZero()
        {
            Assert.NotZero(TestStruct.GetHashCode());
        }

        [Test]
        public void Equals_EmptyEmpty_IsTrue()
        {
            Assert.IsTrue(InternetMediaType.Empty.Equals(InternetMediaType.Empty));
        }

        [Test]
        public void Equals_FormattedAndUnformatted_IsTrue()
        {
            var l = InternetMediaType.Parse("application/x-chess-pgn");
            var r = InternetMediaType.Parse("application/X-chess-PGN");

            Assert.IsTrue(l.Equals(r));
        }

        [Test]
        public void Equals_TestStructTestStruct_IsTrue()
        {
            Assert.IsTrue(TestStruct.Equals(TestStruct));
        }

        [Test]
        public void Equals_TestStructEmpty_IsFalse()
        {
            Assert.IsFalse(TestStruct.Equals(InternetMediaType.Empty));
        }

        [Test]
        public void Equals_EmptyTestStruct_IsFalse()
        {
            Assert.IsFalse(InternetMediaType.Empty.Equals(TestStruct));
        }

        [Test]
        public void Equals_TestStructObjectTestStruct_IsTrue()
        {
            Assert.IsTrue(TestStruct.Equals((object)TestStruct));
        }

        [Test]
        public void Equals_TestStructNull_IsFalse()
        {
            Assert.IsFalse(TestStruct.Equals(null));
        }

        [Test]
        public void Equals_TestStructObject_IsFalse()
        {
            Assert.IsFalse(TestStruct.Equals(new object()));
        }

        [Test]
        public void OperatorIs_TestStructTestStruct_IsTrue()
        {
            var l = TestStruct;
            var r = TestStruct;
            Assert.IsTrue(l == r);
        }

        [Test]
        public void OperatorIsNot_TestStructTestStruct_IsFalse()
        {
            var l = TestStruct;
            var r = TestStruct;
            Assert.IsFalse(l != r);
        }

        #endregion

        #region IComparable tests

        /// <summary>Orders a list of internet media types ascending.</summary>
        [Test]
        public void OrderBy_InternetMediaType_AreEqual()
        {
            var item0 = InternetMediaType.Parse("audio/mp3");
            var item1 = InternetMediaType.Parse("image/jpeg");
            var item2 = InternetMediaType.Parse("text/x-markdown");
            var item3 = InternetMediaType.Parse("video/quicktime");

            var inp = new List<InternetMediaType> { InternetMediaType.Empty, item3, item2, item0, item1, InternetMediaType.Empty };
            var exp = new List<InternetMediaType> { InternetMediaType.Empty, InternetMediaType.Empty, item0, item1, item2, item3 };
            var act = inp.OrderBy(item => item).ToList();

            CollectionAssert.AreEqual(exp, act);
        }

        /// <summary>Orders a list of internet media types descending.</summary>
        [Test]
        public void OrderByDescending_InternetMediaType_AreEqual()
        {
            var item0 = InternetMediaType.Parse("audio/mp3");
            var item1 = InternetMediaType.Parse("image/jpeg");
            var item2 = InternetMediaType.Parse("text/x-markdown");
            var item3 = InternetMediaType.Parse("video/quicktime");

            var inp = new List<InternetMediaType> { InternetMediaType.Empty, item3, item2, item0, item1, InternetMediaType.Empty };
            var exp = new List<InternetMediaType> { item3, item2, item1, item0, InternetMediaType.Empty, InternetMediaType.Empty };
            var act = inp.OrderByDescending(item => item).ToList();

            CollectionAssert.AreEqual(exp, act);
        }

        /// <summary>Compare with a to object casted instance should be fine.</summary>
        [Test]
        public void CompareTo_ObjectTestStruct_0()
        {
            object other = TestStruct;

            var exp = 0;
            var act = TestStruct.CompareTo(other);

            Assert.AreEqual(exp, act);
        }

        /// <summary>Compare with null should return 1.</summary>
        [Test]
        public void CompareTo_null_1()
        {
            object @null = null;
            Assert.AreEqual(1, TestStruct.CompareTo(@null));
        }

        /// <summary>Compare with a random object should throw an exception.</summary>
        [Test]
        public void CompareTo_newObject_ThrowsArgumentException()
        {
            Func<int> compare = () => TestStruct.CompareTo(new object());
            compare.Should().Throw<ArgumentException>();
        }
        #endregion

        #region Properties

        [Test]
        public void Length_DefaultValue_0()
        {
            var exp = 0;
            var act = InternetMediaType.Empty.Length;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Length_TestStruct_23()
        {
            var exp = 23;
            var act = TestStruct.Length;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void TopLevel_DefaultValue_0()
        {
            var exp = string.Empty;
            var act = InternetMediaType.Empty.TopLevel;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevel_TextHtml_Text()
        {
            var exp = "text";
            var act = TextHtml.TopLevel;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevel_XConferenceXCooltalk_XConference()
        {
            var exp = "x-conference";
            var act = XConferenceXCooltalk.TopLevel;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevel_TestStruct_Application()
        {
            var exp = "application";
            var act = TestStruct.TopLevel;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void TopLevelType_DefaultValue_0()
        {
            var exp = InternetMediaTopLevelType.None;
            var act = InternetMediaType.Empty.TopLevelType;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevelType_TextHtml_Text()
        {
            var exp = InternetMediaTopLevelType.Text;
            var act = TextHtml.TopLevelType;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevelType_XConferenceXCooltalk_Unregistered()
        {
            var exp = InternetMediaTopLevelType.Unregistered;
            var act = XConferenceXCooltalk.TopLevelType;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void TopLevelType_TestStruct_Application()
        {
            var exp = InternetMediaTopLevelType.Application;
            var act = TestStruct.TopLevelType;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Subtype_DefaultValue_0()
        {
            var exp = string.Empty;
            var act = InternetMediaType.Empty.Subtype;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Subtype_TextHtml_Html()
        {
            var exp = "html";
            var act = TextHtml.Subtype;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Subtype_XConferenceXCooltalk_XCooltalk()
        {
            var exp = "x-cooltalk";
            var act = XConferenceXCooltalk.Subtype;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Subtype_TestStruct_XChessPgn()
        {
            var exp = "x-chess-pgn";
            var act = TestStruct.Subtype;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void IsRegistered_DefaultValue_IsFalse()
        {
            var exp = false;
            var act = InternetMediaType.Empty.IsRegistered;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void IsRegistered_TextHtml_IsTrue()
        {
            var exp = true;
            var act = TextHtml.IsRegistered;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void IsRegistered_XConferenceXCooltalk_IsFalse()
        {
            var exp = false;
            var act = XConferenceXCooltalk.IsRegistered;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void IsRegistered_TestStruct_IsFalse()
        {
            var exp = false;
            var act = TestStruct.IsRegistered;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void IsRegistered_VideoSlashXDotTest_IsFalse()
        {
            var mime = InternetMediaType.Parse("video/x.test");
            var exp = false;
            var act = mime.IsRegistered;
            Assert.AreEqual(exp, act);
        }


        [Test]
        public void Suffix_DefaultValue_None()
        {
            var exp = InternetMediaSuffixType.None;
            var act = InternetMediaType.Empty.Suffix;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Suffix_TestStruct_None()
        {
            var exp = InternetMediaSuffixType.None;
            var act = TestStruct.Suffix;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Suffix_ApplicationAtomXml_Xml()
        {
            var mime = InternetMediaType.Parse("application/atom+xml");

            var exp = InternetMediaSuffixType.xml;
            var act = mime.Suffix;
            Assert.AreEqual(exp, act);
        }

        #endregion

        #region IsValid tests

        [Test]
        public void IsValid_Data_IsFalse()
        {
            Assert.IsFalse(InternetMediaType.IsValid("test/d"), "Complex");
            Assert.IsFalse(InternetMediaType.IsValid((String)null), "(String)null");
            Assert.IsFalse(InternetMediaType.IsValid(string.Empty), "string.Empty");
        }
        [Test]
        public void IsValid_Data_IsTrue()
        {
            Assert.IsTrue(InternetMediaType.IsValid("application/x-chess-pgn"));
            Assert.IsTrue(InternetMediaType.IsValid("VIDEO/Mp3"));
        }
        #endregion
    }

    [Serializable]
    public class InternetMediaTypeSerializeObject
    {
        public int Id { get; set; }
        public InternetMediaType Obj { get; set; }
        public DateTime Date { get; set; }
    }
}
