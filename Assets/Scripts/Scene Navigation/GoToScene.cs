using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Go);
    }


    public void Go()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
