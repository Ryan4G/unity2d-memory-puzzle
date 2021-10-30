using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;

    public const float offsetX = 2;
    public const float offsetY = 2.5f;

    [SerializeField]
    private MemoryCard originalCard;

    [SerializeField]
    private Sprite[] images;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    private int _score = 0;

    [SerializeField]
    private TMPro.TextMeshPro _text;

    public bool canReveal
    {
        get
        {
            return _secondRevealed == null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (var i = 0; i < gridCols; i++)
        {
            for (var j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard);
                }

                var index = i + j * gridCols;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = offsetX * i + startPos.x;
                float posY = -offsetY * j + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];

        for(int i = 0; i < newArray.Length; i++)
        {
            var temp = newArray[i];
            var r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }

        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;

            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        Debug.Log($"{_firstRevealed.CardID},{_secondRevealed.CardID}");
        if (_firstRevealed.CardID == _secondRevealed.CardID)
        {
            _score++;
            _text.text = $"Score: {_score}";
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }
}
