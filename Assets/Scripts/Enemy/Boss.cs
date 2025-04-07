using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public Transform firePoint;
    public Mover fireBall;
    public float fireRate = 1f;
    public float speed = 5f;
    public bool isMoving = true;
    public bool verticalMovement = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        InvokeRepeating("Fire", fireRate, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (verticalMovement)
            {
				float novaPosY = Mathf.Lerp(
								transform.position.y,
								player.position.y,
								speed * Time.deltaTime
							);

				transform.position = new Vector3(transform.position.x, novaPosY, transform.position.z);
			}
            else
            {
				float novaPosX = Mathf.Lerp(
								transform.position.x,
								player.position.x,
								speed * Time.deltaTime
							);

                transform.position = new Vector3(novaPosX, transform.position.y, transform.position.z);

			}
			
		}

        if(!isMoving)
        {
			
			transform.Translate(transform.up * speed * Time.deltaTime * 4, Space.Self);
		}
        
	}

    void Fire()
    {
        if (!isMoving)
        {
			return;
		}
        Mover fireBallInstance = Instantiate(fireBall, firePoint.position, transform.rotation);
    }

    public void StopMoving()
    {
		isMoving = false;
	}
}
