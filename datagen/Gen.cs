using harvester.Data;
using Npgsql;

namespace datagen;

public static class Gen
{
    public static async Task GenSpecifier(NpgsqlDataSource source)
    {
        var spec = new DtoSpecifier()
        {
            Id = Guid.Parse("11119686-9e27-46bf-aa26-139c271531a0"),
            Name = "person kind",
            Options =
            [
                "external",
                "internal",
                "vendor"
            ]
        };

        await spec.Save(source);
    }

    private static List<string> GetCatIds()
    {
        return
        [
            "3729e111-af53-44ad-ae03-8b5989633361",
            "cfdd0850-1515-45ee-a2e7-3ed75fb93c12",
            "2b3888f0-3fa3-434f-a556-9cf869e2484a",
            "7329f3ea-276c-4c11-b562-158da62d5c11",
            "576d5448-2972-4648-9629-9a62ac6873dc",
            "ca3d8d56-fe67-485d-8a5b-78d75eab9788",
            "31070098-a270-4b3b-bd36-5953fa2164dd",
            "9b281505-a0b1-4062-a826-07c26881b830",
            "cf42d3a5-994e-4904-990f-6739772fe1b2",
            "69582c1a-cc0c-4019-8b40-e0a1f76c2714",
        ];
    }
}