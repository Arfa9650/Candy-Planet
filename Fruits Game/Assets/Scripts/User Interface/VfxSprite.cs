using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VfxSprite : MonoBehaviour
{
    [SerializeField]
    Sprite Img1;

    private void Start()
    {
        if(AudioManager.audioSource.mute == true)
        {
            GetComponent<Image>().sprite = Img1;
        }
    }
}
