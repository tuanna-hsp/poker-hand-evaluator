using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindingHand
{
    enum Suit
    {
        HEART = 0,
        DIAMOND = 1,
        CLUB = 2,
        SPADE = 3
    }

    enum Rank
    {
        TWO = 0,
        THREE = 1,
        FOUR = 2,
        FIVE = 3,
        SIX = 4,
        SEVEN = 5,
        EIGHT = 6,
        NINE = 7,
        TEN = 8,
        PRINCE = 9,
        QUEEN = 10,
        KING = 11,
        ACE = 12
    }

    enum HandType
    {
        ROYAL_FLUSH,
        STRAIGHT_FLUSH,
        FOUR_OF_A_KIND,
        FULL_HOUSE,
        FLUSH,
        STRAIGHT,
        THREE_OF_A_KIND,
        TWO_PAIR,
        ONE_PAIR,
        HIGH_CARD
    }

    class Card : IComparable
    {
        public Suit suit { get; set; }
        public Rank rank { get; set; }

        public int Value
        {
            get { return ((int) suit) * 13 + (int) rank; }
        }

        public Card(Suit s, Rank r)
        {
            suit = s;
            rank = r;
        }

        public override string ToString()
        {
            return suit.ToString() + '_' + rank.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj is Card)
            {
                return Value.CompareTo(((Card)obj).Value);
            }

            throw new ArgumentException("Obj must be of type Card");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Card[] royalFlushSet = {
                new Card(Suit.CLUB, Rank.ACE),
                new Card(Suit.CLUB, Rank.KING),
                new Card(Suit.CLUB, Rank.QUEEN),
                new Card(Suit.CLUB, Rank.PRINCE),
                new Card(Suit.CLUB, Rank.TEN),
                new Card(Suit.DIAMOND, Rank.NINE),
                new Card(Suit.HEART, Rank.SIX),
            };

            HandType type = evaluateHand(royalFlushSet);
            Console.WriteLine("First hand type: " + type.ToString());

            Console.ReadLine();
        }

        static HandType evaluateHand(Card[] cards)
        {
            Array.Sort(cards);
            printCards(cards);

            return HandType.HIGH_CARD;
        }

        static void printCards(Card[] cards)
        {
            Console.Write("Set: { ");

            foreach (Card card in cards)
            {
                Console.Write(card.ToString() + ", ");
            }

            Console.WriteLine("}");
        }
    }
}
