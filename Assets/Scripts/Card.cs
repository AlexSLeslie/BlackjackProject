using UnityEngine;

public class Card
{
    public enum Suit{
        HEARTS,
        DIAMONDS,
        CLUBS,
        SPADES
    }


    private Suit _suit;
    private int _rank;
    private Sprite _image;
    private bool _faceup;

    private string cardBack = "Cards/back";


    public Card(Suit s, int r){
            _suit = s;
            _rank = r;
            _faceup = true;

            _image = Resources.Load<Sprite>(imagePath());
        }


    public Suit suit{ get => _suit; set => _suit = value; }
    public int rank { get => _rank; set => _rank = value; }
    public Sprite image { get => _image; set => _image = value; }
    
    // public void faceup(bool isFaceup){
    //     _faceup = isFaceup;
    //     _image = _faceup? Resources.Load<Sprite>(imagePath()) : Resources.Load<Sprite>(cardBack);

    // }

    public bool faceup{ 
        get { return _faceup; }
        set { 
            _faceup = value;
            _image = value? Resources.Load<Sprite>(imagePath()) : Resources.Load<Sprite>(cardBack);
        } }

    public int Value(){
        switch(_rank){
            case 1:
                return 11;
                
            case 11:
            case 12:
            case 13:
                return 10;
            
            default:
                return _rank;
        }
    }

    // Image source https://code.google.com/archive/p/vector-playing-cards/ 
    public string imagePath(){

        string s = "Cards/";

        switch(_rank){
            case 1:
                s += "ace_of_";
                break;
            case 11:
                s += "jack_of_";
                break;
            case 12:
                s += "queen_of_";
                break;
            case 13:
                s += "king_of_";
                break;
            default:
                s += _rank.ToString() + "_of_";
                break;
        }

        switch(_suit){
            case Suit.CLUBS:
                return s + "clubs";
            case Suit.DIAMONDS:
                return s + "diamonds";
            case Suit.HEARTS:
                return s + "hearts";
            case Suit.SPADES:
                return s + "spades";
        }

        // Shouldnt reach here but just in case
        return s;
    }

}
