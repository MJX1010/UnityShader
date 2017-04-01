using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

    public float scrollSpeed = 10f;
    public Transform scrollImage1;
    public Transform scrollImage2;
    public float length;
    public AudioClip bgmClip;

    private AudioSource audioSource;

    // Use this for initialization
    void Awake () {

        audioSource = this.transform.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = this.gameObject.AddComponent<AudioSource>();

        Init();
    }

    void Init()
    {
        scrollImage2.gameObject.SetActive(true);
        scrollImage1.gameObject.SetActive(true);

        scrollImage1.localPosition = Vector3.zero;
        scrollImage2.localPosition = new Vector3(length, 0, 0);

        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.volume = 0.1f;
    }

    void OnEnable()
    {
        Init();
    }

    void OnDisable()
    {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update () {

        scrollImage1.localPosition = new Vector3(scrollImage1.localPosition.x - scrollSpeed, 0, 0);
        scrollImage2.localPosition = new Vector3(scrollImage2.localPosition.x - scrollSpeed, 0, 0);

        if(scrollImage1.localPosition.x < -length)
        {
            scrollImage1.localPosition = scrollImage2.localPosition + new Vector3(length, 0, 0);
        }
        else if(scrollImage2.localPosition.x < -length)
        {
            scrollImage2.localPosition = scrollImage1.localPosition + new Vector3(length, 0, 0);
        }
    }
}
