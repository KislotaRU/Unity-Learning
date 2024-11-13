using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private FoodStepsSounds _stepsSounds;
    [SerializeField] private float _stepDistance;

    private float _coveredDistance = 0f;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * _rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        float direction = Input.GetAxis(Vertical);

        if (direction == 0f)
        {
            _coveredDistance = 0f;
            return;
        }

        float distance = direction * _moveSpeed * Time.deltaTime;
        _coveredDistance += Mathf.Abs(distance);

        transform.Translate(distance * Vector3.forward);

        if (_coveredDistance >= _stepDistance)
        {
            _coveredDistance -= _stepDistance;
            _stepsSounds.Play();
        }
    }
}