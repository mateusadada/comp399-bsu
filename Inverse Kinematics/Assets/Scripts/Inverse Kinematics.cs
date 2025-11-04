using System;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{
    public Transform centralBody;
    public Vector3 offset;
    public float rotationSpeed = 5f;
    public GameObject E;
    public GameObject G;
    public GameObject T;

    public double theta = 30;
    public Vector3 third;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currentAng = 0;
    void Start()
    {
        E = GameObject.Find("E");
        G = GameObject.Find("G");
        T = GameObject.Find("T");
        if (centralBody != null)
            offset = transform.position - centralBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        third = Vector3.Cross(G.transform.position, T.transform.position);

        theta = (180 / Math.PI) * Math.Acos(Vector3.Dot(G.transform.position, T.transform.position)/(Vector3.Magnitude(G.transform.position)*Vector3.Magnitude(T.transform.position)));
        
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
