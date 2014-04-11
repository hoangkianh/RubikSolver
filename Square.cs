namespace RubikSolver
{
    public class Square
    {
        private char _side; // F, T, D, R, L, B
        private int _row;
        private int _col;
        
        public char Side
        {
            get { return _side; }
            set { _side = value; }
        }
        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public int Col
        {
            get { return _col; }
            set { _col = value; }
        }
    }
}
