using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public PauseMenu pauseMenu;

    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2,
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float mouseHor = 9.0f;
    public float mouseVert = 9.0f;

    public float minVert = -45f;
    public float maxVert = 45f;

    private float vertRot = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (PauseMenu.isGameActive)
        {
            if (axes == RotationAxes.MouseX)
            {
                // Horizontal Movement
                transform.Rotate(0, Input.GetAxis("Mouse X") * mouseHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                // Vertical Movement
                vertRot -= Input.GetAxis("Mouse Y") * mouseVert;
                vertRot = Mathf.Clamp(vertRot, minVert, maxVert);

                float horRot = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(vertRot, horRot, 0);

            }
            else
            {
                // Horizontal and Vertical Movement
                vertRot -= Input.GetAxis("Mouse Y") * mouseVert;
                vertRot = Mathf.Clamp(vertRot, minVert, maxVert);

                float delta = Input.GetAxis("Mouse X") * mouseHor;
                float horRot = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(vertRot, horRot, 0);
            }


        }
    }
}
