﻿namespace Qowaiv.UnitTests.IO
{
    /// <summary>Tests the stream size SVO.</summary>
    public class StreamSizeTest
    {
        /// <summary>The test instance for most tests.</summary>
        public static readonly StreamSize TestStruct = 123456789;

        #region stream size const tests

        /// <summary>StreamSize.Empty should be equal to the default of stream size.</summary>
        [Test]
        public void Empty_None_EqualsDefault()
        {
            Assert.AreEqual(default(StreamSize), StreamSize.Zero);
        }

        #endregion

        #region From byte factory methods

        [Test]
        public void FromKilobytes_2_2000()
        {
            var size = StreamSize.FromKilobytes(2);
            var act = (long)size;
            var exp = 2000L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromMegabytes_3Dot5_3500000()
        {
            var size = StreamSize.FromMegabytes(3.5);
            var act = (long)size;
            var exp = 3500000L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromGigabytes_0Dot8_800000000()
        {
            var size = StreamSize.FromGigabytes(0.8);
            var act = (long)size;
            var exp = 800000000L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromTerabytes_10_10000000000000()
        {
            var size = StreamSize.FromTerabytes(10);
            var act = (long)size;
            var exp = 10000000000000L;
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void FromKibibytes_2_2048()
        {
            var size = StreamSize.FromKibibytes(2);
            var act = (long)size;
            var exp = 2048L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromMebibytes_3Dot5_3670016()
        {
            var size = StreamSize.FromMebibytes(3.5);
            var act = (long)size;
            var exp = 3670016L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromGibibytes_0Dot8_858993459()
        {
            var size = StreamSize.FromGibibytes(0.8);
            var act = (long)size;
            var exp = 858993459L;
            Assert.AreEqual(exp, act);
        }
        [Test]
        public void FromTebibytes_10_10995116277760()
        {
            var size = StreamSize.FromTebibytes(10);
            var act = (long)size;
            var exp = 10995116277760L;
            Assert.AreEqual(exp, act);
        }

        #endregion

        #region (XML) (De)serialization tests

        [Test]
        public void GetObjectData_SerializationInfo_AreEqual()
        {
            ISerializable obj = TestStruct;
            var info = new SerializationInfo(typeof(StreamSize), new System.Runtime.Serialization.FormatterConverter());
            obj.GetObjectData(info, default);

            Assert.AreEqual((Int64)123456789, info.GetInt64("Value"));
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
            var exp = "123456789 byte";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void XmlDeserialize_XmlString_AreEqual()
        {
            var act =Deserialize.Xml<StreamSize>("123456789 byte");
            Assert.AreEqual(TestStruct, act);
        }

        [Test]
        [Obsolete("Usage of the binary formatter is considered harmful.")]
        public void SerializeDeserialize_StreamSizeSerializeObject_AreEqual()
        {
            var input = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new StreamSizeSerializeObject
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
        public void XmlSerializeDeserialize_StreamSizeSerializeObject_AreEqual()
        {
            var input = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new StreamSizeSerializeObject
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
        public void DataContractSerializeDeserialize_StreamSizeSerializeObject_AreEqual()
        {
            var input = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = TestStruct,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new StreamSizeSerializeObject
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
        public void SerializeDeserialize_Default_AreEqual()
        {
            var input = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = default,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = default,
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
            var input = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = StreamSize.Zero,
                Date = new DateTime(1970, 02, 14),
            };
            var exp = new StreamSizeSerializeObject
            {
                Id = 17,
                Obj = StreamSize.Zero,
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
        public void ToString_Zero_StringEmpty()
        {
            var act = StreamSize.Zero.ToString();
            var exp = "0 byte";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_CustomFormatter_SupportsCustomFormatting()
        {
            var act = TestStruct.ToString("0.0 F", FormatProvider.CustomFormatter);
            var exp = "Unit Test Formatter, value: '123.5 Megabyte', format: '0.0 F'";

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_Null_ComplexPattern()
        {
            var act = TestStruct.ToString((String)null);
            var exp = "123456789";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_TestStruct_ComplexPattern()
        {
            var act = TestStruct.ToString("");
            var exp = "123456789";
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToString_b_AreEqual()
        {
            using (TestCultures.Nl_NL.Scoped())
            {
                var act = TestStruct.ToString("#,##0b");
                var exp = "123.456.789b";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_kB_AreEqual()
        {
            using (TestCultures.Nl_NL.Scoped())
            {
                var act = TestStruct.ToString("#,##0.00 kB");
                var exp = "123.456,79 kB";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_MegaByte_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0.0 MegaByte");
                var exp = "123,5 MegaByte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_Negative_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = (-TestStruct).ToString("0.0 F");
                var exp = "-123,5 Megabyte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_GB_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0.00GB");
                var exp = "0,12GB";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_GiB_AreEqual()
        {
            using (TestCultures.De_DE.Scoped())
            {
                var act = TestStruct.ToString("0.0000 GiB");
                var exp = "0,1150 GiB";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_tb_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = StreamSize.PB.ToString("tb");
                var exp = "1000tb";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_pb_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = StreamSize.TB.ToString(" petabyte");
                var exp = "0,001 petabyte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_Exabyte_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = StreamSize.MaxValue.ToString("#,##0.## Exabyte");
                var exp = "9,22 Exabyte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_spaceF_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("#,##0.## F");
                var exp = "123,46 Megabyte";
                Assert.AreEqual(exp, act);
            }
        }
        [Test]
        public void ToString_spaceFLower_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0 f");
                var exp = "123 megabyte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_spaceS_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0000 S");
                var exp = "0123 MB";
                Assert.AreEqual(exp, act);
            }
        }
        [Test]
        public void ToString_spaceSLower_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0 s");
                var exp = "123 mb";
                Assert.AreEqual(exp, act);
            }
        }
        [Test]
        public void ToString_SLower_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0s");
                var exp = "123mb";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_SpaceSiLower_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = TestStruct.ToString("0.0 si");
                var exp = "117,7 mib";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_ValueDutchBelgium_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = StreamSize.Parse("1600,1").ToString();
                var exp = "1600 byte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_ValueEnglishGreatBritain_AreEqual()
        {
            using (TestCultures.En_GB.Scoped())
            {
                var act = StreamSize.Parse("1600.1").ToString();
                var exp = "1600 byte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_FormatValueDutchBelgium_AreEqual()
        {
            using (TestCultures.Nl_BE.Scoped())
            {
                var act = StreamSize.Parse("800").ToString("0000 byte");
                var exp = "0800 byte";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_FormatValueEnglishGreatBritain_AreEqual()
        {
            using (TestCultures.En_GB.Scoped())
            {
                var act = StreamSize.Parse("800").ToString("0000");
                var exp = "0800";
                Assert.AreEqual(exp, act);
            }
        }

        [Test]
        public void ToString_FormatValueSpanishEcuador_AreEqual()
        {
            var act = StreamSize.Parse("1700").ToString("00000.0", new CultureInfo("es-EC"));
            var exp = "01700,0";
            Assert.AreEqual(exp, act);
        }

        #endregion

        #region IEquatable tests

        [Test]
        public void Equals_FormattedAndUnformatted_IsTrue()
        {
            var l = StreamSize.Parse("12,345 byte", CultureInfo.InvariantCulture);
            var r = StreamSize.Parse("12345", CultureInfo.InvariantCulture);

            Assert.IsTrue(l.Equals(r));
        }

        #endregion

        #region IComparable tests

        /// <summary>Orders a list of stream sizes ascending.</summary>
        [Test]
        public void OrderBy_StreamSize_AreEqual()
        {
            StreamSize item0 = 13465;
            StreamSize item1 = 83465;
            StreamSize item2 = 113465;
            StreamSize item3 = 773465;

            var inp = new List<StreamSize> { StreamSize.Zero, item3, item2, item0, item1, StreamSize.Zero };
            var exp = new List<StreamSize> { StreamSize.Zero, StreamSize.Zero, item0, item1, item2, item3 };
            var act = inp.OrderBy(item => item).ToList();

            CollectionAssert.AreEqual(exp, act);
        }

        /// <summary>Orders a list of stream sizes descending.</summary>
        [Test]
        public void OrderByDescending_StreamSize_AreEqual()
        {
            StreamSize item0 = 13465;
            StreamSize item1 = 83465;
            StreamSize item2 = 113465;
            StreamSize item3 = 773465;

            var inp = new List<StreamSize> { StreamSize.Zero, item3, item2, item0, item1, StreamSize.Zero };
            var exp = new List<StreamSize> { item3, item2, item1, item0, StreamSize.Zero, StreamSize.Zero };
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

        [Test]
        public void LessThan_17LT19_IsTrue()
        {
            StreamSize l = 17;
            StreamSize r = 19;

            Assert.IsTrue(l < r);
        }
        [Test]
        public void GreaterThan_21LT19_IsTrue()
        {
            StreamSize l = 21;
            StreamSize r = 19;

            Assert.IsTrue(l > r);
        }

        [Test]
        public void LessThanOrEqual_17LT19_IsTrue()
        {
            StreamSize l = 17;
            StreamSize r = 19;

            Assert.IsTrue(l <= r);
        }
        [Test]
        public void GreaterThanOrEqual_21LT19_IsTrue()
        {
            StreamSize l = 21;
            StreamSize r = 19;

            Assert.IsTrue(l >= r);
        }

        [Test]
        public void LessThanOrEqual_17LT17_IsTrue()
        {
            StreamSize l = 17;
            StreamSize r = 17;

            Assert.IsTrue(l <= r);
        }
        [Test]
        public void GreaterThanOrEqual_21LT21_IsTrue()
        {
            StreamSize l = 21;
            StreamSize r = 21;

            Assert.IsTrue(l >= r);
        }
        #endregion

        #region Casting tests

        [Test]
        public void Implicit_Int32ToStreamSize_AreEqual()
        {
            StreamSize exp = TestStruct;
            StreamSize act = 123456789;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Explicit_StreamSizeToInt32_AreEqual()
        {
            var exp = 123456789;
            var act = (Int32)TestStruct;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Implicit_Int64ToStreamSize_AreEqual()
        {
            var exp = TestStruct;
            StreamSize act = 123456789L;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Explicit_StreamSizeToInt64_AreEqual()
        {
            var exp = 123456789L;
            var act = (Int64)TestStruct;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Explicit_DoubleToStreamSize_AreEqual()
        {
            var exp = TestStruct;
            var act = (StreamSize)123456789d;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Explicit_StreamSizeToDouble_AreEqual()
        {
            var exp = 123456789d;
            var act = (Double)TestStruct;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Explicit_DecimalToStreamSize_AreEqual()
        {
            var exp = TestStruct;
            var act = (StreamSize)123456789m;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Explicit_StreamSizeToDecimal_AreEqual()
        {
            var exp = 123456789m;
            var act = (Decimal)TestStruct;

            Assert.AreEqual(exp, act);
        }


        #endregion

        [TestCase(-1, "-23KB")]
        [TestCase(0, "0KB")]
        [TestCase(+1, "16KB")]
        public void Sign(int expected, StreamSize size)
        {
            var actual = size.Sign();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1234, -1234)]
        [TestCase(1234, +1234)]
        public void Abs(StreamSize expected, StreamSize value)
        {
            var abs = value.Abs();
            Assert.AreEqual(expected, abs);
        }

        [Test]
        public void Increment_21_22()
        {
            StreamSize act = 21;
            StreamSize exp = 22;
            act++;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Decrement_21_20()
        {
            StreamSize act = 21;
            StreamSize exp = 20;
            act--;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Plus_21_21()
        {
            StreamSize act = +((StreamSize)21);
            StreamSize exp = 21;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Negate_21_Minus21()
        {
            StreamSize act = -((StreamSize)21);
            StreamSize exp = -21;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Addition_17Percentage10_18()
        {
            StreamSize act = 17;
            StreamSize exp = 18;
            act += Percentage.Create(0.1);

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Addition_17And5_24()
        {
            StreamSize act = 17;
            StreamSize exp = 24;
            act += (StreamSize)7;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Subtraction_17Percentage10_16()
        {
            StreamSize act = 17;
            StreamSize exp = 16;
            act -= Percentage.Create(0.1);

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Subtraction_17And5_12()
        {
            StreamSize act = 17;
            StreamSize exp = 12;
            act -= (StreamSize)5;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Division_81And2Int16_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= (Int16)2;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And2Int32_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= 2;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Division_81And2Int64_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= (long)2;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And2UInt16_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= (ushort)2;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And2UInt32_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= (uint)2;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And2UInt64_40()
        {
            StreamSize act = 81;
            StreamSize exp = 40;
            act /= (ulong)2;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And150Percentage_54()
        {
            StreamSize act = 81;
            StreamSize exp = 54;
            act /= (Percentage)1.50;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And1Point5Single_54()
        {
            StreamSize act = 81;
            StreamSize exp = 54;
            act /= (float)1.5;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And1Point5Double_54()
        {
            StreamSize act = 81;
            StreamSize exp = 54;
            act /= 1.5;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Division_81And1Point5Decimal_54()
        {
            StreamSize act = 81;
            StreamSize exp = 54;
            act /= 1.5d;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Multiply_42And3Int16_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= (short)3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And3Int32_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= 3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And3Int64_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= (long)3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And3UInt16_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= (ushort)3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And3UInt32_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= (uint)3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And3UInt64_126()
        {
            StreamSize act = 42;
            StreamSize exp = 126;
            act *= (ulong)3;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42And50Percentage_21()
        {
            StreamSize act = 42;
            StreamSize exp = 21;
            act *= 50.Percent();

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42AndHalfSingle_21()
        {
            StreamSize act = 42;
            StreamSize exp = 21;
            act *= (float)0.5;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42AndHalfDouble_21()
        {
            StreamSize act = 42;
            StreamSize exp = 21;
            act *= 0.5;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Multiply_42AndHalfDecimal_21()
        {
            StreamSize act = 42;
            StreamSize exp = 21;
            act *= 0.5d;

            Assert.AreEqual(exp, act);
        }

        #region IsValid tests

        [Test]
        public void IsValid_Data_IsFalse()
        {
            using (CultureInfoScope.NewInvariant())
            {
                Assert.IsFalse(StreamSize.IsValid("Complex"), "Complex");
                Assert.IsFalse(StreamSize.IsValid((String)null), "(String)null");
                Assert.IsFalse(StreamSize.IsValid(string.Empty), "string.Empty");

                Assert.IsFalse(StreamSize.IsValid("1234 EB"), "1234 EB, to big");
                Assert.IsFalse(StreamSize.IsValid("-1234EB"), "-1234EB, to small");

                Assert.IsFalse(StreamSize.IsValid("12.9 EB"), "12.9 EB, to big");
                Assert.IsFalse(StreamSize.IsValid("-12.9EB"), "-12.9EB, to small");

                Assert.IsFalse(StreamSize.IsValid("79,228,162,514,264,337,593,543,950,335 kB"), "12.9 EB, to big for decimal");
                Assert.IsFalse(StreamSize.IsValid("-9,228,162,514,264,337,593,543,950,335 kB"), "-12.9EB, to small for decimal");
            }
        }
        [Test]
        public void IsValid_Data_IsTrue()
        {
            using (CultureInfoScope.NewInvariant())
            {
                Assert.IsTrue(StreamSize.IsValid("19 MB"));
                Assert.IsTrue(StreamSize.IsValid("1,456.134 MB"));
            }
        }
        #endregion

        #region Extension tests

        [Test]
        public void GetStreamSize_Stream_17Byte()
        {
            using var stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });

            StreamSize act = stream.GetStreamSize();
            StreamSize exp = 17;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void GetStreamSize_FileInfo_9Byte()
        {
            using var dir = new TemporaryDirectory();

            FileInfo file = dir.CreateFile("GetStreamSize_FileInfo_9.test");
            using (var writer = new StreamWriter(file.FullName, false))
            {
                writer.Write("Unit Test");
            }

            StreamSize act = file.GetStreamSize();
            StreamSize exp = 9;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Average_ArrayOfStreamSizes_5Byte()
        {
            var arr = new StreamSize[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            StreamSize act = arr.Average();
            StreamSize exp = 5;

            Assert.AreEqual(exp, act);
        }
        [Test]
        public void Sum_ArrayOfStreamSizes_45Byte()
        {
            var arr = new StreamSize[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            StreamSize act = arr.Sum();
            StreamSize exp = 45;

            Assert.AreEqual(exp, act);
        }

        #endregion
    }

    [Serializable]
    public class StreamSizeSerializeObject
    {
        public int Id { get; set; }
        public StreamSize Obj { get; set; }
        public DateTime Date { get; set; }
    }
}
