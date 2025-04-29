using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    public float maxX,minX,minY,maxY;
    public float speed;
    public Vector2 motion;

    public void CameraMove(InputAction.CallbackContext context)
    {
        motion = context.ReadValue<Vector2>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x < maxX && motion.x > 0) || transform.position.x > minX && motion.x < 0) //valid x move
        { 
            gameObject.transform.position += new Vector3(motion.x, 0) * Time.deltaTime * speed; 
        }


        if ((transform.position.y < maxY && motion.y > 0) || transform.position.y > minY && motion.y < 0) //valid y move
        {
            gameObject.transform.position += new Vector3(0, motion.y) * Time.deltaTime * speed; 
        }

    }
}
