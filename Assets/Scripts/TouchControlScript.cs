using UnityEngine;
using System.Collections;

public class TouchControlScript : MonoBehaviour
{
    public int direction;

    enum directions
    {
        up,
        down,
        right,
        left
    }

    public void OnMouseDown()
    {
        switch(direction)
        {
            case 0:     // up
                break;
            case 1:     // down
                break;
            case 2:     // right
                print("Touched right");
                FindObjectOfType<SnakeController>().Move(direction);
                break;
            case 3:     // left
                print("Touched left");
                FindObjectOfType<SnakeController>().Move(direction);
                break;
        }
    }

}
