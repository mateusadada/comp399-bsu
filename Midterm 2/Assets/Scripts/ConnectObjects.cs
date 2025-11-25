using UnityEngine;

public class ConnectObjectsTriangle : MonoBehaviour
{
    public Transform Origin; // ponto A
    public Transform Cube; // ponto B
    public Transform Sphere; // ponto C

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 4; // quantidade de conexão entre os objetos
    }

    void Update()
    {
        // conexão entre os objetos
        lr.SetPosition(0, Origin.position);
        lr.SetPosition(1, Cube.position);
        lr.SetPosition(2, Sphere.position);
        lr.SetPosition(3, Origin.position);
    }
}
