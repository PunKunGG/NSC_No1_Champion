using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Transform lookTarget;

    public void SetHealth(float healthPercent)
    {
        fillImage.fillAmount = healthPercent;
    }

    private void Update()
    {
        if (lookTarget != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - lookTarget.position);
        }
    }
}
