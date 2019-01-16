using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Values to tweak for the movement. 
    public float walkSpeed = 5f;

    // We walk by using AddForce, so we have to specify how fast we accelerate
    public float walkAccelerationForce = 100f;

    public float jumpForce = 500f;

    // A constant for damping our movement when we stop walking or jumping.
    public float dampingK = 10f;

    // Controls
    public KeyCode walkRightKey = KeyCode.RightArrow;
    public KeyCode walkLeftKey = KeyCode.LeftArrow;
    public KeyCode jumpKey = KeyCode.Space;

    // If we fall too far, we lose
    public float loseYPos = -6f;


    [HideInInspector]
    public bool onGround = false;

    protected Rigidbody2D _body;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
    }

    // Use update to check for jumping (since jumping is a one-time thing)
    void Update() {
        if (onGround && Input.GetKeyDown(jumpKey)) {
            onGround = false;
            _body.AddForce(jumpForce * Vector2.up);
        }

        if (transform.position.y < loseYPos) {
            EndScene.loadLoseScene();
        }
    }

    // Meanwhile, use fixed update to check for continuous movement like walking. 
    void FixedUpdate() {
        bool walkingRight = false;
        if (Input.GetKey(walkRightKey) && _body.velocity.x < walkSpeed) {
            _body.AddForce(walkAccelerationForce * Vector2.right);
            walkingRight = true;
        }
        bool walkingLeft = false;
        if (Input.GetKey(walkLeftKey) && _body.velocity.x > -walkSpeed) {
            _body.AddForce(-walkAccelerationForce * Vector2.right);
            walkingLeft = true;
        }
        // If we're moving too fast or we've stopped pressing move keys, apply a damping force
        if (Mathf.Abs(_body.velocity.x) > walkSpeed || (!walkingLeft && !walkingRight)) {
            _body.AddForce(-dampingK * _body.velocity.x * Vector2.right);
        }
        // Useful trick for variable jumps. Just damp our y velocity when we release the key (only during mid jump)
        if (!onGround && _body.velocity.y > 0 && !Input.GetKey(jumpKey)) {
            _body.AddForce(-dampingK * _body.velocity.y * Vector2.up);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        // We have two colliders, so confirm that our feet collider was involved before allowing us to jump again.
        if (collisionInfo.otherCollider.gameObject.CompareTag("Feet")) {
            onGround = true;
        }
    }
}
