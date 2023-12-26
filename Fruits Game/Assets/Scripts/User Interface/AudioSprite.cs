using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSprite : MonoBehaviour
{
    [SerializeField]
    Sprite Img1;

    private void Start()
    {
        if(AudioManager.audioSource.clip == null)
        {
            GetComponent<Image>().sprite = Img1;
        }
    }
}
