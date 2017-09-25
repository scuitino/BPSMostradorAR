using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuManager : MonoBehaviour {

    [Header("ListItems"), SerializeField]
    GameObject _itemsList;
    [SerializeField]
    List<GameObject> _itemInfoPanels;
    GameObject _actualItem;

    private void Start()
    {
        _actualItem = _itemsList;
    }

    public void OpenInfoPanel(int pSelectedItem)
    {
        _actualItem.SetActive(false);
        _actualItem = _itemInfoPanels[pSelectedItem];
        _actualItem.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        _actualItem.SetActive(false);
        _actualItem = _itemsList;
        _actualItem.SetActive(true);
    }
}
