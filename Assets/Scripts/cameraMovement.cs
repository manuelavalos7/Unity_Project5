using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform lookAt;//player object(for camera to be able to look at player)
    public Transform camTransform;//transform of this camera

    private float distance = 14f;//distance of camera from player
    private float currentX = 0f;//current y offset given by mouse ( to calculate angle)
    private float currentY = 0f;//current y offset given by mouse ( to calculate angle)
    private float sensitivityX = 4f;//horizontal sensitivity of the camera
    private float sensitivityY = 2f;//vertical sensitivity of the camera
    private Quaternion projectileRotation;//rotation that new projectile will be instantiated
    private float coolDown = 6.5f;//cooldown timer for after firing projectile
    private float charge = 6f;//charge up timer before projectile instantiated
    private bool animationTriggered = false;//flag to make sure animattion is not triggered more than once each time
    private bool fire = false;//flag for starting fire animation and instantiating projectile
    public AudioSource[] audioSources;//sounds to be played by this script

    private const float Y_ANGLE_MIN = -50f;//min angle that mouse can move the camera
    private const float Y_ANGLE_MAX = 75f;//max angle that mouse can move the camera


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//hides mouse from screen for third person camera
        camTransform = transform;//for better understanding of which transform will be changed

    }

    private void Update()
    {
        coolDown -= Time.deltaTime * 10;//cooldown for magic ball decreased each frame
        currentX += Input.GetAxis("Mouse X") * sensitivityX;//current x rotation of camera (for camera rotation calulation)
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;//current y rotation of camera (for camera rotation calulation)
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);//clamp the camera so that it will not go too far above/below 

    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);//how far the camera should be from player
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);//rotation of the camera based on mouse movement
        camTransform.position = lookAt.position + rotation * dir;//place camera at player position but with an offset of the determined direction
        camTransform.LookAt(lookAt.position + Vector3.up * 2);//look at the player (offset of 2)
        projectileRotation = Quaternion.Euler(currentY - 15, currentX, 0);//rotation of the projectile so it will be launched at appropriate angle
        lookAt.rotation = Quaternion.Euler(0, currentX, 0);//change x rotation of player to match rotation of camera (horizontal only)

    }

}
