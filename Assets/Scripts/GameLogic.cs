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


    public int chips;

    void Start(){

        deck = new Stack<Card>();
        playerHand = new List<Card>();
        dealerHand = new List<Card>();

        chips = 500;

        InitDeck();
        deck = Shuffle(deck);
        StartRound();

        // UIButton
    }

    void Update(){}

    void InitDeck(){

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
        for(int i=0; i<2; ++i){
            playerHand.Add(deck.Pop());
            dealerHand.Add(deck.Pop());
        }

        // TODO: Dealer's second card must appear facedown
        
    }
}