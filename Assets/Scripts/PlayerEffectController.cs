using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class PlayerEffectController : MonoBehaviour
{
    PlayerActionHandler playerActionHandler;
    [SerializeField] ParticleSystem changeEffect;
    [SerializeField] ParticleSystem zzz;
    [SerializeField] List<Color> changeEffectColor;
    // Start is called before the first frame update
    void Start()
    {
        playerActionHandler = GetComponent<PlayerActionHandler>();
        playerActionHandler.ChangeCharacterObservable.Subscribe(info => PlayChangeEffect(info.characterType));
        GameManager.isGame.Subscribe(flag => PlayZZZ(!flag));
    }

    void PlayChangeEffect(CharacterType characterType){
        ParticleSystem.MainModule main = changeEffect.main;
        main.startColor = changeEffectColor[(int)characterType];
        changeEffect.Play();
    }

    void PlayZZZ(bool flag){
        if(flag){
            zzz.Play();
        }
        else{
            zzz.Stop();
        }
    }
}