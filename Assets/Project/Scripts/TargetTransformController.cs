using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTransformController : MonoBehaviour
{
	[SerializeField] private EventTrigger centerTrigger;
	[SerializeField] private EventTrigger topTrigger;
	[SerializeField] private EventTrigger rightTrigger;
	[SerializeField] private EventTrigger leftTrigger;
	[SerializeField] private EventTrigger bottomTrigger;

	private const float MIN_SIZE = 120.0f;
	private enum ScalerType
	{
		Top = 1,
		Left = 2,
		Right = 3,
		Bottom = 4,
	}
	private Vector2 prevPos = new Vector2();
	private void Awake()
	{
		// 押下開始処理
		EventTrigger.Entry onDown = new EventTrigger.Entry();
		onDown.eventID = EventTriggerType.PointerDown;
		onDown.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		rightTrigger.triggers.Add(onDown);
		leftTrigger.triggers.Add(onDown);
		topTrigger.triggers.Add(onDown);
		bottomTrigger.triggers.Add(onDown);
		centerTrigger.triggers.Add(onDown);
		
		// 右スケーラ処理
		EventTrigger.Entry rightDrag = new EventTrigger.Entry();
		rightDrag.eventID = EventTriggerType.Drag;
		rightDrag.callback.AddListener((data) => { OnScalerDrag((PointerEventData)data, ScalerType.Right); });
		rightTrigger.triggers.Add(rightDrag);
		
		// 左スケーラ処理
		EventTrigger.Entry leftDrag = new EventTrigger.Entry();
		leftDrag.eventID = EventTriggerType.Drag;
		leftDrag.callback.AddListener((data) => { OnScalerDrag((PointerEventData)data, ScalerType.Left); });
		leftTrigger.triggers.Add(leftDrag);
		
		// 上スケーラ処理
		EventTrigger.Entry topDrag = new EventTrigger.Entry();
		topDrag.eventID = EventTriggerType.Drag;
		topDrag.callback.AddListener((data) => { OnScalerDrag((PointerEventData)data, ScalerType.Top); });
		topTrigger.triggers.Add(topDrag);
		
		// 下スケーラ処理
		EventTrigger.Entry bottomDrag = new EventTrigger.Entry();
		bottomDrag.eventID = EventTriggerType.Drag;
		bottomDrag.callback.AddListener((data) => { OnScalerDrag((PointerEventData)data, ScalerType.Bottom); });
		bottomTrigger.triggers.Add(bottomDrag);
		
		// 中心移動処理
		EventTrigger.Entry drag = new EventTrigger.Entry();
		drag.eventID = EventTriggerType.Drag;
		drag.callback.AddListener((data) => { OnCenterDrag((PointerEventData)data); });
		centerTrigger.triggers.Add(drag);
	}
	
	private void OnScalerDown(PointerEventData data)
	{
		prevPos = data.position;
	}

	private void OnScalerDrag(PointerEventData data, ScalerType type)
	{
		Vector2 deltaPos = data.position - prevPos;
		RectTransform rectTrans = transform as RectTransform;
		Vector2 newScale = rectTrans.sizeDelta;
		Vector2 newPos = transform.position;

		switch (type)
		{
			case ScalerType.Right:
				newScale.x += deltaPos.x;
				newPos.x += deltaPos.x / 2;
				break;
			case ScalerType.Left:
				newScale.x -= deltaPos.x;
				newPos.x += deltaPos.x / 2;
				break;
			case ScalerType.Top:
				newScale.y += deltaPos.y;
				newPos.y += deltaPos.y / 2;
				break;
			case ScalerType.Bottom:
				newScale.y -= deltaPos.y;
				newPos.y += deltaPos.y / 2;
				break;
		}

		// 辺の長さが最低値を切るようであれば終了
		if (newScale.x < MIN_SIZE || newScale.y < MIN_SIZE) return;
		
		// 値設定
		rectTrans.sizeDelta = newScale;
		transform.position = newPos;
		
		// キャッシュ
		prevPos = data.position;
	}
	
	private void OnCenterDrag(PointerEventData data)
	{
		Vector2 newPos = transform.position;
		newPos += data.position - prevPos;
		transform.position = newPos;
		prevPos = data.position;
	}
}
