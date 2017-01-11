using UnityEngine;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour
{
	public enum ElementType
	{
		Floor, Wall, SnakeBody, SnakeHead, Cherry, Apple, length
	}

	[Header("Map Parts")]
    public GameObject FloorSprite;
    public GameObject WallSprite;
    public GameObject MapParent;
	public GameObject MapParentStatic;
	[Header("Snake Parts")]
	public GameObject SnakeBody;
	public GameObject SnakeHead;
	[Header("Fruits")]
    public GameObject CherrySprite;
    public GameObject AppleSprite;

	public int FruitsInMap = 10;

    int widthInTiles = 42;
    int heightInTiles = 24;

    ElementType[,] Map;

    int NewSnakeElements;
    List<SnakeElement> bodyParts;

	List<Fruit> FruitList;
	int FruitCounter;

	bool Dead = false;

	Camera theCam;

    void Start()
    {
		// Map generation
        Map = new ElementType[heightInTiles, widthInTiles];
		Rooms rooms = new Rooms();
        for (int x = 0; x < rooms.MapRoomsTopL.GetLength(1); x++)
        {
            for (int y = 0; y < rooms.MapRoomsTopL.GetLength(0); y++)
            {
                Map[y, x] = rooms.MapRoomsTopL[y, x];
                Map[y, x + widthInTiles / 2] = rooms.MapRoomsTopR[y, x];
                Map[y + heightInTiles / 2, x] = rooms.MapRoomsBotL[y, x];
                Map[y + heightInTiles / 2, x + widthInTiles / 2] = rooms.MapRoomsBotR[y, x];
            }
        }

		// Random Fruits
		Random.InitState(1337);

		FruitList = new List<Fruit> ();

		// place random fruits
		for (int i = 0; i < FruitsInMap; i++) {
			Vector2 fruitPos = new Vector2(Random.value * widthInTiles, Random.value * heightInTiles);
			// choose new position until not on wall
			while (Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.Wall) {
				fruitPos = new Vector2(Random.value * widthInTiles, Random.value * heightInTiles);
			}

			FruitList.Add(new Fruit(randomFruitType(Random.value), fruitPos, Random.value * 0.01f));
			FruitCounter++;
		}

		// place wall tiles
		for (int x = 0; x < Map.GetLength(1); x++) {
			for (int y = 0; y < Map.GetLength(0); y++) {
				GameObject NewMapElement;
				
				switch (Map[y, x]) {
					// Floor
					case ElementType.Floor:
						NewMapElement = Instantiate(FloorSprite) as GameObject;
						break;
					// Wall
					case ElementType.Wall:
						NewMapElement = Instantiate(WallSprite) as GameObject;
						break;
					
					default:
						NewMapElement = Instantiate(FloorSprite) as GameObject;
						break;
				}
				
				// position fuckup
				NewMapElement.transform.position = new Vector3(x, -y, -1);
				NewMapElement.transform.parent = MapParentStatic.transform;
			}
		}


        bodyParts = FindObjectOfType<SnakeController>().bodyParts;
		theCam = Camera.main;
    }
		
    void Update() {
		if (Dead) { return; }

		//Decay Fruits
		if (FruitList.Count > 0) {
			foreach (var fruit in new List<Fruit>(FruitList)) {
				fruit.UpdateTimers();
				if (fruit.Alive()) { continue; }
				Map[(int)fruit.fruitPos.y, (int)fruit.fruitPos.x] = ElementType.Floor;
				FruitList.Remove(fruit);
			}	
		}

		// respawn new fruits
		if (FruitCounter < FruitsInMap) {
			Vector2 fruitPos = new Vector2(Random.value * widthInTiles, Random.value * heightInTiles);

			while((Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.Wall) 
					&& (Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.Cherry)
					&& (Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.Apple) 
					&& (Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.SnakeBody)
					&& (Map[(int)fruitPos.y, (int)fruitPos.x] == ElementType.SnakeHead)) {
				fruitPos = new Vector2(Random.value * widthInTiles, Random.value * heightInTiles);
			}
			
			FruitList.Add(new Fruit(randomFruitType(Random.value), fruitPos, Random.value * 0.01f));
			FruitCounter++;
		}

		// collide snek head with wall
		if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == ElementType.Wall) {
			Dead = true;
		}

		// collide snek head with cherry
		if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == ElementType.Cherry) {
			NewSnakeElements += 5;
			FruitCounter--;
			foreach (var fruit in new List<Fruit>(FruitList)) {
				if (new Vector3((int)fruit.fruitPos.x, (int)fruit.fruitPos.y, (int)bodyParts[0].MapPosition.z) == bodyParts[0].MapPosition) {
					FruitList.Remove(fruit);
				}
			}
		}

		// collide snek head with apple
		if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == ElementType.Apple) {
			NewSnakeElements += 10;
			FruitCounter--;
			foreach (var fruit in new List<Fruit>(FruitList)) {
				if (new Vector3((int)fruit.fruitPos.x, (int)fruit.fruitPos.y, (int)bodyParts[0].MapPosition.z) == bodyParts[0].MapPosition) {
					FruitList.Remove(fruit);
				}
			}
		}

		// set fruit type
		foreach (var fruit in FruitList) {
			Map[(int)fruit.fruitPos.y, (int)fruit.fruitPos.x] = fruit.type;
		}

		// add new snake elements when eaten
		while (NewSnakeElements > 0) {
			bodyParts.Add(new SnakeElement(new Vector3(bodyParts[bodyParts.Count - 1].MapPosition.x, bodyParts[bodyParts.Count - 1].MapPosition.y, -2)));
			NewSnakeElements--;
		}

		// draw snake only when not dead because better looks
		if (!Dead) {
			// head
			Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] = ElementType.SnakeHead;
			 
			// body
			for (int i = 1; i < bodyParts.Count; i++) {
				Map[(int)bodyParts[i].MapPosition.y, (int)bodyParts[i].MapPosition.x] = ElementType.SnakeBody;
			}
		}

		// self collision
		if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == ElementType.SnakeBody) {
			Dead = true;
		}

		// attach camera to snake head
		theCam.transform.position = new Vector3(bodyParts[0].MapPosition.x, -bodyParts[0].MapPosition.y, theCam.transform.position.z);

		// cleanup draw list
		for (int child = 0; child < MapParent.transform.childCount; child++) {
			GameObject.Destroy(MapParent.transform.GetChild(child).gameObject);
		}

		for (int x = 0; x < Map.GetLength(1); x++) {
			for (int y = 0; y < Map.GetLength(0); y++) {
				switch (Map[y, x]) {
					// Snake Head
					case ElementType.SnakeHead:
						GameObject NewSnakeHeadElement;
						NewSnakeHeadElement = Instantiate(SnakeHead) as GameObject;
						NewSnakeHeadElement.transform.position = new Vector3(x, -y, -1);
						NewSnakeHeadElement.transform.parent = MapParent.transform;
						break;
					// Snake Body
					case ElementType.SnakeBody:
						GameObject NewSnakeBodyElement;
						NewSnakeBodyElement = Instantiate(SnakeBody) as GameObject;
						NewSnakeBodyElement.transform.position = new Vector3(x, -y, -1);
						NewSnakeBodyElement.transform.parent = MapParent.transform;
						break;
					// Cherry
					case ElementType.Cherry:
						GameObject NewCherryElement;
						NewCherryElement = Instantiate(CherrySprite) as GameObject;
						NewCherryElement.transform.position = new Vector3(x, -y, -1);
						NewCherryElement.transform.parent = MapParent.transform;
						break;
					// Apple
					case ElementType.Apple:
						GameObject NewAppleElement;
						NewAppleElement = Instantiate(AppleSprite) as GameObject;
						NewAppleElement.transform.position = new Vector3(x, -y, -1);
						NewAppleElement.transform.parent = MapParent.transform;
						break;
				}

				if (Map[y, x] == ElementType.SnakeHead || Map[y, x] == ElementType.SnakeBody) {
                	Map[y, x] = ElementType.Floor;
                }
			}
		}
    }

	private ElementType randomFruitType(float randNum) {
		float randScaledToEnum = randNum * 2;
		if (randScaledToEnum >= 0 && randScaledToEnum < 1) {
			return ElementType.Cherry;
		} else if (randScaledToEnum > 1) {
			return ElementType.Apple;
		} else {
			return ElementType.Apple;
		}
	}
}
