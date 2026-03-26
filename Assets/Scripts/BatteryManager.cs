using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [Header("Settings")]
    public float maxBattery = 100f;
    public float currentBattery;
    public float passiveDrainRate = 1.0f;
    public bool pausePassiveEnergy = false;

    public int activeHazardCount;


    [Header("UI")]
    public Slider batteryBar;

    private void Start()
    {
        currentBattery = maxBattery;
        if (batteryBar != null) batteryBar.maxValue = maxBattery;
        activeHazardCount = 0;
    }

    private void FixedUpdate()
    {
        if (activeHazardCount <= 0)
        {
            ApplyEnergyChange(-passiveDrainRate * Time.fixedDeltaTime);
        }

        
        
        
        UpdateUI();
    }

    public void RegisterHazard()
    {
        activeHazardCount++;
    }

    public void UnregisterHazard()
    {
        activeHazardCount = Mathf.Max(activeHazardCount -1, 0);
    }

    public void ApplyEnergyChange(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0, maxBattery);
    }

    private void UpdateUI()
    {
        if(batteryBar  != null)
        {
            batteryBar.value = currentBattery;

        }
    }



}
