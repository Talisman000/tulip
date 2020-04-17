using System;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Subject<bool> OnSeedButtonSubject = new Subject<bool>();
    private Subject<bool> OnWaterButtonSubject = new Subject<bool>();
    private Subject<bool> OnWeaponButtonSubject = new Subject<bool>();
    private Subject<bool> OnChangeButtonSubject = new Subject<bool>();


    public IObservable<bool> OnSeedButtonObservable
    {
        get { return OnSeedButtonSubject; }
    }
    public IObservable<bool> OnWaterButtonObservable
    {
        get { return OnWaterButtonSubject; }
    }
    public IObservable<bool> OnWeaponButtonObservable
    {
        get { return OnWeaponButtonSubject; }
    }
    public IObservable<bool> OnChangeButtonObservable
    {
        get { return OnChangeButtonSubject; }
    }

    public ReactiveProperty<Vector2> MoveDirectionReactiveProperty { get; set; } = new ReactiveProperty<Vector2>();
    public ReactiveProperty<float> MoveAngleReactiveProperty { get; set; } = new ReactiveProperty<float>();

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector2 moveDirection = new Vector2(moveX, moveY);
            MoveDirectionReactiveProperty.Value = moveDirection;
            if (Mathf.Abs(moveX) + Mathf.Abs(moveY) > 0.2f)
            {
                float angle = (float)Math.Atan2(moveY, moveX);
                MoveAngleReactiveProperty.Value = angle * Mathf.Rad2Deg;
            }
        }
        else
        {
            MoveDirectionReactiveProperty.Value = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.M))
        {
            OnSeedButtonSubject.OnNext(true);
        }
        else
        {
            OnSeedButtonSubject.OnNext(false);
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Semicolon))
        {
            OnWaterButtonSubject.OnNext(true);
        }
        else if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Semicolon))
        {
            OnWaterButtonSubject.OnNext(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            OnWeaponButtonSubject.OnNext(true);
        }
        else
        {
            OnWeaponButtonSubject.OnNext(false);
        }
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Colon))
        {
            OnChangeButtonSubject.OnNext(true);
        }
        else
        {
            OnChangeButtonSubject.OnNext(false);
        }
    }
}