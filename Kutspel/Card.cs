namespace Kutspel
{
    public enum Value
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum Suit
    {
        Clubs,
        Hearts,
        Spades,
        Diamonds
    }

    public struct Card
    {
        public Value value;
        public Suit suit;

        public string Print()
        {
            var result = "";
            switch (suit)
            {
                case Suit.Clubs:
                    result += "♣";
                    break;
                case Suit.Hearts:
                    result += "♥";
                    break;
                case Suit.Spades:
                    result += "♠";
                    break;
                case Suit.Diamonds:
                    result += "♦";
                    break;
                default:
                    result += "?";
                    break;
            }

            switch (value)
            {
                case Value.Two:
                    result += "2";
                    break;
                case Value.Three:
                    result += "3";
                    break;
                case Value.Four:
                    result += "4";
                    break;
                case Value.Five:
                    result += "5";
                    break;
                case Value.Six:
                    result += "6";
                    break;
                case Value.Seven:
                    result += "7";
                    break;
                case Value.Eight:
                    result += "8";
                    break;
                case Value.Nine:
                    result += "9";
                    break;
                case Value.Ten:
                    result += "10";
                    break;
                case Value.Jack:
                    result += "J";
                    break;
                case Value.Queen:
                    result += "Q";
                    break;
                case Value.King:
                    result += "K";
                    break;
                case Value.Ace:
                    result += "A";
                    break;
                default:
                    result += "?";
                    break;
            }
            return result;
        }
    }
}