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
    public ReactiveProperty<bool> isMove = new ReactiveProperty<bool>();
    // Start is called before the first frame update
    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        Rb2D = GetComponent<Rigidbody2D>();
        playerInput.MoveDirectionReactiveProperty
            .Skip(1)
            .Subscribe(x => Move(x))
            .AddTo(gameObject);
    }
    private void Update()
    {
        if (!GameManager.isGame.Value)
        {
            Rb2D.velocity = Vector2.zero;
        }
    }
    void Move(Vector2 direction)
    {

        if (direction != Vector2.zero)
        {
            isMove.Value = true;
        }
        else
        {
            isMove.Value = false;
        }
        Rb2D.velocity = direction * playerSpeed;
    }
}
