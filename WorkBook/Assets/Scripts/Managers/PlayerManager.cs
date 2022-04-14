using UnityEngine;

public class PlayerManager : GenericSingletonClass<PlayerManager>
{
    private static float moveSpeed;
    private static float jumpVelocity;
    private static float dashSpeed;
    private static float dashDistance;
    private static GameObject playerPrefab;
    private static GameObject player;

    private static int maxLife = 1;
    private static int life;

    public static bool keyAcquired = false;
    public static bool keyCrayonAcquired = false;

    public static GameObject PlayerPrefab { set { playerPrefab = value; } }
    public static float MoveSpeed { set { moveSpeed = value; } }
    public static float JumpVelocity { set { jumpVelocity = value; } }
    public static float DashSpeed { set { dashSpeed = value; } }
    public static float DashDistance { set { dashDistance = value; } }

    public static int MaxLife {  set { 
            maxLife = value;
            life = value;
        } }


    public static void DesroyPlayer()
    {
        if (player != null)
            player.GetComponent<PlayerHealth>().Invincible = true;
            Destroy(player);
    }

    public static void Spawn(Vector3 pos)
    {
        Debug.Log("Spawn to : " + pos);
        player = Instantiate(playerPrefab, LevelManager.GetCurrentLevel());
        player.GetComponent<BridgeController>().SetBridges(GameManager.Instance.fileNameBridges);
        player.transform.localPosition = pos;
        player.GetComponent<InventoryController>().keyAcquired = keyAcquired;
        player.GetComponent<InventoryController>().keyCrayonAcquired = keyCrayonAcquired;
        player.GetComponent<PlayerController>().MoveSpeed = moveSpeed;
        player.GetComponent<PlayerController>().JumpVelocity = jumpVelocity;
        player.GetComponent<PlayerController>().DashSpeed = dashSpeed;
        player.GetComponent<PlayerController>().DashDistance = dashDistance;

    }

    public static GameObject GetPlayer()
    {
        return player;
    }

    public void RestartLevel()
    {
        player.GetComponent<PlayerHealth>().Invincible = true;
        DesroyPlayer();
        LevelManager.RestartPlayerLevel();
        Spawn(LevelManager.GetPosSpawnCurLevel());
    }

    public void DamagePlayer(int Damage)
    {
        life -= Damage;

        //Frame invicible need here

        if (life <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        //Lance une animation/son ?

        Debug.LogWarning("Player died!");
        life = maxLife;
        RestartLevel();
        //recharge le niveau / charge écran de mort
    }

    public void BrigdeActivation()
    {
        player.GetComponent<BridgeController>().SetBridges(GameManager.Instance.fileNameBridges);
        player.GetComponent<BridgeController>().BrigdeActivation();
    }

    public static Rigidbody2D GetRigidbody2D()
    {
        return player.GetComponent<PlayerController>().RB;
    }
}