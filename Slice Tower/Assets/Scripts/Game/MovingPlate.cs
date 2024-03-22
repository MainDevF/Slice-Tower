
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlate : MonoBehaviour
{
    public static Action OnGameOwer;
    public static MovingPlate CurrentPlate { get; private set; } 
    public static MovingPlate LastPlate { get; private set; }

    public MoveDirection MoveDirection;

    [SerializeField] private float moveSpeed = 1.0f;

    private void OnEnable()
    {
        if(LastPlate == null)
        {
            LastPlate = GameObject.Find("StartPlate").GetComponent<MovingPlate>();
        }

        CurrentPlate = this;

        transform.localScale = new Vector3(LastPlate.transform.localScale.x, transform.localScale.y, LastPlate.transform.localScale.z);
    }

    public bool Stop()
    {
        moveSpeed = 0f;
        float trimmings = GetTrimmings();

        float max = MoveDirection == MoveDirection.Z ? LastPlate.transform.localScale.z : LastPlate.transform.localScale.x;

        if (Mathf.Abs(trimmings) >= max)
        {
            LastPlate = null;
            CurrentPlate = null;
            OnGameOwer?.Invoke();
            gameObject.AddComponent<Rigidbody>();

            return false;
        }
        else
        {
            float direction = trimmings > 0 ? 1 : -1f;

            if (MoveDirection == MoveDirection.Z)
            {
                SplitPlateOnZ(trimmings, direction);
            }
            else
            {
                SplitPlateOnX(trimmings, direction);
            }

            LastPlate = this;

            return true;    
        }
    }

    private float GetTrimmings()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastPlate.transform.position.z;
        }
        else
        {
            return transform.position.x - LastPlate.transform.position.x;
        }
    }

    private void SplitPlateOnX(float trimmings, float direction)
    {
        float newXSize = LastPlate.transform.localScale.x - Mathf.Abs(trimmings);
        float fallingPlateSize = transform.localScale.x - newXSize;

        float newZposition = LastPlate.transform.position.x + (trimmings / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newZposition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingPlateSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingPlateSize);
    }

    private void SplitPlateOnZ(float trimmings, float direction)
    {
        float newZSize = LastPlate.transform.localScale.z - Mathf.Abs(trimmings);
        float fallingPlateSize = transform.localScale.z - newZSize;

        float newZposition = LastPlate.transform.position.z + (trimmings / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingPlateSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingPlateSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingPlateSize)
    {
        var plate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(MoveDirection == MoveDirection.Z)
        {
            plate.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingPlateSize);
            plate.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            plate.transform.localScale = new Vector3(fallingPlateSize, transform.localScale.y, transform.localScale.z);
            plate.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }

        plate.AddComponent<Rigidbody>();
        plate.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(plate.gameObject, 1f);
    }

    private void Update()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }

    }
}


