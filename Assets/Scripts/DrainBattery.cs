using UnityEngine;

public class DrainBattery : MonoBehaviour
{

    public float drainRate;
    private IDamagable currentColision;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable bm = collision.gameObject.GetComponent<IDamagable>();
        if(bm != null)
        {
            currentColision = bm;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        IDamagable bm = collision.gameObject.GetComponent<IDamagable>();
        if (bm != null && bm == currentColision)
        {
            currentColision = null;
        }
    }

    private void FixedUpdate()
    {
        if (currentColision != null)
        {
            currentColision.TakeDamage(-drainRate * Time.fixedDeltaTime);
        }

    }


}
