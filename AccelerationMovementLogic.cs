using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationMovementLogic : MonoBehaviour
{
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _maxSpeed = 10f;

    private Rigidbody2D _rigidBody;
    private Animator anim;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public Fillbar fb;
    private void Start()
    {
        fb = GetComponent<Fillbar>();
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 velocity = _rigidBody.velocity;
        if(move != Vector3.zero)
        {
            velocity += move * _acceleration * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -_maxSpeed, _maxSpeed);
            velocity.y = Mathf.Clamp(velocity.y, -_maxSpeed, _maxSpeed);
        }
        else
        {
            velocity.x += (velocity.x > 0 ? -1 : 1) * _acceleration * Time.deltaTime;// Mathf.Clamp(velocity.x, -_maxSpeed, _maxSpeed);
            velocity.y = (velocity.y > 0 ? -1 : 1) * _acceleration * Time.deltaTime;// Mathf.Clamp(velocity.y, -_maxSpeed, _maxSpeed);
        }        
        _rigidBody.velocity = velocity;
        dash();
    }
    private bool boostUsed = false;
    private float spdDash = 100f;

    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Fillbar.instance.currentDash >= 50)
        {
     
            Fillbar.instance.kurang(50);
            _rigidBody.AddForce(_rigidBody.velocity * spdDash);
            boostUsed = true;
        }
    }
}
