using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoPlayController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private EventTrigger loadTrigger;
    

    private void Awake()
    {
        // videoPlayer.gameObject.SetActive(false);

        EventTrigger.Entry loadClick = new EventTrigger.Entry();
        loadClick.eventID = EventTriggerType.PointerClick;
        loadClick.callback.AddListener((data) => { PickVideo(); });
        loadTrigger.triggers.Add(loadClick);
    }

    // 動画の読み込みと再生
    private void PickVideo()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;
        
        // videoPlayer.gameObject.SetActive(true);
        
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            if (path != null)
            {
                // 動画サイズを取得する
                NativeGallery.VideoProperties properties = NativeGallery.GetVideoProperties(path);
                Vector2 size = new Vector2(properties.width, properties.height);
                Debug.Log($"movieSize:{size}");
                
                // 動画の再生
                videoPlayer.url = path;
                videoPlayer.Play();
            }
        }, "Select a video" );
    }
}
