using Assets.Scripts.NameTag;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Board board;
    private float minX, minY, maxX, maxY;
    private Camera cam;
    private Transform player;
    Vector3 os = new Vector3(0, 0, -10);
    Vector3 targetcam;
    private void Awake()
    {
        LoadComponents();
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_CAMERA_SETUP, SetUpCamera);
    }
    void Start()
    {
    
    }
    private void LateUpdate()
    {
        ControllerCamera();
    }
    void LoadComponents()
    {
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.player = GameObject.FindWithTag("Player").transform;
        this.cam = Camera.main;
    }
    void SetUpCamera()
    {
        this.cam.orthographicSize = this.board.height * 0.4f;
        float height = cam.orthographicSize;
        float with = cam.orthographicSize * cam.aspect;
        this.minX = with -1;
        this.minY = height -1;
        this.maxX = this.board.with  - with;
        this.maxY = this.board.height  - height;
        Vector3 startPos = this.player.position + this.os;
        startPos.x = Mathf.Clamp(startPos.x, this.minX, this.maxX);
        startPos.y = Mathf.Clamp(startPos.y, this.minY, this.maxY);
        this.transform.position = startPos;
    }
    void ControllerCamera()
    {
        this.targetcam = this.player.position + this.os;
        this.targetcam.x = Mathf.Clamp(this.targetcam.x, this.minX, this.maxX);
        this.targetcam.y = Mathf.Clamp(this.targetcam.y, this.minY, this.maxY);
        this.targetcam.z = this.os.z;
        this.transform.position = Vector3.Lerp(this.transform.position, this.targetcam, 7 * Time.deltaTime);
    }
    #region Event
    private void OnDestroy()
    {
        EventManager.OP_EventManager.Unsubscribe(EventName.EVENT_CAMERA_SETUP, SetUpCamera);
    }
    #endregion
}
