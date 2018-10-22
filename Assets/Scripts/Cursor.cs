using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject ASDHASFOHDF;
    private readonly Vector2Int _defaultVector = Vector2.negativeInfinity.FloorToInt();
    private Vector2Int _start, _end;
    private Grid2D _grid;

    public void Start()
    {
        _start = _defaultVector;
        _end = _defaultVector;
    }

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        var screenPosition = Input.mousePosition;
    //        var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

    //        if (_start.Equals(_defaultVector))
    //        {
    //            _start = worldPosition.ToVector2().FloorToInt();
    //        }
    //        else if (_end.Equals(_defaultVector))
    //        {
    //            _end = worldPosition.ToVector2().FloorToInt();

    //            var w = (LevelController.Instance.Tilemap.size.x/2)-2;
    //            var h = (LevelController.Instance.Tilemap.size.y/2)-2;
    //            var walls = LevelController.Instance.GetObstacles();
    //            _grid = new Grid2D(walls, LevelController.Instance.Tilemap.size.y, LevelController.Instance.Tilemap.size.x);

    //            Debug.Log("Start x" + (_start.x+w) + " y" + (_start.y + h));
    //            Debug.Log("End x" + (_end.x+w) + " y" + (_end.y + h));

    //            _grid.Start = _grid.Grid[_start.x+w][_start.y+h];
    //            _grid.Goal= _grid.Grid[_end.x+w][_end.y+h];

    //            var astar= new AStar(_grid.Start, _grid.Goal);
    //            var result = astar.Run();

    //            if (result == AStarState.GoalFound)
    //            {
    //                Debug.Log("FOUND");
    //                var path = astar.GetPath();
    //                foreach (GridNode node in path)
    //                {
    //                    var dl = Instantiate(ASDHASFOHDF);
    //                    dl.transform.position = new Vector3(node.X, node.Y, 0);
    //                    transform.AddChild(dl);
    //                }
    //            }


    //            //var distance = Vector2.Distance(new Vector2(_start.x, _start.y), new Vector2(_end.x, _start.y));
    //            //for (var i = 0; i < distance; i++)
    //            //{
    //            //    var dl = Instantiate(ASDHASFOHDF);
    //            //    dl.transform.position = new Vector3(_start.x + i, 0, 0);
    //            //    transform.AddChild(dl);
    //            //}
    //        }
    //        else
    //        {
    //            Debug.Log("reset");
    //            _start = _defaultVector;
    //            _end = _defaultVector;
    //            transform.DestroyChildren();
    //        }
    //    }
    //}
}
