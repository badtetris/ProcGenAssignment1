using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    protected Rigidbody2D _body;
    protected PlayerMovement _movement;
    protected Animator _anim;
    protected SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        _body = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        bool walking = _movement.onGround 
                    && _body.velocity.x != 0 
                    && (Input.GetKey(_movement.walkRightKey) || Input.GetKey(_movement.walkLeftKey));

        _spriteRenderer.flipX = _body.velocity.x < 0;

        bool jumping = _body.velocity.y > 0;
        bool falling = _body.velocity.y < 0 && !_movement.onGround;

        _anim.SetBool("Walking", walking);

        // Hey, I reused an old animator so I 'm just using the climbing animations to represent a jump.
        _anim.SetBool("Climbing", jumping);
        _anim.SetBool("OnLadder", jumping);

        _anim.SetBool("OnGround", _movement.onGround);
    }
}
