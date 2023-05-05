using KellermanSoftware.CompareNetObjects;

namespace Actie.Common.Tests;

public static class DeepAssert
{
    public static void Equal<T>(T? expected, T? actual, params string[] propertiesToIgnore)
    {
        CompareLogic compareLogic = new()
        {
            Config =
            {
                MembersToIgnore = propertiesToIgnore.ToList(),
                IgnoreCollectionOrder = true,
                IgnoreObjectTypes = true,
                CompareStaticProperties = false,
                CompareStaticFields = false,
                MaxMillisecondsDateDifference = 60 * 1000 // set the maximum difference to 60 seconds (1 minute)
            }
        };

        ComparisonResult comparisonResult = compareLogic.Compare(expected!, actual!);
        if (!comparisonResult.AreEqual)
        {
            throw new ObjectEqualException(expected!, actual!, comparisonResult.DifferencesString);
        }
    }
}

