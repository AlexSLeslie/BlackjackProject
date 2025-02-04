using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GameLogic: MonoBehaviour
{
    public Stack<Card> deck;
    public List<Card> playerHand;
    public List<Card> dealerHand;

    public GameObject cardPrefab;
    public GameObject playerHandObject;
    public GameObject dealerHandObject;
    public GameObject buttonHandlerObject;

    private ButtonHandler buttonHandler;

    public int chips;

    void Start(){

        deck = new Stack<Card>();
        playerHand = new List<Card>();
        dealerHand = new List<Card>();

        buttonHandler = buttonHandlerObject.GetComponent<ButtonHandler>();

        chips = 500;

        InitDeck();
        deck = Shuffle(deck);
        StartRound();

        // UIButton
    }

    void Update(){}

    void InitDeck(){
        // Iterate through a suit enum, then ranks of cards
        foreach(Card.Suit suit in System.Enum.GetValues(typeof(Card.Suit))){
            for(int i=1; i<14; ++i) deck.Push(new Card(suit, i));
        }
    }

    // Borrowed from https://stackoverflow.com/questions/273313/randomize-a-listt
    Stack<Card> Shuffle(Stack<Card> stack){

        List<Card> c = stack.ToList();
        int n = c.Count;

        if(n < 2) return stack;
        
        while (n > 1){
            --n;
            int k = Random.Range(0,n+1);
            Card swap = c[k];
            c[k] = c[n];
            c[n] = swap;
        }

        return new Stack<Card>(c);
    }

    void StartRound(){
        // deal cards
        for(int i=0; i<2; ++i){
            playerHand.Add(deck.Pop());
            dealerHand.Add(deck.Pop());
        }
        dealerHand[0].faceup(false);

        UpdateCards();
        buttonHandler.ShowElement(ButtonHandler.Root.HIT_STAND);
    }

    // Called from ButtonHandler.OnButtonClick()
    public void Hit(){
        playerHand.Add(new Card(Card.Suit.HEARTS, 1));
        // playerHand.Add(deck.Pop());
        Debug.Log(SumHand(playerHand));
        UpdateCards();
        if(SumHand(playerHand) > 21) Bust();
    }

    void Bust(){
        buttonHandler.HideElement(ButtonHandler.Root.HIT_STAND);
        Debug.Log("Bust,,,,");
        // TODO: Start new round, or show a "new round" button
        // TODO: Add Stand Button func
    }

    void ClearCards(){ foreach(GameObject c in GameObject.FindGameObjectsWithTag("Card")) Destroy(c); }

    // TODO: layer new cards over old ones
    void UpdateCards(){
        ClearCards();
        float x = -playerHand.Count/2;
        for(int i=0; i<playerHand.Count; ++i){
            GameObject newCard = Instantiate(cardPrefab, new Vector3(x + i, playerHandObject.transform.position.y,0), Quaternion.identity, playerHandObject.transform);
            newCard.GetComponent<SpriteRenderer>().sprite = playerHand[i].image;
        }

        x = -dealerHand.Count/2;
        for(int i=0; i<dealerHand.Count; ++i){
            GameObject newCard = Instantiate(cardPrefab, new Vector3(x + i, dealerHandObject.transform.position.y, 0), Quaternion.identity, dealerHandObject.transform);
            newCard.GetComponent<SpriteRenderer>().sprite = dealerHand[i].image;
        }
    }

    // Returns the value of a hand, factoring in aces as being worth either 11 or 1
    int SumHand(List<Card> hand){
        int sum = 0;
        int aces = 0;

        foreach(Card card in hand){
            sum += card.Value();
            if(card.Value() == 11) ++aces;
        }

        // Lower sum if hand contains aces, if necessary
        if(sum > 21 && aces > 0){
            while(aces > 0){
                sum -= 10;
                if(sum <= 21) return sum;
                --aces;
            }
        }
        return sum;
    }

    
}