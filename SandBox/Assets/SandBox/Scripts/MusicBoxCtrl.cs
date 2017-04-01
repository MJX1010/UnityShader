using UnityEngine;
using System.Collections;

public class MusicBoxCtrl : RigidObject
{
    public Transform center;
    public AudioSource audioPlayer;

    private bool isRaiseMusic;
    private RigidHand mHand;

    private Vector3 prePos;
    private Vector3 nowPos;

    private float rotationAmount;
    private float perTimeRotation;
    private float countHandlerRotation;

    // Use this for initialization
    void Start()
    {
        audioPlayer.Stop();
        countHandlerRotation = 0;

        rotationAmount = 360f * 2f;
        isRaiseMusic = false;
        perTimeRotation = rotationAmount / audioPlayer.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRaiseMusic)
        {
            RaiseMusic();
        }
        else
        {
            if(audioPlayer.isPlaying)
            {
                countHandlerRotation = (audioPlayer.clip.length - audioPlayer.time) * perTimeRotation;
                center.localEulerAngles = new Vector3(0.0f, 0.0f, countHandlerRotation);
            }
        }
    }

    void RaiseMusic()
    {
        prePos = nowPos;
        nowPos = mHand.transform.position;

        float angle = Vector3.Angle(nowPos - center.position, prePos - center.position);
        float dir_index = Vector3.Dot(Vector3.Cross(nowPos - center.position, prePos - center.position), center.transform.forward);
        if (dir_index > 0)
        {
            angle = -angle;
        }

        //check if can rotate when wave handler come to end
        countHandlerRotation += angle;
        if (countHandlerRotation < 0.0f)
        {
            countHandlerRotation = 0.0f;
        }
        else if(countHandlerRotation > rotationAmount)
        {
            countHandlerRotation = rotationAmount;
        }

        center.localEulerAngles = new Vector3(0, 0, countHandlerRotation);
    }

    public override void GraspStart(RigidHand myHand)
    {
        isRaiseMusic = true;
        this.mHand = myHand;
        nowPos = myHand.transform.position;
        audioPlayer.Stop();
    }

    public override void GraspEnd()
    {
        isRaiseMusic = false;
        this.mHand = null;

        audioPlayer.Play();
        float temp = audioPlayer.clip.length - countHandlerRotation / perTimeRotation;
        if(temp > audioPlayer.clip.length)
        {
            audioPlayer.Stop();
        }
        else if(temp < 0.0f)
        {
            temp = 0.0f;
        }

        audioPlayer.time = temp;
        Debug.Log(audioPlayer.clip.length + " musicBox  " + temp);
    }
}