/// <summary>
///     AStar algorithm states while searching for the goal.
/// </summary>
/// <remarks>https://github.com/jbaldwin/astar.cs</remarks>
public enum AStarState
{
    /// <summary>
    ///     The AStar algorithm is still searching for the goal.
    /// </summary>
    Searching,

    /// <summary>
    ///     The AStar algorithm has found the goal.
    /// </summary>
    GoalFound,

    /// <summary>
    ///     The AStar algorithm has failed to find a solution.
    /// </summary>
    Failed
}