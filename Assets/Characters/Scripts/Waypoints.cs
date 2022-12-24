using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Vector2[] Points;
    private int _currentWP = 1;
    private int currentWP
    {
        get { return _currentWP; }
        set { 
            _currentWP = value;
            if (_currentWP >= Points.Length)
            {
                _collider.enabled = true;
                GetComponentInChildren<Missile>().Attack();
                Destroy(this);
            }
        }
    }

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Start()
    {   
        transform.position = Points[0];
        _collider = GetComponentInChildren<Collider2D>();
        _collider.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentWP++;
        if (_currentWP >= Points.Length) return;
        _rb.MovePosition(Points[currentWP]);
    }

    private void OnDestroy()
    {
        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.Play();
        }
        if (GetComponentInChildren<Missile>() is CrowShall)
        {
            gameObject.SetActive(false);
        }
    }
}
