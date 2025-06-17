using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform Camera;
    public float sensitivity = 100f;
    public float maxYAngle = 60f; // Giới hạn quay ngang tối đa (tương đối, ví dụ: ±60 độ)

    public CharacterController characterController;

    private float xRot, yRot;
    private float initialYRot; // Lưu trữ hướng ban đầu của nhân vật

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Khởi tạo hướng ban đầu
        initialYRot = transform.localRotation.eulerAngles.y; // Lưu hướng ban đầu
        yRot = initialYRot; // Khởi tạo yRot bằng hướng ban đầu
        xRot = Camera.localRotation.eulerAngles.x;
    }

    void Update()
    {
        // Lấy input chuột
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Cập nhật góc quay
        yRot += mouseX;
        xRot -= mouseY;

        // Giới hạn góc quay ngang tương đối so với hướng ban đầu
        float relativeYRot = yRot - initialYRot; // Tính góc tương đối
        relativeYRot = Mathf.Clamp(relativeYRot, -maxYAngle, maxYAngle); // Giới hạn trong khoảng ±maxYAngle
        yRot = initialYRot + relativeYRot; // Cập nhật yRot dựa trên góc tương đối

        // Giới hạn góc quay dọc (cho phép nhìn lên và xuống)
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // Quay player (ngang) và camera (dọc)
        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        Camera.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}