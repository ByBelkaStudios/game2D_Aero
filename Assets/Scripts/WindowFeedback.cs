using DG.Tweening;
using UnityEngine;

public class WindowFeedback : MonoBehaviour
{
    private Material objectMaterial;
    private Color originalObjectColor;

    [Header("Flash Settings")]
    public Color hitColor = Color.red;
    public float flashDuration = 0.2f;

    private void OnEnable()
    {
        GameManager.Instance.OnHitTaken += Feedback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHitTaken -= Feedback;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnHitTaken -= Feedback;
    }

    private void Feedback()
    {
        objectMaterial.DOColor(hitColor, flashDuration / 2)
            .OnComplete(() =>
            {
                objectMaterial.DOColor(originalObjectColor, flashDuration / 2);
            });

        //transform.DOShakePosition(flashDuration, 0.1f, 10, 90, false, true).SetRelative(); -- movement
        transform.DOShakeRotation(flashDuration, new Vector3(0, 0, 2));
    }

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        objectMaterial = renderer.material;
        originalObjectColor = objectMaterial.color;
    }
}
