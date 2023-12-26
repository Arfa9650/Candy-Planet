using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetName : MonoBehaviour
{
    [SerializeField]
    GameObject MainCanvas;

    [SerializeField]
    GameObject GetNameCanvas;

    [SerializeField]
    TMP_InputField getName;

    [SerializeField]
    GameObject empty;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Username"))
        {
            MainCanvas.SetActive(false);
            GetNameCanvas.SetActive(true);
        }
    }

    public void SetName()
    {
        AudioManager.Play(AudioClipNames.Button);
        if (string.IsNullOrWhiteSpace(getName.text))
        {
            empty.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("Username", getName.text);
            MainCanvas.SetActive(true);
            GetNameCanvas.SetActive(false);
        }

    }
}
