using UnityEngine;
using System.Collections;

public enum CarStatus
{
    Original = 0,

    Separating =1,

    Part = 2
}

public class CarObjectPart : MonoBehaviour
{

  //  public Transform carWheel_Left;
  //  public Transform carWheel_Right;
  //  public Transform[] carWheels;
    public Transform carRoof;
    public Transform carBody;
    public Transform carInner;

    public float carBody_height_offset;
    public float carRoof_height_offset;
    public float carInner_height_offset;
  
  //  public float wheel_side_Max;
    public float separateSpeed_Body = 20f;
    public float separateSpeed_Roof = 20f;//...car object separating speed
    public float separateSpeed_Inner = 20f;
 //   public float separateSpeed_Wheel = 20f;

    public float carObjectMoveDurationTime = 0.1f;

#region PRIVATE

    private CarStatus currStatus = CarStatus.Original;

    private Vector3 carBodyOriginal;
    private Vector3 carRoofOriginal;
    private Vector3 carInnerOriginal;
    private Vector3 carWheelOriginal;

    private float carBody_height_Max;//the offset value
    private float carRoof_height_Max;
    private float carInner_height_Max;

    private float carRoof_offset = 0f;
    private float carBody_offset = 0f;
    private float carInner_offset = 0f;
    private float carWheel_offset = 0f;

    private bool isCatchingCar = false;

    private float lastPosY = 0;
    private float posY = 0;

    private bool isGrasping = false;
    private RigidHand rightHand;
    private RigidHand leftHand;
    private RigidHand cacheMyHand = null;
    private RigidHand changedMyHand = null;

#endregion

	void Start ()
	{
        carBodyOriginal = carBody.localPosition;
        carInnerOriginal = carInner.localPosition;
        carRoofOriginal = carRoof.localPosition;

        carBody_height_Max = carBodyOriginal.y + carBody_height_offset;
        carRoof_height_Max = carRoofOriginal.y + carRoof_height_offset;
        carInner_height_Max = carInnerOriginal.y + carInner_height_offset;
	    //    carWheelOriginal = carWheel_Left.localPosition;

        rightHand = GameObject.Find("Palm_Right").GetComponent<RigidHand>();
        leftHand = GameObject.Find("Palm_Left").GetComponent<RigidHand>();

        lastPosY = -10000f;
	}
	
	void Update ()
	{
        if (cacheMyHand == null) {
            return;
        }
	    if (!rightHand.meshHand.isDetected && !leftHand.meshHand.isDetected)
	        return;
        if (rightHand.meshHand.isDetected && leftHand.meshHand.isDetected)
            changedMyHand = rightHand;
        else if (!rightHand.meshHand.isDetected && leftHand.meshHand.isDetected)
            changedMyHand = leftHand;
        else if (rightHand.meshHand.isDetected && !leftHand.meshHand.isDetected)
            changedMyHand = rightHand;

        if (cacheMyHand != changedMyHand) {
            lastPosY = -10000f;
            cacheMyHand = changedMyHand;
        }
        if (isGrasping) 
        {
            if (transform.GetComponent<BoxCollider>().bounds.Contains(cacheMyHand.meshHand.middleDistal.position))
            {
                switch (currStatus)
                {
                    case CarStatus.Original:
                        HandGetUpOperation(cacheMyHand.meshHand.wrist.position.y);
                        break;
                    case CarStatus.Separating:
                        HandGetUpOperation(cacheMyHand.meshHand.wrist.position.y);
                        break;
                    case CarStatus.Part:
                        ControlCoroutine(false);
                        break;
                }
            }
        }
        else
        {
            if (transform.GetComponent<BoxCollider>().bounds.Contains(cacheMyHand.meshHand.middleDistal.position)) 
            {
                switch (currStatus) {
                    case CarStatus.Original:
                       // isCatchingCar = false;
                        ControlCoroutine(false);
                        break;
                    case CarStatus.Separating:
                        HandGetDownOperation(cacheMyHand.meshHand.wrist.position.y);
                        break;
                    case CarStatus.Part:
                        HandGetDownOperation(cacheMyHand.meshHand.wrist.position.y);
                        break;
                }
            }
        }
	
    }

   

    #region Hand Interaction

    private void HandGetUpOperation(float verticalY) {

        isCatchingCar =  true;
        if (isCatchingCar) {

            posY = verticalY;
            if (Mathf.Approximately(lastPosY, -10000f)) { }
            else if (posY > lastPosY) {
                float offsetY = posY - lastPosY;

                float velocity = offsetY / Time.deltaTime;

                if (Mathf.Abs(velocity) < 0.3f)
                    return;

                TransformerTheCar(velocity);
            }

            lastPosY = posY;
        }
    }


    private void HandGetDownOperation(float verticalY) {

        if (isCatchingCar) {
            posY = verticalY;
            if (Mathf.Approximately(lastPosY, -10000f)) { }
            else if (posY < lastPosY) {
                float offsetY = posY - lastPosY;

                float velocity = offsetY / Time.deltaTime;

                if (Mathf.Abs(velocity) < 0.3f)
                    return;

                TransformerTheCar(velocity);

            }

            lastPosY = posY;
        }
    }

    public void OnGraspStart()
    {
        isGrasping = true;
    }

    public void OnGraspEnd()
    {
        isGrasping = false;
    }

    #endregion

    private void TransformerTheCar(float speed)
    {
        if (Mathf.Approximately(speed, 0f))
            return;
        if ((currStatus == CarStatus.Original && speed < 0)
            || (currStatus == CarStatus.Part && speed > 0))
            return;

        if (IsSpeedDirectionChanged(speed, carRoof_offset)||
            IsSpeedDirectionChanged(speed, carBody_offset)||
            IsSpeedDirectionChanged(speed, carInner_offset))
        {
            carRoof_offset = 0f;
            carBody_offset = 0f;
            carInner_offset = 0f;
        }
        speed = MathfRound(speed,1);

        carRoof_offset += speed*Time.deltaTime* separateSpeed_Roof;
        carBody_offset += speed*Time.deltaTime* separateSpeed_Body;
        carInner_offset += speed*Time.deltaTime* separateSpeed_Inner;
       // carWheel_offset += speed * Time.deltaTime * separateSpeed_Wheel;
      

        currStatus = CarStatus.Separating;

        if (speed > 0)
        {
            if (carRoof.localPosition.y >= carRoof_height_Max ||
                carBody.localPosition.y >= carBody_height_Max ||
                carInner.localPosition.y >= carInner_height_Max)
            {
                carRoof_offset = 0f;
                carBody_offset = 0f;
                carInner_offset = 0f;
                if (carRoof.localPosition.y >= carRoof_height_Max &&
                    carBody.localPosition.y >= carBody_height_Max &&
                    carInner.localPosition.y >= carInner_height_Max)
                {
                    currStatus = CarStatus.Part;
                }
            }
        }
        else if (speed < 0)
        {
            if (carRoof.localPosition.y <= carRoofOriginal.y ||
             carBody.localPosition.y <= carBodyOriginal.y ||
             carInner.localPosition.y <= carInnerOriginal.y)
            {
                carRoof_offset = 0f;
                carBody_offset = 0f;
                carInner_offset = 0f;
                if (carRoof.localPosition.y <= carRoofOriginal.y &&
                   carBody.localPosition.y <= carBodyOriginal.y &&
                   carInner.localPosition.y <= carInnerOriginal.y)
                {
                    currStatus = CarStatus.Original;
                    isCatchingCar = false;
                }
            }
        }
        ControlCoroutine(true);
    }

    private void ControlCoroutine(bool doStart)
    {
        if( doStart )
        {
            StartCoroutine(MoveCarObject(carRoof , carRoofOriginal , carRoof_height_Max , carRoof_offset , carObjectMoveDurationTime));
            StartCoroutine(MoveCarObject(carBody , carBodyOriginal , carBody_height_Max , carBody_offset , carObjectMoveDurationTime));
            StartCoroutine(MoveCarObject(carInner , carInnerOriginal , carInner_height_Max , carInner_offset , carObjectMoveDurationTime));
        }
        else
        {
            StopCoroutine(MoveCarObject(carRoof , carRoofOriginal , carRoof_height_Max , carRoof_offset , carObjectMoveDurationTime));
            StopCoroutine(MoveCarObject(carBody , carBodyOriginal , carBody_height_Max , carBody_offset , carObjectMoveDurationTime));
            StopCoroutine(MoveCarObject(carInner , carInnerOriginal , carInner_height_Max , carInner_offset , carObjectMoveDurationTime));
        }
    }

    private IEnumerator MoveCarObject(Transform carObject,Vector3 carObjectOriginal,float carObjectMaxHeight,float offset, float durationTime)
    {
        float wholeOffset = carObject.localPosition.y + offset;
      
        if (wholeOffset >= carObjectMaxHeight)
        {
            wholeOffset = carObjectMaxHeight;
        }
        else if (wholeOffset <= carObjectOriginal.y)
        {
            wholeOffset = carObjectOriginal.y;
        }

        float dur = 0.0f;
        while (dur <= durationTime)
        {
            dur += Time.deltaTime;
            carObject.localPosition = Vector3.Lerp(carObject.localPosition,
                new Vector3(carObject.localPosition.x, wholeOffset, carObject.localPosition.z),
                 dur / durationTime);

            yield return null;
        }

    }

    private bool IsSpeedDirectionChanged(float speed, float carObjectOffset)
    {
        if (speed > 0)
        {
            return carObjectOffset < 0 ? true : false;
        }
        else if (speed < 0)
        {
            return carObjectOffset > 0 ? true : false;
        }
        return false;
    }

    private float MathfRound(float f,int roundInt)
    {
        float temp = f - (int) f;
        if (roundInt < 0)
        {
            return 10000f;
        }
        else if (roundInt == 0)
        {
            temp *= 1;
        }
        else if (roundInt > 0)
        {
            temp *= Mathf.Pow(10, roundInt);
        }
        int tempInt = (int) temp;
        return (int)f + (float)tempInt / Mathf.Pow(10, roundInt);
    }

}
