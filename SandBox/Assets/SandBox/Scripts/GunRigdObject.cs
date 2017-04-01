using UnityEngine;
using System.Collections;

public class GunRigdObject : RigidObject {

	public GameObject Bullet;

	public override void GraspStart (RigidHand myHand)
	{
		base.GraspStart (myHand);
		Bullet.SetActive (true);
	}

	public override void Parabolic ()
	{
		base.Parabolic ();
		Bullet.SetActive (false);
	}

	public override void GraspEnd ()
	{
		base.GraspEnd ();
		Bullet.SetActive (false);
	}
			 
}
