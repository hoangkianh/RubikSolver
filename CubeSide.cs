namespace RubikSolver
{
    public class CubeSide
    {
        private char _side;
        private char[,] _block = new char[3, 3];
        private Square[] moves;

        public char Side
        {
            get { return _side; }
        }

        public char[,] Block
        {
            get { return _block; }
            set { _block = value; }
        }

        internal Square[] Moves
        {
            get { return moves; }
        }
        
        // return color of a side
        public char Color { get { return _block[1, 1]; } }

        public CubeSide(char color, char side, Square[] move)
        {
            SetColor(color);
            _side = side;
            moves = move;
        }

        /// <summary>
        /// set color for a side
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(char color)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _block[i, j] = color;
                }
            }
        }
    }
}