# Ball-Runner 🎮

![Ball-Runner](https://user-images.githubusercontent.com/62818241/216779257-76b626b0-6e5e-47c8-9107-b5004bf70709.PNG)

## 📌 Introduction
**Ball-Runner** is a fast-paced endless runner game where the player controls a ball that needs to avoid obstacles and collect coins to score points. The game features a dynamic environment where the player must jump, move, and avoid obstacles while continuously progressing through the level. The game is designed with smooth controls, interactive sound effects, and a dynamic tile system for an endless experience.

## 🔥 Features
- 🎮 Smooth player movement with joystick controls.
- 🏃‍♂️ Avoid obstacles and jump to survive.
- 💰 Collect coins to increase your score.
- 🚧 Procedurally generated tile system for endless gameplay.
- 🎶 Audio feedback for player actions (coin collection, death).
- 🎮 Game over mechanics and UI display.
- 📊 Score tracking and UI display.

---

## 🏗️ How It Works
The game is structured with several scripts that manage different aspects, from player movement to level events and score tracking.

### 📌 **FollowPlayer Script**
This script ensures that the camera follows the player’s movement, keeping them centered on the screen while allowing the player to control the ball’s movement along the Z-axis.

```csharp
public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float speed = 15f;
 
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(transform.position.x, transform.position.y, target.position.z - 10), speed * Time.deltaTime);
    }
}
```

### 📌 **LevelEvents Script**
Handles level-specific events such as restarting the game or quitting the game.

```csharp
public class LevelEvents : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
```

### 📌 **Player Script**
Handles player movement, jumping, and collision detection. It also manages the score by collecting coins and detects when the player falls off the level or hits an obstacle.

```csharp
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = true;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpSpeed = 8f;
    private float x;
    private float z;
    public Joystick joystick;
    [SerializeField] private AudioSource playerDiedSoundEffect;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private Text scoreText;
    private int coins = 0;

    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Move();
    
        if (rb.position.y < -1f)
        {
            PlayerManager.gameOver = true;
        }
    }

    private void Inputs()
    {
        x = joystick.Horizontal;
        z = joystick.Vertical;
    }

    private void Move()
    {
        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * speed, Space.World);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            playerDiedSoundEffect.Play();
        }
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.tag == "Coins")
        {
            collectionSoundEffect.Play();
            coins++;
            scoreText.text = "Score: " + coins;
            Destroy(collision.gameObject);
        }    
    }
}
```

### 📌 **PlayerManager Script**
Manages the game state, including detecting when the game is over and handling the UI elements for the game-over state.

```csharp
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
```

### 📌 **TileManager Script**
Handles the procedural generation of the game’s environment. Tiles are spawned dynamically as the player progresses, ensuring an endless experience.

```csharp
public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;
    public float tileLength = 60;
    public int numberOfTiles = 5;
    public int totalNumOfTiles = 6;
    public float zSpawn = 0;
    public Transform playerTransform;

    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 65 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, totalNumOfTiles));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
```

---

## 🎯 Conclusion
**Ball-Runner** offers an exciting endless runner experience with engaging mechanics such as tile generation, coin collection, and player movement. The smooth controls, dynamic level generation, and polished UI make it a fun game to play and a great foundation for future enhancements. 🚀
