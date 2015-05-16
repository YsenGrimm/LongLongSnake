using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour
{

    public GameObject FloorSprite;
	public GameObject WallSprite;
	public GameObject MapParent;

    int[,] Map;
    int WidthTiles;
    int HeightTiles;

    // Use this for initialization
    void Start()
    {
		WidthTiles = 16;//(int)Mathf.Ceil(Screen.width / 64.0f);
		HeightTiles = 12;//(int)Mathf.Ceil(Screen.height / 64.0f);
        
		Map = new int[,] {
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
		};

    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.W)) {
			Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, 
			                                                        Camera.main.gameObject.transform.position.y + 12, 
			                                                        Camera.main.gameObject.transform.position.z);
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x - 24, 
			                                                        Camera.main.gameObject.transform.position.y, 
			                                                        Camera.main.gameObject.transform.position.z);
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, 
			                                                        Camera.main.gameObject.transform.position.y - 12, 
			                                                        Camera.main.gameObject.transform.position.z);
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x + 24, 
			                                                        Camera.main.gameObject.transform.position.y, 
			                                                        Camera.main.gameObject.transform.position.z);
		}
		
		for (int child = 0; child < MapParent.transform.childCount; child++) {
			GameObject.Destroy(MapParent.transform.GetChild(child).gameObject);
		}

		for (int y = 0; y < Map.GetLength(0); y++)
		{
			for (int x = 0; x < Map.GetLength(1); x++)
			{
				GameObject NewMapElement ;
				switch (Map[y,x]) {
				case 0:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				case 1:
					NewMapElement = Instantiate(WallSprite) as GameObject;
					break;
				case 2:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				case 3:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				case 4:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				default:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				}
				NewMapElement.transform.position = new Vector3(x - (WidthTiles/2.0f), y - (HeightTiles/2.0f), 0);
				NewMapElement.transform.parent = MapParent.transform;
			}
		}
    }
}
