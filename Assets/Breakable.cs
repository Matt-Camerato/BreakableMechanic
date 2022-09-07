using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Breakable : MonoBehaviour
{
    [Tooltip("How many times this object can broken into smaller pieces")] public int size;
    [SerializeField, Tooltip("Any pieces spawned smaller than this will be destroyed automatically")] private float minSizeThreshold;
    [SerializeField, Tooltip("How far the pieces spread out when this object is broken")] private float breakForce;
    [SerializeField, Tooltip("The minimum number of pieces this object can break into")] private int minPieces;
    [SerializeField, Tooltip("The maximum number of pieces this object can break into")] private int maxPieces;
    [Range(0f, 1f), SerializeField, Tooltip("The minimum size a broken piece can be")] private float minPieceSize;
    [Range(0f, 1f), SerializeField, Tooltip("The maximum size a broken piece can be")] private float maxPieceSize;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) BreakApart();
    }

    //breaks the current breakable into smaller pieces (if not size 0, otherwise just destroys object)
    private void BreakApart()
    {
        int numPieces = Random.Range(minPieces, maxPieces + 1); //determine how many pieces to break into
        if (size != 0) for (int i = 0; i < numPieces; i++)
        {
            GameObject piece = Instantiate(gameObject, transform.position, Quaternion.identity);
            piece.GetComponent<Breakable>().size--; //decrement size value of each piece

            //determine the scale of each new piece
            float scale = Random.Range(minPieceSize, maxPieceSize);
            piece.transform.localScale *= scale;
            if (piece.transform.localScale.x < minSizeThreshold) Destroy(piece); //dont spawn pieces smaller than this value regardless of their "size"

            //make each piece spread out in a random direction
            piece.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * breakForce, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
}