using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class PlayerStateController : MonoBehaviour
{
    PlayerActionHandler playerActionHandler;
    [SerializeField] int maxHP = 100;
    [SerializeField] int initialHP = 50;
    public ReactiveProperty<int> HP;
    IObservable<bool> OnCharacterDamaged;
    IObservable<bool> OnCharacterDead;

    private CharacterInfo characterInfo;

    private void Start()
    {
        HP = new ReactiveProperty<int>(initialHP);
        playerActionHandler = GetComponent<PlayerActionHandler>();
        playerActionHandler.ChangeCharacterObservable.Skip(1).Subscribe(x => ChangeCharacter(x)).AddTo(gameObject);
        StartCoroutine(DecreaseHPCoroutine());
    }

    private void Update() {
        if(HP.Value <= 0){
            GameManager.isResult.Value = true;
            GameManager.isGame.Value = false;
        }
    }

    private void ChangeCharacter(CharacterInfo character)
    {
        characterInfo = character;
        if (HP.Value + characterInfo.recoverHP <= maxHP)
        {
            HP.Value += characterInfo.recoverHP;
        }
        else
        {
            HP.Value = maxHP;
        }
    }

    IEnumerator DecreaseHPCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.isGame.Value) HP.Value--;
        }
    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), string.Format("HP:{0}", HP.Value));
    }
#endif

}