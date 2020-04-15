using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public interface IPlayerInput
{
    IObservable<bool> OnSeedButtonObservable{get;}
    IObservable<bool> OnWaterButtonObservable{get;}
    ReactiveProperty<Vector2> MoveDirectionReactiveProperty{ get; set; }
}