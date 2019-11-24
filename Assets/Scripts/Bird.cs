using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	public float speed;

	private float min = -150f;
	private float max = 150f;

	// Use this for initialization
	void Awake () {

		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();

		float r = Random.value * 4f;

		float depth = r * 50f + 5;

		float xdir = (Random.value < 0.5f) ? 1f : -1f;
		float ydir = (Random.value < 0.5f) ? 1f : -1f;

		transform.localPosition = new Vector3 (Random.Range(min, max), transform.localPosition.y + Random.Range(-5, 5), 0);
        //transform.localScale = new Vector3 (xdir * (1f + r), ydir * (1f + r), 1f);

        float mod = Random.Range(0.7f, 1.3f);
        transform.localScale *= mod;
        speed *= mod;

		//sprite.color = new Color (1, 1, 1, 0.2f + Random.value / 3f);
	}

	void Update() {
		transform.Translate(Vector3.right * Time.deltaTime * speed);

		if (transform.localPosition.x > max)
        {
			transform.localPosition = new Vector3 (min, transform.localPosition.y, transform.localPosition.z);
		}

        if (transform.localPosition.x < min)
        {
            transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
