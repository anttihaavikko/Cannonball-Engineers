using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectCamera : MonoBehaviour {

	private float cutoff = 1f, targetCutoff = 1f;
	private float prevCutoff = 1f;
	private float cutoffPos = 0f;
	private float transitionTime = 0.5f;
    public Cinemachine.CinemachineImpulseSource impulseSource;

    private PostProcessVolume ppVolume;
	private float chromaAmount = 0f;
	private float chromaSpeed = 1f;

	private float shakeAmount = 0f, shakeTime = 0f;

	private Vector3 originalPos;

    private ChromaticAberration ca;

    public Cinemachine.CinemachineBrain brain;

	void Start() {
        ppVolume = GetComponent<PostProcessVolume>();
        originalPos = transform.position;
        ppVolume.profile.TryGetSettings(out ca);
    }

	void Update() {
        // chromatic aberration update
        if (ppVolume)
        {
            chromaAmount = Mathf.MoveTowards(chromaAmount, 0, Time.deltaTime * chromaSpeed);
            ca.intensity.value = chromaAmount;
        }

        Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, Time.unscaledDeltaTime);

  //      if (shakeTime > 0f) {
		//	shakeTime -= Time.deltaTime;
		//	transform.position = transform.position + new Vector3 (Random.Range (-shakeAmount, shakeAmount), Random.Range (-shakeAmount, shakeAmount), 0);
  //          brain.enabled = false;
  //      } else {
		//	transform.position = originalPos;
  //          brain.enabled = true;
		//}
	}

	public void Chromate(float amount, float speed) {
		chromaAmount = amount;
		chromaSpeed = speed;
	}

	public void Shake(float amount, float time) {
        shakeAmount = amount;
		shakeTime = time;
	}

	public void BaseEffect(float mod = 1f) {
        impulseSource.GenerateImpulse(Vector3.one * mod * 40f);
        //Shake(0.04f * mod, 0.075f * mod);
        Chromate (1.5f * mod, 2f * mod);

        Time.timeScale = Mathf.Clamp(1f - 0.3f * mod, 0f, 1f);
    }
}
