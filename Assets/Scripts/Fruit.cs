using UnityEngine;
using System.Collections;

public class Fruit {

	public enum FruitType {
		Cherry,
		Apple,

		length
	}

	public Vector2 fruitPos;
	FruitType type;

	float livetime;
	float decaySpeed;

	bool alive;

	public int FruitTypeInt;

	public Fruit(FruitType type, Vector2 pos, float decay) {
		this.fruitPos = pos;
		this.type = type;
		this.alive = true;

		this.decaySpeed = decay;

		switch (this.type) {
		case Fruit.FruitType.Cherry:
			this.livetime = 10;
			this.FruitTypeInt = 4;
			break;
		case Fruit.FruitType.Apple:
			this.livetime = 20;
			this.FruitTypeInt = 5;
			break;
		default:
			this.livetime = 10;
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
		return this.alive;
	}
}
