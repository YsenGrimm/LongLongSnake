using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour
{

    public GameObject FloorSprite;
	public GameObject WallSprite;
	public GameObject MapParent;
	public GameObject CherrySprite;
	public GameObject AppleSprite;
	public GameObject SnakeBody;
	//public GameObject SnakeHead;

    int[,] Map;
    int WidthTiles;
    int HeightTiles;

	bool Dead = false;

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
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,3,0,0,0,1,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
		};

    }

    // Update is called once per frame
    void Update()
    {


		List<SnakeElement> bodyParts = FindObjectOfType<SnakeController>().getBodyParts();
		//SnakeController.SnakeDirections moveDirection = FindObjectOfType<SnakeController>().getMoveDirection();

		// Collision importand before replace ;)
		if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 1) {
			Dead = true;
		}

		if (Dead) {
			Application.LoadLevel("Home");
		}

		// replace for snake draw
		if (!Dead) {
			foreach (var snakeElem in bodyParts) {
				Map[(int)snakeElem.MapPosition.y, (int)snakeElem.MapPosition.x] = 2;
			}
		}

		Camera.main.gameObject.transform.position = new Vector3(bodyParts[0].MapPosition.x - 8, bodyParts[0].MapPosition.y - 6, -10);


		
		for (int child = 0; child < MapParent.transform.childCount; child++) {
			GameObject.Destroy(MapParent.transform.GetChild(child).gameObject);
		}

		for (int y = 0; y < Map.GetLength(0); y++)
		{
			for (int x = 0; x < Map.GetLength(1); x++)
			{
			 	GameObject NewMapElement;
				GameObject FloorMapElement;

				switch (Map[y,x]) {
				case 0:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				case 1:
					NewMapElement = Instantiate(WallSprite) as GameObject;
					break;
				case 2:
					NewMapElement = Instantiate(SnakeBody) as GameObject;

					FloorMapElement = Instantiate(FloorSprite) as GameObject;
					FloorMapElement.transform.position = new Vector3(x - (WidthTiles/2.0f), y - (HeightTiles/2.0f), 0);
					FloorMapElement.transform.parent = MapParent.transform;
					break;
				case 3:
					NewMapElement = Instantiate(CherrySprite) as GameObject;

					FloorMapElement = Instantiate(FloorSprite) as GameObject;
					FloorMapElement.transform.position = new Vector3(x - (WidthTiles/2.0f), y - (HeightTiles/2.0f), 0);
					FloorMapElement.transform.parent = MapParent.transform;
					break;
				case 4:
					NewMapElement = Instantiate(AppleSprite) as GameObject;

					FloorMapElement = Instantiate(FloorSprite) as GameObject;
					FloorMapElement.transform.position = new Vector3(x - (WidthTiles/2.0f), y - (HeightTiles/2.0f), 0);
					FloorMapElement.transform.parent = MapParent.transform;
					break;
				default:
					NewMapElement = Instantiate(FloorSprite) as GameObject;
					break;
				}

				NewMapElement.transform.position = new Vector3(x - (WidthTiles/2.0f), y - (HeightTiles/2.0f), -1);
				NewMapElement.transform.parent = MapParent.transform;

				if (Map[y,x] == 2) {
					Map[y,x] = 0;
				}
			}
		}
    }
}
