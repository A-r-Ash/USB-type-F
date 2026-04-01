using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    PlayerController playerController;
    public Slider slider;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();

    }

    private void Update()
    {
        if (playerController != null)
        {
            slider.value = playerController.CurrentHealth / playerController.MaxHealth;
        }
    }
}
