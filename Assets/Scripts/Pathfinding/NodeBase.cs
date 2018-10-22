using UnityEngine;

public class NodeBase
{

    public NodeBase(Vector2Int position, bool isObstacle = false)
    {
        IsObstacle = isObstacle;
        Position = position;
    }

    /// <summary>
    ///     Gets the total cost for this node.
    ///     f = g + h
    ///     TotalCost = MovementCost + EstimatedCost
    /// </summary>
    public int TotalCost
    {
        get { return MovementCost + EstimatedCost; }
    }

    /// <summary>
    ///     Gets the estimated cost for this node.
    ///     This is the heuristic from this node to the target node, or h.
    /// </summary>
    public int EstimatedCost { get; private set; }

    /// <summary>
    ///     Gets a value that indicates if the Node is walkable.
    /// </summary>
    public bool IsObstacle { get; private set; }

    /// <summary>
    ///     Gets the movement cost for this node.
    ///     This is the movement cost from this node to the starting node, or g.
    /// </summary>
    public int MovementCost { get; private set; }

    /// <summary>
    ///     Gets or sets the parent node to this node.
    /// </summary>
    public NodeBase Parent { get; set; }

    /// <summary>
    ///     Gets the x,y coordinate of the Node.
    /// </summary>
    public Vector2Int Position { get; private set; }

    /// <summary>
    ///     Two nodes are equal if they share the same position in the grid.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>T<see langword="true" /> if the nodes are in the same position.</returns>
    public override bool Equals(object obj)
    {
        var node = obj as NodeBase;
        if (node != null)
        {
            return Position.Equals(node.Position);
        }

        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }

    /// <summary>
    ///     Simple manhattan distance.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Manhattan_distance</remarks>
    /// <param name="target">Target node, for access to the goals position.</param>
    public void SetEstimatedCost(NodeBase target)
    {
        EstimatedCost = Mathf.Abs(Position.x - target.Position.x) + Mathf.Abs(Position.y - target.Position.y);
    }

    /// <summary>
    ///     Parent.MovementCost + 1
    /// </summary>
    /// <param name="parent">Parent node, for access to the parents movement cost.</param>
    public void SetMovementCost(NodeBase parent)
    {
        MovementCost = parent.MovementCost + 1;
    }

    public override string ToString()
    {
        return "Node (" + Position + ") isObstacle:" + IsObstacle + " MovementCost:" + MovementCost + "EstimatedCost:" + EstimatedCost; 
    }
}