using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidatePlayButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().interactable = PlayerPersistence.GetPlayerDeck().Count > 0;
    }
}
