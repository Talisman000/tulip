using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    IPlayerInput playerInput;
    Rigidbody2D Rb2D;
    [SerializeField] float playerSpeed;
    [SerializeField] bool isMove = true;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        Rb2D = GetComponent<Rigidbody2D>();
        playerInput.MoveDirectionReactiveProperty
            .Skip(1)
            .Where(x => isMove)
            .Subscribe(x => Rb2D.velocity = x * playerSpeed)
            .AddTo(gameObject);
    }
}
