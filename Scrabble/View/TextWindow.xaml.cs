﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Scrabble.View;
using Scrabble.Model;
using Scrabble.Controller;
using Scrabble.Model.Game;
using Scrabble2018;

namespace Scrabble
{
    public partial class TextWindow : Window, IView
    {

        Game game;
        List<char> RackChar;
        private int ThisPlayer;
        int PlayerNow;
        Button[,] BoardButtons = new Button[15, 15];
        char[,] BoardCharView = new char[15, 15];
        public TextWindow(int P, Game g)
        {
            InitializeComponent();
            ThisPlayer = P;
            game = g;
            game.Subs(this);
            GameState.GSInstance.OnStateChanged += OnStateChanged;
            //this.Title = "Player " + (P + 1) + " - ScrabbleConsole";
            this.Title = String.Format(Scrabble2018.Properties.Skin.ViewTextPlayer, P + 1);
            RackChar = new List<char>();

            ConsoleBoardWriter(Welcome.WelcomeText);
            //ConsoleBoardWriter("Game Starts...");
            ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextStartGame);
            //ConsoleBoardWriter("This is a " + GameState.GSInstance.NumOfPlayers + " players game.");
            ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextNumofPlayer, GameState.GSInstance.NumOfPlayers));

            foreach (KeyValuePair<int, Tile> kvp in GameStartDraw.Drawn)
            {
                //ConsoleBoardWriter("Player " + (kvp.Key + 1) + " gets " + kvp.Value.TileChar + "!");
                ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextScore, kvp.Key + 1, kvp.Value.TileChar));
            }
            //ConsoleBoardWriter("Player " + (GameState.GSInstance.PlayerNow + 1) + " first!");
            ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextFirstPlayer, GameState.GSInstance.PlayerNow + 1));

            for (int i = 0; i < GameState.GSInstance.ListOfPlayers[ThisPlayer].PlayingTiles.Count; ++i)
            {
                char c = GameState.GSInstance.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar;
                RackChar.Add(c);
                if (GameState.GSInstance.PlayerNow == ThisPlayer) { EnableAll(); }

                else { DisableAll(); }
            }
            LoadBoardView();
            LoadRackView();
            StorageLbl.Content = '\0';
        }

        private void LoadBoardView()
        {
            ConsoleBoardWriter("      0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 Y");
            //I'm not sure if this needs to be localized
            for (int i = 0; i < BoardCharView.GetLength(0); ++i)
            {
                if (i < 10) ConsoleBoardWrite(i.ToString() + "   ");
                else ConsoleBoardWrite(i.ToString() + " ");
                for (int j = 0; j < BoardCharView.GetLength(1); ++j)
                {
                    BoardCharView[i, j] = game.gs.BoardChar[i, j];
                    ConsoleBoardWrite(BoardCharView[i, j]);
                }
                ConsoleBoardWriter("");
            }
            ConsoleBoardWriter("X");
        }

        private void ConsoleBoardWrite(object c)
        {
            ConsoleBoard.Text = ConsoleBoard.Text + c.ToString() + " ";
        }

        private void ConsoleBoardWriter(string s)
        {
            ConsoleBoard.Text = ConsoleBoard.Text + s + "\n";
            ConsoleBoard.Focus();
            ConsoleBoard.CaretIndex = ConsoleBoard.Text.Length;
            ConsoleBoard.ScrollToEnd();
        }

        private void GetNewTiles(List<char> LoC)
        {
            game.GetNewTiles(LoC, ThisPlayer);
        }

        private void Retry()
        {
            game.ClearMovement();
        }

        private void PrintScore()
        {
            //ConsoleBoardWrite("Scores of each player: ");
            ConsoleBoardWrite(Scrabble2018.Properties.Skin.TextPlayerScores);
            string str = "";
            foreach (Player p in GameState.GSInstance.ListOfPlayers)
            {
                //str += "Player " + p.Id + " - " + p.Score + " | ";
                str += String.Format(Scrabble2018.Properties.Skin.TextOnePlayerScore, p.Id, p.Score);
            }
            ConsoleBoardWrite(str.Substring(0, str.Length - 2));
            ConsoleBoardWriter("");
        }


        private void LoadRackView()
        {
            DisableAll();
            UserInputBox.Text = "";
            //ConsoleBoardWrite("***You have tiles: ");
            ConsoleBoardWrite(Scrabble2018.Properties.Skin.TextYourTiles);

            for (int i = 0; i < game.gs.ListOfPlayers[ThisPlayer].PlayingTiles.Count; ++i)
            {
                RackChar.Clear();
                RackChar.Add(game.gs.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar);
                ConsoleBoardWrite(game.gs.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar);
            }
            ConsoleBoardWriter("***");
            if (ThisPlayer == GameState.GSInstance.PlayerNow)
            {
                EnableAll();
                //ConsoleBoardWriter("Your turn now!");
                //ConsoleBoardWriter("Input by typing:");
                //ConsoleBoardWriter("\"SWAP A B...\" or \"MOVE A(7,7) ...\" to place or \"PASS\" or \"RANK\".");
                //ConsoleBoardWriter("Press Submit or ENTER key when you're done.");
                ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextYourTurn);
                ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextInput);
                ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextInstructions);
                ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextPressThis);

            }
        }


        private void ListingPrevWords()
        {
            //string s = "Player " + (GameState.GSInstance.PrevPlayer + 1) + " made the words: ";
            string s = String.Format(Scrabble2018.Properties.Skin.TextPlayerWord, GameState.GSInstance.PrevPlayer + 1);
            foreach (KeyValuePair<string, int> kvp in GameState.GSInstance.CorrectWords)
            {
                //s += kvp.Key + "(" + kvp.Value + " scores) ";
                s += kvp.Key + String.Format(Scrabble2018.Properties.Skin.TextWordScore, kvp.Value);
            }
            ConsoleBoardWriter(s);
        }

        private void Swap(string s)
        {
            s = s.Substring(5, s.Length - 5);
            List<string> StrGot = new List<string>(s.Split(' '));
            foreach (string str in StrGot)
            {
                if (RackChar.Contains(str[0]))
                {
                    game.SwapChar((str[0]));
                }
            }
            game.UpdateState(null);
        }

        private void Pass()
        {
            GameState.GSInstance.GamePass();
        }

        private void RejectInput()
        {
            //ConsoleBoardWriter("Game Judge: You've input a wrong command. Please try again!");
            ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextJudgeCommand);
            LoadRackView();
        }

        private void RejectSwap()
        {
            //ConsoleBoardWriter("Game Judge: You can't swap from now on!");
            ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextNoSwap);
            LoadRackView();
        }

        private void Validate(string s)
        {
            s = s.Substring(5, s.Length - 5);
            List<string> StrGot = new List<string>(s.Split(' '));
            List<char> CharList = new List<char>();
            foreach (string g in StrGot)
            {
                string[] ss = g.Split(',');
                ss[0] = ss[0].Substring(2, ss[0].Length - 2);
                ss[1] = ss[1].Remove(ss[1].Length - 1);
                int X;
                int Y;
                if (Int32.TryParse(ss[0], out X) && Int32.TryParse(ss[1], out Y) && (X >= 0 && X <= 14 && Y >= 0 && Y <= 14 && g[0] != '\0'))
                {
                    BoardCharView[X, Y] = g[0];
                }
                else
                {
                    ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextJudgeMove);
                    LoadBoardView();
                    LoadRackView();
                    Retry();
                    return;
                }
                game.moveRecorder.Record(X, Y);
                CharList.Add(g[0]);
            }
            if (game.Validate(BoardCharView))
            {
                GetNewTiles(CharList);
                game.UpdateState(BoardCharView);
                PlayerNow = GameState.GSInstance.PlayerNow;
            }
            else
            {
                ConsoleBoardWriter(Scrabble2018.Properties.Skin.TextNoScore);
                LoadBoardView();
                Retry();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // UpdateBoard();
            if (UserInputBox.Text.Equals(""))
            {
                RejectInput();
                return;
            }
            switch (UserInputBox.Text.ToString().Substring(0, 4))
            {
                case "SWAP":
                    {
                        if (game.CanSwap()) { Swap(UserInputBox.Text.ToString()); }
                        else RejectSwap();
                        return;
                    }
                case "PASS":
                    Pass();
                    return;
                case "MOVE":
                    Validate(UserInputBox.Text.ToString());
                    return;
                case "RANK":
                    PrintScore();
                    return;
                default:
                    RejectInput();
                    // None of the above
                    return;
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBoardView();
            Retry();
        }

        private void EnableAll()
        {
            this.Topmost = true;
            UserInputBox.IsEnabled = true;
            SubmitButton.IsEnabled = true;

        }

        private void DisableAll()
        {
            this.Topmost = false;
            UserInputBox.IsEnabled = false;
            SubmitButton.IsEnabled = false;

        }

        public void OnStateChanged()
        {
            // Enable all buttons

            //ConsoleBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1) + " finished his turn!");
            ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextPlayerFinish, GameState.GSInstance.PrevPlayer + 1));
            if (game.GameEnd())
            {
                foreach (Player p in game.gs.ListOfPlayers)
                {
                    ConsoleBoardWriter(game.gs.ListOfPlayers[PlayerNow].ToString());
                }
                game.gs.ListOfPlayers.Sort();
                //ConsoleBoardWriter("Winner is P" + (game.gs.ListOfPlayers[0].Id + 1) + " with scores" + (game.gs.ListOfPlayers[0].Score) + "!");
                ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextWinner, game.gs.ListOfPlayers[0].Score, game.gs.ListOfPlayers[0].Id + 1));
                DisableAll();
                return;
            }
            if (GameState.GSInstance.LastAction == "play")
            {
                ListingPrevWords();
                ConsoleBoardWriter(game.gs.ListOfPlayers[GameState.GSInstance.PrevPlayer].ToString());
            }
            else if (GameState.GSInstance.LastAction == "pass")
            {
                //ConsoleBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1) + " passed!");
                ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextPass, GameState.GSInstance.PrevPlayer + 1));
            }
            else if (GameState.GSInstance.LastAction == "swap")
            {
                //ConsoleBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1) + " swapped his tiles!");
                ConsoleBoardWriter(String.Format(Scrabble2018.Properties.Skin.TextSwap, GameState.GSInstance.PrevPlayer + 1));
            }
            PlayerNow = GameState.GSInstance.PlayerNow;
            LoadBoardView();
            LoadRackView();
        }

        private void UserInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitButton_Click(sender, e);
            }
        }
    }
}
