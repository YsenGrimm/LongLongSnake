using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{

    enum directions
    {
        up,
        down,
        right,
        left
    }

    Vector2 position;
    Vector3 spawnVector = new Vector3(0, 0, -1f);
    List<GameObject> bodyParts;

    // placeholder value for length, might be replaced by bodyParts.Length
    uint bodyLength = 1;
    directions moveDirection;
    float timerDefault;

    public float speed = 1.0f;
    public float timerInSeconds = 1;
    public GameObject SnakeHead;
    public GameObject SnakeBody;

    // Use this for initialization
    void Start()
    {
        timerDefault = timerInSeconds;
        moveDirection = directions.down;

        // initialize list of bodyparts
        bodyParts = new List<GameObject>();

        // spawn head in center of maps
        SnakeHead = Instantiate(SnakeHead);
        SnakeHead.transform.position = spawnVector;
        bodyParts.Add(SnakeHead);

        // create first bodypart and add it to list
        GameObject bodyPart0 = Instantiate(SnakeBody);
        bodyPart0.transform.position = spawnVector;
        bodyParts.Add(bodyPart0);

        GameObject bodyPart1 = Instantiate(SnakeBody);
        bodyPart1.transform.position = spawnVector;
        bodyParts.Add(bodyPart1);
    }

    // Update is called once per frame
    void Update()
    {
        // capture time
        timerInSeconds -= Time.deltaTime;

        // check, if timespan has passed
        #region Move snake
        if (timerInSeconds <= 0)
        {
            // move snake to new direction
            switch (moveDirection)
            {
                case directions.down:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
                    }

                    // update head
                    bodyParts[0].transform.position += new Vector3(0, -1f);
                    break;

                case directions.up:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
                    }

                    // update head
                    bodyParts[0].transform.position += new Vector3(0, 1f);
                    break;

                case directions.right:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
                    }

                    // update head
                    bodyParts[0].transform.position += new Vector3(1f, 0);
                    break;

                case directions.left:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
                    }

                    // update head
                    bodyParts[0].transform.position += new Vector3(-1f, 0);
                    break;

            }

            timerInSeconds = timerDefault;
        }
        #endregion

        // fetch keyboard input
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                moveDirection = directions.left;
            }
            else
            {
                moveDirection = directions.right;
            }
        }
        else if(Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                moveDirection = directions.down;
            }
            else
            {
                moveDirection = directions.up;
            }
        }

    }
}
