

using Bogus;
using LanguageExt;

namespace Resume.API.Tests.Integration;

internal class ModelPropertyGenerator
{
    internal static int CreateValidCommonIdentity() => 0;

    internal static int[] CreateValidArrayOfCommonIdentity() => new int[]{ 0 };

    internal static string CreateValidSemver()
    {
        var faker = new Faker();
        return faker.System.Semver();
    }

    internal static void CreateValidAddress()
    {
        var faker = new Faker();
        var address = faker.Address.FullAddress();
    }

    internal static DateTime CreateValidDateTime()
    {
        var faker = new Faker("en");
        return faker.Date.PastDateOnly(10).ToDateTime(TimeOnly.MinValue);
    }

    internal static string CreateValidHyperlink()
    {
        var faker = new Faker("en");
        return faker.Internet.Url();
    }

    internal static List<string> CreateValidListOfHyperlinks()
    {
        var faker = new Faker("en");
        var count = faker.Random.Int(1, 10);
        var list = new List<string>();
        for (int i = 0; i < count; i++)
        {
            list.Add(faker.Internet.Url());
        }
        return list;
    }

    internal static string CreateValidEmail()
    {
        var faker = new Faker("en");
        return faker.Internet.Email();
    }

    internal static List<string> CreateValidListOfEmails()
    {
        var faker = new Faker("en");
        var count = faker.Random.Int(1, 10);
        var list = new List<string>();
        for (int i = 0; i < count; i++)
        {
            list.Add(faker.Internet.Email());
        }
        return list;
    }

    internal static string CreateValidFileName()
    {
        var faker = new Faker("en");
        return faker.System.FileName();
    }

    internal static string CreateValidName()
    {
        var faker = new Faker("en");
        return faker.Name.FullName();
    }

    internal static List<string> CreateValidListOfStrings()
    {
        var faker = new Faker("en");
        var count = faker.Random.Int(1, 10);
        var list = new List<string>();
        for (int i = 0; i < count; i++)
        {
            list.Add(faker.Random.String());
        }
        return list;
    }

    internal static string CreateValidString()
    {
        var faker = new Faker("en");
        return faker.Random.String();
    }

    internal static SByte CreateValidSByteValue()
    {
        var faker = new Faker("en");
        return faker.Random.SByte();
    }

    internal static Byte CreateValidByteValue()
    {
        var faker = new Faker("en");
        return faker.Random.Byte();
    }

    internal static Int16 CreateValidInt16Value(Int16 min = Int16.MinValue, Int16 max = Int16.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.Short(min, max);
    }

    internal static int CreateValidInt32Value(int min = int.MinValue, int max = int.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.Int(min, max);
    }

    internal static Int64 CreateValidInt64Value(Int64 min = Int64.MinValue, Int64 max = Int64.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.Long(min, max);
    }

    //internal static Int128 CreateValidInt128Value()
    //{
    //    var faker = new Faker("en");
    //    return faker.Random.Int();
    //}

    internal static UInt16 CreateValidUInt16Value(UInt16 min = UInt16.MinValue, UInt16 max = UInt16.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.UShort(min, max);
    }

    internal static uint CreateValidUInt32Valu(uint min = uint.MinValue, uint max = uint.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.UInt(min, max);
    }

    internal static UInt64 CreateValidUInt64Value(UInt64 min = UInt64.MinValue, UInt64 max = UInt64.MaxValue)
    {
        var faker = new Faker("en");
        return faker.Random.ULong(min, max);
    }

    //internal static UInt128 CreateValidUInt128Value()
    //{
    //    var faker = new Faker("en");
    //    return faker.Random.
    //}

    //internal static Half CreateValidHalfValue()
    //{
    //    var faker = new Faker("en");
    //    return faker.Random.Ha
    //}

    internal static float CreateValidFloatValue(float min = -1.0f, float max = 1.0f)
    {
        var faker = new Faker("en");
        return faker.Random.Float(min,max);
    }

    internal static double CreateValidDoubleValue(double min = -1.0, double max = 1.0)
    {
        var faker = new Faker("en");
        return faker.Random.Double(min, max);
    }

    internal static decimal CreateValidDecimalValue(decimal min = -1, decimal max = 1)
    {
        var faker = new Faker("en");
        return faker.Random.Decimal(min, max);
    }

    internal static char CreateValidCharValue()
    {
        var faker = new Faker("en");
        return faker.Random.Char();
    }

}