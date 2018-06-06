using System;
using System.Collections.Generic;

namespace Kutspel
{
    public class Deck
    {
        private List<Card> _list;
        public Deck()
        {
            _list = new List<Card>();
        }

        public void AddCard(Card c)
        {
            _list.Add(c);
        }

        public int Count => _list.Count;

        public bool Empty => _list.Count == 0;

        public Card? GetCard(int i) => i < _list.Count ? (Card?)_list[i] : null;

        public Card? Pop()
        {
            if (_list.Count == 0)
                return null;
            var c = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            return c;
        }

        public Card? GetLast()
        {
            if (_list.Count == 0)
                return null;
            var c = _list[_list.Count - 1];
            return c;
        }

        public void Shuffle()
        {
            Random r = new Random();
            for (var i = _list.Count - 1; i > 0; i--)
            {
                var j = r.Next(0, i);
                var temp = _list[j];
                _list[j] = _list[i];
                _list[i] = temp;
            }
        }

        public static Deck RandomDeck()
        {
            var d = Deck.FullDeck();
            d.Shuffle();
            return d;
        }

        public static Deck FullDeck()
        {
            var d = new Deck();
            foreach (var s in Enum.GetValues(typeof(Suit)))
            {
                foreach (var v in Enum.GetValues(typeof(Value)))
                {
                    var c = new Card
                    {
                        value = (Value)v,
                        suit = (Suit)s
                    };
                    d.AddCard(c);
                }
            }
            return d;
        }
    }
}
