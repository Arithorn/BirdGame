using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRender;

    public bool IsDragging { get; private set; }

    private void Awake() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRender = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("Starting Up");
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
    }
    void OnMouseDown() {
        _spriteRender.color = Color.red;
        IsDragging = true;

    }

    void OnMouseUp() {
        _spriteRender.color = Color.white;
        Vector2 currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction*_launchForce);
        IsDragging = false;
        

    }
    void OnMouseDrag() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
        
        float distance = Vector2.Distance(desiredPosition,_startPosition);
        if (distance > _maxDragDistance) {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        if (desiredPosition.x > _startPosition.x) desiredPosition.x = _startPosition.x;
        _rigidBody2D.position = desiredPosition;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ResetAfterDelay() {

        yield return new WaitForSeconds(3);
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;  
        _rigidBody2D.velocity = Vector2.zero;
    }
    void OnCollisionEnter2D(Collision2D other) {
        StartCoroutine(ResetAfterDelay());
    }
}
