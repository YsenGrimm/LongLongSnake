using UnityEngine;
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

    public Vector3 SpawnPosition;
    public float Speed;
	public float StartSpeed;
	public float MaxSpeed;

	[System.NonSerialized]
	public List<SnakeElement> bodyParts;

	float timerInSeconds = 1f;

	SnakeDirections moveDirection;
	float timerDefault;
	SnakeElement SnakeHead;

    // Use this for initialization
    void Start()
    {
        timerDefault = timerInSeconds;
        moveDirection = SnakeDirections.Down;

        // initialize list of bodyparts
        bodyParts = new List<SnakeElement>();

        // spawn head in center of maps
        SnakeHead = new SnakeElement(SpawnPosition);
        bodyParts.Add(SnakeHead);

        // create first bodypart and add it to list
        SnakeElement bodyPart0 = new SnakeElement(new Vector3(SpawnPosition.x, SpawnPosition.y - 1, SpawnPosition.z));
        bodyParts.Add(bodyPart0);

        SnakeElement bodyPart1 = new SnakeElement(new Vector3(SpawnPosition.x, SpawnPosition.y - 1, SpawnPosition.z));
        bodyParts.Add(bodyPart1);
    }
		
    void Update()
    {
        // capture time
        timerInSeconds -= Time.deltaTime * Speed;

        // check, if timespan has passed
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

			Speed = Mathf.Lerp(StartSpeed, MaxSpeed, bodyParts.Count / 200.0f);

        }

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

	public List<SnakeElement> getBodyParts()
	{
		return bodyParts;
	}

	public SnakeDirections getMoveDirection()
	{
		return moveDirection;
	}
}
