using System.Reflection;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace MilestoneWebApplication.Models
{
    public class Board
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public Cell[,] Grid { get; set; }

        public int Id { get; set; }
        public DateTime TimeSaved { get; set; }
        public bool HasWon { get; set; }
        public bool IsFinished { get; set; }

        public Board(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            TimeSaved = DateTime.Now;
        }


        public Cell[,] createBoard(int numBombs)
        {
            Cell[,] grid = new Cell[Row, Col];

            // create cells using for loop
            for (int x = 0; x < Row; x++)
            {
                for (int y = 0; y < Col; y++)
                {
                    Cell cell = new Cell();
                    cell.Row = x;
                    cell.Col = y;
                    cell.Visited = false;
                    cell.IsBomb = false;
                    cell.Neighbors = 0;
                    cell.Id = (x * Row) + y;

                    grid[x, y] = cell;
                }
            }
            Random random = new Random();
            for (int i = 0; i < numBombs; i++)
            {
                bool isBombAlreadyThere = true;
                while (isBombAlreadyThere)
                {
                    int ranRow = random.Next(grid.GetLength(0));
                    int ranCol = random.Next(grid.GetLength(1));
                    if (grid[ranRow, ranCol].IsBomb == false)
                    {
                        grid[ranRow, ranCol].IsBomb = true;
                        isBombAlreadyThere = false;
                    }
                }
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].Neighbors = createNeighbor(grid[x, y], grid);
                }
            }
            return grid;
        }

        public int createNeighbor(Cell cell, Cell[,] grid)
        {
            int neighbor = 0;
            for (int i = cell.Row - 1; i < cell.Row + 2; i++)
            {
                for (int j = cell.Col - 1; j < cell.Col + 2; j++)
                {
                    try
                    {
                        if (grid[i, j].IsBomb == true)
                        {
                            neighbor++;
                        }
                    }
                    catch { }
                }
            }
            return neighbor;
        }

        public void floodFill(int x, int y)
        {
            if (isValid(x, y) && Grid[x, y].Visited == false)
            {
                Grid[x, y].Visited = true;

                if (Grid[x, y].Neighbors == 0)
                {
                    floodFill(x + 1, y);
                    floodFill(x, y + 1);
                    floodFill(x - 1, y);
                    floodFill(x, y - 1);
                }
            }
        }

        public bool isValid(int x, int y)
        {
            if (x >= 0 && x < Row && y >= 0 && y < Col)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void checkGame()
        {

            if(checkBomb())
            {
                checkFlags();
            }

        }

        public string gameMessage()
        {
            if(IsFinished && !HasWon)
            {
                return "You lost!";
            }
            else if(IsFinished && HasWon)
            {
                return "You Won!";
            }
            else
            {
                return "You have not won yet.";
            }
        }

        public void checkFlags()
        {
            bool gamewon = true;
            // check to see if a bomb has been visited.
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    if (!Grid[x, y].Flag && Grid[x,y].IsBomb)
                    {
                        gamewon = false;
                    }
                    else if (Grid[x,y].Flag && !Grid[x,y].IsBomb)
                    {
                        gamewon = false;
                    }
                }
            }
            if(gamewon)
            {
                IsFinished = true;
                HasWon = true;
            }
            else
            {
                IsFinished = false;
                HasWon = false;
            }
        }


        public bool checkBomb()
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    if (Grid[x, y].IsBomb == true && Grid[x, y].Visited == true)
                    {
                        IsFinished = true;
                        HasWon = false;
                        return false;
                    }
                }
            }
            return true;
        }

        public int checkForFlags()
        {
            int counter = 0;
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    if (Grid[x, y].Flag == true)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
