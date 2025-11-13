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
    public double thetaR; // first angle join
    public double thetaM; // second angle join
    public double thetaE; // end angle join
    public Vector3 third;
    public Matrix4x4 rotMat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currentAng = 0;
    void Start()
    {
        Origin = GameObject.Find("Origin");
        P0 = GameObject.Find("P0");
        P1 = GameObject.Find("P1");
        P2 = GameObject.Find("P2");
        Target = GameObject.Find("Target");
        thetaR = 0.0f;
        thetaM = 0.0f;
        thetaE = 0.0f;
        if (centralBody != null)
            offset = transform.position - centralBody.position;
    }

    // Update is called once per frame
    void Update()
    {

    // Outermost joint rotate, first to be called in cycle
        Matrix4x4 translateBackP1 = Matrix4x4.Translate(-P1.transform.position);
        Matrix4x4 translateForwardP1 = Matrix4x4.Translate(P1.transform.position);

        P2.transform.position = translateBackP1 * P2.transform.position;

        Target.transform.position = translateBackP1 * Target.transform.position;

        third = Vector3.Cross(P2.transform.position, Target.transform.position);

        thetaE = (180 / Math.PI) * Math.Acos(Vector3.Dot(P2.transform.position, Target.transform.position)/(Vector3.Magnitude(P2.transform.position)*Vector3.Magnitude(Target.transform.position)));

        if(third.y>0)
        {
            if (5 < thetaE){
                currentAng += rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));   
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardP1 * P2.transform.position;
                Target.transform.position = translateForwardP1 * Target.transform.position;
            }
        }
        else
        {
            if (thetaE > 5)
            {
                currentAng -= rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardP1 * P2.transform.position;
                Target.transform.position = translateForwardP1 * Target.transform.position;
            }
        }
        
    // Middle joint rotate, second to be called in cycle
        Matrix4x4 translateBackP0 = Matrix4x4.Translate(-P0.transform.position);
        Matrix4x4 translateForwardP0 = Matrix4x4.Translate(P0.transform.position);

        P2.transform.position = translateBackP0 * P2.transform.position;

        Target.transform.position = translateBackP0 * Target.transform.position;

        third = Vector3.Cross(P2.transform.position, Target.transform.position);

        thetaM = (180 / Math.PI) * Math.Acos(Vector3.Dot(P2.transform.position, Target.transform.position)/(Vector3.Magnitude(P2.transform.position)*Vector3.Magnitude(Target.transform.position)));
        
        if(third.y>0)
        {
            if (5 < thetaM){
                currentAng += rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));   
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardP0 * P2.transform.position;
                Target.transform.position = translateForwardP0 * Target.transform.position;
                P1.transform.position = translateBackP0 * P1.transform.position;
                P1.transform.position = rotMat * P1.transform.position;
                P1.transform.position = translateForwardP0 * P1.transform.position;
            }
        }
        else
        {
            if (thetaM > 5)
            {
                currentAng -= rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardP0 * P2.transform.position;
                Target.transform.position = translateForwardP0 * Target.transform.position;
                P1.transform.position = translateBackP0 * P1.transform.position;
                P1.transform.position = rotMat * P1.transform.position;
                P1.transform.position = translateForwardP0 * P1.transform.position;
            }
        }
        
    // Innermost joint rotate, last to be called in cycle
        Matrix4x4 translateBackOrigin = Matrix4x4.Translate(-Origin.transform.position);
        Matrix4x4 translateForwardOrigin = Matrix4x4.Translate(Origin.transform.position);

        P2.transform.position = translateBackOrigin * P2.transform.position;

        Target.transform.position = translateBackOrigin * Target.transform.position;

        third = Vector3.Cross(P2.transform.position, Target.transform.position);

        thetaR = (180 / Math.PI) * Math.Acos(Vector3.Dot(P2.transform.position, Target.transform.position)/(Vector3.Magnitude(P2.transform.position)*Vector3.Magnitude(Target.transform.position)));
        
        if(third.y>0)
        {
            if (5 < thetaR){
                currentAng += rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));   
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardOrigin * P2.transform.position;
                Target.transform.position = translateForwardOrigin * Target.transform.position;
                P1.transform.position = translateBackOrigin * P1.transform.position;
                P1.transform.position = rotMat * P1.transform.position;
                P1.transform.position = translateForwardOrigin * P1.transform.position;
                P0.transform.position = translateBackOrigin * P0.transform.position;
                P0.transform.position = rotMat * P0.transform.position;
                P0.transform.position = translateForwardOrigin * P0.transform.position;
            }
        }
        else
        {
            if (thetaR > 5)
            {
                currentAng -= rotationSpeed * Time.deltaTime;
                rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, currentAng, 0));
                P2.transform.position = rotMat * P2.transform.position;
                P2.transform.position = translateForwardOrigin * P2.transform.position;
                Target.transform.position = translateForwardOrigin * Target.transform.position;
                P1.transform.position = translateBackOrigin * P1.transform.position;
                P1.transform.position = rotMat * P1.transform.position;
                P1.transform.position = translateForwardOrigin * P1.transform.position;
                P0.transform.position = translateBackOrigin * P0.transform.position;
                P0.transform.position = rotMat * P0.transform.position;
                P0.transform.position = translateForwardOrigin * P0.transform.position;
            }
        }
    }
}
