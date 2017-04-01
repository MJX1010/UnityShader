using UnityEngine;
using System.Collections;
using Fingo;

public class RigidHand : MonoBehaviour
{
    public MeshHand meshHand;
    public SkinnedMeshRenderer handRenderer;
    public Transform handRoot;
    public int graspDelayFrame = 6;//delay for confirm grsp action

    private int graspFingersNum;
    private bool isGraspObj;
    private bool isPostGraspEvent;
    private Transform interactionThing;
    private RigidObject graspObj;

    private int GraspFrame;

    void Start()
    {
        isGraspObj = false;
        isPostGraspEvent = false;
    }

	void UpdateHideOrShowHand()
	{
		if (isGraspObj) {
			handRenderer.enabled = meshHand.isDetected;
            if (meshHand.isDetected == false) {
				if(graspObj.transform.parent == this.transform)
					graspObj.gameObject.SetActive (false);

			}
			else {
				if (graspObj.isHideInGrasp) {
					handRenderer.enabled = false;
				}
				graspObj.gameObject.SetActive (true);

			}
		} else {
            handRenderer.enabled = meshHand.isDetected;

		}
	}


    void Update()
    {
        //show or hide hand by hand's detect
		UpdateHideOrShowHand();


			
		if (graspObj != null && graspObj.GetComponent<MusicBoxCtrl> () != null) {
            if (!meshHand.isDetected) {
				isGraspObj = false;
				handRenderer.enabled = true;
				graspObj.gameObject.SetActive (true);
				graspObj.HandRelease ();
				graspObj = null;
				GraspFrame = 0;
			}
		}

        if (!meshHand.isDetected) {
			if (isPostGraspEvent) {
				isPostGraspEvent = false;
				if (FingoGestureEvent.OnFingoRelease != null)
                    FingoGestureEvent.OnFingoRelease(meshHand.handType);
			}
		}

        if (graspFingersNum < 1 && meshHand.isDetected)
        {
			//Where's my hand?
			if (Vector3.Distance (this.transform.position, Camera.main.transform.position) > 2.0f)
				return;
				//post release event
			if (isPostGraspEvent) {
				isPostGraspEvent = false;
				if (FingoGestureEvent.OnFingoRelease != null)
                    FingoGestureEvent.OnFingoRelease(meshHand.handType);
			}

            //grasp release
            if (isGraspObj)
            {
                GraspFrame++;
                if (GraspFrame % graspDelayFrame == 0)
                {
                    isGraspObj = false;
                    handRenderer.enabled = true;
                    graspObj.gameObject.SetActive(true);
                    graspObj.HandRelease();
                    graspObj = null;
                    GraspFrame = 0;
                }
            }

            return;
        }

        if (graspFingersNum > 2 && meshHand.isDetected)
        {
            //post grasp start event
            if (!isPostGraspEvent)
            {
                isPostGraspEvent = true;
                if (FingoGestureEvent.OnFingoGraspStart != null)
                    FingoGestureEvent.OnFingoGraspStart(meshHand.handType);
            }

            if (!isGraspObj && interactionThing != null)
            {
                GraspFrame++;

                if (GraspFrame % graspDelayFrame == 0)
                {
                    //if obj is in another hand, this hand will be ignored
                    graspObj = interactionThing.GetComponent<RigidObject>();
                    if (!graspObj.isGrasped)
                    {
                        //grasp start
                        isGraspObj = true;
                        graspObj.gameObject.SetActive(true);
                        graspObj.HandGrasp(this);
                        GraspFrame = 0;
                    }
                }
            }
        }

        if (isPostGraspEvent)
        {
            if (FingoGestureEvent.OnFingoGraspingPalmInfo != null)
            {
                FingoGestureEvent.OnFingoGraspingPalmInfo(meshHand.handType, this);
            }
        }
    }

    void OnTriggerEnter(Collider e)
    {
        if (e.transform.tag == "Finger" + "_" + meshHand.handType.ToString())
        {
            graspFingersNum++;
        }
        if (e.transform.GetComponent<RigidObject>() != null)
        {
            interactionThing = e.transform;
            if (graspFingersNum > 2)
                GraspFrame = 0;
        }
    }

    void OnTriggerExit(Collider e)
    {
        if (e.transform.tag == "Finger" + "_" + meshHand.handType.ToString())
        {
            graspFingersNum--;
        }
        if (e.transform == interactionThing)
        {
            interactionThing = null;
            GraspFrame = 0;
        }
    }

    public void GraspGameObjectInHand(RigidObject obj)
    {
        if (obj.isGrasped || meshHand.handType == Fingo.HandType.Left)
            return;

        if (graspObj != obj && graspObj != null)
        {
            graspObj.gameObject.SetActive(true);
            graspObj.HandRelease();
        }

        graspObj = obj;
        isGraspObj = true;
        if (graspObj.leftHoldPoistion == null || graspObj.rightHoldPoistion == null)
            graspObj.transform.position = this.transform.position;
        graspObj.gameObject.SetActive(true);
        graspObj.HandGrasp(this);
    }

	public void ReleaseGameObject()
	{
		if (isGraspObj) {
			isGraspObj = false;
			handRenderer.enabled = true;
			graspObj.gameObject.SetActive (true);
			graspObj.HandRelease ();
			graspObj = null;
			GraspFrame = 0;
		}
	}

    public Transform GetGraspObj()
    {
        if (graspObj)
            return graspObj.transform;
        else
            return null;
    }
}
