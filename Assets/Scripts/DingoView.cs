﻿using UnityEngine;
using System.Collections;
using thelab.mvc;

public class DingoView : View<DingoApplication> {

	public float svrContentsHiddenY = -7f;
	public float svrContentsTopY = 1f;
	public float svrContentsLiftRate = 1.5f;
	public float sphereLowY = 0.9f;
	public float sphereHighY = 1.74f;
	public float cloudY = 40f;
	public float cloudWaitForDeath = 5f;
	public GameObject svrContentsPrefab;
	public GameObject svrPrefab;
	public GameObject cloudPrefab;

	public AvailabilityZonesView AvailabilityZones { get { return p_azs = base.Assert<AvailabilityZonesView> (p_azs); } }
	AvailabilityZonesView p_azs;

	public TileSlotView FindTileSlot(ServiceInstanceModel serviceInstance, string role)
	{
		TileSlotView[] slots = transform.GetComponentsInChildren<TileSlotView> ();
		for (int i = 0; i < slots.Length; i++) {
			if (slots [i].allocatedServiceInstance != null && 
				slots [i].allocatedServiceInstance.Equals(serviceInstance) &&
				slots [i].isRole(role)) {
				return slots [i];
			}
		}
		return null;
	}

	public GameObject ShowCloudOverLeader (TileSlotView tileSlot)
	{
		return tileSlot.ShowCloud ();
	}
}
