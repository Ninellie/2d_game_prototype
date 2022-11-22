using UnityEngine;

public class Firearm : MonoBehaviour
{
    //Input: 1
    private static bool IsFireButtonPressed =>
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().isFireButtonPressed;
    //Shooting: 1
    private Shooter Shooter => GetComponent<Shooter>();
    //Magazine: 1
    private Magazine Magazine => GetComponent<Magazine>();
    //Reload: 1
    private Reload Reload => GetComponent<Reload>();
    private void Update()
    {
        if (!Reload.IsInProcess && Magazine.IsEmpty)
        {
            Magazine.onEmpty?.Invoke();
            Debug.Log("Magazine of " + gameObject.name + " is empty");
        }
        if (!IsFireButtonPressed) return;
        if (Shooter.CanShoot) Shooter.Shoot();
    }
}