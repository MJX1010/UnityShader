using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCharacter : MonoBehaviour {

    public Texture[] runSequenceFrame;
    public Texture jumpFrame;
    public float runSpeed;
    public float jumpTime;
    public float jumpHeight;
    public AnimationCurve anCurve;
    public AudioClip jumpAudio;

    private float timeRate;
    private int textureNum;
    private bool isRun;
    private Coroutine jump;
    private Vector3 originPos;

    private RawImage characterImage;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = this.transform.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = this.gameObject.AddComponent<AudioSource>();

        characterImage = this.transform.GetComponent<RawImage>();

        characterImage.texture = runSequenceFrame[0];
        textureNum = 1;
        timeRate = 0;
        isRun = true;
        originPos = this.transform.localPosition;

    }

    // Update is called once per frame
    void Update () {
        timeRate += Time.deltaTime;

        if (isRun)
            OnRun();

        if(Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }
	}

    void OnEnable()
    {
        if(jump != null)
        {
            jump = null;
            textureNum = 1;
            timeRate = 0;
            isRun = true;
            this.transform.localPosition = originPos;
        }
    }

    void OnRun()
    {
        if(timeRate > runSpeed)
        {
            timeRate = 0;
            characterImage.texture = GetRunFrame();
        }
    }

    Texture GetRunFrame()
    {
        int no = textureNum++;
        if(textureNum >= runSequenceFrame.Length)
        {
            textureNum = 0;
        }

        return runSequenceFrame[no];
    }

    IEnumerator OnJump()
    {
        float countTime = 0.0f;
        characterImage.texture = jumpFrame;

        while (countTime < jumpTime)
        {
            if(countTime <= jumpTime/2)
            {
                this.transform.localPosition = new Vector3(originPos.x, anCurve.Evaluate(countTime * 2.0f / jumpTime) * jumpHeight + originPos.y, originPos.z);
            }
            else
            {
                this.transform.localPosition = new Vector3(originPos.x, originPos.y + anCurve.Evaluate((jumpTime - countTime) * 2.0f / jumpTime) * jumpHeight, originPos.z);
            }

            countTime += Time.deltaTime;
            yield return 0;
        }

        jump = null;
        isRun = true;
        this.transform.localPosition = originPos;
        characterImage.texture = GetRunFrame();
    }

    public void Jump()
    {
        if(jump == null && this.gameObject.activeInHierarchy)
        {
            isRun = false;
            jump = StartCoroutine(OnJump());
            audioSource.clip = jumpAudio;
            audioSource.Play();
        }
    }
}
