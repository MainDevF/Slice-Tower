using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField] private MovingPlate _platePrefab;

    [SerializeField] private MoveDirection _moveDirection;

    public void SpawnPlate()
    {
        var plate = Instantiate(_platePrefab);
        if(MovingPlate.LastPlate != null && MovingPlate.LastPlate.gameObject != GameObject.Find("StartPlate"))
        {
            float x = _moveDirection == MoveDirection.X ? transform.position.x : MovingPlate.LastPlate.transform.position.x;
            float z = _moveDirection == MoveDirection.Z ? transform.position.z : MovingPlate.LastPlate.transform.position.z;

            plate.transform.position = new Vector3(x, MovingPlate.LastPlate.transform.position.y + _platePrefab.transform.localScale.y, z);
        }
        else
        {
            plate.transform.position = transform.position;
        }
        plate.MoveDirection = _moveDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _platePrefab.transform.localScale);
    }
}

public enum MoveDirection
{
    X,
    Z
}