using System;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Subject<bool> OnActionButtonSubject = new Subject<bool>();
    private Subject<bool> OnChangeButtonSubject = new Subject<bool>();

    public IObservable<bool> OnActionButtonObservable
    {
        get { return OnActionButtonSubject; }
    }

    public IObservable<bool> OnChangeButtonObservable
    {
        get { return OnChangeButtonSubject; }
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
            OnActionButtonSubject.OnNext(true);
        }
        else
        {
            OnActionButtonSubject.OnNext(false);
        }
        if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Colon))
        {
            OnChangeButtonSubject.OnNext(true);
        }
        else
        {
            OnChangeButtonSubject.OnNext(false);
        }
    }
}