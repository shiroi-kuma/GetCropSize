using System.Collections;
using System.Collections.Generic;
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
	private Vector2 prevPos = new Vector2();
	private void Awake()
	{
		// 右スケーラ処理
		EventTrigger.Entry rightDown = new EventTrigger.Entry();
		rightDown.eventID = EventTriggerType.PointerDown;
		rightDown.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		rightTrigger.triggers.Add(rightDown);
		
		EventTrigger.Entry rightDrag = new EventTrigger.Entry();
		rightDrag.eventID = EventTriggerType.Drag;
		rightDrag.callback.AddListener((data) => { OnRightDrag((PointerEventData)data); });
		rightTrigger.triggers.Add(rightDrag);
		
		// 左スケーラ処理
		EventTrigger.Entry leftDown = new EventTrigger.Entry();
		leftDown.eventID = EventTriggerType.PointerDown;
		leftDown.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		leftTrigger.triggers.Add(leftDown);
		
		EventTrigger.Entry leftDrag = new EventTrigger.Entry();
		leftDrag.eventID = EventTriggerType.Drag;
		leftDrag.callback.AddListener((data) => { OnLeftDrag((PointerEventData)data); });
		leftTrigger.triggers.Add(leftDrag);
		
		// 上スケーラ処理
		EventTrigger.Entry topDown = new EventTrigger.Entry();
		topDown.eventID = EventTriggerType.PointerDown;
		topDown.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		topTrigger.triggers.Add(topDown);
		
		EventTrigger.Entry topDrag = new EventTrigger.Entry();
		topDrag.eventID = EventTriggerType.Drag;
		topDrag.callback.AddListener((data) => { OnTopDrag((PointerEventData)data); });
		topTrigger.triggers.Add(topDrag);
		
		// 下スケーラ処理
		EventTrigger.Entry bottomDown = new EventTrigger.Entry();
		bottomDown.eventID = EventTriggerType.PointerDown;
		bottomDown.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		bottomTrigger.triggers.Add(bottomDown);
		
		EventTrigger.Entry bottomDrag = new EventTrigger.Entry();
		bottomDrag.eventID = EventTriggerType.Drag;
		bottomDrag.callback.AddListener((data) => { OnBottomDrag((PointerEventData)data); });
		bottomTrigger.triggers.Add(bottomDrag);
		
		// 中心移動処理
		EventTrigger.Entry down = new EventTrigger.Entry();
		down.eventID = EventTriggerType.PointerDown;
		down.callback.AddListener((data) => { OnScalerDown((PointerEventData)data); });
		centerTrigger.triggers.Add(down);
		
		EventTrigger.Entry drag = new EventTrigger.Entry();
		drag.eventID = EventTriggerType.Drag;
		drag.callback.AddListener((data) => { OnCenterDrag((PointerEventData)data); });
		centerTrigger.triggers.Add(drag);
	}
	
	private void OnScalerDown(PointerEventData data)
	{
		prevPos = data.position;
	}
	
	private void OnRightDrag(PointerEventData data)
	{
		Vector2 deltaPos = data.position - prevPos;
		Vector2 newScale = (transform as RectTransform).sizeDelta;
		newScale.x += deltaPos.x;
		
		// 辺の長さが最低値を切るようであれば終了
		if (newScale.x < MIN_SIZE) return;
		
		(transform as RectTransform).sizeDelta = newScale;

		Vector2 targetPos = transform.position;
		targetPos.x += deltaPos.x / 2;
		transform.position = targetPos;
		
		prevPos = data.position;
	}
	
	private void OnLeftDrag(PointerEventData data)
	{
		Vector2 deltaPos = data.position - prevPos;
		Vector2 newScale = (transform as RectTransform).sizeDelta;
		newScale.x -= deltaPos.x;
		
		// 辺の長さが最低値を切るようであれば終了
		if (newScale.x < MIN_SIZE) return;
		
		(transform as RectTransform).sizeDelta = newScale;

		Vector2 targetPos = transform.position;
		targetPos.x += deltaPos.x / 2;
		transform.position = targetPos;
		
		prevPos = data.position;
	}
	
	private void OnTopDrag(PointerEventData data)
	{
		Vector2 deltaPos = data.position - prevPos;
		Vector2 newScale = (transform as RectTransform).sizeDelta;
		newScale.y += deltaPos.y;
		
		// 辺の長さが最低値を切るようであれば終了
		if (newScale.y < MIN_SIZE) return;
		
		(transform as RectTransform).sizeDelta = newScale;

		Vector2 targetPos = transform.position;
		targetPos.y += deltaPos.y / 2;
		transform.position = targetPos;
		
		prevPos = data.position;
	}
	
	private void OnBottomDrag(PointerEventData data)
	{
		Vector2 deltaPos = data.position - prevPos;
		Vector2 newScale = (transform as RectTransform).sizeDelta;
		newScale.y -= deltaPos.y;
		
		// 辺の長さが最低値を切るようであれば終了
		if (newScale.y < MIN_SIZE) return;
		
		(transform as RectTransform).sizeDelta = newScale;

		Vector2 targetPos = transform.position;
		targetPos.y += deltaPos.y / 2;
		transform.position = targetPos;
		
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
