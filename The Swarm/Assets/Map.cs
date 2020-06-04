using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Map : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector2Int mapSize;

    private GameObject[,] tiles;
    private new static Camera camera;
    private Vector3 cameraTopRightCorner;
    private Vector3 cameraBottomLeftCorner;
    private void Start()
    {
        tiles = new GameObject[mapSize.x,mapSize.y];
        camera = Camera.main;
        var cameraPosition = camera.transform.position;
        cameraTopRightCorner = camera.ViewportToWorldPoint(Vector3.one) - cameraPosition;
        cameraBottomLeftCorner = camera.ViewportToWorldPoint(Vector3.zero) - cameraPosition;
        
    }

    private void Update()
    {
        UpdateMap();
    }

    private void UpdateMap()
    {        
        var stopwatch = Stopwatch.StartNew();
        var cameraPosition = camera.transform.position;
        var leftRenderEdge = (int)(cameraPosition.x + cameraBottomLeftCorner.x - 1);
        var rightRenderEdge = (int)(cameraPosition.x + cameraTopRightCorner.x + 1);
        var bottomRenderEdge = (int)(cameraPosition.y + cameraBottomLeftCorner.y - 1);
        var topRenderEdge = (int)(cameraPosition.y + cameraTopRightCorner.y + 1);
        
        for (var i = leftRenderEdge; i < rightRenderEdge; i++)
        for (var j = bottomRenderEdge; j < topRenderEdge; j++)
        {
            if (i < 0 || i >= mapSize.x || j < 0 || j >= mapSize.y)
            {
                continue;
            }

            var pos = transform.position + new Vector3(i, j, 0);
            if (tiles[i, j] == null)
            {
                tiles[i, j] = Instantiate(tilePrefab, pos, Quaternion.identity);
            }
        }

        Debug.Log(stopwatch.ElapsedMilliseconds);
        stopwatch.Stop();
    }
}
