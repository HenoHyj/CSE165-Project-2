using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingArrow : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject arrow;
    [SerializeField] Renderer arrowRenderer;
    // Start is called before the first frame update
    void Start()
    {
        arrowRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.isGaming)
        {
            arrowRenderer.enabled = true;
            Vector3 position = camera.transform.position + 3.0f * camera.transform.forward;
            position.y -= 1.0f;
            arrowRenderer.transform.position = position;
            Vector3 pointTo = GlobalVariables.points[GlobalVariables.nextPoint] - position;
            Quaternion basic = Quaternion.Euler(0, 90, 0);
            arrowRenderer.transform.rotation = Quaternion.LookRotation(pointTo) * basic;
            Vector3 scl = new Vector3(5,5,5);
            scl.x = Mathf.Max(GlobalVariables.distance/2.0f, 5);
            arrowRenderer.transform.localScale = scl;
        }
        else
        {
            arrowRenderer.enabled = false;
        }
    }
}
