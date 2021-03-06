﻿using System;
using System.Globalization;
using Bdf.Extensions;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void EnsureEndsWith_Test()
        {
            //Expected use-cases
            "Test".EnsureEndsWith('!').ShouldBe("Test!");
            "Test!".EnsureEndsWith('!').ShouldBe("Test!");
            @"C:\test\folderName".EnsureEndsWith('\\').ShouldBe(@"C:\test\folderName\");
            @"C:\test\folderName\".EnsureEndsWith('\\').ShouldBe(@"C:\test\folderName\");

            //Case differences
            "TurkeY".EnsureEndsWith('y').ShouldBe("TurkeYy");
            "TurkeY".EnsureEndsWith('y', StringComparison.InvariantCultureIgnoreCase).ShouldBe("TurkeY");

            //Edge cases for Turkish 'i'.
            "TAKSİ".EnsureEndsWith('i', true, new CultureInfo("tr-TR")).ShouldBe("TAKSİ");
            "TAKSİ".EnsureEndsWith('i', false, new CultureInfo("tr-TR")).ShouldBe("TAKSİi");
        }

        [Fact]
        public void EnsureStartsWith_Test()
        {
            //Expected use-cases
            "Test".EnsureStartsWith('~').ShouldBe("~Test");
            "~Test".EnsureStartsWith('~').ShouldBe("~Test");

            //Case differences
            "Test".EnsureStartsWith('t').ShouldBe("tTest");
            "Test".EnsureStartsWith('t', StringComparison.InvariantCultureIgnoreCase).ShouldBe("Test");

            //Edge cases for English 'i'.
            "Istanbul".EnsureStartsWith('i', true, new CultureInfo("en-us")).ShouldBe("Istanbul");
            "Istanbul".EnsureStartsWith('i', false, new CultureInfo("en-us")).ShouldBe("iIstanbul");
        }

        [Fact]
        public void ToPascalCase_Test()
        {
            "helloWorld".ToPascalCase().ShouldBe("HelloWorld");
            "istrue".ToPascalCase().ShouldBe("Istrue");
            "istrue".ToPascalCase(new CultureInfo("en-us")).ShouldBe("Istrue");
        }

        [Fact]
        public void ToCamelCase_Test()
        {
            "HelloWorld".ToCamelCase().ShouldBe("helloWorld");
            "Istrue".ToCamelCase().ShouldBe("istrue");
            "Istrue".ToCamelCase(new CultureInfo("en-us")).ShouldBe("istrue");
        }

        [Fact]
        public void Right_Test()
        {
            const string str = "This is a test string";

            str.Right(3).ShouldBe("ing");
            str.Right(0).ShouldBe("");
            str.Right(str.Length).ShouldBe(str);
        }

        [Fact]
        public void Left_Test()
        {
            const string str = "This is a test string";

            str.Left(3).ShouldBe("Thi");
            str.Left(0).ShouldBe("");
            str.Left(str.Length).ShouldBe(str);
        }

        [Fact]
        public void NormalizeLineEndings_Test()
        {
            const string str = "This\r\n is a\r test \n string";
            var normalized = str.NormalizeLineEndings();
            var lines = normalized.SplitToLines();
            lines.Length.ShouldBe(4);
        }

        [Fact]
        public void NthIndexOf_Test()
        {
            const string str = "This is a test string";

            str.NthIndexOf('i', 0).ShouldBe(-1);
            str.NthIndexOf('i', 1).ShouldBe(2);
            str.NthIndexOf('i', 2).ShouldBe(5);
            str.NthIndexOf('i', 3).ShouldBe(18);
            str.NthIndexOf('i', 4).ShouldBe(-1);
        }

        [Fact]
        public void Truncate_Test()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.Truncate(7).ShouldBe("This is");
            str.Truncate(0).ShouldBe("");
            str.Truncate(100).ShouldBe(str);

            nullValue.Truncate(5).ShouldBe(null);
        }

        [Fact]
        public void TruncateWithPostFix_Test()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.TruncateWithPostfix(3).ShouldBe("...");
            str.TruncateWithPostfix(12).ShouldBe("This is a...");
            str.TruncateWithPostfix(0).ShouldBe("");
            str.TruncateWithPostfix(100).ShouldBe(str);

            nullValue.Truncate(5).ShouldBe(null);

            str.TruncateWithPostfix(3, "~").ShouldBe("Th~");
            str.TruncateWithPostfix(12, "~").ShouldBe("This is a t~");
            str.TruncateWithPostfix(0, "~").ShouldBe("");
            str.TruncateWithPostfix(100, "~").ShouldBe(str);

            nullValue.TruncateWithPostfix(5, "~").ShouldBe(null);
        }

        [Fact]
        public void ToEnum_Test()
        {
            "MyValue1".ToEnum<MyEnum>().ShouldBe(MyEnum.MyValue1);
            "MyValue2".ToEnum<MyEnum>().ShouldBe(MyEnum.MyValue2);
        }

        private enum MyEnum
        {
            MyValue1,
            MyValue2
        } 
    }
}