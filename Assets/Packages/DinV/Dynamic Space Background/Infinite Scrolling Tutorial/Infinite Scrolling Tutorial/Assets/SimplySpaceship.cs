using UnityEngine;
using System.Collections;
public class SimplySpaceship : MonoBehaviour {

	float speed = 5f;

	void Update () {
		transform.Translate(new Vector3(0,1,0) * Input.GetAxis("Vertical") * speed * Time.deltaTime);
	}
}
