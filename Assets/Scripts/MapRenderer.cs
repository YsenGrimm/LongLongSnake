using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour
{

    enum MapElement
    {
        Floor,
        Wall,
        Snake,
        Cherry,
        Apple
    }

    public GameObject floorSprite;

    MapElement[,] Map;
    int WidthTiles;
    int HeightTiles;

    // Use this for initialization
    void Start()
    {
        
        Map = new MapElement[Screen.width / 64, Screen.height / 64];
        for (int x = 0; x < Map.GetLength(0); x++)
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                Map[x, y] = MapElement.Floor;
            }
        }

        // Initialize map
        for (int x = 0; x < Map.GetLength(0); x++)
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                GameObject temp = Instantiate(floorSprite) as GameObject;
                temp.transform.position = new Vector3(x * 32, y * 32);
                Debug.Log("position: " + x.ToString() + ", " + y.ToString());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
