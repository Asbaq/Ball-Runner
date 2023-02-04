using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float speed = 15f;
 
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position , new Vector3(transform.position.x, transform.position.y,target.position.z -10), speed * Time.deltaTime);
        
        
    }
}
