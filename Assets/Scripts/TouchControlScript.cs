using UnityEngine;
using System.Collections;

public class TouchControlScript : MonoBehaviour
{
    public int direction;
    public Canvas touchCanvas;
    
    public void Start()
    {
        // check for touch device, not working as intended
        //if (Input.touchSupported)
        //{
        //    print("touchscreen supported");
        //    touchCanvas.enabled = true;
        //}
        //else
        //{
        //    print("touchscreen not detected");
        //    touchCanvas.enabled = false;
        //}
    }

    public void OnTouched()
    {
        switch (direction)
        {
            case 0:     // up
                print("Touched up");
                FindObjectOfType<SnakeController>().Move(direction);
                break;
            case 1:     // down
                print("Touched down");
                FindObjectOfType<SnakeController>().Move(direction);
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
