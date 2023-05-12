using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockManager.FM.minimumSpeed, FlockManager.FM.maximumSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < FlockManager.FM.speedResetFrequency)
        {
            speed = Random.Range(FlockManager.FM.minimumSpeed, FlockManager.FM.maximumSpeed);
        }
        if (Random.Range(0, 100) < FlockManager.FM.applyRuleFrequency)
        {
            ApplyRules();
        }   
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
    void ApplyRules()
    {
        GameObject[] gameobjectArrayOfAllFish;
        gameobjectArrayOfAllFish = FlockManager.FM.allFish;
        Vector3 centringVelocity = Vector3.zero;
        Vector3 avoidingVelocity = Vector3.zero;
        float groupSpeed = 0.01f;
        float currentNeighbourDistance;
        int groupSize = 0;
        foreach(GameObject fish in gameobjectArrayOfAllFish)
        {
            if(fish != this.gameObject)             //iterating for every fish except this fish
            {
                currentNeighbourDistance = Vector3.Distance(fish.transform.position, this.transform.position);
                if(currentNeighbourDistance <= FlockManager.FM.neighbourDistance)
                {
                    centringVelocity += fish.transform.position;
                    groupSize++;
                    if(currentNeighbourDistance < 1.0f)
                    {
                        avoidingVelocity = avoidingVelocity + (this.transform.position - fish.transform.position);
                    }
                    Flock otherFish = fish.GetComponent<Flock>();
                    groupSpeed = groupSpeed + otherFish.speed;
                }
            }
        }
        if(groupSize > 0)
        {
            centringVelocity = centringVelocity / groupSize + (FlockManager.FM.goalPosition-this.transform.position);
            speed = groupSpeed / groupSize;
            if(speed > FlockManager.FM.maximumSpeed)
            {
                speed = FlockManager.FM.maximumSpeed;
            }
            Vector3 direction = (centringVelocity + avoidingVelocity) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
