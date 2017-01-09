using UnityEngine;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour
{
	public enum ElementType
	{
		Floor,
		Wall,
		Fruit,
		Snake
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

    int[,] Map;

    int NewSnakeElements;
    List<SnakeElement> bodyParts;

	List<Fruit> FruitList;
	int FruitCounter;

	bool Dead = false;

	Camera theCam;

    void Start()
    {
		// Map generation
        Map = new int[heightInTiles, widthInTiles];
		Rooms rooms = new Rooms();
        for (int x = 0; x < rooms.MapRoomsTopL[0].GetLength(1); x++)
        {
            for (int y = 0; y < rooms.MapRoomsTopL[0].GetLength(0); y++)
            {
                Map[y, x] = rooms.MapRoomsTopL[0][y, x];
                Map[y, x + widthInTiles / 2] = rooms.MapRoomsTopR[0][y, x];
                Map[y + heightInTiles / 2, x] = rooms.MapRoomsBotL[0][y, x];
                Map[y + heightInTiles / 2, x + widthInTiles / 2] = rooms.MapRoomsBotR[0][y, x];
            }
        }

		// Random Fruits
		Random.InitState(1337);

		FruitList = new List<Fruit> ();

		for (int i = 0; i < FruitsInMap; i++) {
			Vector2 fruitPos = Vector2.zero;
			while (Map[(int)fruitPos.y, (int)fruitPos.x] == 1) {
				fruitPos = new Vector2(Random.value*widthInTiles, Random.value*heightInTiles);
			}
			FruitList.Add(new Fruit(randomFruitType(Random.value), fruitPos, Random.value * 0.01f));
			FruitCounter++;
		}

		for (int x = 0; x < Map.GetLength(1); x++)
		{
			for (int y = 0; y < Map.GetLength(0); y++)
			{
				GameObject NewMapElement;
				
				switch (Map[y, x])
				{
					// Ground
					case 0:
						NewMapElement = Instantiate(FloorSprite) as GameObject;
						break;
					// Wall
					case 1:
						NewMapElement = Instantiate(WallSprite) as GameObject;
						break;
					
					default:
						NewMapElement = Instantiate(FloorSprite) as GameObject;
						break;
				}
				
				NewMapElement.transform.position = new Vector3(x, -y, -1);
				NewMapElement.transform.parent = MapParentStatic.transform;
			}
		}

        bodyParts = FindObjectOfType<SnakeController>().bodyParts;

		theCam = Camera.main;
    }
		
    void Update()
    {
        if (bodyParts == null)
        {
            bodyParts = FindObjectOfType<SnakeController>().bodyParts;
        }
	
        if (!Dead)
        {
			//Decay Fruits
			if (FruitList.Count > 0) {
				foreach (var fruit in new List<Fruit>(FruitList)) {
					fruit.UpdateTimers();
					if (!fruit.Alive()) {
						Map[(int)fruit.fruitPos.y, (int)fruit.fruitPos.x] = 0;
						FruitList.Remove(fruit);
					}
				}	
			}

			//spawn new fruits
			if (FruitCounter < FruitsInMap) {
				Vector2 fruitPos;
				do {
					fruitPos = new Vector2(Random.value * widthInTiles, Random.value * heightInTiles);
				} while ((Map[(int)fruitPos.y, (int)fruitPos.x] == 1) && (Map[(int)fruitPos.y, (int)fruitPos.x] == 2) 
				         &&  (Map[(int)fruitPos.y, (int)fruitPos.x] == 3));
				FruitList.Add(new Fruit(randomFruitType(Random.value), fruitPos, Random.value * 0.01f));
				FruitCounter++;
			}

            // Collision importand before replace ;)
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 1)
            {
                Dead = true;
            }

            // collision with cherries
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 4)
            {
                NewSnakeElements += 5;
				FruitCounter--;
				foreach (var fruit in new List<Fruit>(FruitList)) {
					if (new Vector3(fruit.fruitPos.x, fruit.fruitPos.y, bodyParts[0].MapPosition.z) == bodyParts[0].MapPosition) {
						FruitList.Remove(fruit);
					}
				}
            }

            //collision with apple
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 5)
            {
                NewSnakeElements += 10;
				FruitCounter--;
				foreach (var fruit in new List<Fruit>(FruitList)) {
					if (new Vector3(fruit.fruitPos.x, fruit.fruitPos.y, bodyParts[0].MapPosition.z) == bodyParts[0].MapPosition) {
						FruitList.Remove(fruit);
					}
				}
            }

			foreach (var fruit in FruitList) {
				Map[(int)fruit.fruitPos.y, (int)fruit.fruitPos.x] = fruit.FruitTypeInt;
			}

			// add new snake elements when eaten
            while (NewSnakeElements > 0)
            {
                bodyParts.Add(new SnakeElement(new Vector3(bodyParts[bodyParts.Count - 1].MapPosition.x, bodyParts[bodyParts.Count - 1].MapPosition.y, -2)));
                NewSnakeElements--;
            }

            // draw snake only when not dead because better looks
            if (!Dead)
            {
                Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] = 2;

                for (int i = 1; i < bodyParts.Count; i++)
                {
                    Map[(int)bodyParts[i].MapPosition.y, (int)bodyParts[i].MapPosition.x] = 3;
                }
            }

			// self collision
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 3)
            {
                Dead = true;
            }

            // attach camera to snake head
			theCam.transform.position = new Vector3(bodyParts[0].MapPosition.x, -bodyParts[0].MapPosition.y, theCam.transform.position.z);

			// cleanup draw list
            for (int child = 0; child < MapParent.transform.childCount; child++)
            {
                GameObject.Destroy(MapParent.transform.GetChild(child).gameObject);
            }

            for (int x = 0; x < Map.GetLength(1); x++)
            {
                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    switch (Map[y, x])
                    {
                        // Snake Head
                        case 2:
							GameObject NewSnakeHeadElement;
                            NewSnakeHeadElement = Instantiate(SnakeHead) as GameObject;
							NewSnakeHeadElement.transform.position = new Vector3(x, -y, -1);
							NewSnakeHeadElement.transform.parent = MapParent.transform;
                            break;
                        // Snake Body
                        case 3:
							GameObject NewSnakeBodyElement;
                            NewSnakeBodyElement = Instantiate(SnakeBody) as GameObject;
							NewSnakeBodyElement.transform.position = new Vector3(x, -y, -1);
							NewSnakeBodyElement.transform.parent = MapParent.transform;
                            break;
                        // Cherry
                        case 4:
							GameObject NewCherryElement;
                            NewCherryElement = Instantiate(CherrySprite) as GameObject;
							NewCherryElement.transform.position = new Vector3(x, -y, -1);
							NewCherryElement.transform.parent = MapParent.transform;
                            break;
                        // Apple
                        case 5:
							GameObject NewAppleElement;
                            NewAppleElement = Instantiate(AppleSprite) as GameObject;
							NewAppleElement.transform.position = new Vector3(x, -y, -1);
							NewAppleElement.transform.parent = MapParent.transform;
                            break;
                    }

                    if (Map[y, x] == 2 || Map[y, x] == 3)
                    {
                        Map[y, x] = 0;
                    }
                }
            }

            
        }
    }

	private Fruit.FruitType randomFruitType(float randNum) {
		float randScaledToEnum = randNum * (int)Fruit.FruitType.length;
		if (randScaledToEnum >= 0 && randScaledToEnum < 1) {
			return Fruit.FruitType.Cherry;
		} else if (randScaledToEnum > 1) {
			return Fruit.FruitType.Apple;
		} else {
			return Fruit.FruitType.Apple;
		}
	}
}
