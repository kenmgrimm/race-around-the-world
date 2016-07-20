using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {
	public Camera defaultCamera;

	public GameObject target;

	private void DetectHit(Ray ray) {
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit))  {
			Debug.Log(" You just hit " + hit.collider.gameObject.name);
			
			WaypointHitHandler hitHandler = target.GetComponent<WaypointHitHandler>();
			hitHandler.Hit();
		}
	}

	private void HandleTouch() {
		if(Input.touchCount > 0) {
			Ray ray = defaultCamera.ScreenPointToRay (Input.GetTouch(0).position);
			
			DetectHit(ray);
		}
		else if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePosition = Input.mousePosition + Vector3.forward;
			Ray ray = defaultCamera.ScreenPointToRay (mousePosition);

			DetectHit(ray);
		}
	}

	void Update () {
		HandleTouch();
	}

}
