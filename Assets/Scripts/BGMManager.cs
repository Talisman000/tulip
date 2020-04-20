using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UniRx;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource atmosphereBGM;
    [SerializeField] AudioSource mainBGM;
    [SerializeField] float duration = 1;
    [SerializeField] float maxVolume = 1;
    // Start is called before the first frame update
    void Start()
    {
        atmosphereBGM.volume = maxVolume;
        atmosphereBGM.Play();
        GameManager.isGame.Skip(1).Subscribe(flag =>
        {
            if (flag)
            {
                CrossFadeBGM(atmosphereBGM, mainBGM);
            }
            else
            {
                CrossFadeBGM(mainBGM, atmosphereBGM);
            }
        });
    }


    void CrossFadeBGM(AudioSource prev, AudioSource next)
    {
        StartCoroutine(CrossFadeCoroutine(prev, next));
    }
    IEnumerator CrossFadeCoroutine(AudioSource prev, AudioSource next)
    {
        prev.volume = maxVolume;
        next.volume = 0;
        next.Play();
        prev.DOFade(0, duration);
        yield return new WaitForSeconds(duration / 2);
        prev.Stop();
        next.DOFade(maxVolume, duration);
        yield break;
    }

}
