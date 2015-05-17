using UnityEngine;
using UnityEngine.UI;
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
    public GameObject SnakeHead;

    public GameObject Score;
    public GameObject gameOverMenu;

    int widthInTiles = 42;
    int heightInTiles = 24;

    int[,] Map;
    int NewSnakeElements;
    bool Dead = false;
    bool menuShown = false;
    List<SnakeElement> bodyParts;

    // Use this for initialization
    void Start()
    {
        Rooms rooms = new Rooms();

        Map = new int[heightInTiles, widthInTiles];

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

        bodyParts = FindObjectOfType<SnakeController>().bodyParts;
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyParts == null)
        {
            bodyParts = FindObjectOfType<SnakeController>().bodyParts;
        }

        //SnakeController.SnakeDirections moveDirection = FindObjectOfType<SnakeController>().getMoveDirection();
        if (!Dead)
        {

            // Collision importand before replace ;)
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 1)
            {
                Dead = true;
            }

            // collision with cherries
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 4)
            {
                NewSnakeElements += 5;
            }

            //collision with apple
            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 5)
            {
                NewSnakeElements += 15;
            }

            while (NewSnakeElements > 0)
            {
                bodyParts.Add(new SnakeElement(new Vector3(bodyParts[bodyParts.Count - 1].MapPosition.x, bodyParts[bodyParts.Count - 1].MapPosition.y, -2)));
                NewSnakeElements--;
            }

            // commented because of menu
            // reset level on death
            //if (Dead)
            //{
            //    Application.LoadLevel("Home");
            //}

            // draw snake only when not dead because better looks
            if (!Dead)
            {
                Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] = 2;

                for (int i = 1; i < bodyParts.Count; i++)
                {
                    Map[(int)bodyParts[i].MapPosition.y, (int)bodyParts[i].MapPosition.x] = 3;
                }
            }

            if (Map[(int)bodyParts[0].MapPosition.y, (int)bodyParts[0].MapPosition.x] == 3)
            {
                Dead = true;
            }

            // attach camera to snake head
            Camera.main.gameObject.transform.position = new Vector3(bodyParts[0].MapPosition.x, -bodyParts[0].MapPosition.y, -10);

            for (int child = 0; child < MapParent.transform.childCount; child++)
            {
                GameObject.Destroy(MapParent.transform.GetChild(child).gameObject);
            }

            for (int x = 0; x < Map.GetLength(1); x++)
            {
                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    GameObject NewMapElement;
                    GameObject FloorMapElement;

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
                        // Snake Head
                        case 2:
                            NewMapElement = Instantiate(SnakeHead) as GameObject;

                            FloorMapElement = Instantiate(FloorSprite) as GameObject;
                            FloorMapElement.transform.position = new Vector3(x, -y, 0);
                            FloorMapElement.transform.parent = MapParent.transform;
                            break;
                        // Snake Body
                        case 3:
                            NewMapElement = Instantiate(SnakeBody) as GameObject;

                            FloorMapElement = Instantiate(FloorSprite) as GameObject;
                            FloorMapElement.transform.position = new Vector3(x, -y, 0);
                            FloorMapElement.transform.parent = MapParent.transform;
                            break;
                        // Cherry
                        case 4:
                            NewMapElement = Instantiate(CherrySprite) as GameObject;

                            FloorMapElement = Instantiate(FloorSprite) as GameObject;
                            FloorMapElement.transform.position = new Vector3(x, -y, 0);
                            FloorMapElement.transform.parent = MapParent.transform;
                            break;
                        // Apple
                        case 5:
                            NewMapElement = Instantiate(AppleSprite) as GameObject;

                            FloorMapElement = Instantiate(FloorSprite) as GameObject;
                            FloorMapElement.transform.position = new Vector3(x, -y, 0);
                            FloorMapElement.transform.parent = MapParent.transform;
                            break;
                        default:
                            NewMapElement = Instantiate(FloorSprite) as GameObject;
                            break;
                    }

                    NewMapElement.transform.position = new Vector3(x, -y, -1);
                    NewMapElement.transform.parent = MapParent.transform;

                    if (Map[y, x] == 2 || Map[y, x] == 3)
                    {
                        Map[y, x] = 0;
                    }
                }
            }

            Score.GetComponent<Text>().text = "Score: " + bodyParts.Count;
        }
        else
        {
            //print(gameOverMenu == null);
            if (!menuShown)
            {
                gameOverMenu = Instantiate(gameOverMenu) as GameObject;
                menuShown = true;
            }
        }
    }
}
