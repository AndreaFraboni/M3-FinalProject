using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<EnemyController> listEnemies = new List<EnemyController>();

    public void RegistEnemy(EnemyController enemy)
    {
        if (enemy == null) return;

        if (!listEnemies.Contains(enemy))
        {
            listEnemies.Add(enemy);
        }
        else
        {
            Debug.Log("l'enemy non verrà registrato in quanto è già registrato !!!!");
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (enemy == null) return;

        if (listEnemies.Remove(enemy))
        {
            Debug.Log("Ho rimosso un enemy!!!");
        }
        else
        {
            Debug.Log("Stò provando a rimuovere un enemy che non è nella lista !!!");
        }
    }
}
