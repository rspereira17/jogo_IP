using System.Numerics;
using System.Reflection;

namespace testeFinal
{
    internal class Program
    {
        static ListaJogadores listaJogadores = new ListaJogadores();
        static Jogo jogo = null;
        static int opcao;
        static int pecasEspeciais = 3;

        static void Main(string[] args)
        {



            Menu();
        }
        static void Menu()
        {
            
            do
            {

                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Inserir Jogadores");
                Console.WriteLine("2. Listar Jogadores");
                Console.WriteLine("3. Comecar Jogo");
                Console.WriteLine("4. Statisticas do jogador");
                Console.WriteLine("5. Remover Jogador");
                Console.WriteLine("6. Exit");


                Console.WriteLine("Insira uma opção 1-6");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.ReadKey();
                        Console.Clear();

                        Console.Clear();

                        Console.Write("Quantos Jogadores:  ");
                        if (int.TryParse(Console.ReadLine(), out int numberOfPlayers) && numberOfPlayers > 0)
                        {
                            listaJogadores.IP(numberOfPlayers);
                            Console.WriteLine($"Sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Numeor de jogadroes invalido");
                        }
                        break;

                        
                    case 2:
                        Console.ReadKey();
                        Console.Clear();
                        listaJogadores.LJ();
                        break;


                    case 3:
                        Console.ReadKey();
                        Console.Clear();
                        Console.Write("Insira o numero de jogadores a escolher ");
                        if (int.TryParse(Console.ReadLine(), out int numberOfPlayersToPick) && numberOfPlayersToPick > 0)
                        {
                            
                            List<Jogador> pickedPlayers = listaJogadores.PP(numberOfPlayersToPick);

                            if (pickedPlayers.Count > 0)
                            {
                                listaJogadores.DPP(pickedPlayers);

                                Console.ReadKey();
                                Console.WriteLine();
                                Console.WriteLine("Para poder ser usada a peça especial, o número de colunas do jogo tem de ser superior a 4.");
                                Console.WriteLine("A jogada especial insere 3 peças de uma só vez, na direção que o utilizador pretender(Esquerda ou Direita).");
                                Console.Write("Insira o numero de linhas para o jogo: ");
                                if (int.TryParse(Console.ReadLine(), out int numRows) && numRows > 0)
                                {
                                    Console.Write("Insira o numero de colunas para o jogo: ");
                                    if (int.TryParse(Console.ReadLine(), out int numCols) && numCols > 0)
                                    {
                                        jogo = new Jogo(numRows, numCols);
                                        Console.WriteLine($"Jogo criado com {numRows} linhas e {numCols} colunas.");
                                        jogo.DisplayBoard();
                                        int turn = 0;
                                        Jogador winner = null;

                                        if(numCols > 4)
                                        {                                   
                                            while (!jogo.IsBoardFull() && winner == null)
                                            {
                                                Jogador currentPlayer = pickedPlayers[turn % pickedPlayers.Count];

                                                Console.WriteLine($"\n{currentPlayer.Name} turno ({currentPlayer.Simbolo}) jogadas especiais({currentPlayer.jogadasEspecias}):");
                                                //Console.Write("Linha: ");
                                                //int row = int.Parse(Console.ReadLine());

                                                Console.Write("Insira o nº da Coluna, insira 'E' para a fazer a jogada especial(caso tenha jogadas) ou 'D' para desistir: ");
                                                string col2 = Console.ReadLine();
                                                int col;

                                                while (currentPlayer.jogadasEspecias == 0 && col2 == "e" || col2 == "E")
                                                {
                                                    Console.Write("Não tem mais jogadas especiais! Insira o nº da Coluna ou D para desistir: ");
                                                    col2 = Console.ReadLine();
                                                }

                                                if (col2 == "D" || col2 == "d")
                                                {
                                                    listaJogadores.Desistir(currentPlayer, pickedPlayers);
                                                    break;
                                                }
                                                

                                                if (col2 == "E" || col2 == "e")
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("Peça Especial. Insira o nº da Coluna em que quer jogar: ");
                                                    col = int.Parse(Console.ReadLine());

                                                    Console.WriteLine("Indique em que direção quer fazer a jogada. Clique E (esquerda) ou D (direita): ");
                                                    string direcao = Console.ReadLine();

                                                    currentPlayer.jogadasEspecias--;

                                                    if(direcao != "E" && direcao != "e" && direcao != "D" && direcao != "d")
                                                    {
                                                        while (direcao != "E" && direcao != "e" && direcao != "D" && direcao != "d")
                                                        {
                                                            Console.WriteLine("Inválido! Tem que inserir a letra E (esquerda) ou D (direita).");
                                                            direcao = Console.ReadLine();
                                                        }
                                                    }
                                                    

                                                    if (direcao == "E" || direcao == "e")
                                                    {
                                                        for (int i = 0; i < pecasEspeciais; i++)
                                                        {

                                                            jogo.Play(col - i, currentPlayer);

                                                        }
                                                        jogo.DisplayBoard();

                                                        winner = jogo.CheckWin();
                                                        if (winner != null)
                                                        {
                                                            listaJogadores.UpdatePlayerVictories(winner);
                                                        }
                                                        turn++;

                                                    }
                                                    else if (direcao == "D" || direcao == "d")
                                                    {
                                                        for (int i = 0; i < pecasEspeciais; i++)
                                                        {
                                                            jogo.Play(col + i, currentPlayer);

                                                        }
                                                        jogo.DisplayBoard();

                                                        winner = jogo.CheckWin();
                                                        if (winner != null)
                                                        {
                                                            listaJogadores.UpdatePlayerVictories(winner);
                                                        }
                                                        turn++;
                                                    }


                                                }
                                                else
                                                {
                                                    col = int.Parse(col2);
                                                    jogo.Play(col, currentPlayer);
                                                    jogo.DisplayBoard();

                                                    //Console.WriteLine();
                                                    //Console.WriteLine("Jogada especial - Clicar tecla: E ");

                                                    winner = jogo.CheckWin();

                                                    if (winner != null)
                                                    {
                                                        listaJogadores.UpdatePlayerVictories(winner);
                                                    }
                                                    //Console.Write("Queres desistir (s/n): ");
                                                    //if (Console.ReadLine().ToLower() == "s")
                                                    //{
                                                    //    listaJogadores.Desistir(currentPlayer, pickedPlayers);
                                                    //    break;
                                                    //}
                                                    turn++;
                                                }
                                                //col = int.Parse(Console.ReadLine());


                                            }
                                        }
                                        else
                                        {
                                            while (!jogo.IsBoardFull() && winner == null)
                                            {
                                                Jogador currentPlayer = pickedPlayers[turn % pickedPlayers.Count];

                                                Console.WriteLine($"\n{currentPlayer.Name} turno ({currentPlayer.Simbolo}):");
                                                //Console.Write("Linha: ");
                                                //int row = int.Parse(Console.ReadLine());

                                                Console.Write("Insira o nº da Coluna ou D para desistir: ");
                                                string col2 = Console.ReadLine();
                                                int col;

                                                if (col2 == "D" || col2 == "d")
                                                {
                                                    listaJogadores.Desistir(currentPlayer, pickedPlayers);
                                                    break;
                                                }                                                
                                                else
                                                {
                                                    col = int.Parse(col2);
                                                    jogo.Play(col, currentPlayer);
                                                    jogo.DisplayBoard();

                                                    //Console.WriteLine();
                                                    //Console.WriteLine("Jogada especial - Clicar tecla: E ");

                                                    winner = jogo.CheckWin();

                                                    if (winner != null)
                                                    {
                                                        listaJogadores.UpdatePlayerVictories(winner);
                                                    }
                                                    //Console.Write("Queres desistir (s/n): ");
                                                    //if (Console.ReadLine().ToLower() == "s")
                                                    //{
                                                    //    listaJogadores.Desistir(currentPlayer, pickedPlayers);
                                                    //    break;
                                                    //}
                                                    turn++;
                                                }
                                                //col = int.Parse(Console.ReadLine());


                                            }

                                        }



                                        if (winner == null)
                                        {
                                            Console.WriteLine("Empate tabuleiro cheio.");
                                        }


                                      
                                        listaJogadores.LJ();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Coluna Invalida");
                                    }
                                }
                                    else
                                    {
                                        Console.WriteLine("Coluna INvalida");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Linha Invalida");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhum jogador selecionado.Seleciona um primeiro");
                            }
                        
                       
                        break;

                        

                    case 4:
                        Console.ReadKey();
                        Console.Clear();
                        listaJogadores.LJ();
                        break;

                    case 5:
                        Console.ReadKey();
                        Console.Clear();

                        listaJogadores.LJ();
                        Console.Write("Insira o Id para Remover ");
                        if (int.TryParse(Console.ReadLine(), out int playerToRemoveId))
                        {
                            listaJogadores.RemovePlayer(playerToRemoveId);
                        }
                        else
                        {
                            Console.WriteLine("Id Invalido");
                        }
                        break;

                    case 6:
                        break;
                    default:
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Valor errado escolh aum valor entre 1-7!");
                        break;
                }
            }while (opcao != 6);
            }
           

    }
}



