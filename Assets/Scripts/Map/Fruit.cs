using UnityEngine;

public class Fruit {

	public Vector2 fruitPos;
	public MapRenderer.ElementType type;

	float livetime;
	float decaySpeed;

	bool alive;

	public Fruit(MapRenderer.ElementType type, Vector2 pos, float decay) {
		fruitPos = pos;
		this.type = type;
		alive = true;

		decaySpeed = decay;

		switch (type) {
			case MapRenderer.ElementType.Cherry:
				livetime = 10;
				break;
			case MapRenderer.ElementType.Apple:
				livetime = 20;
				break;
			default:
				livetime = 10;
				break;
		}
	}

	public void UpdateTimers() {
		if (livetime < 0) {
			alive = false;
		}
		if (alive) {
			livetime -= decaySpeed;
		}
	}

	public bool Alive() {
		return alive;
	}
}
