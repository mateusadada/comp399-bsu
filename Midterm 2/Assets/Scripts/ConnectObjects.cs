using UnityEngine;

public class ConnectObjects : MonoBehaviour
{
    public Transform Origin;
    public Transform Sphere;
    private LineRenderer Line_Renderer;

    void Start()
    {
        Line_Renderer = GetComponent<LineRenderer>();
        Line_Renderer.positionCount = 2;
    }

    void Update()
    {
        Line_Renderer.SetPosition(0, Origin.position);
        Line_Renderer.SetPosition(1, Sphere.position);
    }
}
