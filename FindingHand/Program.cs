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
        SPADE = 3,
        NONE = 4,
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

            Card[] fullHouseSet = {
                new Card(Suit.CLUB, Rank.ACE),
                new Card(Suit.HEART, Rank.ACE),
                new Card(Suit.CLUB, Rank.QUEEN),
                new Card(Suit.DIAMOND, Rank.ACE),
                new Card(Suit.CLUB, Rank.TEN),
                new Card(Suit.DIAMOND, Rank.NINE),
                new Card(Suit.HEART, Rank.QUEEN),
            };

            type = evaluateHand(fullHouseSet);
            Console.WriteLine("Second hand type: " + type.ToString());

            Console.ReadLine();
        }

        static HandType evaluateHand(Card[] cards)
        {
            Array.Sort(cards);

            int[] rankCount = new int[13];
            int[] suitCount = new int[4];

            foreach (Card card in cards)
            {
                rankCount[(int)card.rank]++;
                suitCount[(int)card.suit]++;
            }

            int pairCount = 0;
            int threeCount = 0;
            int fourCount = 0;
            int straightStart = 0, straightEnd = 0;
     
            for (int i = 0; i < rankCount.Length; i++)
            {
                if (rankCount[i] == 2) { pairCount++; }
                if (rankCount[i] == 3) { threeCount++; }
                if (rankCount[i] == 4) { fourCount++; }

                if (i > 0 && rankCount[i] > 0 && rankCount[i-1] > 0)
                {
                    straightEnd = i;
                }
                else
                {
                    straightStart = straightEnd = i;
                }
            }

            bool hasStraight = (straightEnd - straightStart) >= 4;

            Suit flushType = Suit.NONE;
            for (int i = 0; i < suitCount.Length; i++)
            {
                if (suitCount[i] >= 4)
                {
                    flushType = (Suit)i;
                    break;
                }
            }

            if (hasStraight && flushType != Suit.NONE)
            {
                // Check if there is a straight flush by checking the suit of cards in straight
                // Acknowledge the fact that a hand has total of 5 cards
                for (int i = 0; i < (cards.Length - 4); i++)
                {
                    bool isStraightFlush = true;

                    for (int j = i; j <= (i + 4); j++)
                    {
                        bool differentSuit = cards[j].suit != flushType;
                        bool notContinuous = ((j > i) && ((int)cards[j].rank != ((int)cards[j - 1].rank + 1)));
                        if (differentSuit || notContinuous)
                        {
                            isStraightFlush = false;
                            break;
                        }
                    }

                    if (isStraightFlush)
                    {
                        if (cards[i].rank == Rank.TEN)
                            return HandType.ROYAL_FLUSH;
                        else
                            return HandType.STRAIGHT_FLUSH;
                    }
                }
            }

            if (fourCount > 0)
                return HandType.FOUR_OF_A_KIND;

            if (pairCount > 0 && threeCount > 0)
                return HandType.FULL_HOUSE;

            if (flushType != Suit.NONE)
                return HandType.FLUSH;

            if (hasStraight)
                return HandType.STRAIGHT;

            if (threeCount > 0)
                return HandType.THREE_OF_A_KIND;

            if (pairCount > 1)
                return HandType.TWO_PAIR;

            if (pairCount == 1)
                return HandType.ONE_PAIR;

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
