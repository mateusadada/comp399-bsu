using UnityEngine;

public class Spring3 : MonoBehaviour
{
    public GameObject Origin;
    public GameObject Cube;
    public GameObject Sphere;

    public float mass = 1f; // massa
    public float k_s = 3f; // rigidez
    public float restLength = 3f; // tamanho
    public float k_d = 0.5f; // damping
    public float rGhost = 0.2f; // força anti-ghost
    public float dt = 0.02f; // deltaTime do integrador

    private Vector3 velO = Vector3.zero;
    private Vector3 velC = Vector3.zero;
    private Vector3 velS = Vector3.zero;

    void Update()
    {
        // posição atual
        Vector3 pO = Origin.transform.position;
        Vector3 pC = Cube.transform.position;
        Vector3 pS = Sphere.transform.position;

        // forças resultantes
        Vector3 fO = Vector3.zero;
        Vector3 fC = Vector3.zero;
        Vector3 fS = Vector3.zero;

        ApplySpring(
            pO, pC,
            velO, velC,
            out Vector3 f0, out Vector3 f1
        );
        fO += f0;
        fC += f1;

        ApplySpring(
            pC, pS,
            velC, velS,
            out Vector3 f2, out Vector3 f3
        );
        fC += f2;
        fS += f3;

        // integração entre os objetos
        velO += (fO / mass) * dt;
        velC += (fC / mass) * dt;
        velS += (fS / mass) * dt;

        pO += velO * dt;
        pC += velC * dt;
        pS += velS * dt;

        Origin.transform.position = pO;
        Cube.transform.position = pC;
        Sphere.transform.position = pS;
    }

    // calcular a mola entre 2 objetos
    void ApplySpring(
        Vector3 aPos, Vector3 bPos,
        Vector3 aVel, Vector3 bVel,
        out Vector3 forceA, out Vector3 forceB
    )
    {
        Vector3 dir = bPos - aPos;
        float L = dir.magnitude;
        Vector3 n = dir.normalized;

        // força da mola
        Vector3 F_s = -k_s * (L - restLength) * n;

        // damping + ghost
        Vector3 F_da = -k_d * aVel - rGhost * aVel;
        Vector3 F_db = -k_d * bVel - rGhost * bVel;

        // final
        forceA = -F_s + F_da;
        forceB =  F_s + F_db;
    }
}
