/*************************************************************************\
*                           USENS CONFIDENTIAL                            *
* _______________________________________________________________________ *
*                                                                         *
* [2014] - [2017] USENS Incorporated                                      *
* All Rights Reserved.                                                    *
*                                                                         *
* NOTICE:  All information contained herein is, and remains               *
* the property of uSens Incorporated and its suppliers,                   *
* if any.  The intellectual and technical concepts contained              *
* herein are proprietary to uSens Incorporated                            *
* and its suppliers and may be covered by U.S. and Foreign Patents,       *
* patents in process, and are protected by trade secret or copyright law. *
* Dissemination of this information or reproduction of this material      *
* is strictly forbidden unless prior written permission is obtained       *
* from uSens Incorporated.                                                *
*                                                                         *
\*************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fingo;

public class SwipeController_v2 : MonoBehaviour
{
	public Transform left_button;
	public Transform top_button;
	public Transform right_button;
	public Transform bottom_button;
    private float[] bound;   // the bound of the object
	private float return_duration = 0.2f;  //animation duration of returning to original position (center)
	private float move_duration = 0.1f;		//animation duration of moving to nearest point
    private Vector3 origin_position = Vector3.zero; 
    private bool is_under_touch = false;    // is under swiping or not
	private float distance_between_buttons; // distance from the end ( the red bottons) to the center;

    void Start()
    {
        bound = new float[4];
        bound[0] = -transform.localScale.x / 2f;  //left
        bound[1] = transform.localScale.x / 2f;  //right
        bound[2] = -transform.localScale.y / 2f;  //bottom
        bound[3] = transform.localScale.y / 2f;  //top
        origin_position = transform.position;
		distance_between_buttons = top_button.position.y;
    }

    void OnEnable()
    {
        SwipeHand_v2.SwipeEvent += SwipeResponseCtrl;
    }

    void OnDisable()
    {
        SwipeHand_v2.SwipeEvent -= SwipeResponseCtrl;
    }

	/* dis : distance(vector) the hand has moved from last frame */
	void SwipeResponseCtrl(Vector3 handPos, float dis,float hand_width,float hand_height, SwipeType swipeType)
    {

        Vector3 pos = transform.position;
		float object_right_bound = pos.x + bound[1];
		float object_left_bound = pos.x + bound[0];
		float object_top_bound  = pos.y + bound[3];
		float object_bottom_bound = pos.y + bound[2];

        /* project hand position to button position by button position.z*/
        float r = pos.z / handPos.z;
        handPos.x *= r;
        handPos.y *= r;
		hand_width *= r;
		hand_height *= r;
		dis *= r;
        

		float hand_right_bound = handPos.x + hand_width;
		float hand_left_bound = handPos.x - hand_width;
		float hand_top_bound = handPos.y + hand_height;
		float hand_bottom_bound = handPos.y - hand_height;

		/* if hand palm is not located within the button bound */
		if(hand_right_bound < object_left_bound || hand_left_bound > object_right_bound 
		|| hand_top_bound < object_bottom_bound || hand_bottom_bound > object_top_bound){
			CheckAnimationDirection ();
			//AnimateObject(origin_position);
		}else{
			/* stop all animations */
			StopAllCoroutines();
			/* the moving distance(vector) */
			Vector3 move_vector = Vector3.zero;
			switch (swipeType)
			{
			case SwipeType.Left:
				move_vector.x = -dis;
				break;
			case SwipeType.Right:
				move_vector.x = dis;
				break;
			case SwipeType.Up:
				move_vector.y = dis;
				break;
			case SwipeType.Down:
				move_vector.y = -dis;
				break;
			}
			is_under_touch = true;
			GetComponent<Renderer>().material.color = Color.red;
			MoveObject (move_vector);
		}
    }

	void CheckAnimationDirection(){

		if( Mathf.Abs(transform.position.x) > distance_between_buttons/2 || Mathf.Abs(transform.position.y) > distance_between_buttons/2){
			MoveToNearest ();
		}else{
			AnimateObject(origin_position);
		}
	}


	void MoveToNearest(){
		Vector3 pos = transform.position;
		if (pos.x >= 0) {
			if (pos.y >= 0) {
				if( Vector3.Distance(pos,top_button.position) > Vector3.Distance(pos,right_button.position)){
					AnimateObject (right_button.position);
				}else{
					AnimateObject (top_button.position);
				}
			} else {
				if( Vector3.Distance(pos,bottom_button.position) > Vector3.Distance(pos,right_button.position)){
					AnimateObject (right_button.position);
				}else{
					AnimateObject (bottom_button.position);
				}
			}	
		} else {
			if (pos.y >= 0) {
				if( Vector3.Distance(pos,top_button.position) > Vector3.Distance(pos,left_button.position)){
					AnimateObject (left_button.position);
				}else{
					AnimateObject (top_button.position);
				}
			} else {
				if( Vector3.Distance(pos,bottom_button.position) > Vector3.Distance(pos,left_button.position)){
					AnimateObject (left_button.position);
				}else{
					AnimateObject (bottom_button.position);
				}
			}	
		}
	}


    /// <summary>
    /// move to the destination.
    /// </summary>
	public void AnimateObject(Vector3 destination)
    {
		if (is_under_touch)
        {
            StopAllCoroutines();
			StartCoroutine(DoAnimation(destination));
			is_under_touch = false;
        }
    }

	IEnumerator DoAnimation(Vector3 destination)
    {
        yield return new WaitForSeconds(0.2f);
		int t = (int)(move_duration / 0.01f);
        Vector3 pos = transform.position;
        for (int i = 0; i < t; i++)
        {
			transform.position = Vector3.Lerp(pos, destination, (float)(i + 1) / (float)t);
            yield return new WaitForSeconds(0.01f);
        }
		StartCoroutine (BackToOrigin());
    }

	IEnumerator BackToOrigin(){
		yield return new WaitForSeconds(1f);
		int t = (int)(return_duration / 0.01f);
		Vector3 pos = transform.position;
		for (int i = 0; i < t; i++)
		{
			GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, (float)(i + 1) / (float)t);
			transform.position = Vector3.Lerp(pos, origin_position, (float)(i + 1) / (float)t);
			yield return new WaitForSeconds(0.01f);
		}
	}

	/* move button*/
	void MoveObject(Vector3 move_vector){
		if( Mathf.Abs(transform.position.x) > distance_between_buttons || Mathf.Abs(transform.position.y) > distance_between_buttons){
			return;
		}else{
			transform.Translate(move_vector);
		}
	}

}
