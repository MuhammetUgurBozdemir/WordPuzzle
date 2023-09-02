using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Scriptables;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class TileView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;
    [SerializeField] private GameObject disablePanel;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image hitTarget;
 
    public void Init(TileData tileData)
    {
        letter.text = tileData.character;
        transform.position = tileData.position;
    }

    public void SetTileState(bool state)
    {
        disablePanel.SetActive(state);
        canvas.overrideSorting = true;
        canvas.sortingOrder = state ? 0 : 1;
        hitTarget.raycastTarget = !state;
    }
}