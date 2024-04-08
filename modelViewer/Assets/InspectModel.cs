using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectModel : MonoBehaviour
{
    private Transform examinedObject;
    private bool isExamining = false;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        examinedObject = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            ToggleExamination();

            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.CompareTag("Object"))
            //    {
            //        Debug.Log(hit);
            //    }
            //}
        }

        if(isExamining)
        {
            Examine();
            StartExamination();
        }
    }

    void Examine()
    {
        if (examinedObject != null)
        {
            //examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = .05f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    public void ToggleExamination()
    {
        isExamining = !isExamining;

    }

    void StartExamination()
    {

        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //_playerInput.enabled = false;
    }

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //_playerInput.enabled = true;
    }
}
