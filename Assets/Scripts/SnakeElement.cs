using UnityEngine;
using System.Collections;

public class SnakeElement {

	public Vector3 MapPosition { get; set; } 
	
	public SnakeElement(Vector3 mapPosition) {
		this.MapPosition = mapPosition;
	}

}
