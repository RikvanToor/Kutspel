using System;
using System.Collections.Generic;
using System.Text;

namespace Kutspel
{
    public class Game
    {
        const int nrOfStacks = 4;
        Deck[] stacks = new Deck[nrOfStacks];
        Deck deck;
        public Game()
        {
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < nrOfStacks; i++)
                stacks[i] = new Deck();
            deck = Deck.RandomDeck();
        }

        public void Turn()
        {
            if (deck.Count == 0)
            {
                var won = true;
                foreach(var s in stacks)
                {
                    if(s.Count > 1)
                    {
                        won = false;
                        break;
                    }
                    var c = s.GetLast();
                    if (c?.value != Value.Ace)
                    {
                        won = false;
                        break;
                    }
                }

                PrintStacks();
                Console.WriteLine();
                if (!won)
                    Console.WriteLine("Game over!");
                else
                    Console.WriteLine("Hey gefeliciteerd pik");
                Console.ReadKey();
                Init();
                Turn();
            }
            else
            {
                DrawCards();
                PrintStacks();

                GetInput();
            }
        }

        public void CheckTurn()
        {
            var l = new List<Suit>();
            foreach (var s in stacks)
            {
                var c = s.GetLast();
                if (c != null) l.Add((Suit)c?.suit);
            }
            var allUnique = true;
            for (var i = 1; i < l.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (l[j] == l[i])
                        allUnique = false;
                }
            }

            var empty = false;
            var multiple = false;
            foreach (var s in stacks)
            {
                if (s.Count > 1)
                    multiple = true;
                if (s.Count == 0)
                    empty = true;
            }
            var movable = empty && multiple;
            var movesPossible = movable || !allUnique;

            if (movesPossible)
            {
                PrintStacks(true, "Cannot end turn yet.");
                GetInput();
            }
            else
            {
                Turn();
            }
        }

        public void GetInput()
        {
            var k = Console.ReadKey();
            switch (k.Key)
            {
                case ConsoleKey.Q:
                    break;
                case ConsoleKey.R:
                    Remove();
                    break;
                case ConsoleKey.E:
                    CheckTurn();
                    break;
                case ConsoleKey.M:
                    Move();
                    break;
                case ConsoleKey.X:
                    Init();
                    Turn();
                    break;
                default:
                    PrintStacks(true, "Unknown command " + k.Key);
                    GetInput();
                    break;
            }
        }

        public void Move(string error = "")
        {
            PrintStacks(false, error);
            Console.WriteLine("Enter the number of the stack of which you want to move a card, followed by the number of the stack to which you want to move the card, split by a space, e.g. \"1 2\". Enter nothing to go back.");
            var rl = Console.ReadLine().Trim();
            if (rl == "")
            {
                PrintStacks();
                GetInput();
                return;
            }
            var l = rl.Split(' ');

            if (l.Length < 2)
            {
                Move("Only one value given.");
                return;
            }
            var froms = l[0];
            var tos = l[1];
            var fromb = int.TryParse(froms, out int from);
            var tob = int.TryParse(tos, out int to);
            if (!fromb || !tob)
            {
                Move("Not a valid number.");
                return;
            }
            from--;
            to--;
            if (from >= 0 && from < stacks.Length && to >= 0 && to < stacks.Length)
            {
                if (stacks[from].Count < 2)
                {
                    Move("Can't move from stack " + from + ".");
                }
                else if (stacks[to].Count > 0)
                {
                    Move("Can't move to stack " + to + " as it is not empty.");
                }
                else
                {
                    var c = stacks[from].Pop();
                    stacks[to].AddCard((Card)c);
                    PrintStacks();
                    GetInput();
                }
            }
            else
            {
                Move("Not in the range of [0.." + stacks.Length + "].");
            }
        }

        public void Remove(string error = "")
        {
            PrintStacks(false, error);
            Console.WriteLine("Enter the number of the stack that you want to remove a card from or nothing to go back.");
            var l = Console.ReadLine().Trim();
            if (int.TryParse(l, out int i))
            {
                i--;
                if (i >= 0 && i < nrOfStacks)
                {
                    TryToRemove(i);
                }
                else
                {
                    Remove("Number not in range of [1.." + nrOfStacks + "].");
                }
            }
            else if (l == "")
            {
                PrintStacks();
                GetInput();
            }
            else
            {
                Remove("Not a number.");
            }
        }

        private void TryToRemove(int i)
        {
            bool valid = false;
            var c1 = stacks[i].GetLast();
            for (int j = (i + 1) % nrOfStacks; j != i; j = (j + 1) % nrOfStacks)
            {
                var c2 = stacks[j].GetLast();
                if (c2?.suit == c1?.suit && c1?.value < c2?.value)
                {
                    valid = true;
                    break;
                }

            }
            if (valid)
            {
                stacks[i].Pop();
                PrintStacks();
                GetInput();
            }
            else
            {
                Remove("Not a valid move.");
            }
        }

        public void PrintStacks(bool showControls = true, string error = "")
        {
            Console.Clear();

            Console.WriteLine("  1       2       3       4      Cards left: " + deck.Count);
            Console.WriteLine(" ___     ___     ___     ___");
            var maxLength = Math.Max(stacks[0].Count, Math.Max(stacks[1].Count, Math.Max(stacks[2].Count, stacks[3].Count)));
            for (int i = 0; i < maxLength; i++)
            {
                var line = "";
                foreach (var s in stacks)
                {
                    var temp = " ";
                    var cs = s.GetCard(i)?.Print() ?? "   ";
                    for (var j = cs.Length; j <= 3; j++)
                        cs += " ";
                    temp += cs + "   ";

                    line += temp;
                }
                Console.WriteLine(line);
            }
            if (error != "")
                Console.WriteLine(error);
            if (showControls)
                Console.WriteLine("(r) Remove card  | (m) Move card | (e) End turn | (x) Restart | (q) Quit");
        }

        private void AddCardToStack(ref Deck stack)
        {
            var c = deck.Pop();
            if (c != null)
            {
                stack.AddCard((Card)c);
            }
        }

        public void DrawCards()
        {
            for (int i = 0; i < stacks.Length; i++)
            {
                AddCardToStack(ref stacks[i]);
            }
        }
    }
}
