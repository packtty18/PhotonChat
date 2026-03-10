using System;
using TMPro;
using UnityEngine;

public class UI_ChatHeader : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _userCount;
    private void OnEnable()
    {
        ChatManager.OnSubscribersChanged += Refresh;
    }

    private void OnDisable()
    {
        ChatManager.OnSubscribersChanged -= Refresh;
    }

    private void Refresh()
    {
        _userCount.text = $"{ChatManager.Instance.ChannelSubscribersCount}명 구독중";
    }
}
