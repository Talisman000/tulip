using System;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Subject<bool> OnSeedButtonSubject = new Subject<bool>();
    private Subject<bool> OnWaterButtonSubject = new Subject<bool>();

    public IObservable<bool> OnSeedButtonObservable
    {
        get { return OnSeedButtonSubject; }
    }

    public IObservable<bool> OnWaterButtonObservable
    {
        get { return OnWaterButtonSubject; }
    }

    public ReactiveProperty<Vector2> MoveDirectionReactiveProperty { get; set; } = new ReactiveProperty<Vector2>();

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            MoveDirectionReactiveProperty.Value = new Vector2(moveX, moveY);
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.M))
        {
            OnSeedButtonSubject.OnNext(true);
        }
        else
        {
            OnSeedButtonSubject.OnNext(false);
        }
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Semicolon))
        {
            OnWaterButtonSubject.OnNext(true);
        }
        else
        {
            OnWaterButtonSubject.OnNext(false);
        }
    }
}