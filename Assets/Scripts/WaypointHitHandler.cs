using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class WaypointHitHandler : MonoBehaviour {	
	public Camera defaultCamera;
	public Camera skyboxCamera;
	public GameObject arCamera;

	private Quaternion initialRotation;

	private ParticleSystem particleSystem;
	private AudioSource audioSource;

	private bool alreadyHit = false;

	public void Start() {
		initialRotation = gameObject.transform.rotation;
	}

	public void Hit() {
		if(alreadyHit) { return; }

		Debug.Log("Hit");

		iTween.RotateBy(gameObject, iTween.Hash("x", 1f, "y", 2f, "z", 3f, "time", 2f, "easeType", 
			"easeInOutBack", "transition", "easeInOutSine", "loopType", "none", "delay", 0f,
			"oncomplete", "TransitionToNewScene", "oncompletetarget", gameObject));

		audioSource = GetComponent<AudioSource>();
		Debug.Log(audioSource);
		audioSource.Play();
		
		particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
		Debug.Log(particleSystem);
		particleSystem.Play();

		alreadyHit = true;
	}

	private void TransitionToNewScene() {
		ToggleView();

		StopTransitionEffects();
	}

	private void StopTransitionEffects() {
		iTween.RotateTo(gameObject, iTween.Hash("rotation", initialRotation.eulerAngles, "time", 2f, "easeType", 
			"easeInOutBack", "transition", "easeInOutSine", "loopType", "none"));
		particleSystem.Stop();

		alreadyHit = false;
	}

	private void TransitionBackToInitial() {
		ToggleView();
	}

	private void ToggleView() {
		arCamera.SetActive(!arCamera.activeSelf);
		defaultCamera.gameObject.SetActive(!defaultCamera.gameObject.activeSelf);
		skyboxCamera.GetComponent<Camera>().enabled = !skyboxCamera.GetComponent<Camera>().enabled;
	}
}

