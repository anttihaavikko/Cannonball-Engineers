using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectCamera : MonoBehaviour {

    private static readonly float defaultLensDistortion = 30f;

    private float cutoff = 1f, targetCutoff = 1f;
	private float prevCutoff = 1f;
	private float cutoffPos;
	private float transitionTime = 0.5f;
    public Cinemachine.CinemachineImpulseSource impulseSource;

    private PostProcessVolume ppVolume;
	private float chromaAmount, splitAmount;
    private float bulgeAmount = defaultLensDistortion;
    private float bulgeSpeed;
	private float chromaSpeed = 1f;
    private float splitSpeed = 1f;

    private float shakeAmount = 0f, shakeTime = 0f;

	private Vector3 originalPos;

    private ChromaticAberration ca;
    private LensDistortion ld;
    private ColorSplit cs;

    public Cinemachine.CinemachineBrain brain;

	void Start() {
        ppVolume = GetComponent<PostProcessVolume>();
        originalPos = transform.position;
        ppVolume.profile.TryGetSettings(out ca);
        ppVolume.profile.TryGetSettings(out ld);
        ppVolume.profile.TryGetSettings(out cs);
    }

	void Update() {
        // chromatic aberration update
        if (ppVolume)
        {
            chromaAmount = Mathf.MoveTowards(chromaAmount, 0, Time.deltaTime * chromaSpeed);
            ca.intensity.value = chromaAmount;

            bulgeAmount = Mathf.MoveTowards(bulgeAmount, defaultLensDistortion, Time.deltaTime * bulgeSpeed);
            ld.intensity.value = bulgeAmount;

            splitAmount = Mathf.MoveTowards(splitAmount, 0, Time.deltaTime * splitSpeed);
            cs.amount.value = splitAmount;
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

        splitAmount = amount * 0.003f;
        splitSpeed = speed * 0.003f;
    }

	public void Shake(float amount, float time) {
        shakeAmount = amount;
		shakeTime = time;
	}

    public void Bulge(float amount, float speed)
    {
        bulgeAmount = amount;
        bulgeSpeed = speed;
    }

	public void BaseEffect(float mod = 1f) {
        impulseSource.GenerateImpulse(Vector3.one * mod * 40f);
        //Shake(0.04f * mod, 0.075f * mod);
        Chromate (1.5f * mod, 2f * mod);
        Bulge(defaultLensDistortion * 1.1f * mod, 50f * mod);

        //Time.timeScale = Mathf.Clamp(1f - 0.2f * mod, 0f, 1f);
    }
}
