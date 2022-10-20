using UnityEngine;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private GameObject _back;
    [SerializeField] private GameObject _highlight;

    public void ToggleBack()
    {
        _back.SetActive(!_back.activeInHierarchy);
    }

    public void ToggleBack(bool status)
    {
        _back.SetActive(status);
    }

    public void ToggleHighlight(bool status)
    {
        _highlight.SetActive(status);
    }

    public void ResetVisuals()
    {
        ToggleBack(false);
        ToggleHighlight(false);
    }
}
