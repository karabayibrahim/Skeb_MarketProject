using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public static SwerveMovement Instance;
    public float swerveSpeedVertical;
    public float swerveSpeedHorizontal;
    //public float runSpeed;
    private float _lastFrameFingerPositionX;
    private float _lastFrameFingerPositionY;
    private float _moveFactorX;
    private float _moveFactorY;
    public float MoveFactorX => _moveFactorX;
    public float MoveFactorY => _moveFactorY;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        //Debug.Log(MoveFactorX);
        MovementInput();
        float swerveAmountVertical = Time.deltaTime * swerveSpeedVertical* MoveFactorX;
        float swerveAmounHorizontal = Time.deltaTime * swerveSpeedHorizontal * MoveFactorY;
        //Debug.Log(swerveAmount);
        transform.Translate(swerveAmounHorizontal, 0, -swerveAmountVertical);
        //gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * runSpeed;
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            _lastFrameFingerPositionY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _moveFactorY = Input.mousePosition.y - _lastFrameFingerPositionY;
            _lastFrameFingerPositionX = Input.mousePosition.x;
            _lastFrameFingerPositionY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            _moveFactorY = 0f;
        }
    }

}

