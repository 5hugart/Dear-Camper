using UnityEngine;

public class TouchController : MonoBehaviour
{
    public FixedTouchField _FixedTouchField;
    public CameraLook _CameraLook;
    public PlayerMove _PlayerMove;
    public FixedButton _FixedButton;

    void Update()
    {
        if (_CameraLook != null && _FixedTouchField != null)
        {
            _CameraLook.LockAxis = _FixedTouchField.TouchDist;
        }

        if (_PlayerMove != null && _FixedButton != null)
        {
            _PlayerMove.Pressed = _FixedButton.Pressed;
        }
    }
}