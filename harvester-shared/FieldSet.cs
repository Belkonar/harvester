namespace harvester_shared;

public class FieldSet : Dictionary<string, HashSet<string>>
{
    public void Add(string key, string kind)
    {
        kind = kind.ToLower();

        if (TryGetValue(key, out var set))
        {
            set.Add(kind);
        }
        else
        {
            Add(key, [kind]);
        }
    }

    public void Merge(FieldSet set, string prefix = "")
    {
        foreach (var pair in set)
        {
            foreach (var kind in pair.Value)
            {
                Add($"{prefix}{pair.Key}", kind);
            }
        }
    }
}
