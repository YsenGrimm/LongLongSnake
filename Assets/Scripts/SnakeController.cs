using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{

    public enum SnakeDirections
    {
        Up,
        Down,
        Right,
        Left
    }

    Vector2 position;
    public List<SnakeElement> bodyParts;

    SnakeDirections moveDirection;
    float timerDefault;
    SnakeElement SnakeHead;

    // diffrent speedlevels
    float firstBoost;
    float secondBoost;
    float thirdBoost;
    float fourthBoost;
    float fifthBoost;
    float ultraBoost;

    public Vector3 spawnVector;
    public float speed;
    public float timerInSeconds = 1f;
    public float boostInterval = 50;

    public List<SnakeElement> getBodyParts()
    {
        return bodyParts;
    }

    public SnakeDirections getMoveDirection()
    {
        return moveDirection;
    }

    // Use this for initialization
    void Start()
    {
        timerDefault = timerInSeconds;
        moveDirection = SnakeDirections.Down;

        // initialize list of bodyparts
        bodyParts = new List<SnakeElement>();

        // spawn head in center of maps
        SnakeHead = new SnakeElement(spawnVector);
        bodyParts.Add(SnakeHead);

        // create first bodypart and add it to list
        SnakeElement bodyPart0 = new SnakeElement(new Vector3(spawnVector.x, spawnVector.y - 1, spawnVector.z));
        bodyParts.Add(bodyPart0);

        SnakeElement bodyPart1 = new SnakeElement(new Vector3(spawnVector.x, spawnVector.y - 1, spawnVector.z));

        bodyParts.Add(bodyPart1);

        // assign boostlevels, so they aren't exponentially increased
        firstBoost = speed * 2;
        secondBoost = speed * 3;
        thirdBoost = speed * 4;
        fourthBoost = speed * 5;
        fifthBoost = speed * 6;
        ultraBoost = speed * 7;
    }


    // Update is called once per frame
    void Update()
    {
        // capture time
        timerInSeconds -= Time.deltaTime * speed;

        // check, if timespan has passed
        #region Move snake
        if (timerInSeconds <= 0)
        {
            // move snake to new direction
            switch (moveDirection)
            {
                case SnakeDirections.Down:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].MapPosition = bodyParts[i - 1].MapPosition;
                    }

                    // update head
                    bodyParts[0].MapPosition += new Vector3(0, 1f);
                    break;

                case SnakeDirections.Up:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].MapPosition = bodyParts[i - 1].MapPosition;
                    }

                    // update head
                    bodyParts[0].MapPosition += new Vector3(0, -1f);
                    break;

                case SnakeDirections.Right:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].MapPosition = bodyParts[i - 1].MapPosition;
                    }

                    // update head
                    bodyParts[0].MapPosition += new Vector3(1f, 0);
                    break;

                case SnakeDirections.Left:
                    for (int i = bodyParts.Count - 1; i > 0; i--)
                    {
                        // move to new position
                        bodyParts[i].MapPosition = bodyParts[i - 1].MapPosition;
                    }

                    // update head
                    bodyParts[0].MapPosition += new Vector3(-1f, 0);
                    break;

            }

            timerInSeconds = timerDefault;

            // check for body length and adjust speed
            if (bodyParts.Count >= boostInterval * 6)
            {
                speed = ultraBoost;
            }
            else if (bodyParts.Count >= boostInterval * 5)
            {
                speed = fifthBoost;
            }
            else if (bodyParts.Count >= boostInterval * 4)
            {
                speed = fourthBoost;
            }
            else if (bodyParts.Count >= boostInterval * 3)
            {
                speed = thirdBoost;
            }
            else if (bodyParts.Count >= boostInterval * 2)
            {
                speed = secondBoost;
            }
            else if (bodyParts.Count >= boostInterval)
            {
                speed = firstBoost;
            }

        }
        #endregion

        // fetch keyboard input
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                moveDirection = SnakeDirections.Left;
            }
            else
            {
                moveDirection = SnakeDirections.Right;
            }
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                moveDirection = SnakeDirections.Down;
            }
            else
            {
                moveDirection = SnakeDirections.Up;
            }
        }

    }

    public void Move(int toDirection)
    {
        switch (toDirection)
        {
            case 0:     // up
                moveDirection = SnakeDirections.Up;
                break;
            case 1:     // down
                moveDirection = SnakeDirections.Down;
                break;
            case 2:     // right
                moveDirection = SnakeDirections.Right;
                break;
            case 3:     // left
                moveDirection = SnakeDirections.Left;
                break;
        }
    }
}
