using UnityEngine;

public class SingleMassSpring : MonoBehaviour
{
    public Transform fixedAnchor; 

    public float mass_m = 1f;        
    public float stiffness_ks = 25f; 
    public float restLength_lr = 3f; 
    public float dampingConstant_kd = 2.5f;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;
    private bool estaConectado = true;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null) rb.mass = mass_m;
        if (lineRenderer != null) lineRenderer.positionCount = 2;
    }

    public void SoltarMola()
    {
        estaConectado = false;
        
        if (lineRenderer != null) lineRenderer.enabled = false; 

        if (rb != null) rb.gravityScale = 1f; 
    }

    void FixedUpdate()
    {
        if (fixedAnchor == null || !estaConectado) return;

        float dt = Time.fixedDeltaTime;
        Vector2 pA = transform.position;
        Vector2 pB = fixedAnchor.position;
        Vector2 v = rb.linearVelocity;

        Vector2 displacement = pA - pB;
        float currentLength = displacement.magnitude;
        Vector2 direction = currentLength > 0 ? displacement.normalized : Vector2.zero;

        Vector2 fSpring = -stiffness_ks * (currentLength - restLength_lr) * direction;
        Vector2 fDamping = -dampingConstant_kd * v;
        
        Vector2 fGravity = mass_m * Physics2D.gravity;

        Vector2 totalForce = fSpring + fDamping + fGravity;

        rb.linearVelocity = v + (totalForce / mass_m) * dt;

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, pB); 
            lineRenderer.SetPosition(1, pA); 
        }
    }
}
