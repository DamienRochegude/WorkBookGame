using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float minRotX, maxRotX, minRotY, maxRotY;

    [SerializeField]
    private AnimationCurve NonLinearSlerpCamera, NonLinearZoomCamera;

    [SerializeField]
    private float boardZoom;

    [SerializeField]
    private float TransitionTimeToPause;

    [SerializeField]
    private GameObject Player, Board;

    [SerializeField]
    private bool isInPause = true;


    private bool hasNegativeValueX, hasNegativeValueY = false;
    private float minCameraFov, maxCameraFov, BoardDist;
    private float NonLinearTransition, mixTransition = 0;
    private Vector3 v3_PlayerDir, v3_BoardDir;
    private Quaternion Qu_PlayerDir, Qu_BoardDir;
    private Camera cam;

    private bool isLaunched = false;

    float posx;

    public bool IsInPause { get => isInPause; }

    // Start is called before the first frame update
    void Start()
    {
        isInPause = true;
    }

    private void Update()
    {

        if (Player == null)
        {
            Player = CameraManager.PlayerPosConver;
        }
        if (Input.GetButtonDown("Pause"))
        {
            setPauseState(!isInPause);
        }
    }

    public void setPauseState(bool pause_state)
    {
        isInPause = pause_state;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLaunched)
        {
            if (isInPause)
            {
                mixTransition = 1f;
            }
            cam = GetComponent<Camera>();
            Player = CameraManager.PlayerPosConver;
            Board = CameraManager.BlackBoard;
            minCameraFov = cam.fieldOfView;
            BoardDist = Vector3.Distance(Player.transform.position, Board.transform.position);

            maxCameraFov = boardZoom * Mathf.Atan(Board.GetComponent<Renderer>().bounds.size.y * 0.5f / BoardDist) * Mathf.Rad2Deg;

            if (minRotX < 0)
            {
                hasNegativeValueX = true;
            }
            if (minRotY < 0)
            {
                hasNegativeValueY = true;
            }
            isLaunched = true;
        }
        v3_BoardDir = (Board.GetComponent<Renderer>().bounds.center - transform.position);
        v3_PlayerDir = (Player.transform.position - transform.position); //We get the distance between the two objects

        Qu_PlayerDir = Quaternion.LookRotation(v3_PlayerDir);
        Qu_BoardDir = new Quaternion(0, 0, 0, 1);
        //Qu_BoardDir = Quaternion.LookRotation(v3_BoardDir); //We transform the distance to quaternion

        v3_PlayerDir = Qu_PlayerDir.eulerAngles;


        if (hasNegativeValueX && v3_PlayerDir.x > maxRotX)
        {
            if (v3_PlayerDir.x - 360 > minRotX)
            {
                v3_PlayerDir.x -= 360;
            }
            else if ((minRotX + 360) - v3_PlayerDir.x <= ((-minRotX + maxRotX) / 2))
            {
                v3_PlayerDir.x -= 360;
            }
        }

        if (hasNegativeValueY && v3_PlayerDir.y > maxRotY)
        {
            if (v3_PlayerDir.y - 360 > minRotY)
            {
                v3_PlayerDir.y -= 360;
            }
            else if ((minRotY + 180) - v3_PlayerDir.y <= ((-minRotY + maxRotY) / 2))
            {
                v3_PlayerDir.y -= 360;
            }
        }


        v3_PlayerDir = new Vector3(Mathf.Clamp(v3_PlayerDir.x, minRotX, maxRotX),
            Mathf.Clamp(v3_PlayerDir.y, minRotY, maxRotY),
            0);



        Qu_PlayerDir = Quaternion.Euler(v3_PlayerDir);

        if (!isInPause)
        {
            mixTransition = Mathf.Clamp(mixTransition - Time.fixedDeltaTime * (1 / TransitionTimeToPause), 0f, 1f);
            NonLinearTransition = NonLinearSlerpCamera.Evaluate(mixTransition);
        }
        else
        {
            mixTransition = Mathf.Clamp(mixTransition + Time.fixedDeltaTime * (1 / TransitionTimeToPause), 0f, 1f);
            NonLinearTransition = NonLinearSlerpCamera.Evaluate(mixTransition);
        }
        transform.rotation = Quaternion.Slerp(Qu_PlayerDir, Qu_BoardDir, NonLinearTransition);
        NonLinearTransition = NonLinearZoomCamera.Evaluate(mixTransition);
        //cam.fieldOfView = Mathf.Lerp(minCameraFov, maxCameraFov, NonLinearTransition);
    }


}