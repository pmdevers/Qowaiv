﻿using NUnit.Framework;
using Qowaiv.Formatting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Qowaiv.UnitTests.Formatting
{
    [TestFixture]
    public class StringFormatterTest
    {
        [Test]
        public void Apply_InvalidFormat_ThrowsFormatException()
        {
            Assert.Catch<FormatException>(() =>
            {
                StringFormatter.Apply(Int32.MinValue, "\\", CultureInfo.InvariantCulture, new Dictionary<char, Func<Int32, IFormatProvider, string>>());
            },
            "Input string was not in a correct format.");
        }

        [Test]
        public void ToNonDiacritic_Null_AreEqual()
        {
            var str = (string)null;

            var exp = (string)null;
            var act = StringFormatter.ToNonDiacritic(str);

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToNonDiacritic_StringEmpty_AreEqual()
        {
            var str = string.Empty;

            var exp = string.Empty;
            var act = StringFormatter.ToNonDiacritic(str);

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToNonDiacritic_CafeUndStrasse_AreEqual()
        {
            var str = "Café & Straße";

            var exp = "Cafe & Strasze";
            var act = StringFormatter.ToNonDiacritic(str);

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void ToNonDiacritic_CafeUndStrasseIgnoreE_AreEqual()
        {
            var str = "Café & Straße";

            var exp = "Café & Strasze";
            var act = StringFormatter.ToNonDiacritic(str, "é");

            Assert.AreEqual(exp, act);
        }
    }
}
