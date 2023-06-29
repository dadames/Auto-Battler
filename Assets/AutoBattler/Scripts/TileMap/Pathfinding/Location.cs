namespace AutoBattler
{
    public struct Location
    {
        private int _id, _x, _y, _cost;
        public int Id => _id;
        public int X => _x;
        public int Y => _y;
        public int Cost => _cost;

        public Location(int id, int x, int y, int cost)
        {
            _id = id;
            _x = x;
            _y = y;
            _cost = cost;
        }
    }
}
