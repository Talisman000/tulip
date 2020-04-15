using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public interface IPlayerInput
{
    IObservable<bool> OnActionButtonObservable{get;}
    IObservable<bool> OnChangeButtonObservable{get;}
    ReactiveProperty<Vector2> MoveDirectionReactiveProperty{ get; set; }
}