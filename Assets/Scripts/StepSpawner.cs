using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject step;
    [SerializeField] private float stepSpawnHigh;
    [SerializeField] private float stepBounds;
    [SerializeField] private float stepNum;
    [SerializeField] private float maxAngle = 6f;
    private void OnEnable()
    {
        SquareController.StepSpawnControl += StepSpawn;
    }
    private void OnDisable()
    {
        SquareController.StepSpawnControl -= StepSpawn;
    }
    private void Start()
    {
        step.transform.position = new Vector2(0, -4f);
        for (int i = 0; i < stepNum; i++)
        {
            step.transform.position = new Vector2(Random.Range(-stepBounds, stepBounds), step.transform.position.y + stepSpawnHigh);
            Instantiate(step, step.transform.position, Quaternion.identity);
        }
    }
    public void StepSpawn()
    {
        var newScale = step.transform.localScale;
        newScale.x = Random.Range(3, maxAngle);
        step.transform.localScale = newScale;

        step.transform.position = new Vector2(Random.Range(-stepBounds, stepBounds), step.transform.position.y + stepSpawnHigh);
        Instantiate(step, step.transform.position, Quaternion.identity);
    }
}
