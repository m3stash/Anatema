using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour {
    [Header("Fields to complete")]
    [SerializeField] private float defaultScreenY;
    [SerializeField] private float minScreenY;
    [SerializeField] private float maxScreenY;
    [SerializeField] private float moveSpeedScreenY;

    [Header("Don't touch it")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineFramingTransposer framingTransposer;
    [SerializeField] private bool decreasingScreenY;
    [SerializeField] private bool increasingScreenY;
    [SerializeField] private bool needToReset;

    public static CameraManager instance;

    private void Awake() {
        instance = this;

        this.virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        this.framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Start() {
        this.framingTransposer.m_ScreenY = this.defaultScreenY;
        InputManager.gameplayControls.Camera.LookVertical.performed += OnLookingVertically;
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Camera.LookVertical.performed -= OnLookingVertically;
    }

    private void Update() {
        if(this.decreasingScreenY) {
            DecreaseScreenY();
        } else if(this.increasingScreenY) {
            IncreaseScreenY();
        } else if(this.needToReset) {
            ResetScreenY();
        }
    }

    private void OnLookingVertically(InputAction.CallbackContext ctx) {
        float lookDirection = ctx.ReadValue<float>();

        this.SetIsIncreasingScreenY(lookDirection == 1);

        this.SetIsDecreasingScreenY(lookDirection == -1);
    }

    public void SetIsDecreasingScreenY(bool value) {
        this.decreasingScreenY = value;
    }

    public void SetIsIncreasingScreenY(bool value) {
        this.increasingScreenY = value;
    }

    public void DecreaseScreenY() {
        if(this.framingTransposer.m_ScreenY > this.minScreenY) {
            this.framingTransposer.m_ScreenY -= this.moveSpeedScreenY * Time.deltaTime;
            this.needToReset = true;
        }
    }

    public void IncreaseScreenY() {
        if(this.framingTransposer.m_ScreenY < this.maxScreenY) {
            this.framingTransposer.m_ScreenY += this.moveSpeedScreenY * Time.deltaTime;
            this.needToReset = true;
        }
    }

    public void ResetScreenY() {
        int direction = (this.defaultScreenY - this.framingTransposer.m_ScreenY) > 0 ? 1 : -1;
        this.framingTransposer.m_ScreenY += direction * (this.moveSpeedScreenY * 2 * Time.deltaTime);

        if(Mathf.Abs(this.framingTransposer.m_ScreenY - this.defaultScreenY) <= 0.01f) {
            this.needToReset = false;
        }
    }
}
