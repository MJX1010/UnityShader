using UnityEngine;
using System.Collections;

public class BellCtrl : MonoBehaviour {

    public AudioClip lingAudio;
    public GameObject bellParticle;
     
	// Use this for initialization
	void Start () {
	
	}
	
    void OnCollisionEnter(Collision other)
    {
		if(other.transform.CompareTag("WindBell") && other.transform.GetComponent<Rigidbody> ().velocity.magnitude > 0.018f)
        {
			//var v = other.transform.GetComponent<Rigidbody> ().velocity;
			//Debug.Log("ding"+other.transform.name+" "+v.x+" "+v.y+" "+v.z+" "+v.magnitude);
            CreateAudio();

            Vector3 pos = Vector3.zero;
            foreach(ContactPoint point in other.contacts)
            {
                pos += point.point;
            }
            pos = pos / other.contacts.Length;
            CreateParticle(pos);
        }
    }

    void CreateAudio()
    {
        GameObject obj = new GameObject();
        AudioSource audioPlayer = obj.AddComponent<AudioSource>();
        audioPlayer.clip = lingAudio;
        audioPlayer.Play();

        StartCoroutine(DestoryGameObject(obj, lingAudio.length));
    }

    IEnumerator DestoryGameObject(GameObject audio, float second)
    {
        yield return new WaitForSeconds(second);

        GameObject.Destroy(audio);
    }

    void CreateParticle(Vector3 position)
    {
        GameObject obj = Instantiate(bellParticle.gameObject, position, Quaternion.Euler(Vector3.zero)) as GameObject;
        ParticleSystem temp = obj.GetComponent<ParticleSystem>();
        temp.Play();

        StartCoroutine(DestoryGameObject(obj, temp.startLifetime));
    }
}
