using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testeFinal
{
    internal class ListaJogadores
    {
        private List<Jogador> todosJogadores = new List<Jogador>();
        private Dictionary<int, int> victoriesCount = new Dictionary<int, int>();
        public void IP(int numberOfPlayers)
        {
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.Write($"Meta o nome do jogador {i}: ");
                string playerName = Console.ReadLine();
                Console.Write($"Meta o simbolo do jogador {i}: ");
                char playerSymbol = char.Parse(Console.ReadLine());




                Jogador player = new Jogador(i, playerName,playerSymbol);
                todosJogadores.Add(player);
                victoriesCount.Add(i, 0);
            }
        }
        public void RemovePlayer(int playerId)
        {
            Jogador playerToRemove = todosJogadores.Find(p => p.Id == playerId);

            if (playerToRemove != null)
            {
                todosJogadores.Remove(playerToRemove);
                victoriesCount.Remove(playerId);
                Console.WriteLine($"Player {playerToRemove.Name} Removido.");
            }
            else
            {
                Console.WriteLine($"Jogador com ID {playerId} Removido ");
            }
        }
        public void UpdatePlayerVictories(Jogador winner)
        {
            victoriesCount[winner.Id]++;
            winner.Victories++;
            Console.WriteLine($"{winner.Name} ganhou! Total victorias: {winner.Victories}");
        }

        public int GetPlayerVictories(Jogador player)
        {
            return victoriesCount[player.Id];
        }
        public void LJ()
        {
            Console.WriteLine("\nTodos os Jogadores:");
            foreach (Jogador player in todosJogadores)
            {
                Console.WriteLine($"ID: {player.Id}, Nome: {player.Name}, Victórias: {player.Victories}");
            }
        }

        public List<Jogador> PP(int numberOfPlayersToPick)
        {
            List<Jogador> pickedPlayers = new List<Jogador>();

            for (int i = 0; i < numberOfPlayersToPick; i++)
            {
                Console.Write($"Insere o Id do jogador a escolher (1 até {todosJogadores.Count}): ");
                if (int.TryParse(Console.ReadLine(), out int playerId))
                {
                    Jogador pickedPlayer = todosJogadores.Find(p => p.Id == playerId);

                    if (pickedPlayer != null)
                    {
                        pickedPlayers.Add(pickedPlayer);
                        Console.WriteLine($"Jogador escolhido{pickedPlayer.Id}: {pickedPlayer.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Jogador com Id {playerId} não encontrado");
                        i--; 
                    }
                }
                else
                {
                    Console.WriteLine("Insira um ID Válido");
                    i--; 
                }
            }

            return pickedPlayers;
        }
        public void Desistir(Jogador forfeitingPlayer, List<Jogador> pickedPlayers)
        {
            Console.WriteLine($"{forfeitingPlayer.Name} desistiu.");
            pickedPlayers.Remove(forfeitingPlayer);
        }
        public void DPP(List<Jogador> pickedPlayers)
        {
            Console.WriteLine("\n Jogadores Escolhidos");
            foreach (Jogador pickedPlayer in pickedPlayers)
            {
                Console.WriteLine($"ID: {pickedPlayer.Id}, Nome: {pickedPlayer.Name}");
            }
        }
    }


}

    
