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
                Monster monster = world[x, y].MonsterInRoom;
                Console.WriteLine($"Encountered a {monster.Name}!");
                Console.WriteLine($"Fight it? (y/n)");
                bool loop = true;
                do
                {
                    string key = Console.ReadLine();
                    switch (key)
                    {
                        case "y":
                            Fight(player, monster);
                            if (world[x, y].MonsterInRoom.Health <= 0)
                                world[x, y].MonsterInRoom = null;
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

        private void Fight(Character player, Character monster)
        {
            
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine($"Player HP = {player.Health}                    {monster.Name} HP = {monster.Health}");
                Console.WriteLine("Attack: A, Run: R");
                string key = Console.ReadLine();
                switch (key)
                {
                    case "a":
                        player.Fight(monster);
                        Console.WriteLine($"take that you Beast!!");
                        Console.WriteLine($"{monster.Name} lost {player.AttackDamage} HP!");
                        monster.Fight(player);
                        Console.WriteLine($"{monster.Name} hit you for {monster.AttackDamage} HP!");
                        Console.WriteLine("press any key...");
                        Console.ReadKey();
                        if (player.Health<=0)
                        {
                            Console.Clear();
                            Console.WriteLine("Not liike thiiis");
                            Console.WriteLine("You died a horrible death");
                            Console.WriteLine("press any key");
                            Console.ReadKey();
                            GameOver();
                            loop = false;
                        }
                        else if(monster.Health<=0)
                        {
                            Console.Clear();
                            Console.WriteLine($"You slayed the {monster.Name}, good job!");
                            Console.WriteLine("press any key...");
                            Console.ReadKey();
                            loop = false;
                        }
                        break;
                    case "r":
                        
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
                        world[x, y].MonsterInRoom = new Goblin("Goblin", 50, 3, 1, "Alive");
                    else if (newMonster == 5)
                        world[x, y].MonsterInRoom = new Dragon("Dragon", 65, 8, 10, "Angry");
                }
                 
            }
            world[2, 2].MonsterInRoom = new Monster("Ozma", 1000, 50,15,"Almighty");
            world[3, 3].ItemInRoom = new Weapon("Great Sword", "Sword", 2, false, 5);
            world[4, 3].ItemInRoom = new Weapon("Giant Sword","Sword", 10,false,2);

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

                       // Monster monster = room.MonsterInRoom;
                        Console.Write(room.MonsterInRoom.Name.Substring(0,1));
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
