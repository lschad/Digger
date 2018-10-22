using System.Collections.Generic;

/// <summary>
///     System.Collections.Generic.SortedList by default does not allow duplicate items.
///     Since items are keyed by TotalCost there can be duplicate entries per key.
/// </summary>
/// <remarks>https://github.com/jbaldwin/astar.cs</remarks>
internal class DuplicateComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return x <= y ? -1 : 1;
    }
}