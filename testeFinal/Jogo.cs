using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testeFinal
{
    internal class Jogo
    {
        private Jogador[,] gameBoard;

        public Jogo(int rows, int cols)
        {
            gameBoard = new Jogador[rows, cols];
        }

        public void DisplayBoard()
        {
            Console.WriteLine("Tabuleiro Atual");
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == null)
                        Console.Write(" - ");
                    else
                        Console.Write($" {gameBoard[i, j].Simbolo} ");
                }
                Console.WriteLine();
            }
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Jogador CheckWin()
        {
            // Check rows
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (CheckLine(gameBoard, i, 0, 0, 1))
                {
                    return gameBoard[i, 0];
                }
            }
            
            // Check columns
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                if (CheckLine(gameBoard, 0, i, 1, 0))
                {
                    return gameBoard[0, i];
                }
            }

            // Check diagonals
            if (CheckLine(gameBoard, 0, 0, 1, 1))
            {
                return gameBoard[0, 0];
            }

            if (CheckLine(gameBoard, 0, gameBoard.GetLength(1) - 1, 1, -1))
            {
                return gameBoard[0, gameBoard.GetLength(1) - 1];
            }

            return null;
        }

        private bool CheckLine(Jogador[,] board, int startRow, int startCol, int rowIncrement, int colIncrement)
        {
            Jogador firstCell = board[startRow, startCol];
            if (firstCell == null)
            {
                return false;
            }

            for (int i = 1; i < board.GetLength(0) && i < board.GetLength(1); i++)
            {
                int row = startRow + i * rowIncrement;
                int col = startCol + i * colIncrement;

                if (row < 0 || row >= board.GetLength(0) || col < 0 || col >= board.GetLength(1) || board[row, col] != firstCell)
                {
                    return false;
                }
            }

            return true;
        }

        public void Play(int col, Jogador player)
        {
            if (IsValidColumn(col))
            {
                int row = GetLowestEmptyRow(col);
                if (row != -1)
                {
                    gameBoard[row, col] = player;
                }
                else
                {
                    Console.WriteLine("Coluna cheia. Jogada Inválida.");
                }
            }
            else
            {
                Console.WriteLine("Coluna Inválida. Jogada Inválida.");
            }
        }

        private bool IsValidColumn(int col)
        {
            return col >= 0 && col < gameBoard.GetLength(1);
        }

        private int GetLowestEmptyRow(int col)
        {
            for (int row = gameBoard.GetLength(0) - 1; row >= 0; row--)
            {
                if (gameBoard[row, col] == null)
                {
                    return row;
                }
            }
            return -1; // Retorna -1 se a coluna estiver cheia
        }

        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < gameBoard.GetLength(0) && col >= 0 && col < gameBoard.GetLength(1);
        }
    }


}
