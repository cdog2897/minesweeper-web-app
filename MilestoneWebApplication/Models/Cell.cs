namespace MilestoneWebApplication.Models
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Visited { get; set; }
        public bool IsBomb { get; set; }
        public int Neighbors { get; set; }
        public bool Flag { get; set; }
        public int Id { get; set; }

        // defualt
        public Cell()
        {
            Row = -1;
            Col = -1;
            Visited = false;
            IsBomb = false;
            Neighbors = 0;
            Flag = false;
            Id = 0;
        }
        public override string ToString()
        {
            return "Cell row: "+this.Row+" Cell column: "+this.Col+" Visited: "+this.Visited;
        }

    }
}
