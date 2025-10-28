using UnityEngine;

public class SimpleOrbit : MonoBehaviour
{
    public Transform centralBody;
    public float orbitSpeed = 10f;
    public float rotationSpeed = 50f;

    private Vector3 offset;
    private float angle = 0f;

    void Start()
    {
        if (centralBody != null)
            offset = transform.position - centralBody.position;
    }

    void Update()
    {
        Matrix4x4 SelfRotMat = Matrix4x4.Rotate(Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0));
        transform.localRotation *= SelfRotMat.rotation;

        if (centralBody != null)
        {
            angle += orbitSpeed * Time.deltaTime;
            Matrix4x4 rotMat = Matrix4x4.Rotate(Quaternion.Euler(0, angle, 0));
            transform.position = centralBody.position + rotMat.MultiplyPoint(offset);
        }
    }
}
