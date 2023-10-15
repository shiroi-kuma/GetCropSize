using TMPro;
using UnityEngine;

public class TargetTransformViewer : MonoBehaviour
{
    [SerializeField]
    private RectTransform targetRectTrans;

    [SerializeField]
    private TMP_Text textUi;

    private Vector2 cacheSize = Vector2.zero;
    private Vector2 cachePos = Vector2.zero;

    private float widthScale = 1f;
    private float heightScale = 1f;

    private bool isScaleChanged;

    public void SetScreenScale(float width, float height)
    {
        if (Screen.width != 0 || Screen.height != 0 || width != 0 || height != 0)
        {
            widthScale = width / Screen.width;
            heightScale = height / Screen.height;

            isScaleChanged = true;
        }
    }

    private void Awake()
    {
        CacheTransform();
        UpdateText(cacheSize, cachePos);
    }

    private void Update()
    {
        // 差分があれば表示更新する
        bool isDiff = false;
        isDiff |= cacheSize != targetRectTrans.sizeDelta;

        var corners = new Vector3[4];
        targetRectTrans.GetWorldCorners(corners);
        isDiff |= cachePos != (Vector2)corners[1];

        if (isDiff || isScaleChanged)
        {
            isScaleChanged = false;
            CacheTransform();
            UpdateText(targetRectTrans.sizeDelta, corners[1]);
        }
    }

    private void CacheTransform()
    {
        cacheSize = targetRectTrans.sizeDelta;

        var corners = new Vector3[4];
        targetRectTrans.GetWorldCorners(corners);
        cachePos = corners[1];
    }

    private void UpdateText(Vector2 size, Vector2 pos)
    {
        string text = $"width:{Mathf.RoundToInt(size.x * widthScale)}, height:{Mathf.RoundToInt(size.y * heightScale)}\n" +
            $"x:{Mathf.RoundToInt(pos.x * widthScale)}, y:{Mathf.RoundToInt((Screen.height - pos.y) * heightScale)}";
        textUi.SetText(text);
    }
}
