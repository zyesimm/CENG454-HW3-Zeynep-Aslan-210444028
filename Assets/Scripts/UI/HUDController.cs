using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private LifeBlossom lifeBlossom;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text timerText;

    private float elapsedTime;

    private void OnEnable()
    {
        if (lifeBlossom != null)
        {
            lifeBlossom.OnHealthChanged += UpdateHealth;
        }
    }

    private void OnDisable()
    {
        if (lifeBlossom != null)
        {
            lifeBlossom.OnHealthChanged -= UpdateHealth;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}