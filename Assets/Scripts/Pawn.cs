using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float speed = 5f;
    public PieceColor color;
    public PieceState state;

    public BaseTile currentTile;
    
   
    void Update()
    {
        //This was movement code for the demonstration but we're not gonna need this anymore
        /*float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }*/

    }
}
public enum PieceColor { Red, Blue, Green, Yellow, None }
public enum PieceState { Active, Start, Home, Protected}
