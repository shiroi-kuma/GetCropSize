using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GetSizeController : MonoBehaviour
{
    [FormerlySerializedAs("targetImage")]
    [SerializeField]
    private RectTransform targetRectTrans;

    [SerializeField]
    private TMP_Text textUi;

    private Vector2 cacheSize = Vector2.zero;
    private Vector2 cachePos = Vector2.zero;

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

        if (isDiff)
        {
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
        string text = $"width:{Mathf.RoundToInt(size.x)}, height:{Mathf.RoundToInt(size.y)}\n" +
            $"x:{Mathf.RoundToInt(pos.x)}, y:{Mathf.RoundToInt(Screen.height - pos.y)}";
        textUi.SetText(text);
    }
}
