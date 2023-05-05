using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    private Transform cameraTransform;
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = cameraTransform.position;

        currentPosition.y = player.transform.position.y;

        cameraTransform.position = currentPosition;
    }
}
