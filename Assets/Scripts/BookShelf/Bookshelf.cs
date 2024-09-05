using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfSpawner : MonoBehaviour
{
    [Header("Bookshelf Settings")]
    public Transform[] bookshelfRows; 
    public GameObject[] bookPrefabs;  
    public float rowWidth = 10f;     
    public int minBooksPerRow = 8;    
    public int maxBooksPerRow = 15;   

    [Header("Book Size Settings")]
    public float bookLength = 0.2f;
    public float minBookWidth = 0.002f;
    public float maxBookWidth = 0.005f;
    public float minBookHeight = 1.2f;
    public float maxBookHeight = 2.6f;

    private void Start()
    {
        SpawnBooksOnShelf();
    }

    void SpawnBooksOnShelf()
    {
        foreach (Transform row in bookshelfRows)
        {
            SpawnBooksInRow(row);
        }
    }

    void SpawnBooksInRow(Transform row)
    {
        float currentX = -rowWidth / 2.0f;
        int booksToSpawn = Random.Range(minBooksPerRow, maxBooksPerRow);
        float zOffset = 0.05f;

        for (int i = 0; i < booksToSpawn; i++)
        {
            GameObject bookPrefab = bookPrefabs[Random.Range(0, bookPrefabs.Length)];

            GameObject bookInstance = Instantiate(bookPrefab, row);

            float bookWidth = Random.Range(minBookWidth, maxBookWidth);
            float bookHeight = Random.Range(minBookHeight, maxBookHeight);

            bookInstance.transform.localScale = new Vector3(bookLength, bookWidth, bookHeight);

            float halfBookWidth = bookWidth / 2.0f;
            bookInstance.transform.localPosition = new Vector3(currentX + halfBookWidth, zOffset, 0);

            currentX += bookWidth;

            if (currentX > rowWidth / 2.0f)
                break;
        }
    }
}