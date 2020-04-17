using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class PlayerStateController:MonoBehaviour{
    PlayerActionHandler playerActionHandler;
    [SerializeField] float HP = 100;
    IObservable<bool> OnCharacterDamaged;
    IObservable<bool> OnCharacterDead;
    private CharacterInfo characterInfo;

    private void Start() {
        playerActionHandler = GetComponent<PlayerActionHandler>();
        playerActionHandler.ChangeCharacterObservable.Skip(1).Subscribe(x => ChangeCharacter(x)).AddTo(gameObject);
    }
    private void Update() {
        HP -= Time.deltaTime;
    }

    private void ChangeCharacter(CharacterInfo character){
        characterInfo = character;
        HP += characterInfo.recoverHP;
    }
    #if UNITY_EDITOR
    private void OnGUI() {
        GUI.Label(new Rect(0,0,100,30),string.Format("HP:{0}",HP));
    }
    #endif

}