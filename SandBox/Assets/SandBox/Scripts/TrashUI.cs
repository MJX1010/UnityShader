using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrashUI : MonoBehaviour {

    public Text shootNum;
    public Text shootText;

    private bool isShow;
    private int num;


	// Use this for initialization
	void Start () {
        isShow = false;
        num = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void IncreaseShootNum()
    {
        num++;
        shootNum.text = num.ToString();
        PlayUI();
    }

    public void DecreaseShootNum()
    {
        num--;
        shootNum.text = num.ToString();
		if (num < 0) 
		{
			num = 0;
			isShow = false;
			HideUI ();
		}
    }

    void PlayUI()
    {
        if(isShow)
        {
            return;
        }

        isShow = true;
        StartCoroutine(PlayFadeAnim(0.0f, 1.0f, 1f));
        //StartCoroutine(PlayFadeOut());
    }

    IEnumerator PlayFadeAnim(float start, float end, float time)
    {
        Color temp = shootNum.color;
        Color temp2 = shootText.color;

        float a = start;
        float speed = (end - start) / time;
        do
        {
            shootNum.gameObject.SetActive(false);
            shootText.gameObject.SetActive(false);

            shootNum.color = new Color(temp.r, temp.g, temp.b, a);
            shootText.color = new Color(temp2.r, temp2.g, temp2.b, a);
            a += speed * Time.deltaTime;

            shootNum.gameObject.SetActive(true);
            shootText.gameObject.SetActive(true);
            yield return 0;
        } while (a < end);

        shootNum.gameObject.SetActive(false);
        shootText.gameObject.SetActive(false);

        shootNum.color = new Color(temp.r, temp.g, temp.b, end);
        shootText.color = new Color(temp2.r, temp2.g, temp2.b, end);

        shootNum.gameObject.SetActive(true);
        shootText.gameObject.SetActive(true);
    }

    IEnumerator PlayFadeOut()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(PlayFadeAnim(1.0f, 0.0f, 1.0f));

        Invoke("ChangeStateToHide", 1.0f);
    }

	void HideUI()
	{
		Color temp = shootNum.color;
		Color temp2 = shootText.color;

		shootNum.color = new Color(temp.r, temp.g, temp.b, 0.0f);
		shootText.color = new Color(temp2.r, temp2.g, temp2.b, 0.0f);
	}

    void ChangeStateToHide()
    {
        isShow = false;
    }
}
