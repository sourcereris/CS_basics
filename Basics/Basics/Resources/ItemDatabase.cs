using System;
using System.Collections.Generic;
using System.Text;

static class ItemDatabase
{

    private static Dictionary<string, float> itemWeights;

    public static IReadOnlyDictionary<string, float> ItemWeights => itemWeights; // Expose the item weights as a read-only property

    static ItemDatabase()
    {
        itemWeights = new Dictionary<string, float>();
    }

    public static void AddItem(string item, float weight)
    {
        // To avoid checking i would use set instead of dictionary.
        // Does Set have functionality where you can store name and weight in this case?
        if (itemWeights.ContainsKey(item))
        {
            return; // Item already exists, do not add it again
                    // Since it is a database, used for a referece.
        }
        itemWeights.Add(item, weight);
    }
}