using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class AStarTests : MonoBehaviour
{
    [Test]
    public void RunCube()
    {
        var grid = new NodeBase[5, 5];

        Set(grid, 0, 0);
        Set(grid, 0, 1);
        Set(grid, 0, 2);
        Set(grid, 0, 3);
        Set(grid, 0, 4);

        Set(grid, 1, 0);
        Set(grid, 1, 1);
        Set(grid, 1, 2);
        Set(grid, 1, 3);
        Set(grid, 1, 4);

        Set(grid, 2, 0);
        Set(grid, 2, 1);
        Set(grid, 2, 2);
        Set(grid, 2, 3);
        Set(grid, 2, 4);

        Set(grid, 3, 0);
        Set(grid, 3, 1);
        Set(grid, 3, 2);
        Set(grid, 3, 3);
        Set(grid, 3, 4);

        Set(grid, 4, 0);
        Set(grid, 4, 1);
        Set(grid, 4, 2);
        Set(grid, 4, 3);
        Set(grid, 4, 4);

        var start = new NodeBase(new Vector2Int(0, 0));
        var end = new NodeBase(new Vector2Int(4, 4));
        var g = new Grid2D(grid);

        var aStar = new AStar(g, start, end);
        var result = aStar.Run();

        Assert.AreEqual(AStarState.GoalFound, result);
        var path = aStar.GetPath().ToArray();

        Assert.AreEqual(9, path.Length);

        Assert.AreEqual(new Vector2Int(0, 0), path[0].Position);
        Assert.AreEqual(new Vector2Int(1, 0), path[1].Position);
        Assert.AreEqual(new Vector2Int(2, 0), path[2].Position);
        Assert.AreEqual(new Vector2Int(3, 0), path[3].Position);
        Assert.AreEqual(new Vector2Int(4, 0), path[4].Position);
        Assert.AreEqual(new Vector2Int(4, 1), path[5].Position);
        Assert.AreEqual(new Vector2Int(4, 2), path[6].Position);
        Assert.AreEqual(new Vector2Int(4, 3), path[7].Position);
        Assert.AreEqual(new Vector2Int(4, 4), path[8].Position);
    }

    [Test]
    public void RunRectangle()
    {
        var grid = new NodeBase[3, 7];

        Set(grid, 0, 0);
        Set(grid, 0, 1);
        Set(grid, 0, 2);
        Set(grid, 0, 3);
        Set(grid, 0, 4);
        Set(grid, 0, 5);
        Set(grid, 0, 6);

        Set(grid, 1, 0);
        Set(grid, 1, 1);
        Set(grid, 1, 2);
        Set(grid, 1, 3);
        Set(grid, 1, 4);
        Set(grid, 1, 5);
        Set(grid, 1, 6);

        Set(grid, 2, 0);
        Set(grid, 2, 1);
        Set(grid, 2, 2);
        Set(grid, 2, 3);
        Set(grid, 2, 4);
        Set(grid, 2, 5);
        Set(grid, 2, 6);

        var start = new NodeBase(new Vector2Int(0, 0));
        var end = new NodeBase(new Vector2Int(2, 6));
        var g = new Grid2D(grid);

        var aStar = new AStar(g, start, end);
        var result = aStar.Run();

        Assert.AreEqual(AStarState.GoalFound, result);
        var path = aStar.GetPath().ToArray();

        Assert.AreEqual(9, path.Length);

        Assert.AreEqual(new Vector2Int(0, 0), path[0].Position);
        Assert.AreEqual(new Vector2Int(1, 0), path[1].Position);
        Assert.AreEqual(new Vector2Int(2, 0), path[2].Position);
        Assert.AreEqual(new Vector2Int(2, 1), path[3].Position);
        Assert.AreEqual(new Vector2Int(2, 2), path[4].Position);
        Assert.AreEqual(new Vector2Int(2, 3), path[5].Position);
        Assert.AreEqual(new Vector2Int(2, 4), path[6].Position);
        Assert.AreEqual(new Vector2Int(2, 5), path[7].Position);
        Assert.AreEqual(new Vector2Int(2, 6), path[8].Position);
    }

    [Test]
    public void RunCube_WithObstacles()
    {
        var grid = new NodeBase[5, 5];

        Set(grid, 0, 0);
        Set(grid, 0, 1);
        Set(grid, 0, 2);
        Set(grid, 0, 3);
        Set(grid, 0, 4);

        Set(grid, 1, 0, true);
        Set(grid, 1, 1);
        Set(grid, 1, 2);
        Set(grid, 1, 3);
        Set(grid, 1, 4);

        Set(grid, 2, 0, true);
        Set(grid, 2, 1, true);
        Set(grid, 2, 2);
        Set(grid, 2, 3);
        Set(grid, 2, 4);

        Set(grid, 3, 0, true);
        Set(grid, 3, 1, true);
        Set(grid, 3, 2);
        Set(grid, 3, 3);
        Set(grid, 3, 4);

        Set(grid, 4, 0, true);
        Set(grid, 4, 1);
        Set(grid, 4, 2, true);
        Set(grid, 4, 3);
        Set(grid, 4, 4);


        var output = "=================";
        for (var y = 4; y >= 0; y--)
        {
            for (var x = 0; x < 5; x++)
            {
                output += (grid[x, y].IsObstacle ? "x" : "_");
            }
            output += Environment.NewLine;
        }
        output += "=================";
        Debug.Log(output);

        var start = new NodeBase(new Vector2Int(0, 0));
        var end = new NodeBase(new Vector2Int(4, 4));
        var g = new Grid2D(grid);

        var aStar = new AStar(g, start, end);
        var result = aStar.Run();

        Assert.AreEqual(AStarState.GoalFound, result);
        var path = aStar.GetPath().ToArray();

        Assert.AreEqual(9, path.Length);

        Assert.AreEqual(new Vector2Int(0, 0), path[0].Position);
        Assert.AreEqual(new Vector2Int(0, 1), path[1].Position);
        Assert.AreEqual(new Vector2Int(1, 1), path[2].Position);
        Assert.AreEqual(new Vector2Int(1, 2), path[3].Position);
        Assert.AreEqual(new Vector2Int(2, 2), path[4].Position);
        Assert.AreEqual(new Vector2Int(3, 2), path[5].Position);
        Assert.AreEqual(new Vector2Int(3, 3), path[6].Position);
        Assert.AreEqual(new Vector2Int(4, 3), path[7].Position);
        Assert.AreEqual(new Vector2Int(4, 4), path[8].Position);
    }

    [Test]
    public void RunRectangle_WithObstacles()
    {
        var grid = new NodeBase[3, 7];

        Set(grid, 0, 0);
        Set(grid, 0, 1);
        Set(grid, 0, 2);
        Set(grid, 0, 3);
        Set(grid, 0, 4);
        Set(grid, 0, 5);
        Set(grid, 0, 6);

        Set(grid, 1, 0, true);
        Set(grid, 1, 1, true);
        Set(grid, 1, 2, true);
        Set(grid, 1, 3, true);
        Set(grid, 1, 4);
        Set(grid, 1, 5, true);
        Set(grid, 1, 6);

        Set(grid, 2, 0);
        Set(grid, 2, 1);
        Set(grid, 2, 2);
        Set(grid, 2, 3);
        Set(grid, 2, 4);
        Set(grid, 2, 5);
        Set(grid, 2, 6);

        var start = new NodeBase(new Vector2Int(0, 0));
        var end = new NodeBase(new Vector2Int(2, 6));
        var g = new Grid2D(grid);

        var aStar = new AStar(g, start, end);
        var result = aStar.Run();

        Assert.AreEqual(AStarState.GoalFound, result);
        var path = aStar.GetPath().ToArray();

        Assert.AreEqual(9, path.Length);

        Assert.AreEqual(new Vector2Int(0, 0), path[0].Position);
        Assert.AreEqual(new Vector2Int(0, 1), path[1].Position);
        Assert.AreEqual(new Vector2Int(0, 2), path[2].Position);
        Assert.AreEqual(new Vector2Int(0, 3), path[3].Position);
        Assert.AreEqual(new Vector2Int(0, 4), path[4].Position);
        Assert.AreEqual(new Vector2Int(1, 4), path[5].Position);
        Assert.AreEqual(new Vector2Int(2, 4), path[6].Position);
        Assert.AreEqual(new Vector2Int(2, 5), path[7].Position);
        Assert.AreEqual(new Vector2Int(2, 6), path[8].Position);
    }


    private void Set(NodeBase[,] grid, int x, int y, bool isObstacle = false)
    {
        grid[x, y] = new NodeBase(new Vector2Int(x, y), isObstacle);
    }
}