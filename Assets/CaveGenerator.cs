using UnityEngine;
using System.Collections;

public class CaveGenerator : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public int iterations;
    public bool stepped;
    public GameObject cellPrefab;
    public Transform gridParent;
    public Color wallColor;
    public Color floorColor;

    private Cell[,] grid;
    private int currentIteration;

    void Start()
    {
        currentIteration = 0;
    }

    public void GenerateGrid()
    {
        if (grid != null)
        {
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }
        }

        grid = new Cell[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x + 960, y + 540, 0); // Adjust the position to start at (960, 540)
                GameObject newCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, gridParent);
                Cell cell = newCell.GetComponent<Cell>();
                if (cell != null)
                {
                    cell.Initialize(Random.value < 0.45f, wallColor, floorColor);
                    grid[x, y] = cell;
                }
                else
                {
                    Debug.LogError("Cell component missing from prefab instance");
                }
            }
        }

        currentIteration = 0;
    }

    public void StartSimulation()
    {
        if (!stepped)
        {
            StartCoroutine(UpdateGrid());
        }
    }

    public void StepSimulation()
    {
        if (currentIteration < iterations)
        {
            StartCoroutine(StepUpdateGrid());
        }
    }

    IEnumerator UpdateGrid()
    {
        for (int i = 0; i < iterations; i++)
        {
            StepUpdate();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StepUpdateGrid()
    {
        StepUpdate();
        yield return new WaitForSeconds(0.1f);
    }

    void StepUpdate()
    {
        bool[,] newStates = new bool[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                newStates[x, y] = CalculateNewState(x, y);
            }
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                grid[x, y].SetState(newStates[x, y]);
            }
        }

        currentIteration++;
    }

    bool CalculateNewState(int x, int y)
    {
        int wallCount = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int nx = x + i;
                int ny = y + j;
                if (nx >= 0 && nx < sizeX && ny >= 0 && ny < sizeY)
                {
                    if (grid[nx, ny].isAlive)
                    {
                        wallCount++;
                    }
                }
                else
                {
                    wallCount++; // Consider out-of-bounds cells as walls
                }
            }
        }

        if (grid[x, y].isAlive)
        {
            return wallCount >= 4;
        }
        else
        {
            return wallCount >= 5;
        }
    }
}
