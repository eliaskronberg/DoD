using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Game
    {
        const int WorldWidth = 20;
        const int WorldHeight = 10;
        Player player;
        Room[,] world;
        public void Start()
        {
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();
                DisplayStats();
                DisplayWorld();
                AskForMovement();
                player.Health--;
            } while (player.Health > 0);
            GameOver();
        }

        private void DisplayStats()
        {
            Console.WriteLine($"Name: {player.Name}, Health: {player.Health}, Dmg: {player.AttackDamage}");
            Console.WriteLine($"Pos: {player.X},{player.Y}");
            string backpack = "Backpack: ";
            foreach (var item in player.BackPack)
            {
                backpack += item.Name + " (weight: " + item.Weight + ")";
            }
            Console.WriteLine(backpack);
        }

        private void AskForMovement()
        {
            Console.WriteLine("Move");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int x = player.X;
            int y = player.Y;
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: x++; break;
                case ConsoleKey.LeftArrow: x--; break;
                case ConsoleKey.UpArrow: y--; break;
                case ConsoleKey.DownArrow: y++; break;
            }
            if (x >= 0 && x < WorldWidth && y >= 0 && y < WorldHeight)
            {
                player.X = x;
                player.Y = y;
                EnterRoom(x, y);
            }
            else
            {
                Console.WriteLine("Can't go there!");
                AskForMovement();
            }
        }
        private void EnterRoom(int x, int y)
        {
            if (world[x, y].MonsterInRoom != null)
            {
                Console.WriteLine($"Encountered a {world[x,y].MonsterInRoom.Name}!");
                Console.WriteLine($"Fight it? (y/n)");
                bool loop = true;
                do
                {
                    string key = Console.ReadLine();
                    switch (key)
                    {
                        case "y":
                            player.Fight(world[x, y].MonsterInRoom);
                            if (player.Status == "Dead")
                            {
                                GameOver();   
                            }
                            else
                            {
                                world[x, y].MonsterInRoom = null;
                            }
                            loop = false;
                            
                            break;
                        case "n":
                            Flee();
                            loop = false;
                            
                            break;
                        default:
                            Console.WriteLine("what do you mean, fool?");
                            break;
                    }
                } while (loop);
                //System.Threading.Thread.Sleep(3000);


            }
            if (world[x, y].ItemInRoom != null)
            {
                if (player.Capacity >= world[x, y].ItemInRoom.Weight)
                {
                    player.BackPack.Add(world[x, y].ItemInRoom);
                    player.Capacity -= world[x, y].ItemInRoom.Weight;
                    Console.WriteLine($"You found a {world[x,y].ItemInRoom.Name}");
                    System.Threading.Thread.Sleep(2000);
                    world[x, y].ItemInRoom = null;

                }
                else
                {
                    Console.WriteLine("Hmm, what't this? Too bad my backpack is full!");
                    //Console.WriteLine("Press any key to continue...");
                    //Console.ReadKey();
                    System.Threading.Thread.Sleep(3000);
                }
            }

        }

        private void Flee()
        {
            Console.WriteLine("Live to fight another day....coward");
        }

        private void Fight(int x, int y)
        {
            
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine($"Player HP = {player.Health}                    {world[x,y].MonsterInRoom.Name} HP = {world[x, y].MonsterInRoom.Health}");
                Console.WriteLine("Attack: A, Run: R");
                string key = Console.ReadLine();
                switch (key)
                {
                    case "a":
                        Console.Clear();
                        Console.WriteLine("take that you Beast!!");
                        
                        break;
                    case "r":
                        Flee();
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("what do you mean, fool?");
                        break;
                }
            } while (loop);
        }

        private void GameOver()
        {
            Console.Clear();

            Console.WriteLine("Game Over");
        }
        private void CreatePlayer()
        {
            player = new Player("Player", 100, 10, 10,"Alive",10);
        }

        private void CreateWorld()
        {
            Random random = new Random();
            world = new Room[WorldWidth, WorldHeight];
            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {
                    world[x, y] = new Room();
                    int newMonster = random.Next(1, 11);
                    if (newMonster == 3)
                        world[x, y].MonsterInRoom = new Monster("Mud Golem", 50, 3,1,"Alive");
                }
                 
            }
            world[2, 2].MonsterInRoom = new Monster("Ozma", 1000, 50,15,"Almighty");
            world[3, 3].ItemInRoom = new Item("Great Sword", 2,5,0,0,false);
            world[4, 3].ItemInRoom = new Item("Giant Sword", 10,8,0,0,false);

        }
        private void DisplayWorld()
        {
            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write('P');
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (room.MonsterInRoom != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Monster monster = room.MonsterInRoom;
                        Console.Write('M');
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write('.');
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
                Console.WriteLine();

            }
        }
    }
}
