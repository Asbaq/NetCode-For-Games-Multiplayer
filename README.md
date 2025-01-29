# NetCode For Games Multiplayer 🚀

![Netcode](https://user-images.githubusercontent.com/62818241/204098964-6f687e5f-1853-4088-bccf-9f2d831e9612.PNG)

## 📌 Introduction
The **NetCode For Games Multiplayer** feature enables multiplayer functionality in Unity games. By leveraging Unity's Netcode package, players can engage in real-time interactions across multiple devices. The system supports player movement, shooting, and health mechanics, ensuring synchronization across the network. This feature is ideal for creating seamless multiplayer experiences in both local and online game sessions.

## 🔥 Features
- 🕹️ **Player Movement**: Smooth player movement synchronization across the network.
- 💥 **Bullet Mechanics**: Networked bullet behavior, allowing synchronized shooting and damage application.
- ❤️ **Health System**: Real-time health tracking and updates across connected clients.
- 🌐 **Multiplayer Lobby**: Simple UI for creating, hosting, and joining multiplayer sessions.

---

## 🏗️ How It Works
This feature involves several scripts to handle key multiplayer actions such as movement, firing bullets, managing health, and connecting to the network.

### 📌 **Bullet Script**
This script handles the bullet movement, including targeting, collision detection, and network synchronization. The bullet moves towards its target and is destroyed once it reaches the destination.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : MonoBehaviour
{
    private float speed = 1f;
    private float timeToDestroy = 3f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle bullet collision (e.g., apply damage)
    }
}
```

### 📌 **PlayerNetwork Script**
This script manages player movement and controls. It ensures the player's input is properly synchronized across all clients in a multiplayer session. The script also includes the movement speed and camera rotation functionalities.

```csharp
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;
    public float movespeed = 6;
    private int damage = 1;
    GameObject s;
    new Rigidbody rigidbody;
    Vector3 velocity; 

    void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        if(!IsOwner)
        {
            cameraTransform.GetComponent<Camera>().enabled = false;
            cameraTransform.GetComponent<AudioListener>().enabled = false;
        }
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!IsOwner) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movespeed;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
```

### 📌 **NetworkManagerUI Script**
This script provides a simple UI to start and manage multiplayer sessions. Players can host a server, join an existing server, or start a client connection.

```csharp
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
  [SerializeField] private Button serverBtn;
  [SerializeField] private Button hostBtn;
  [SerializeField] private Button clientBtn;

  private void Awake()
  {
    serverBtn.onClick.AddListener(() => {
      NetworkManager.Singleton.StartServer();
    });
    hostBtn.onClick.AddListener(() => {
      NetworkManager.Singleton.StartHost();
    });
    clientBtn.onClick.AddListener(() => {
      NetworkManager.Singleton.StartClient();
    });
  }
}
```

---

## 🎯 Conclusion
The **NetCode For Games Multiplayer** feature enables seamless multiplayer experiences in Unity. With real-time player movement synchronization, bullet mechanics, and health tracking, this system ensures that all players have a smooth and synchronized experience across devices. The integration of network management features makes it easy to host, join, and play multiplayer games, providing a robust foundation for developing multiplayer games with Unity. 🚀🌟
