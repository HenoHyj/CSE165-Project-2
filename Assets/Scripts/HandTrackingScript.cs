using Oculus.Interaction.PoseDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Oculus.Interaction.Input;
using System;
using System.Linq;
using UnityEngine.Assertions;

public class HandTrackingScript : MonoBehaviour
{
    public Camera sceneCamera;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public OVRSkeleton skeleton;
    [SerializeField] public ShapeRecognizerActiveState upState;
    [SerializeField] public ShapeRecognizerActiveState downState;
    [SerializeField] public ShapeRecognizerActiveState rightState;
    [SerializeField] public ShapeRecognizerActiveState leftState;


    // private Vector3 targetPosition;
    // private Quaternion targetRotation;
    // private float step;
    private bool isIndexFingerPinching;
    private bool isThumbsUp;
    private bool isThumbsDown;
    private bool isLeft;
    private bool isRight;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 1.0f;
        // line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // step = 5.0f * Time.deltaTime;

        if (leftHand.IsTracked)
        {
            isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (isIndexFingerPinching)
            {
                GlobalVariables.isMoving = true;
            }
            else
            {
                GlobalVariables.isMoving = false;
            }

        
        }

        if (rightHand.IsTracked)
        {
            isThumbsUp = upState.Active;
            if (isThumbsUp)
            {
                GlobalVariables.rotateUp = true;
            }
            else
            {
                GlobalVariables.rotateUp = false;
            }

            isThumbsDown = downState.Active;
            if (isThumbsDown)
            {
                GlobalVariables.rotateDown = true;
            }
            else
            {
                GlobalVariables.rotateDown = false;
            }

            isRight = rightState.Active;
            if (isRight)
            {
                GlobalVariables.rotateRight = true;
            }
            else
            {
                GlobalVariables.rotateRight = false;
            }

            isLeft = leftState.Active;
            if (isLeft)
            {
                GlobalVariables.rotateLeft = true;
            }
            else
            {
                GlobalVariables.rotateLeft = false;
            }


        }
    }
}
