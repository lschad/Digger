using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Interface to setup and run the AStar algorithm.
/// </summary>
/// <remarks>https://github.com/jbaldwin/astar.cs</remarks>
public class AStar
{
    private readonly Grid2D _grid;
    /// <summary>
    ///     The open list.
    /// </summary>
    private readonly SortedList<int, NodeBase> _openList;

    /// <summary>
    ///     The closed list.
    /// </summary>
    private readonly SortedList<int, NodeBase> _closedList;

    /// <summary>
    ///     The goal node.
    /// </summary>
    private NodeBase _goal;

    /// <summary>
    ///     Creates a new AStar algorithm instance with the provided start and goal nodes.
    /// </summary>
    /// <param name="grid">The grid in which a path shall be found.</param>
    /// <param name="start">The starting node for the AStar algorithm.</param>
    /// <param name="goal">The goal node for the AStar algorithm.</param>
    public AStar(Grid2D grid, NodeBase start, NodeBase goal)
    {
        _grid = grid;
        var duplicateComparer = new DuplicateComparer();
        _openList = new SortedList<int, NodeBase>(duplicateComparer);
        _closedList = new SortedList<int, NodeBase>(duplicateComparer);
        Reset(start, goal);
    }

    /// <summary>
    ///     Gets the current state of the closed list.
    /// </summary>
    public IEnumerable<NodeBase> ClosedList
    {
        get { return _closedList.Values; }
    }

    /// <summary>
    ///     Gets the current state of the open list.
    /// </summary>
    public IEnumerable<NodeBase> OpenList
    {
        get { return _openList.Values; }
    }

    /// <summary>
    ///     Gets the current node that the AStar algorithm is at.
    /// </summary>
    public NodeBase CurrentNode { get; private set; }

    /// <summary>
    ///     Gets the current amount of steps that the algorithm has performed.
    /// </summary>
    public int Steps { get; private set; }

    public IEnumerable<NodeBase> GetChildren(NodeBase node)
    {
        var childXPos = new[] {0, -1, 1, 0};
        var childYPos = new[] {-1, 0, 0, 1};
        var children = new List<NodeBase>();

        for (var i = 0; i < childXPos.Length; i++)
        {
            var x = node.Position.x + childXPos[i];
            var y = node.Position.y + childYPos[i]; // skip any nodes out of bounds.
            if (x >= _grid.Width || x < 0
             || y >= _grid.Height || y < 0)
                continue;

            children.Add(_grid[x, y]);
        }

        return children.ToArray();
    }

    /// <summary>
    ///     Gets the path of the last solution of the AStar algorithm.
    ///     Will return a partial path if the algorithm has not finished yet.
    /// </summary>
    /// <returns>Returns null if the algorithm has never been run.</returns>
    public IEnumerable<NodeBase> GetPath()
    {
        if (CurrentNode != null)
        {
            var next = CurrentNode;
            var path = new List<NodeBase>();
            while (next != null)
            {
                path.Add(next);
                next = next.Parent;
            }

            path.Reverse();
            return path.ToArray();
        }

        return null;
    }

    /// <summary>
    ///     Resets the AStar algorithm with the newly specified start node and goal node.
    /// </summary>
    /// <param name="start">The starting node for the AStar algorithm.</param>
    /// <param name="goal">The goal node for the AStar algorithm.</param>
    public void Reset(NodeBase start, NodeBase goal)
    {
        _openList.Clear();
        _closedList.Clear();
        CurrentNode = start;
        _goal = goal;
        _openList.Add(CurrentNode);
    }

    /// <summary>
    ///     Steps the AStar algorithm forward until it either fails or finds the goal node.
    /// </summary>
    /// <returns>Returns the state the algorithm finished in, Failed or GoalFound.</returns>
    public AStarState Run()
    {
        var s = Step();
        while (s == AStarState.Searching)
        {
            s = Step();
        }

        return s;
    }

    /// <summary>
    ///     Moves the AStar algorithm forward one step.
    /// </summary>
    /// <returns>
    ///     Returns the state the algorithm is in after the step, either <see cref="AStarState.Failed" />,
    ///     <see cref="AStarState.GoalFound" /> or <see cref="AStarState.Searching" />.
    /// </returns>
    public AStarState Step()
    {
        Steps++;
        while (true)
        {
            // There are no more nodes to search, return failure.
            if (_openList.IsEmpty())
            {
                return AStarState.Failed;
            }

            // Check the next best node in the graph by TotalCost.
            CurrentNode = _openList.Pop();

            // This node has already been searched, check the next one.
            if (CurrentNode.IsObstacle || ClosedList.Contains(CurrentNode))
            {
                continue;
            }

            // An unsearched node has been found, search it.
            break;
        }

        _closedList.Add(CurrentNode);

        // Found the goal, stop searching.
        if (CurrentNode.Equals(_goal))
        {
            return AStarState.GoalFound;
        }

        // Node was not the goal so add all children nodes to the open list.
        // Each child needs to have its movement cost set and estimated cost.
        foreach (var child in GetChildren(CurrentNode))
        {
            // If the child has already been searched (closed list) or is on
            // the open list to be searched then do not modify its movement cost
            // or estimated cost since they have already been set previously.
            if (OpenList.Contains(child) || ClosedList.Contains(child))
            {
                continue;
            }

            child.Parent = CurrentNode;
            child.SetMovementCost(CurrentNode);
            child.SetEstimatedCost(_goal);
            _openList.Add(child);
        }

        // This step did not find the goal so return status of still searching.
        return AStarState.Searching;
    }
}