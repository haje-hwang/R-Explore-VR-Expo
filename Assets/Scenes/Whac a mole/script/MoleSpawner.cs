using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField]
    private MoleFSM[] moles;
    [SerializeField]
    private float spawnTime;

    private int[] spawnPercents = new int[4] { 75, 10, 5,10 };

    public int MaxSpawnMole { set; get; } = 1;

    public void SetUp()
    {
        StartCoroutine(SpawnMole());
    }

    private IEnumerator SpawnMole()
    {
        while (true)
        {
            /*
            int index = Random.Range(0, moles.Length);

            moles[index].MoleType = SpawnMoleType();

            moles[index].ChangeState(MoleState.MoveUp);

            yield return new WaitForSeconds(spawnTime);
            */

            StartCoroutine("SpawnMultiMoles");

            yield return new WaitForSeconds(spawnTime);
        }


    }
    private MoleType SpawnMoleType()
    {
        int percent = Random.Range(0, 100);
        float cumulative = 0;

        for(int i = 0; i<spawnPercents.Length; ++i) 
        {
            cumulative += spawnPercents[i];

            if (percent < cumulative)
            {
                return (MoleType)i;
            }
        }

        return MoleType.Normal;
    }

    private IEnumerator SpawnMultiMoles()
    {
        int[] indexs = RandomNumerics(moles.Length, moles.Length);
        int currentSpawnMole = 0;
        int currentIndex = 0;

        while(currentIndex< indexs.Length)
        {
            if (moles[indexs[currentIndex]].MoleState == MoleState.UnderGround)
            {
                moles[indexs[currentIndex]].MoleType = SpawnMoleType();
                moles[indexs[currentIndex]].ChangeState(MoleState.MoveUp);
                currentSpawnMole++;

                yield return new WaitForSeconds(0.1f);
            }

            if (currentSpawnMole == MaxSpawnMole)
            {
                break;
            }

            currentIndex++;

            yield return null;
        }

    }

    private int[] RandomNumerics(int maxCount,int n)
    {
        int[] defaults = new int[maxCount];
        int[] results = new int[n];

        for(int i = 0; i< maxCount; ++i)
        {
            defaults[i] = i;
        }
        for(int i = 0; i < n; ++i)
        {
            int index = Random.Range(0, maxCount);

            results[i] = defaults[index];
            defaults[index] = defaults[maxCount - 1];

            maxCount--;
        }
        return results;
    }
}
