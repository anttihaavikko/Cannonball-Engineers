using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SplatCamera : MonoBehaviour {

	private Camera cam;
	private int _downResFactor = 0;
	private string _globalTextureName = "_GlobalSplatTex";
	private Vector2 lastSize;

	void GenerateRT()
	{
		lastSize = new Vector2(Screen.width, Screen.height);

		cam = GetComponent<Camera>();

		if (cam.targetTexture != null)
		{
			RenderTexture temp = cam.targetTexture;

			cam.targetTexture = null;
			DestroyImmediate(temp);
		}

		cam.targetTexture = new RenderTexture(cam.pixelWidth >> _downResFactor, cam.pixelHeight >> _downResFactor, 16);
		cam.targetTexture.filterMode = FilterMode.Bilinear;

		Shader.SetGlobalTexture(_globalTextureName, cam.targetTexture);
	}

	void OnEnable() {
		GenerateRT();
	}

	void Update() {
		Vector2 cur = new Vector2 (Screen.width, Screen.height);

		if (cur.x != lastSize.x && cur.y != lastSize.y) {
			GenerateRT();
		}
	}
}
