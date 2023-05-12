using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public enum FollowSettings
    {
        Random,
        FollowObject
    }
    public static FlockManager FM;
    public GameObject fishPrefab;
    public int numberOfFishes;
    public GameObject[] allFish;
    public Vector3 tankLimits = new Vector3(5, 3, 5);
    public FollowSettings followSettings;
    public GameObject objectToFollow;
    public Vector3 goalPosition = Vector3.zero;
    [Range(0, 100)]
    [Tooltip("0 means speed isn't reset, 100 means speed resets every frame")]
    public float speedResetFrequency;
    [Range(0, 100)]
    [Tooltip("0 means flocking rules are never applied, 100 means flocking rules are applied every frame. Adjust this to reduce calculation load")]
    public float applyRuleFrequency;
    [Range(0, 100)]
    [Tooltip("0 means position doesn't change, 100 means position changes every frame")]
    public int goalPositionChangeFrequency;
    [Header("Fish properties")]
    [Range(0.0f, 5.0f)]
    public float minimumSpeed;
    [Range(0.0f, 5.0f)]
    public float maximumSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numberOfFishes];
        for(int i = 0; i < numberOfFishes; i++)
        {
            Vector3 fishPosition = this.transform.position + new Vector3(Random.Range(-tankLimits.x, tankLimits.x),
                                                                         Random.Range(-tankLimits.y, tankLimits.y),
                                                                         Random.Range(-tankLimits.z, tankLimits.z));
            allFish[i] = Instantiate(fishPrefab, fishPosition, Quaternion.identity);
        }
        FM = this;
        goalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(followSettings == FollowSettings.Random)
        {
            if (Random.Range(0, 100) < goalPositionChangeFrequency)
            {
                goalPosition = this.transform.position + new Vector3(Random.Range(-tankLimits.x, tankLimits.x),
                                                                             Random.Range(-tankLimits.y, tankLimits.y),
                                                                             Random.Range(-tankLimits.z, tankLimits.z));
            }
        }
        else
        {
            if (objectToFollow != null)
                goalPosition = objectToFollow.transform.position;
            else
                Debug.Log("Object to follow not set");
        }
       
    }
}
