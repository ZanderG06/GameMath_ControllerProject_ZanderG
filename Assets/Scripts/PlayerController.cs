using Unity.Hierarchy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    // Update is called once per frame
    void Update()
    {
        //Get player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        
        movement.Normalize(); //Normalize vector

        transform.Translate(movement * moveSpeed * Time.deltaTime); //Frame independant
    }
}
