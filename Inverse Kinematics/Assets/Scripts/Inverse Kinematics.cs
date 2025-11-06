using System;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{
    public Transform centralBody;
    public Vector3 offset;
    public float rotationSpeed = 5f;
    public GameObject Origin;
    public GameObject P0;
    public GameObject P1;
    public GameObject P2;
    public GameObject Target;

    public double theta = 30;
    public Vector3 third;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currentAng = 0;
    void Start()
    {
        Origin = GameObject.Find("Origin");
        P0 = GameObject.Find("P0");
        P1 = GameObject.Find("P1");
        P2 = GameObject.Find("P2");
        Target = GameObject.Find("Target");
        if (centralBody != null)
            offset = transform.position - centralBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        third = Vector3.Cross(P0.transform.position, Target.transform.position);

        theta = (180 / Math.PI) * Math.Acos(Vector3.Dot(P0.transform.position, Target.transform.position)/(Vector3.Magnitude(P0.transform.position)*Vector3.Magnitude(Target.transform.position)));
        
        if (third.y > 0)
        {
            
            if (5 < theta){
                currentAng += rotationSpeed * Time.deltaTime;
                Matrix4x4 rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));   
            transform.position = centralBody.position + rotMat.MultiplyPoint(offset);    
            }
        
        }
        else
        {
            if (theta > 5)
            {
                currentAng -= rotationSpeed * Time.deltaTime;
                Matrix4x4 rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));
                transform.position = centralBody.position + rotMat.MultiplyPoint(offset);
            }
        }
    }
}
