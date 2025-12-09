using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace shooter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string platform = "-----";
            string mover = "";
            int bulletLen = 50;

            string[] enemies = NewEnemyArray();

            Console.WriteLine(" /$$$$$$$$        /$$$$$$  /$$   /$$  /$$$$$$   /$$$$$$  /$$$$$$$$ /$$$$$$$$ /$$$$$$$ \r\n| $$_____/       /$$__  $$| $$  | $$ /$$__  $$ /$$__  $$|__  $$__/| $$_____/| $$__  $$\r\n| $$            | $$  \\__/| $$  | $$| $$  \\ $$| $$  \\ $$   | $$   | $$      | $$  \\ $$\r\n| $$$$$         |  $$$$$$ | $$$$$$$$| $$  | $$| $$  | $$   | $$   | $$$$$   | $$$$$$$/\r\n| $$__/          \\____  $$| $$__  $$| $$  | $$| $$  | $$   | $$   | $$__/   | $$__  $$\r\n| $$             /$$  \\ $$| $$  | $$| $$  | $$| $$  | $$   | $$   | $$      | $$  \\ $$\r\n| $$$$$$$$      |  $$$$$$/| $$  | $$|  $$$$$$/|  $$$$$$/   | $$   | $$$$$$$$| $$  | $$\r\n|________/       \\______/ |__/  |__/ \\______/  \\______/    |__/   |________/|__/  |__/");
            Console.WriteLine("\n\nWelcome to E shooter!\n\nTo play, you type A or D and press enter to move a plaftorm either left or right.");
            Console.WriteLine("\n\nA bunch of enemies will spawn, and you must defeat all of them by entering spacebar which will shoot a large\nbullet towards them.\nThey will slowly get closer, and if they get too close you lose. Good Luck!");


            //while the returned boolean of GameEndCheck is false, it will keep looping through.
            while (!GameEndCheck(ref enemies))
            {
                GameStep(ref platform, ref enemies, ref mover, ref bulletLen);
            }



            Console.Clear();
            Console.WriteLine("$$\\     $$\\  $$$$$$\\  $$\\   $$\\       $$$$$$$\\  $$$$$$$$\\  $$$$$$\\ $$$$$$$$\\       $$$$$$$$\\ $$\\   $$\\ $$$$$$$$\\        $$$$$$\\   $$$$$$\\  $$\\      $$\\ $$$$$$$$\\ $$\\ \r\n\\$$\\   $$  |$$  __$$\\ $$ |  $$ |      $$  __$$\\ $$  _____|$$  __$$\\\\__$$  __|      \\__$$  __|$$ |  $$ |$$  _____|      $$  __$$\\ $$  __$$\\ $$$\\    $$$ |$$  _____|$$ |\r\n \\$$\\ $$  / $$ /  $$ |$$ |  $$ |      $$ |  $$ |$$ |      $$ /  $$ |  $$ |            $$ |   $$ |  $$ |$$ |            $$ /  \\__|$$ /  $$ |$$$$\\  $$$$ |$$ |      $$ |\r\n  \\$$$$  /  $$ |  $$ |$$ |  $$ |      $$$$$$$\\ |$$$$$\\    $$$$$$$$ |  $$ |            $$ |   $$$$$$$$ |$$$$$\\          $$ |$$$$\\ $$$$$$$$ |$$\\$$\\$$ $$ |$$$$$\\    $$ |\r\n   \\$$  /   $$ |  $$ |$$ |  $$ |      $$  __$$\\ $$  __|   $$  __$$ |  $$ |            $$ |   $$  __$$ |$$  __|         $$ |\\_$$ |$$  __$$ |$$ \\$$$  $$ |$$  __|   \\__|\r\n    $$ |    $$ |  $$ |$$ |  $$ |      $$ |  $$ |$$ |      $$ |  $$ |  $$ |            $$ |   $$ |  $$ |$$ |            $$ |  $$ |$$ |  $$ |$$ |\\$  /$$ |$$ |          \r\n    $$ |     $$$$$$  |\\$$$$$$  |      $$$$$$$  |$$$$$$$$\\ $$ |  $$ |  $$ |            $$ |   $$ |  $$ |$$$$$$$$\\       \\$$$$$$  |$$ |  $$ |$$ | \\_/ $$ |$$$$$$$$\\ $$\\ \r\n    \\__|     \\______/  \\______/       \\_______/ \\________|\\__|  \\__|  \\__|            \\__|   \\__|  \\__|\\________|       \\______/ \\__|  \\__|\\__|     \\__|\\________|\\__|");
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
        }


        //this is the whole game logic. It takes in the platform, enemies, player's choice and the length of the
        //bullet all as refrences (why as refrences? because I wanted to experiment with them lol)

        static void GameStep(ref string platform, ref string[] enemies, ref string mover, ref int bulletLen)
        {

            //only allows valid movements (a, d and spacebar)
            do
            {
                mover = Console.ReadLine().ToLower();
            } while (mover != "a" && mover != "d" && mover != " ");


            //if it is spacebar, it shoots and ends the function here (inversion)
            if(mover == " ")
            {
                //the shoot funciton returns an integer, so i just pass it in immediately like this
                EnemyIntersectCheck(Shoot(ref platform, ref bulletLen), ref enemies);
                return;
            }


            //goes left or right depending on user input
            if(mover == "d")
            {
                Right(ref platform);
            }
            else if (mover == "a")
            {
                Left(ref platform);
            }

            //draws the game grid using platform, enemies and the number is for checking bullet intersections.
            DrawGrid(ref platform, ref enemies);
        }




        //logic for moving right
        static void Right(ref string platform)
        {
            platform = "     " + platform;
            return;
        }

        //logic for moving left
        static void Left(ref string platform)
        {

            if (platform[4] != '-')
            {
                platform = platform.Substring(5, platform.Length - 5);
            }

            return;
        }


        //all the logic for shooting
        static int Shoot(ref string platform, ref int bulletLen)
        {
            //gets the position of the platform
            int pos = platform.IndexOf("-");
            string bullet = "";


            //This loops as many times as bulletLEn to indicate
            //the downwards length of the bullet.
            for(int i = 0; i <= bulletLen; i++)
            {
                string row = "";

                //Adds space until it reaches the 
                //index position of the first dash of the plaftorm, then adds the lines for the bullet.
                for(int x = 0; x <= pos; x++)
                {
                    if(x == pos)
                    {
                        row += "|||||";
                    }
                    else
                    {
                        row += " ";
                    }
                }

                bullet += row + "\n";
            }

            Console.Clear();
            Console.WriteLine(bullet);
            return pos;
        }



        //radnomly creates enemies
        static string[] NewEnemyArray()
        {
            Random r = new Random();

            int enemyNum = 30;
            string[] enemies = new string[enemyNum];


            //it starts at 15 because the enemies will slowly go up (hopefully)
            for (int i = 15; i < enemyNum ; i++)
            {
                string enemy = "e";

                int space = r.Next(1, 200);

                for (int x = 0; x <= space; x++)
                {
                    enemy = " " + enemy;
                }

                enemies[i] += enemy + "\n";
            }

            return enemies;
        }



        static void DrawGrid(ref string plaftorm, ref string[] enemies)
        {
            Console.Clear();
            Console.WriteLine(plaftorm);

            foreach (string e in enemies)
            {
                Console.WriteLine(e);
            }

            return;
        }


        static void EnemyIntersectCheck(int position, ref string[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                //checks if the enemy is in between the index of the bullet lines
                
                if (enemies[i] != null && enemies[i].IndexOf('e') >= position && enemies[i].IndexOf('e') <= position + 4)
                {
                    enemies[i] = " ";
                }
            }
        }

        static bool GameEndCheck(ref string[] enemies)
        {
            foreach (string e in enemies)
            {
                if(e != null && e.IndexOf('e') != -1)
                {
                    return false;
                }
            }

            return true;
        }



    }



}
