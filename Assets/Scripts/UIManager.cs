using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _collectableText;

    public void UpdateCollectableDisplay(int collectable)
    {
        _collectableText.text = "x" + collectable.ToString("00");
    }
}
