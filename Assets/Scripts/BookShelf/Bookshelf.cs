using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BookshelfSpawner : MonoBehaviour
{
    [Header("Bookshelf Settings")]
    public Transform[] bookshelfRows; 
    public GameObject[] bookPrefabs;
    public Color[] colorList;
    private List<GameObject> spawnedBooks = new List<GameObject>();
    public float rowWidth = 10f;     
    public int minBooksPerRow = 8;    
    public int maxBooksPerRow = 15;
    public BookShelfClue clue;

    [Header("Book Size Settings")]
    public float bookLength = 0.2f;
    public float minBookWidth = 0.003f;
    public float maxBookWidth = 0.005f;
    public float minBookHeight = 1.6f;
    public float maxBookHeight = 2.6f;
    public Shader revealShader;

    public class BookInfo
    {
        public GameObject book;
        public int rowIndex;
        public int bookIndexInRow;

        public BookInfo(GameObject book, int rowIndex, int bookIndexInRow)
        {
            this.book = book;
            this.rowIndex = rowIndex;
            this.bookIndexInRow = bookIndexInRow;
        }
    }

    List<BookInfo> allSpawnedBooks = new List<BookInfo>();

    private void Start()
    {
        for(int rowIndex = 0; rowIndex < bookshelfRows.Length; rowIndex++) 
        {
            SpawnBooksInRow(bookshelfRows[rowIndex], rowIndex);
        }

        MarkSpecialBooks(5);
    }

    void SpawnBooksInRow(Transform row, int rowIndex)
    {
        float currentX = -rowWidth / 2.0f;
        int booksToSpawn = Random.Range(minBooksPerRow, maxBooksPerRow);
        float zOffset = 0.08f;

        for(int bookIndex = 0; bookIndex < booksToSpawn; bookIndex++)
        {
            GameObject bookPrefab = bookPrefabs[Random.Range(0, bookPrefabs.Length)];
            GameObject bookInstance = Instantiate(bookPrefab, row);
            float bookWidth = Random.Range(minBookWidth, maxBookWidth);
            float bookHeight = Random.Range(minBookHeight, maxBookHeight);

            bookInstance.transform.localScale = new Vector3(bookLength, bookWidth, bookHeight);

            float halfBookWidth = bookWidth / 2.0f;
            bookInstance.transform.localPosition = new Vector3(currentX + halfBookWidth, zOffset, 0);
            ChangeBookColor(bookInstance);

            BookInfo bookInfo = new BookInfo(bookInstance, rowIndex, bookIndex);
            allSpawnedBooks.Add(bookInfo);

            currentX += bookWidth;

            if (currentX > rowWidth / 2.0f)
                break;
        }
    }

    void ChangeBookColor(GameObject book)
    {
        Transform pagesTransform = book.transform.Find("Book").Find("Cover");

        if (pagesTransform != null)
        {
            MeshRenderer renderer = pagesTransform.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                Color randomColor = colorList[Random.Range(0, colorList.Length)];

                renderer.material.color = randomColor;
            }
            else
            {
                Debug.LogWarning("MeshRenderer not found on Pages object.");
            }
        }
        else
        {
            Debug.LogWarning("Pages child object not found in book prefab.");
        }
    }

    void MarkSpecialBooks(int numberOfSpecialBooks)
    {
        if (allSpawnedBooks.Count < numberOfSpecialBooks)
        {
            Debug.LogWarning("Not enough books to mark as special.");
            return;
        }

        List<int> specialBookIndices = new List<int>();
        string specialBooksText = "";

        while (specialBookIndices.Count < numberOfSpecialBooks)
        {
            int randomIndex = Random.Range(0, allSpawnedBooks.Count);
            if (!specialBookIndices.Contains(randomIndex))
            {
                specialBookIndices.Add(randomIndex);
            }
        }

        for (int i = 0; i < specialBookIndices.Count; i++)
        {
            int specialBookIndex = specialBookIndices[specialBookIndices.Count - 1 - i]; // Reverse order for fun
            BookInfo specialBookInfo = allSpawnedBooks[specialBookIndex];

            GameObject gameObject1 = specialBookInfo.book.transform.Find("SecretComponent").gameObject;
            GameObject specialBookComponent = gameObject1;

            // Mark the book as special by changing its name
            specialBookInfo.book.name = "SpecialBook_" + i;

            if(specialBookComponent != null)
            {
                Debug.Log("SIEMANO");
            }

            specialBooksText += (specialBookInfo.rowIndex + 1) + " : " + (specialBookInfo.bookIndexInRow + 1) + "\n";
        }

        clue.UpdateTextTexture(specialBooksText);
    }
}