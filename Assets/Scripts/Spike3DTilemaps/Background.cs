using UnityEngine;
using System.Collections;
using static Globals;

public class Background : MonoBehaviour
{
    public float zoom;
    public int zPosition;
    private GameObject _camera;

    // Use this for initialization
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag(MainCameraTag);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, zPosition);
        this.transform.localScale = new Vector3(_camera.GetComponent<Camera>().orthographicSize * AspectRatio * zoom, _camera.GetComponent<Camera>().orthographicSize * zoom, 1);
    }
}
