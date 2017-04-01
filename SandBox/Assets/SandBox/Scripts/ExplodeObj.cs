using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeObj : MonoBehaviour {

    public AudioClip explodeAudio;

    private Exploder.ExploderObject exploderObj;
    private Rigidbody mRigidbody;
    private GameObject mAudio;

    private Vector3 originPos;
    private Quaternion originRotation;

    private bool notePostion;
    private bool isExploded;
    private bool revertEvent;
    private Vector3[] framePos;

    private List<Exploder.Fragment> clips;
    private Vector3[] clipsOriginPos;
    private Vector3[] clipsOriginAngle;
    private Fingo.HandType revertHandType;
    private bool canRevertClips;
    private float revertTime;

    // Use this for initialization
    void Awake () {
        notePostion = false;
        isExploded = false;
        revertEvent = false;
        framePos = new Vector3[5];

        originPos = this.transform.position;
        originRotation = this.transform.rotation;

        mRigidbody = this.GetComponent<Rigidbody>();
        exploderObj = GameObject.FindObjectOfType<Exploder.ExploderObject>() as Exploder.ExploderObject;

        revertHandType = Fingo.HandType.Invalid;
        canRevertClips = false;
        revertTime = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        NotePostion();
	}

    void OnHandGraspStart(Fingo.HandType handType)
    {
        // use the first grasp hand
        if(revertHandType == Fingo.HandType.Invalid)
        {
            revertHandType = handType;
        }
    }

    void OnHandGrasping(Fingo.HandType handType, RigidHand myHand)
    {
        //if two hand both grasp, then one hand release, this will use another grasp hand
        if (revertHandType == Fingo.HandType.Invalid)
        {
            revertHandType = handType;
        }

        //exclude another hand's interference
        if (handType == revertHandType && !canRevertClips)
        {
            for (int i = 0; i < clips.Count; i++)
            {
                if (Vector3.Distance(clips[i].transform.position, myHand.transform.position) < 0.2f)
                {
                    canRevertClips = true;
                    this.transform.GetComponent<RigidObject>().GraspStart(myHand);
                }

                if(canRevertClips)
                {
                    for(int j = 0; j < clips.Count; j++)
                    {
                        clipsOriginPos[j] = clips[j].transform.position;
                        clipsOriginAngle[j] = clips[j].transform.eulerAngles;
                        clips[j].rigidBody.isKinematic = true;
                    }
                    return;
                }
            }
        }
        else if(handType == revertHandType && canRevertClips)
        {
            DoRevertClipsAnimation(handType, myHand);
        }
    }

    void OnHandRelease(Fingo.HandType handType)
    {
        StopRevert(handType);
    }

    void StopRevert(Fingo.HandType handType)
    {
        if (revertHandType == handType)
        {
            revertHandType = Fingo.HandType.Invalid;

            if (canRevertClips)
            {
                this.transform.GetComponent<RigidObject>().GraspEnd();
                this.mRigidbody.isKinematic = true;

                for (int j = 0; j < clips.Count; j++)
                {
                    clips[j].rigidBody.isKinematic = false;
                }
                canRevertClips = false;
            }
        }
    }

    void DoRevertClipsAnimation(Fingo.HandType handType, RigidHand myHand)
    {
        //Debug.Log(handType.ToString() + " DoRevertClipsAnimation");
        revertTime += Time.deltaTime * 1.2f;
        for (int i = 0; i < clips.Count; i++)
        {
            Vector3 aminPos = this.transform.TransformPoint(clips[i].explodeMomentlocalPosition);
            Vector3 aminRot = this.transform.TransformVector(clips[i].explodeMomentlocalRotation.eulerAngles);

            if (revertTime > 1.0f)
            {
                clips[i].transform.position = aminPos;
                clips[i].transform.eulerAngles = aminRot;
            }
            else
            {
                clips[i].transform.eulerAngles = Lerp(clipsOriginAngle[i], aminRot, revertTime);
                clips[i].transform.position = Lerp(clipsOriginPos[i], aminPos, revertTime);
            }
        }

        if(revertTime > 1.0f)
            CompleteRevert(myHand);
    }

    void CompleteRevert(RigidHand myHand)
    {
        revertTime = 0.0f;

        FingoGestureEvent.OnFingoGraspStart -= OnHandGraspStart;
        FingoGestureEvent.OnFingoGraspingPalmInfo -= OnHandGrasping;
        FingoGestureEvent.OnFingoRelease -= OnHandRelease;

        canRevertClips = false;
        notePostion = false;
        isExploded = false;
        revertEvent = false;

        GameObject.Destroy(mAudio);

        for (int j = 0; j < clips.Count; j++)
        {
            clips[j].rigidBody.isKinematic = false;
        }
        revertHandType = Fingo.HandType.Invalid;
        Exploder.FragmentPool.Instance.DeactivateFragments();

        //this.mRigidbody.isKinematic = false;
        this.GetComponent<Collider>().enabled = true;

        ShowSelf(this.transform);
        RigidObject rigidObj = this.transform.GetComponent<RigidObject>();

        myHand.GraspGameObjectInHand(rigidObj);
    }

    Vector3 Lerp(Vector3 start, Vector3 end, float t)
    {
        Vector3 temp = end - start;
        return start + temp * t;
    }

    void NotePostion()
    {
        if (mRigidbody.isKinematic || !mRigidbody.useGravity)
        {
            notePostion = false;
            return;
        }

        if (!notePostion)
        {
            notePostion = true;
            for (int i = 0; i < framePos.Length; i++)
            {
                framePos[i] = transform.position;
            }
            return;
        }

        for (int i = framePos.Length - 1; i > 0; i--)
        {
            framePos[i] = framePos[i - 1];
        }
        framePos[0] = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (mRigidbody.isKinematic || !mRigidbody.useGravity || isExploded)
            return;

        //Debug.Log(Vector3.Distance(framePos[0], framePos[framePos.Length - 1]));

        if (Vector3.Distance(framePos[0], framePos[framePos.Length - 1]) > 0.12f)
        {
            Debug.Log("Explode ! " + this.transform.name);

            this.transform.tag = "Exploder";
            exploderObj.transform.position = this.transform.position;
            exploderObj.Explode(ExplodeCallBack);
        }
    }

    void ExplodeCallBack(float timeMS, Exploder.ExploderObject.ExplosionState state)
    {
        if(state == Exploder.ExploderObject.ExplosionState.ExplosionFinished)
        {
            isExploded = true;
            this.mRigidbody.isKinematic = true;
            this.GetComponent<Collider>().enabled = false;
            CreateBreakAudio();

            Invoke("ReadyForRevert", 1.5f);
        }
    }

    void ReadyForRevert()
    {
        revertEvent = true;
        clips = Exploder.FragmentPool.Instance.GetActiveFragments();
        clipsOriginPos = new Vector3[clips.Count];
        clipsOriginAngle = new Vector3[clips.Count];
        FingoGestureEvent.OnFingoGraspStart += OnHandGraspStart;
        FingoGestureEvent.OnFingoGraspingPalmInfo += OnHandGrasping;
        FingoGestureEvent.OnFingoRelease += OnHandRelease;

        this.transform.gameObject.SetActive(true);
    }

    void CreateBreakAudio()
    {
        mAudio = new GameObject();
        mAudio.transform.position = this.transform.position;
        AudioSource audioSource = mAudio.AddComponent<AudioSource>();
        audioSource.clip = explodeAudio;
        audioSource.Play();

        //Invoke("Reset", explodeAudio.length + 3f);
    }

    void Reset()
    {
        notePostion = false;
        isExploded = false;

        GameObject.Destroy(mAudio);
        Exploder.FragmentPool.Instance.DeactivateFragments();

        this.transform.position = originPos;
        this.transform.rotation = originRotation;

		this.mRigidbody.isKinematic = false;
		this.GetComponent<Collider>().enabled = true;

        ShowSelf(this.transform);
    }

    void ShowSelf(Transform obj)
    {
        obj.gameObject.SetActive(true);
        for(int i = 0; i < obj.childCount; i++)
        {
            ShowSelf(obj.GetChild(i));
        }
    }

    public bool ResetToHand(RigidHand rigidHand)
    {
        if (revertEvent)
        {
            CompleteRevert(rigidHand);
            return true;
        }
        else
        {
            return false;
        }
    }
}
