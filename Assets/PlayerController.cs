using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;

    private void Update()
    {
        MovePlayer();
        FireBullet();
    }

    //moves player based on horizontal and vertical input
    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x == 0 && y == 0) return; //don't continue unless there was input

        Vector3 moveDir = new Vector3(x, y, 0f);
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    //fires a bullet towards the player's mouse position when LMB is pressed
    private void FireBullet()
    {
        if (!Input.GetMouseButtonDown(0)) return; //only shoot if player has pressed the LMB

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Vector2 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(forceDir.normalized * _bulletSpeed, ForceMode2D.Impulse);
    }
}