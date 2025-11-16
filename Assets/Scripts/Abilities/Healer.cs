using UnityEngine;

public class Healer : MonoBehaviour, IHealer
{
    public void Heal()
    {
        Debug.Log("Лечение");
    }
}