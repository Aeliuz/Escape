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

        StartCoroutine(FreezeBooksAfterDelay(2.5f));
    }

    void SpawnBooksInRow(Transform row, int rowIndex)
    {
        float currentX = -rowWidth / 2.0f;
        int booksToSpawn = Random.Range(minBooksPerRow, maxBooksPerRow);
        float zOffset = 5f;

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
        HashSet<Color> usedColors = new HashSet<Color>();

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

            Transform secretComponent = specialBookInfo.book.transform.Find("Book").Find("SecretComponent");

            if (secretComponent != null)
            {
                // Activate the child if it exists
                secretComponent.gameObject.SetActive(true);
                Debug.Log("Child activated");
            }
            else
            {
                Debug.LogWarning("SecretComponent not found in " + specialBookInfo.book.name);
            }

            // Mark the book as special by changing its name
            specialBookInfo.book.name = "SpecialBook_" + i;

            Color uniqueColor = GetUniqueColor(usedColors);
            SetSpecialBookColor(specialBookInfo.book, uniqueColor);
            usedColors.Add(uniqueColor);

            // Convert the color to a hex string
            string hexColor = UnityEngine.ColorUtility.ToHtmlStringRGB(uniqueColor);

            specialBooksText += (i + 1) + ": <color=#" + hexColor + ">■</color>\n";
        }

        clue.UpdateTextTexture(specialBooksText);
    }

    Color GetUniqueColor(HashSet<Color> usedColors)
    {
        Color randomColor;
        do
        {
            randomColor = colorList[Random.Range(0, colorList.Length)];
        } while (usedColors.Contains(randomColor)); // Keep generating until we find an unused color

        return randomColor;
    }

    // Method to set the color of a book's cover
    void SetSpecialBookColor(GameObject book, Color color)
    {
        Transform pagesTransform = book.transform.Find("Book").Find("Cover");
        if (pagesTransform != null)
        {
            MeshRenderer renderer = pagesTransform.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }

    IEnumerator FreezeBooksAfterDelay(float delay)
    {
        // Wait for the specified delay time (2 seconds)
        yield return new WaitForSeconds(delay);

        // Now freeze the positions of all books
        FreezeAllBookPositions();
    }

    void FreezeAllBookPositions()
    {
        foreach (BookInfo bookInfo in allSpawnedBooks)
        {
            Rigidbody bookRigidbody = bookInfo.book.transform.Find("Book").GetComponent<Rigidbody>();
            if (bookRigidbody != null)
            {
                // Freeze position and rotation on all axes
                bookRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                //Debug.Log("SIEMANOOO");
            }
            else
            {
                Debug.LogWarning("Rigidbody not found on " + bookInfo.book.name);
            }
        }
    }
}