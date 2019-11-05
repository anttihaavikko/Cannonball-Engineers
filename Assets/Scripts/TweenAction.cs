﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAction {
	
	public enum Type {
		Position,
		LocalPosition,
		Rotation,
		Scale,
		Color,
        BodyPosition
	};

	public Transform theObject;
    public Rigidbody2D body;
	public SpriteRenderer sprite;
	public Vector3 startPos, targetPos;
	public Quaternion startRot, targetRot;
	public Color startColor, targetColor;
	public float tweenPos, tweenDuration, tweenDelay;
	public int customEasing;
	public Type type;
	public System.Func<float, float> easeFunction;

    private bool hasBeenInit;

	public Vector3 Lerp(Vector3 start, Vector3 end, float time) {
		if (customEasing >= 0) {
			return Vector3.LerpUnclamped (start, end, time);
		} else {
			return Vector3.Lerp (start, end, time);
		}
	}

	public Quaternion Lerp(Quaternion start, Quaternion end, float time) {
		if (customEasing >= 0) {
			return Quaternion.LerpUnclamped (start, end, time);
		} else {
			return Quaternion.Lerp (start, end, time);
		}
	}

	public Color Lerp(Color start, Color end, float time) {
		if (customEasing >= 0) {
			return Color.LerpUnclamped (start, end, time);
		} else {
			return Color.Lerp (start, end, time);
		}
	}

	public float DoEase() {
		if (customEasing >= 0) {
			return Tweener.Instance.customEasings[customEasing].Evaluate (tweenPos);
		} else {
			return easeFunction (tweenPos);
		}
	}

    public IEnumerator SetStartPos()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startPos = theObject.transform.position;
    }

    public IEnumerator SetBodyStartPos()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startPos = body.position;
    }

    public IEnumerator SetStartLocalPos()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startPos = theObject.transform.localPosition;
    }

    public IEnumerator SetStartRot()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startRot = theObject.transform.rotation;
    }

    public IEnumerator SetStartScale()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startPos = theObject.transform.localScale;
    }

    public IEnumerator SetStartColor()
    {
        yield return new WaitForSeconds(tweenDelay);
        hasBeenInit = true;
        startColor = sprite.color;
    }

    public bool Process() {

		if (!theObject && !body) {
			return true;
		}

        if (!hasBeenInit)
            return false;

		if (tweenDelay > 0f) {

			tweenDelay -= Time.deltaTime;

		} else {
			tweenPos += Time.deltaTime / tweenDuration;

			if (type == Type.Position) {
				theObject.position = Lerp (startPos, targetPos, DoEase ());
			}

            if (type == Type.BodyPosition)
            {
                body.MovePosition(Lerp(startPos, targetPos, DoEase()));
            }

            if (type == Type.LocalPosition) {
				theObject.localPosition = Lerp (startPos, targetPos, DoEase ());
			}

			if (type == Type.Rotation) {
				theObject.rotation = Lerp (startRot, targetRot, DoEase ());
			}

			if (type == Type.Scale) {
				theObject.localScale = Lerp (startPos, targetPos, DoEase ());
			}

			if (type == Type.Color) {
				sprite.color = Lerp (startColor, targetColor, DoEase ());
			}
		}

		return (tweenPos >= 1f);
	}
}
