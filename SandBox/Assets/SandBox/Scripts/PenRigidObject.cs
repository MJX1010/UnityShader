using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PenRigidObject : RigidObject {

	public GameObject trailRenderPrefab;
	public float invalidTipDistance = 0.06f;//
	private RigidHand rigHand;
	private List<GameObject> trailList;

	public TrailRenderer curTrailRender;
	bool isPinched;
	int pinchState;
	void Start()
	{
		trailList = new List<GameObject> ();
	}

	public override void GraspStart (RigidHand myHand)
	{
		base.GraspStart (myHand);
		Clean ();
		rigHand = myHand;
		AddLine ();
		pinchState = 1;
	}


	public override void Parabolic ()
	{
		base.Parabolic ();
		Clean ();
		rigHand = null;
		curTrailRender = null;
		pinchState = 0;
	}

	public override void GraspEnd ()
	{
		base.GraspEnd ();
		Clean ();
		rigHand = null;
		curTrailRender = null;
		pinchState = 0;
	}


	void Update()
	{
		if (rigHand == null)
			return;
		DoCheckPinch ();

		if (isPinched) {
			if (pinchState == 0) {
				AddLine ();
				pinchState = 1;
			} else if (pinchState == 1) {
				
			}
		} else {
			if (pinchState == 1) {
				Clean ();
				pinchState = 0;
			}
		}


	}


	void AddLine()
	{
		if (curTrailRender != null)
			return;
		
		GameObject go = GameObject.Instantiate (trailRenderPrefab, trailRenderPrefab.transform.position, trailRenderPrefab.transform.rotation) as GameObject;
		go.transform.parent = this.transform;
		curTrailRender = go.GetComponent<TrailRenderer> ();
		curTrailRender.enabled = true;
		trailList.Add (go);

	}




	void Clean()
	{
		for (int i = trailList.Count - 1; i >= 0;i--) 
		{
			var g = trailList[i];
			if (g != null) {
				g.transform.parent = null;
				Destroy (g, g.GetComponent<TrailRenderer>().time * 2f);
				trailList.Remove (g);
			}
		}
		curTrailRender = null;
	}


	void DoCheckPinch()
	{
	    Vector3 thumbPro_Pos = rigHand.meshHand.indexProximal.position;//GetJointPosition (Fingo.JointIndex.INDEX_PROXIMAL);
	    Vector3 indexTip_Pos = rigHand.meshHand.indexTip.position;//GetFingerTipPosition (Fingo.TipIndex.INDEX_TIP);

		float fingers_dis = Vector3.Distance (thumbPro_Pos, indexTip_Pos);
		isPinched = (fingers_dis < invalidTipDistance);

	}


}
