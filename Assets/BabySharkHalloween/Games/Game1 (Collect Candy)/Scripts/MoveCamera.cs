using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 3.5f;
    public float _speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_speed < speed)
            _speed += (Time.deltaTime * 2f);
        else
            _speed = speed;

        transform.position = new Vector3(transform.position.x + (Time.deltaTime * _speed), transform.position.y, -10f);
    }
}