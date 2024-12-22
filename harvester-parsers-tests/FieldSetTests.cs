using harvester_shared;

namespace harvester_parsers_tests;

// NOTE: This stuff is here cause I don't want a new project for one class
public class FieldSetTests
{
    [TestCase]
    public void MergeSimple()
    {
        var set = new FieldSet { { "", "object" } };
        set.Merge(new FieldSet()
        {
            { "hi", "dave" },
            { "", "boolean" }
        });


        Assert.That(set[""], Contains.Item("object"));
        Assert.That(set[""], Contains.Item("boolean"));
        Assert.That(set["hi"], Contains.Item("dave"));
    }

    [TestCase]
    public void MergePrefix()
    {
        var set = new FieldSet { { "", "object" } };
        set.Merge(new FieldSet()
        {
            { "hi", "dave" },
            { "", "boolean" }
        }, "lol.");

        Assert.That(set[""], Contains.Item("object"));
        Assert.That(set["lol."], Contains.Item("boolean"));
        Assert.That(set["lol.hi"], Contains.Item("dave"));
    }
}
