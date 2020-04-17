using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class PlayerActionHandler : MonoBehaviour
{
    IPlayerInput playerInput;
    [SerializeField] ToolsController toolsController;
    GameObject onCharacterTulip;
    private Subject<CharacterInfo> ChangeCharacterSubject = new Subject<CharacterInfo>();
    public IObservable<CharacterInfo> ChangeCharacterObservable{
        get {return ChangeCharacterSubject;}
    }
    float weaponAngle = 90;
    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        playerInput.OnSeedButtonObservable
            .Where(flag => flag)
            .Subscribe(flag => Seed())
            .AddTo(gameObject);
        playerInput.OnWaterButtonObservable
            .Subscribe(flag => Water(flag))
            .AddTo(gameObject);
        playerInput.OnWeaponButtonObservable
            .Where(flag => flag)
            .Subscribe(flag => Weapon())
            .AddTo(gameObject);
        playerInput.OnChangeButtonObservable
            .Where(flag => flag)
            .Subscribe(flag => ChangeCharacter())
            .AddTo(gameObject);
        playerInput.MoveAngleReactiveProperty
            .Subscribe(angle => weaponAngle = angle)
            .AddTo(gameObject);
        
    }

    private void Seed()
    {
        if (toolsController == null) return;
        toolsController.InstantiateSeed();
    }

    private void Water(bool flag)
    {
        if (toolsController == null) return;
        toolsController.WaterPlay(flag);
    }

    private void Weapon()
    {
        if (toolsController == null) return;
        toolsController.WeaponAttack(weaponAngle);
    }

    private void ChangeCharacter()
    {
        if (onCharacterTulip == null) return;
        if (toolsController != null)
        {
            Destroy(toolsController.gameObject);
        }
        Tulip tulip = onCharacterTulip.GetComponent<Tulip>();
        ToolsController tool = Instantiate(tulip.characterInfo.toolsController);
        tool.transform.SetParent(transform);
        tool.transform.localPosition = Vector2.zero;
        ChangeCharacterSubject.OnNext(tulip.characterInfo);
        toolsController = tool;
        Debug.Log(String.Format("Changed -> {0}",tulip.characterInfo.characterType));
        StartCoroutine(tulip.DestroyCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "tulip")
        {
            onCharacterTulip = other.gameObject;
            Debug.Log("tulip");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (onCharacterTulip != other.gameObject)
        {
            onCharacterTulip = null;
        }
    }
}