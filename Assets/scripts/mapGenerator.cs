using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    public static GameObject startTile2;
    public static GameObject endTile2;

    public static List<GameObject> pathTiles2 = new List<GameObject>();

    private bool reachedY = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    public Sprite pathColor;

    public Sprite startColor;
    public Sprite endColor;

    private void Start()
    {
        generateMap();
    }

    private List<GameObject> getTopEdgeTiles(){

       List<GameObject> edgeTiles = new List<GameObject>();

        for(int i = mapWidth * (mapHeight - 1); i < mapWidth * mapHeight ; i++){
            
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> getBottomEdgeTiles(){

        List<GameObject> edgeTiles = new List<GameObject>();

        for(int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void moveDown2()
    {
        pathTiles2.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }


    private void generateMap()
    {
        for(int y = 0; y != mapHeight; y++)
        {
            for(int x = 0; x != mapWidth; x++)
            {
                GameObject newTile = Instantiate(mapTile);   
                
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector2(x,y); 
            }
        }

        List<GameObject> topEdgeTiles = getTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = getBottomEdgeTiles();

        startTile = topEdgeTiles[0];
        endTile = bottomEdgeTiles[0];

        currentTile = startTile;

        moveDown();

        while(reachedY == false)
        {
            if(currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else{
                reachedY = true;
            }
        }

        reachedY = false;

        pathTiles.Add(endTile);

        foreach(GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().sprite = pathColor;
        }
        startTile.GetComponent<SpriteRenderer>().sprite = startColor;
        endTile.GetComponent<SpriteRenderer>().sprite = endColor;

        topEdgeTiles = getTopEdgeTiles();
        bottomEdgeTiles = getBottomEdgeTiles();

        startTile2 = topEdgeTiles[3];
        endTile2 = bottomEdgeTiles[3];

        currentTile = startTile2;

        moveDown2();

        while(reachedY == false)
        {
            if(currentTile.transform.position.y > endTile2.transform.position.y)
            {
                moveDown2();
            }
            else{
                reachedY = true;
            }
        }

        pathTiles2.Add(endTile2);

        foreach(GameObject obj in pathTiles2)
        {
            obj.GetComponent<SpriteRenderer>().sprite = pathColor;
        }
        startTile.GetComponent<SpriteRenderer>().sprite = startColor;
        endTile.GetComponent<SpriteRenderer>().sprite = endColor;
    }
}