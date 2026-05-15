using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private LifeBlossom lifeBlossom;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
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

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth:0} / {maxHealth:0}";
        }
    }
}