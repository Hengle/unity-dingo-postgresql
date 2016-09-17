﻿using UnityEngine;
using System.Collections;
using thelab.mvc;

public class DatabaseSphereView : View<DingoApplication> {
	public bool visible;
	public GameObject outboundTarget;

	float lowY { get { return app != null ? app.view.sphereLowY : -1f; } }
	float highY { get { return app != null ? app.view.sphereHighY : 7f; } }

	LineRenderer lineWaveRenderer;
	LineWave lineWave;

	ArrayList incomingParentObjects;

	void Awake()
	{
		lineWave = GetComponentInChildren<LineWave> ();
		if (lineWave != null) {
			lineWaveRenderer = lineWave.gameObject.GetComponent<LineRenderer> ();
		}
		incomingParentObjects = new ArrayList ();
	}

	void Update ()
	{
		if (visible) {
			if (transform.localPosition.y < highY) {
				transform.Translate (Vector3.up * Time.deltaTime);
			} else {
				if (lineWaveRenderer != null && outboundTarget != null) {
					lineWaveRenderer.enabled = true;
					lineWave.targetOptional = outboundTarget;
				}
			}
		} else {
			if (transform.localPosition.y > lowY) {
				transform.Translate (-Vector3.up * Time.deltaTime);
			}
			if (lineWaveRenderer != null && outboundTarget != null) {
				lineWaveRenderer.enabled = false;
				lineWave.targetOptional = null;
			}
		}
	}

	// Destroy inbound data flow (backups) objects
	void OnTriggerEnter(Collider collider) {
		DataFlowMover parent = collider.GetComponentInParent<DataFlowMover> ();
		if (parent != null) {
			if (incomingParentObjects.Contains (parent.gameObject)) {
				Destroy (parent.gameObject);
				incomingParentObjects.Remove (parent.gameObject);
			}
		} else {
			if (incomingParentObjects.Contains (collider.gameObject)) {
				Destroy (collider.gameObject);
				incomingParentObjects.Remove (collider.gameObject);
			}
		}
	}

	// Do not self destruct until all incoming objects have been received/destroy,
	// and waited a pause period.
	public void IncomingObject(GameObject parentObj) {
		incomingParentObjects.Add (parentObj);
	}
}
