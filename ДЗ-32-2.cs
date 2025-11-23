using System;

class Program
{
    static void Main()
    {      
        const char PlayerSymbol = '@';
        const char WallSymbol = '#';
        const char EmptySymbol = ' ';
        const char TreasureSymbol = '$';
        const char DoorSymbol = 'D';

        const char MoveUp = 'w';
        const char MoveDown = 's';
        const char MoveLeft = 'a';
        const char MoveRight = 'd';
        const char ExitGame = 'q';

        const int TotalTreasures = 3;

        char[,] map = {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '@', ' ', ' ', ' ', '#', ' ', ' ', '$', '#' },
            { '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', '$', '#', ' ', '#' },
            { '#', '$', '#', '#', '#', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', 'D', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
        };

        int playerPositionX = 1;
        int playerPositionY = 1;
        bool isPlaying = true;
        int treasuresFound = 0;

        while (isPlaying)
        {
            Console.Clear();
            DisplayMap(map);
            Console.WriteLine($"\nУправление: {MoveUp} - вверх, {MoveDown} - вниз, {MoveLeft} - влево, {MoveRight} - вправо, {ExitGame} - выход");
            Console.WriteLine($"Собрано сокровищ: {treasuresFound}/{TotalTreasures}");
            Console.Write("Введите команду: ");

            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (input == ExitGame)
            {
                isPlaying = false;
                Console.WriteLine("Выход из игры!");
                break;
            }

            int newPositionX = playerPositionX;
            int newPositionY = playerPositionY;
            CalculateNewPosition(input, ref newPositionX, ref newPositionY);

            if (CanMoveTo(map, newPositionX, newPositionY, WallSymbol))
            {
                char targetCell = map[newPositionY, newPositionX];

                if (targetCell == TreasureSymbol)
                {
                    treasuresFound++;
                    MovePlayer(map, ref playerPositionX, ref playerPositionY, newPositionX, newPositionY, PlayerSymbol, EmptySymbol);
                    Console.WriteLine($"Вы нашли сокровище! Всего собрано: {treasuresFound}/{TotalTreasures}");
                }
                else if (targetCell == DoorSymbol)
                {
                    if (treasuresFound == TotalTreasures)
                    {
                        MovePlayer(map, ref playerPositionX, ref playerPositionY, newPositionX, newPositionY, PlayerSymbol, EmptySymbol);
                        Console.WriteLine("Поздравляем! Вы собрали все сокровища и нашли выход! Победа!");
                        isPlaying = false;
                    }
                    else
                    {
                        Console.WriteLine($"Вам нужно собрать все сокровища! Осталось: {TotalTreasures - treasuresFound}");
                    }
                }
                else
                {
                    MovePlayer(map, ref playerPositionX, ref playerPositionY, newPositionX, newPositionY, PlayerSymbol, EmptySymbol);
                }
            }
            else
            {
                Console.WriteLine("Нельзя пройти сквозь стену!");
            }
        }

        Console.WriteLine("Игра завершена. Спасибо за игру!");
    }

    static void DisplayMap(char[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Console.Write(map[y, x]);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    static bool CanMoveTo(char[,] map, int positionX, int positionY, char wallSymbol)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        if (positionX < 0 || positionX >= cols || positionY < 0 || positionY >= rows)
        {
            return false;
        }

        char targetCell = map[positionY, positionX];
        return targetCell != wallSymbol;
    }

    static void CalculateNewPosition(char input, ref int positionX, ref int positionY)
    {
        switch (char.ToLower(input))
        {
            case 'w':
                positionY--;
                break;
            case 's':
                positionY++;
                break;
            case 'a':
                positionX--;
                break;
            case 'd':
                positionX++;
                break;
            default:
                Console.WriteLine("Неверная команда! Используйте W, A, S, D для движения.");
                break;
        }
    }

    static void MovePlayer(char[,] map, ref int playerPositionX, ref int playerPositionY,
                          int newPositionX, int newPositionY, char playerSymbol, char emptySymbol)
    {
        map[playerPositionY, playerPositionX] = emptySymbol;
        playerPositionX = newPositionX;
        playerPositionY = newPositionY;
        map[playerPositionY, playerPositionX] = playerSymbol;
    }
}