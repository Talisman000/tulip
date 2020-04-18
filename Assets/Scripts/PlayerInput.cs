using System;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Subject<bool> OnSeedButtonSubject = new Subject<bool>();
    private Subject<bool> OnWaterButtonSubject = new Subject<bool>();
    private Subject<bool> OnWeaponButtonSubject = new Subject<bool>();
    private Subject<bool> OnChangeButtonSubject = new Subject<bool>();
    private Subject<bool> OnStartButtonSubject = new Subject<bool>();



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
    public IObservable<bool> OnStartButtonObservable
    {
        get { return OnStartButtonSubject; }
    }

    public ReactiveProperty<Vector2> MoveDirectionReactiveProperty { get; set; } = new ReactiveProperty<Vector2>();
    public ReactiveProperty<float> MoveAngleReactiveProperty { get; set; } = new ReactiveProperty<float>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            OnStartButtonSubject.OnNext(true);
        }
        if (!GameManager.isGame.Value) return;
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
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Colon))
        {
            OnSeedButtonSubject.OnNext(true);
        }
        else
        {
            OnSeedButtonSubject.OnNext(false);
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Slash))
        {
            OnWaterButtonSubject.OnNext(true);
        }
        else if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Slash))
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
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backslash))
        {
            OnChangeButtonSubject.OnNext(true);
        }
        else
        {
            OnChangeButtonSubject.OnNext(false);
        }
    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 30, 200, 60), string.Format("Direction:{0}\nAngle:{1}", MoveDirectionReactiveProperty.Value, MoveAngleReactiveProperty.Value));
    }
#endif
}