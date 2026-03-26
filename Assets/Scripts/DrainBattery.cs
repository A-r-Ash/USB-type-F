using UnityEngine;

public class DrainBattery : MonoBehaviour
{

    public float drainRate = -15f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var bm = collision.gameObject.GetComponent<BatteryManager>();
        if(bm != null)
        {
            bm.RegisterHazard();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {   
        BatteryManager bm = collision.gameObject.GetComponent<BatteryManager>();
        if(bm != null)
        {
            bm.ApplyEnergyChange(drainRate * Time.deltaTime);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        var bm = collision.gameObject.GetComponent<BatteryManager>();
        if (bm != null)
        {
            bm.UnregisterHazard();
        }
    }

}
