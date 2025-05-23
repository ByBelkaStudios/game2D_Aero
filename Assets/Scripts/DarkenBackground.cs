using UnityEngine;

public class DarkenBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;

    [SerializeField] private float colorValue = 0.25f;
    [SerializeField] private float tweenTime = 2f;

    private Color originalColor;
    private Color targetColor;

    private void Start()
    {
        originalColor = render.color;
        targetColor = new Color(colorValue, colorValue, colorValue, 1);

        StartCoroutine(DarkenOverTime());
    }

    private System.Collections.IEnumerator DarkenOverTime()
    {
        float elapsed = 0f;
        while (elapsed < tweenTime)
        {
            render.color = Color.Lerp(originalColor, targetColor, elapsed / tweenTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        render.color = targetColor;
    }

}

