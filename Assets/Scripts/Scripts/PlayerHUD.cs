using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text timeText;
    public Slider slider;
    public PlayerCharacter player;

    private void OnEnable()
    {
        // Subscribe to the OnHealthChanged event
        PlayerCharacter.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnHealthChanged event
        PlayerCharacter.OnHealthChanged -= UpdateHealthBar;
    }

    public void ShowHUD()
    {
        timeText.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        timeText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
    }

    private void UpdateHealthBar(int health)
    {
        
        slider.value = health;
    }

    void Update()
    {
        float time = Time.timeSinceLevelLoad;
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        slider.value = player.GetHealth();

    }
}