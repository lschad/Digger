using System.Collections.Generic;

/// <summary>
///     Extension methods to make the <see cref="System.Collections.Generic.SortedList" /> easier to use.
/// </summary>
/// <remarks>https://github.com/jbaldwin/astar.cs</remarks>
internal static class SortedListExtensions
{
    /// <summary>
    ///     Adds a NodeBase to the SortedList.
    /// </summary>
    /// <param name="sortedList">SortedList to add the node to.</param>
    /// <param name="node">Node to add to the sortedList.</param>
    internal static void Add(this SortedList<int, NodeBase> sortedList, NodeBase node)
    {
        sortedList.Add(node.TotalCost, node);
    }

    /// <summary>
    ///     Checks if the SortedList is empty.
    /// </summary>
    /// <param name="sortedList">SortedList to check if it is empty.</param>
    /// <returns>True if sortedList is empty, false if it still has elements.</returns>
    internal static bool IsEmpty<TKey, TValue>(this SortedList<TKey, TValue> sortedList)
    {
        return sortedList.Count == 0;
    }

    /// <summary>
    ///     Removes the node from the sorted list with the smallest TotalCost and returns that node.
    /// </summary>
    /// <param name="sortedList">SortedList to remove and return the smallest TotalCost node.</param>
    /// <returns>Node with the smallest TotalCost.</returns>
    internal static NodeBase Pop(this SortedList<int, NodeBase> sortedList)
    {
        var top = sortedList.Values[0];
        sortedList.RemoveAt(0);
        return top;
    }
}