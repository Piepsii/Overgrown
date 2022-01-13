using UnityEngine;
using Overgrown.GameManager;

public class Player : MonoBehaviour
{
    public bool enableControls = false;

    Camera cam;
    [SerializeField]
    Transform focus = default;
    [SerializeField, Range(1f, 200f)]
    float distance = 150f;
    [SerializeField, Range(1f, 300f)]
    float minDistance = 30f, maxDistance = 200f;
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;
    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    Vector3 focusPoint, previousFocusPoint;
    Vector2 orbitAngles = new Vector2(45f, 0f);
    string leftMouseButton = "LMB";
    string rightMouseButton = "RMB";
    bool needsSetup = true;

    private void Awake()
    {
        focus = GameObject.FindGameObjectWithTag("Focus").transform;
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
        cam = Camera.main;
    }

    private void Start()
    {
        if (GameManager.Instance.Player == null)
        {
            GameManager.Instance.Player = this;
        }
        CalculateCameraPosition();
        needsSetup = false;
    }

    private void Update()
    {
        if (!enableControls)
            return;

        CheckInput();

        if(Input.GetButton(rightMouseButton))
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }

    private void LateUpdate()
    {
        if (!enableControls)
            AutomaticRotation();
        CalculateCameraPosition();
    }

    private void CalculateCameraPosition()
    {
        Quaternion lookRotation;
        if (ManualRotation() || needsSetup)
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = cam.transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        distance -= Input.mouseScrollDelta.y;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        cam.transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
        if (maxDistance < minDistance)
        {
            maxDistance = minDistance;
        }
    }

    private void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);
        if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
        else if (orbitAngles.y <= 0f)
        {
            orbitAngles.y += 360f;
        }
    }

    private bool ManualRotation()
    {
        Vector2 input = new Vector2(
            -Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X")
            );
        const float e = 0.001f;
        if ((input.x < -e || input.x > e || input.y < -e || input.y > e) && Input.GetButton(rightMouseButton))
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        return false;
    }

    private void AutomaticRotation()
    {
        orbitAngles.y += rotationSpeed * Time.unscaledDeltaTime;
    }

    private void CheckInput()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            var tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                var tileID = tile.unique_ID;
                var levelManager = GameManager.Instance.LevelManager;
                if (Input.GetButtonDown(leftMouseButton))
                { 
                    levelManager.ToggleCellState(tileID);
                }
                else if (Input.GetButtonDown(rightMouseButton))
                {
                    levelManager.CrossCell(tileID);
                }
            }
        }
    }
}
