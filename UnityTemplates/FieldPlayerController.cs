using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class FieldPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private Transform cameraTransform;

    public float DistanceMovedThisFrame { get; private set; }

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);

        var forward = cameraTransform != null ? cameraTransform.forward : Vector3.forward;
        var right = cameraTransform != null ? cameraTransform.right : Vector3.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        var move = (forward * input.y + right * input.x) * moveSpeed;
        var before = transform.position;
        _controller.Move(move * Time.deltaTime);
        DistanceMovedThisFrame = Vector3.Distance(before, transform.position);

        if (move.sqrMagnitude > 0.001f)
            transform.forward = Vector3.Slerp(transform.forward, move.normalized, 12f * Time.deltaTime);
    }
}
