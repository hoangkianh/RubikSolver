using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CSGL12;

namespace RubikSolver
{
    public class Solver
    {
        private const char YELLOW = '1';
        private const char BLUE = '2';
        private const char RED = '3';
        private const char ORANGE = '4';
        private const char GREEN = '5';
        private const char WHITE = '6';

        private const int NUM_EDGES = 12;
        private const int NUM_CORNERS = 8;

        private const int SFRONT = 1;
        private const int SRIGHT = 2;
        private const int SBACK = 4;
        private const int SLEFT = 8;

        private const int FRONTRIGHT = 3;
        private const int FRONTBACK = 5;
        private const int FRONTLEFT = 9;
        private const int RIGHTBACK = 6;
        private const int RIGHTLEFT = 10;
        private const int BACKLEFT = 12;

        private const int MFRONTRIGHT = 1;
        private const int MFRONTLEFT = 2;
        private const int MFRONTBACK = 4;
        private const int MRIGHTBACK = 8;
        private const int MRIGHTLEFT = 16;
        private const int MRIGHTFRONT = 32;
        private const int MLEFTFRONT = 64;
        private const int MLEFTRIGHT = 128;
        private const int MLEFTBACK = 256;
        private const int MBACKLEFT = 512;
        private const int MBACKFRONT = 1024;
        private const int MBACKRIGHT = 2048;

        public CSGL12Control MyGLControl;

        #region Edges & Corners

        private readonly Square[] Corners =
            {
                new Square {Side = 'T', Row = 2, Col = 0}, new Square {Side = 'F', Row = 0, Col = 0}, new Square {Side = 'L', Row = 0, Col = 2},
                new Square {Side = 'T', Row = 2, Col = 2}, new Square {Side = 'F', Row = 0, Col = 2}, new Square {Side = 'R', Row = 0, Col = 0},
                new Square {Side = 'T', Row = 0, Col = 2}, new Square {Side = 'B', Row = 0, Col = 0}, new Square {Side = 'R', Row = 0, Col = 2},
                new Square {Side = 'T', Row = 0, Col = 0}, new Square {Side = 'B', Row = 0, Col = 2}, new Square {Side = 'L', Row = 0, Col = 0},
                new Square {Side = 'D', Row = 0, Col = 2}, new Square {Side = 'F', Row = 2, Col = 2}, new Square {Side = 'R', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 0, Col = 0}, new Square {Side = 'F', Row = 2, Col = 0}, new Square {Side = 'L', Row = 2, Col = 2},
                new Square {Side = 'D', Row = 2, Col = 0}, new Square {Side = 'B', Row = 2, Col = 2}, new Square {Side = 'L', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 2, Col = 2}, new Square {Side = 'B', Row = 2, Col = 0}, new Square {Side = 'R', Row = 2, Col = 2}
            };

        private readonly Square[] Edges =
            {
                new Square {Side = 'T', Row = 2, Col = 1}, new Square {Side = 'F', Row = 0, Col = 1},
                new Square {Side = 'T', Row = 0, Col = 1}, new Square {Side = 'B', Row = 0, Col = 1},
                new Square {Side = 'T', Row = 1, Col = 2}, new Square {Side = 'R', Row = 0, Col = 1},
                new Square {Side = 'T', Row = 1, Col = 0}, new Square {Side = 'L', Row = 0, Col = 1},
                new Square {Side = 'F', Row = 1, Col = 2}, new Square {Side = 'R', Row = 1, Col = 0},
                new Square {Side = 'F', Row = 2, Col = 1}, new Square {Side = 'D', Row = 0, Col = 1},
                new Square {Side = 'F', Row = 1, Col = 0}, new Square {Side = 'L', Row = 1, Col = 2},
                new Square {Side = 'D', Row = 1, Col = 2}, new Square {Side = 'R', Row = 2, Col = 1},
                new Square {Side = 'D', Row = 2, Col = 1}, new Square {Side = 'B', Row = 2, Col = 1},
                new Square {Side = 'D', Row = 1, Col = 0}, new Square {Side = 'L', Row = 2, Col = 1},
                new Square {Side = 'B', Row = 1, Col = 2}, new Square {Side = 'L', Row = 1, Col = 0},
                new Square {Side = 'B', Row = 1, Col = 0}, new Square {Side = 'R', Row = 1, Col = 2}
            };

        #endregion

        private readonly CubeSide _back;
        private readonly CubeSide _down;
        private readonly CubeSide _front;
        private readonly CubeSide _left;
        private readonly CubeSide _right;
        private readonly CubeSide _top;
        // original side (use to save or reset)
        private readonly CubeSide _oBack;
        private readonly CubeSide _oDown;
        private readonly CubeSide _oFront;
        private readonly CubeSide _oLeft;
        private readonly CubeSide _oRight;
        private readonly CubeSide _oTop;

        private string m_S; // solution string
        /// <summary>
        /// List solutions string (always has 24 solutions)
        /// because 6 sides is the TOP 6 times, each times we must to rotate 4 times
        /// </summary>
        private readonly List<string> m_Solution = new List<string>();

        #region Move array

        private readonly Square[] BackMove =
            {
                new Square {Side = 'T', Row = 0, Col = 2}, 
                new Square {Side = 'T', Row = 0, Col = 1},
                new Square {Side = 'T', Row = 0, Col = 0},
                new Square {Side = 'L', Row = 0, Col = 0},
                new Square {Side = 'L', Row = 1, Col = 0},
                new Square {Side = 'L', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 2, Col = 1},
                new Square {Side = 'D', Row = 2, Col = 2},
                new Square {Side = 'R', Row = 2, Col = 2},
                new Square {Side = 'R', Row = 1, Col = 2},
                new Square {Side = 'R', Row = 0, Col = 2}
            };

        private readonly Square[] DownMove =
            {
                new Square {Side = 'F', Row = 2, Col = 0},
                new Square {Side = 'F', Row = 2, Col = 1},
                new Square {Side = 'F', Row = 2, Col = 2},
                new Square {Side = 'R', Row = 2, Col = 0},
                new Square {Side = 'R', Row = 2, Col = 1},
                new Square {Side = 'R', Row = 2, Col = 2},
                new Square {Side = 'B', Row = 2, Col = 0},
                new Square {Side = 'B', Row = 2, Col = 1},
                new Square {Side = 'B', Row = 2, Col = 2},
                new Square {Side = 'L', Row = 2, Col = 0},
                new Square {Side = 'L', Row = 2, Col = 1},
                new Square {Side = 'L', Row = 2, Col = 2}
            };

        private readonly Square[] FrontMove =
            {
                new Square {Side = 'T', Row = 2, Col = 0},
                new Square {Side = 'T', Row = 2, Col = 1},
                new Square {Side = 'T', Row = 2, Col = 2},
                new Square {Side = 'R', Row = 0, Col = 0},
                new Square {Side = 'R', Row = 1, Col = 0},
                new Square {Side = 'R', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 0, Col = 2},
                new Square {Side = 'D', Row = 0, Col = 1},
                new Square {Side = 'D', Row = 0, Col = 0},
                new Square {Side = 'L', Row = 2, Col = 2},
                new Square {Side = 'L', Row = 1, Col = 2},
                new Square {Side = 'L', Row = 0, Col = 2}
            };

        private readonly Square[] LeftMove =
            {
                new Square {Side = 'T', Row = 0, Col = 0},
                new Square {Side = 'T', Row = 1, Col = 0},
                new Square {Side = 'T', Row = 2, Col = 0},
                new Square {Side = 'F', Row = 0, Col = 0},
                new Square {Side = 'F', Row = 1, Col = 0},
                new Square {Side = 'F', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 0, Col = 0},
                new Square {Side = 'D', Row = 1, Col = 0},
                new Square {Side = 'D', Row = 2, Col = 0},
                new Square {Side = 'B', Row = 2, Col = 2},
                new Square {Side = 'B', Row = 1, Col = 2},
                new Square {Side = 'B', Row = 0, Col = 2}
            };

        private readonly Square[] RightMove =
            {
                new Square {Side = 'T', Row = 2, Col = 2},
                new Square {Side = 'T', Row = 1, Col = 2},
                new Square {Side = 'T', Row = 0, Col = 2},
                new Square {Side = 'B', Row = 0, Col = 0},
                new Square {Side = 'B', Row = 1, Col = 0},
                new Square {Side = 'B', Row = 2, Col = 0},
                new Square {Side = 'D', Row = 2, Col = 2},
                new Square {Side = 'D', Row = 1, Col = 2},
                new Square {Side = 'D', Row = 0, Col = 2},
                new Square {Side = 'F', Row = 2, Col = 2},
                new Square {Side = 'F', Row = 1, Col = 2},
                new Square {Side = 'F', Row = 0, Col = 2}
            };

        private readonly Square[] TopMove =
            {
                new Square {Side = 'B', Row = 0, Col = 2},
                new Square {Side = 'B', Row = 0, Col = 1},
                new Square {Side = 'B', Row = 0, Col = 0},
                new Square {Side = 'R', Row = 0, Col = 2},
                new Square {Side = 'R', Row = 0, Col = 1},
                new Square {Side = 'R', Row = 0, Col = 0},
                new Square {Side = 'F', Row = 0, Col = 2},
                new Square {Side = 'F', Row = 0, Col = 1},
                new Square {Side = 'F', Row = 0, Col = 0},
                new Square {Side = 'L', Row = 0, Col = 2},
                new Square {Side = 'L', Row = 0, Col = 1},
                new Square {Side = 'L', Row = 0, Col = 0}
            };

        #endregion

        protected Solver()
        {
            _top = new CubeSide(YELLOW, 'T', TopMove);
            _front = new CubeSide(BLUE, 'F', FrontMove);
            _right = new CubeSide(RED, 'R', RightMove);
            _left = new CubeSide(ORANGE, 'L', LeftMove);
            _back = new CubeSide(GREEN, 'B', BackMove);
            _down = new CubeSide(WHITE, 'D', DownMove);

            _oTop = new CubeSide(YELLOW, 'T', TopMove);
            _oFront = new CubeSide(BLUE, 'F', FrontMove);
            _oRight = new CubeSide(RED, 'R', RightMove);
            _oLeft = new CubeSide(ORANGE, 'L', LeftMove);
            _oBack = new CubeSide(GREEN, 'B', BackMove);
            _oDown = new CubeSide(WHITE, 'D', DownMove);
        }

        public virtual void Reset()
        {
            _top.SetColor('1');
            _front.SetColor('2');
            _right.SetColor('3');
            _left.SetColor('4');
            _back.SetColor('5');
            _down.SetColor('6');
        }

        public void Shuffle(ToolStripProgressBar progressBar)
        {
            progressBar.Maximum = 20;
            progressBar.Minimum = 0;
            progressBar.Value = 1;
            progressBar.Step = 1;
            int lastFace = -1;

            for (int i = 0; i < 20; i++)
            {
                var rd = new Random();

                int face = rd.Next(0, 100) % 6;
                int dir = rd.Next(0, 100) % 2;

                while (face == lastFace)
                {
                    face = rd.Next(0, 100) % 6;
                }

                lastFace = face;
                CubeSide side = null;

                switch (face)
                {
                    case 0:
                        side = _front;
                        break;
                    case 1:
                        side = _back;
                        break;
                    case 2:
                        side = _right;
                        break;
                    case 3:
                        side = _left;
                        break;
                    case 4:
                        side = _down;
                        break;
                    case 5:
                        side = _top;
                        break;
                }

                if (side != null)
                {
                    switch (dir)
                    {
                        case 0:
                            RotateCW(side);
                            break;
                        case 1:
                            RotateCCW(side);
                            break;
                        case 2:
                            RotateCW(side);
                            RotateCW(side);
                            break;
                    }
                }
                if (progressBar.ProgressBar == null)
                {
                    return;
                }
                progressBar.PerformStep();
            }
            progressBar.Value = 0; //reset progressbar to 0
        }

        public string GetMoves()
        {
            return m_S;
        }

        /// <summary>
        /// rotate the entire cube to the left
        /// F = R, R = B, B = L, L = F
        /// </summary>
        private void RotateCubeLeft()
        {
            char[,] tmp = _front.Block;

            _front.Block = _right.Block;
            _right.Block = _back.Block;
            _back.Block = _left.Block;
            _left.Block = tmp;

            RotateFaceCW(_top);
            RotateFaceCCW(_down);
        }

        /// <summary>
        /// rotate the entire cube to the right
        /// F = L, L = B, B = R, R = F
        /// </summary>
        private void RotateCubeRight()
        {
            char[,] tmpBlock = _front.Block;

            _front.Block = _left.Block;
            _left.Block = _back.Block;
            _back.Block = _right.Block;
            _right.Block = tmpBlock;

            RotateFaceCCW(_top);
            RotateFaceCW(_down);
        }

        /// <summary>
        /// rotate the cube upward
        /// F = D, D = B, B = T, T = F
        /// </summary>
        private void RotateCubeUp()
        {
            char[,] tmp = _top.Block;

            _top.Block = _front.Block;
            _front.Block = _down.Block;
            _down.Block = _back.Block;
            _back.Block = tmp;

            RotateFaceCW(_back);
            RotateFaceCW(_back);
            RotateFaceCW(_down);
            RotateFaceCW(_down);
            RotateFaceCW(_right);
            RotateFaceCCW(_left);
        }

        /// <summary>
        /// rotate the cube downward
        /// F = T, T = B, B = D, D = F
        /// </summary>
        private void RotateCubeDown()
        {
            char[,] tmp = _top.Block;

            _top.Block = _back.Block;
            _back.Block = _down.Block;
            _down.Block = _front.Block;
            _front.Block = tmp;

            RotateFaceCW(_back);
            RotateFaceCW(_back);
            RotateFaceCW(_top);
            RotateFaceCW(_top);
            RotateFaceCW(_left);
            RotateFaceCCW(_right);
        }

        /// <summary>
        /// Rotate cube clock wise
        /// </summary>
        /// <param name="side">Side to rotate</param>
        protected virtual void RotateCW(CubeSide side)
        {
            if (side != null)
            {
                RotateFaceCW(side);

                Square[] moves = side.Moves;
                char tmp1 = GetSide(moves[9].Side).Block[moves[9].Row, moves[9].Col];
                char tmp2 = GetSide(moves[10].Side).Block[moves[10].Row, moves[10].Col];
                char tmp3 = GetSide(moves[11].Side).Block[moves[11].Row, moves[11].Col];

                for (int i = 8; i >= 0; i--)
                {
                    GetSide(moves[i + 3].Side).Block[moves[i + 3].Row, moves[i + 3].Col] =
                        GetSide(moves[i].Side).Block[moves[i].Row, moves[i].Col];
                }

                GetSide(moves[0].Side).Block[moves[0].Row, moves[0].Col] = tmp1;
                GetSide(moves[1].Side).Block[moves[1].Row, moves[1].Col] = tmp2;
                GetSide(moves[2].Side).Block[moves[2].Row, moves[2].Col] = tmp3;
            }
        }

        /// <summary>
        /// Rotate cube counter clockwise
        /// </summary>
        /// <param name="side">Side to rotate</param>
        protected virtual void RotateCCW(CubeSide side)
        {
            if (side != null)
            {
                RotateFaceCCW(side);

                Square[] moves = side.Moves;
                char tmp1 = GetSide(moves[0].Side).Block[moves[0].Row, moves[0].Col];
                char tmp2 = GetSide(moves[1].Side).Block[moves[1].Row, moves[1].Col];
                char tmp3 = GetSide(moves[2].Side).Block[moves[2].Row, moves[2].Col];

                for (int i = 0; i < 9; i++)
                {
                    GetSide(moves[i].Side).Block[moves[i].Row, moves[i].Col] =
                        GetSide(moves[i + 3].Side).Block[moves[i + 3].Row, moves[i + 3].Col];
                }

                GetSide(moves[9].Side).Block[moves[9].Row, moves[9].Col] = tmp1;
                GetSide(moves[10].Side).Block[moves[10].Row, moves[10].Col] = tmp2;
                GetSide(moves[11].Side).Block[moves[11].Row, moves[11].Col] = tmp3;
            }
        }

        /// <summary>
        /// Save the cube
        /// </summary>
        protected virtual void SaveCube()
        {
            SaveSide(_top, _oTop);
            SaveSide(_front, _oFront);
            SaveSide(_right, _oRight);
            SaveSide(_left, _oLeft);
            SaveSide(_back, _oBack);
            SaveSide(_down, _oDown);
        }

        /// <summary>
        /// Restore a saved cube
        /// </summary>
        protected virtual void RestoreCube()
        {
            SaveSide(_oTop, _top);
            SaveSide(_oFront, _front);
            SaveSide(_oRight, _right);
            SaveSide(_oLeft, _left);
            SaveSide(_oBack, _back);
            SaveSide(_oDown, _down);
        }

        /// <summary>
        /// Save the face of cube
        /// </summary>
        /// <param name="pSide">Side need to save</param>
        /// <param name="pSaveTo">Side saved to</param>
        private void SaveSide(CubeSide pSide, CubeSide pSaveTo)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pSaveTo.Block[i, j] = pSide.Block[i, j];
                }
            }
        }

        /// <summary>
        /// Return a pointer to a side given the side letter
        /// </summary>
        /// <param name="side">T = Top, F = Front, D = Down, R = Right, L = Left, B = Bottom</param>
        /// <returns>Cube's side which has side leter</returns>
        private CubeSide GetSide(char side)
        {
            side = char.ToUpper(side);
            switch (side)
            {
                case 'T':
                    return _top;
                case 'F':
                    return _front;
                case 'D':
                    return _down;
                case 'R':
                    return _right;
                case 'L':
                    return _left;
                case 'B':
                    return _back;
            }
            return null;
        }

        /// <summary>
        /// find the side with the edge having the two colors 
        /// the side that is returned is the side with "color"
        /// to get the other side reverse the paramters in the
        /// function call
        /// </summary>
        /// <param name="color">first color</param>
        /// <param name="color2">second color</param>
        /// <returns>cube's side which has 'color'</returns>
        private CubeSide FindEdge(char color, char color2)
        {
            for (int i = 0; i < NUM_EDGES; i++)
            {
                CubeSide side1 = GetSide(Edges[i * 2].Side);
                CubeSide side2 = GetSide(Edges[(i * 2) + 1].Side);

                char s1 = side1.Block[Edges[i * 2].Row, Edges[i * 2].Col];
                char s2 = side2.Block[Edges[(i * 2) + 1].Row, Edges[(i * 2) + 1].Col];

                if (s1 == color && s2 == color2)
                {
                    return side1;
                }
                if (s2 == color && s1 == color2)
                {
                    return side2;
                }
            }

            return null;
        }

        /// <summary>
        /// find a corner with the given three colors
        /// the Side that is returned is the side with "color"
        /// </summary>
        /// <param name="color">first color</param>
        /// <param name="color2">second color</param>
        /// <param name="color3">third color</param>
        /// <returns>cube's corner has 'color'</returns>
        private CubeSide FindCorner(char color, char color2, char color3)
        {
            for (int i = 0; i < NUM_CORNERS; i++)
            {
                CubeSide side1 = GetSide(Corners[i * 3].Side);
                CubeSide side2 = GetSide(Corners[(i * 3) + 1].Side);
                CubeSide side3 = GetSide(Corners[(i * 3) + 2].Side);

                char s1 = side1.Block[Corners[i * 3].Row, Corners[i * 3].Col];
                char s2 = side2.Block[Corners[(i * 3) + 1].Row, Corners[(i * 3) + 1].Col];
                char s3 = side3.Block[Corners[(i * 3) + 2].Row, Corners[(i * 3) + 2].Col];

                if (s1 == color &&
                    (s2 == color2 || s2 == color3) &&
                    (s3 == color3 || s3 == color2))
                {
                    return side1;
                }

                if (s2 == color &&
                    (s1 == color2 || s1 == color3) &&
                    (s3 == color3 || s3 == color2))
                {
                    return side2;
                }

                if (s3 == color &&
                    (s1 == color2 || s1 == color3) &&
                    (s2 == color2 || s2 == color3))
                {
                    return side3;
                }
            }
            return null;
        }

        /// <summary>
        /// find a side with a center cubelet has color "color"
        /// </summary>
        /// <param name="color">color of center cubelet</param>
        /// <returns>cube's side has a center cublet has 'color'</returns>
        private CubeSide FindCenter(char color)
        {
            if (_top.Color == color)
            {
                return _top;
            }

            if (_front.Color == color)
            {
                return _front;
            }

            if (_down.Color == color)
            {
                return _down;
            }

            if (_back.Color == color)
            {
                return _back;
            }

            if (_right.Color == color)
            {
                return _right;
            }

            if (_left.Color == color)
            {
                return _left;
            }

            return null;
        }

        /// <summary>
        /// Find color of the side
        /// </summary>
        /// <param name="side">side we need to find color</param>
        /// <returns>color of the side</returns>
        public char FindColor(char side)
        {
            if (side.Equals(_top.Side))
            {
                return _top.Color;
            }
            if (side.Equals(_down.Side))
            {
                return _down.Color;
            }
            if (side.Equals(_left.Side))
            {
                return _left.Color;
            }
            if (side.Equals(_right.Side))
            {
                return _right.Color;
            }
            if (side.Equals(_front.Side))
            {
                return _front.Color;
            }
            return _back.Color;
        }

        /// <summary>
        /// Rotate a face clock wise
        /// </summary>
        /// <param name="side">side of face</param>
        private void RotateFaceCW(CubeSide side)
        {
            char tmp1 = side.Block[1, 2];
            char tmp2 = side.Block[2, 2];

            side.Block[2, 2] = side.Block[0, 2];
            side.Block[1, 2] = side.Block[0, 1];
            side.Block[0, 2] = side.Block[0, 0];

            side.Block[0, 2] = side.Block[0, 0];
            side.Block[0, 1] = side.Block[1, 0];
            side.Block[0, 0] = side.Block[2, 0];

            side.Block[1, 0] = side.Block[2, 1];
            side.Block[2, 0] = tmp2;
            side.Block[2, 1] = tmp1;
        }

        /// <summary>
        /// rotate a face counter clockwise
        /// </summary>
        /// <param name="side">side of face</param>
        private void RotateFaceCCW(CubeSide side)
        {
            char tmp1 = side.Block[0, 0];
            char tmp2 = side.Block[0, 1];

            side.Block[0, 0] = side.Block[0, 2];
            side.Block[0, 1] = side.Block[1, 2];
            side.Block[0, 2] = side.Block[2, 2];

            side.Block[0, 2] = side.Block[2, 2];
            side.Block[1, 2] = side.Block[2, 1];
            side.Block[2, 2] = side.Block[2, 0];

            side.Block[2, 1] = side.Block[1, 0];
            side.Block[2, 0] = tmp1;
            side.Block[1, 0] = tmp2;
        }

        /// <summary>
        /// do move with moves string
        /// </summary>
        /// <param name="moves">solution string</param>
        public void DoMove(string moves)
        {
            if (moves.Length < 3)
            {
                return;
            }

            int start = 0;
            string move = moves.Substring(start, 3);

            while (true)
            {
                CubeSide side = null;
                switch (move[0])
                {
                    case 'C':
                        switch (move[1])
                        {
                            case '+':
                                RotateCubeRight();
                                break;
                            case '-':
                                RotateCubeLeft();
                                break;
                            case '^':
                                RotateCubeUp();
                                break;
                            case 'v':
                                RotateCubeDown();
                                break;
                        }
                        break;
                    case 'F':
                        side = _front;
                        break;
                    case 'R':
                        side = _right;
                        break;
                    case 'B':
                        side = _back;
                        break;
                    case 'L':
                        side = _left;
                        break;
                    case 'T':
                        side = _top;
                        break;
                    case 'D':
                        side = _down;
                        break;
                }

                if (side != null)
                {
                    switch (move[1])
                    {
                        case '+':
                            RotateCW(side);
                            break;
                        case '-':
                            RotateCCW(side);
                            break;
                        case '*':
                            RotateCW(side);
                            RotateCW(side);
                            break;
                    }
                }

                start += 3;
                if (start >= moves.Length)
                {
                    break;
                }
                move = moves.Substring(start, 3);
            }
        }

        /// <summary>
        /// undo a move with move string
        /// </summary>
        /// <param name="move">a move need to undo</param>
        public void UndoMove(string move)
        {
            if (move.Length < 3)
            {
                return;
            }
            CubeSide side = null;

            switch (move[0])
            {
                case 'C':
                    switch (move[1])
                    {
                        case '+':
                            RotateCubeLeft();
                            break;
                        case '-':
                            RotateCubeRight();
                            break;
                        case '^':
                            RotateCubeDown();
                            break;
                        case 'v':
                            RotateCubeUp();
                            break;
                    }
                    break;
                case 'F':
                    side = _front;
                    break;
                case 'R':
                    side = _right;
                    break;
                case 'B':
                    side = _back;
                    break;
                case 'L':
                    side = _left;
                    break;
                case 'T':
                    side = _top;
                    break;
                case 'D':
                    side = _down;
                    break;
            }

            if (side != null)
            {
                switch (move[1])
                {
                    case '+':
                        RotateCCW(side);
                        break;
                    case '-':
                        RotateCW(side);
                        break;
                    case '*':
                        RotateCW(side);
                        RotateCW(side);
                        break;
                }
            }
        }
        
        /// <summary>
        /// Solve the top edge of the cube
        /// </summary>
        /// <param name="progressBar"></param>
        private void SolveTopEdges(ToolStripProgressBar progressBar)
        {
            // while the edges not solved
            for (int i = 0; i < 4; i++)
            {
                progressBar.PerformStep();

                CubeSide side1 = FindEdge(_top.Color, _front.Color);
                CubeSide side2 = FindEdge(_front.Color, _top.Color);

                if (side1 == _right)
                {
                    if (side2 == _front)
                    {
                        m_S += "F-.";
                        DoMove("F-.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "R-.F-.";
                        DoMove("R-.F-.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "T+.T+.B+.T+.T+.";
                        DoMove("T+.T+.B+.T+.T+.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "R+.F-.R-.";
                        DoMove("R+.F-.R-.");
                    }
                }
                else if (side1 == _left)
                {
                    if (side2 == _top)
                    {
                        m_S += "L+.F+.";
                        DoMove("L+.F+.");
                    }
                    else if (side2 == _front)
                    {
                        m_S += "F+.";
                        DoMove("F+.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "L+.L+.F+.L+.L+.";
                        DoMove("L+.L+.F+.L+.L+.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "L-.F+.L+.";
                        DoMove("L-.F+.L+.");
                    }
                }
                else if (side1 == _front)
                {
                    if (side2 == _top)
                    {
                        m_S += "F-.T+.L-.T-.";
                        DoMove("F-.T+.L-.T-.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "T+.L-.T-.";
                        DoMove("T+.L-.T-.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "T-.R+.T+.";
                        DoMove("T-.R+.T+.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "F-.T-.R+.T+.";
                        DoMove("F-.T-.R+.T+.");
                    }
                }
                else if (side1 == _back)
                {
                    if (side2 == _right)
                    {
                        m_S += "T-.R-.T+.";
                        DoMove("T-.R-.T+.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "T+.L+.T-.";
                        DoMove("T+.L+.T-.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "B-.T-.R-.T+.";
                        DoMove("B-.T-.R-.T+.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "D-.R+.F-.R-.";
                        DoMove("D-.R+.F-.R-.");
                    }
                }
                else if (side1 == _down)
                {
                    if (side2 == _front)
                    {
                        m_S += "F+.F+.";
                        DoMove("F+.F+.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "D-.F+.F+.";
                        DoMove("D-.F+.F+.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "D+.F+.F+.";
                        DoMove("D+.F+.F+.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "D+.D+.F+.F+.";
                        DoMove("D+.D+.F+.F+.");
                    }
                }
                else if (side1 == _top)
                {
                    if (side2 == _left)
                    {
                        m_S += "L-.T+.L+.T-.";
                        DoMove("L-.T+.L+.T-.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "R+.T-.R-.T+.";
                        DoMove("R+.T-.R-.T+.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "B-.T+.T+.B+.T+.T+.";
                        DoMove("B-.T+.T+.B+.T+.T+.");
                    }
                }
                m_S += "C+.";
                DoMove("C+.");
            }
        }

        /// <summary>
        /// Solve the top corner of the cube
        /// </summary>
        /// <param name="progressBar"></param>
        private void SolveTopCorner(ToolStripProgressBar progressBar)
        {
            for (int i = 0; i < 4; i++)
            {
                progressBar.PerformStep();

                CubeSide side1 = FindCorner(_top.Color, _front.Color, _right.Color);
                CubeSide side2 = FindCorner(_front.Color, _top.Color, _right.Color);

                if (side1 == _front)
                {
                    if (side2 == _right)
                    {
                        m_S += "F+.D+.F-.D+.D+.R-.D+.R+.";
                        DoMove("F+.D+.F-.D+.D+.R-.D+.R+.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "D+.D+.F+.D-.F-.";
                        DoMove("D+.D+.F+.D-.F-.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "F-.D-.F+.F+.D+.D+.F-.";
                        DoMove("F-.D-.F+.F+.D+.D+.F-.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "D-.R-.D+.R+.";
                        DoMove("D-.R-.D+.R+.");
                    }
                }
                else if (side1 == _right)
                {
                    if (side2 == _front)
                    {
                        m_S += "D+.F+.D-.F-.";
                        DoMove("D+.F+.D-.F-.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "R+.D+.D+.R+.R+.D+.R+.";
                        DoMove("R+.D+.D+.R+.R+.D+.R+.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "R-.D-.R+.D+.D+.F+.D-.F-.";
                        DoMove("R-.D-.R+.D+.D+.F+.D-.F-.");
                    }
                    else if (side2 == _down)
                    {
                        m_S += "D+.D+.R-.D+.R+.";
                        DoMove("D+.D+.R-.D+.R+.");
                    }
                }
                else if (side1 == _left)
                {
                    if (side2 == _down)
                    {
                        m_S += "R-.D+.R+.";
                        DoMove("R-.D+.R+.");
                    }
                    else if (side2 == _front)
                    {
                        m_S += "L+.D+.L-.D-.R-.D+.R+.";
                        DoMove("L+.D+.L-.D-.R-.D+.R+.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "F+.D+.D+.F-.";
                        DoMove("F+.D+.D+.F-.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "L-.D-.L+.F+.D-.F-.";
                        DoMove("L-.D-.L+.F+.D-.F-.");
                    }
                }
                else if (side1 == _back)
                {
                    if (side2 == _down)
                    {
                        m_S += "R-.D+.D+.R+.";
                        DoMove("R-.D+.D+.R+.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "F+.D-.F-.";
                        DoMove("F+.D-.F-.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "B+.D+.B-.R-.D+.R+.";
                        DoMove("B+.D+.B-.R-.D+.R+.");
                    }
                    else if (side2 == _top)
                    {
                        m_S += "B-.F+.D-.F-.B+.";
                        DoMove("B-.F+.D-.F-.B+.");
                    }
                }
                else if (side1 == _top)
                {
                    if (side2 == _left)
                    {
                        m_S += "L+.D-.L-.R-.D+.R+.";
                        DoMove("L+.D-.L-.R-.D+.R+.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "B-.D+.B+.F+.D-.F-.";
                        DoMove("B-.D+.B+.F+.D-.F-.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "L-.R-.D+.D+.R+.L+.";
                        DoMove("L-.R-.D+.D+.R+.L+.");
                    }
                }
                else if (side1 == _down)
                {
                    if (side2 == _front)
                    {
                        m_S += "D+.R-.D+.R+.F+.D+.D+.F-.";
                        DoMove("D+.R-.D+.R+.F+.D+.D+.F-.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "D+.D+.R-.D+.R+.F+.D+.D+.F-.";
                        DoMove("D+.D+.R-.D+.R+.F+.D+.D+.F-.");
                    }
                    else if (side2 == _back)
                    {
                        m_S += "D-.R-.D+.R+.F+.D+.D+.F-.";
                        DoMove("D-.R-.D+.R+.F+.D+.D+.F-.");
                    }
                    else if (side2 == _right)
                    {
                        m_S += "R-.D+.R+.F+.D+.D+.F-.";
                        DoMove("R-.D+.R+.F+.D+.D+.F-.");
                    }
                }
                m_S += "C+.";
                DoMove("C+.");
            }
        }

        /// <summary>
        /// Solve the middle edges
        /// </summary>
        /// <param name="progressBar"></param>
        private void SolveMiddleEdges(ToolStripProgressBar progressBar)
        {
            for (int i = 0; i < 4; i++)
            {
                progressBar.PerformStep();

                CubeSide side1 = FindEdge(_front.Color, _right.Color);
                CubeSide side2 = FindEdge(_right.Color, _front.Color);

                //check to see if the side we are looking for is 
                //in one of the middle sides. If it is move it out

                if (side1 == _left && side2 == _back ||
                    side1 == _back && side2 == _left)
                {
                    m_S += "B+.D-.B-.D-.L-.D+.L+.";
                    DoMove("B+.D-.B-.D-.L-.D+.L+.");
                }
                else if (side1 == _left && side2 == _front ||
                         side1 == _front && side2 == _left)
                {
                    m_S += "L+.D-.L-.D-.F-.D+.F+.";
                    DoMove("L+.D-.L-.D-.F-.D+.F+.");
                }
                else if (side1 == _right && side2 == _back ||
                         side1 == _back && side2 == _right)
                {
                    m_S += "B-.D+.B+.D+.R+.D-.R-.";
                    DoMove("B-.D+.B+.D+.R+.D-.R-.");
                }
                else if (side1 == _right && side2 == _front)
                {
                    m_S += "R-.D+.R+.D+.F+.D-.F-.";
                    DoMove("R-.D+.R+.D+.F+.D-.F-.");
                }

                //now that it is moved out find it again and put it in place
                side1 = FindEdge(_front.Color, _right.Color);
                side2 = FindEdge(_right.Color, _front.Color);

                if (side1 == _down)
                {
                    if (side2 == _right)
                    {
                        m_S += "D+.";
                        DoMove("D+.");
                    }
                    else if (side2 == _front)
                    {
                        m_S += "D+.D+.";
                        DoMove("D+.D+.");
                    }
                    else if (side2 == _left)
                    {
                        m_S += "D-.";
                        DoMove("D-.");
                    }
                    m_S += "F+.D-.F-.D-.R-.D+.R+.";
                    DoMove("F+.D-.F-.D-.R-.D+.R+.");
                }
                else if (side2 == _down)
                {
                    if (side1 == _right)
                    {
                        m_S += "D+.D+.";
                        DoMove("D+.D+.");
                    }
                    else if (side1 == _front)
                    {
                        m_S += "D-.";
                        DoMove("D-.");
                    }
                    else if (side1 == _back)
                    {
                        m_S += "D+.";
                        DoMove("D+.");
                    }
                    m_S += "R-.D+.R+.D+.F+.D-.F-.";
                    DoMove("R-.D+.R+.D+.F+.D-.F-.");
                }
                m_S += "C+.";
                DoMove("C+.");
            }
        }

        /// <summary>
        /// get the orientation of the bottom edges
        /// this funtion will tell how many are oriented correctly
        /// and which ones they are
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int GetDownEdgeOrient(ref int flag)
        {
            int count = 0;
            flag = 0;

            if (_down.Block[0, 1] == _down.Block[1, 1])
            {
                count++;
                flag |= SFRONT;
            }

            if (_down.Block[1, 2] == _down.Block[1, 1])
            {
                count++;
                flag |= SRIGHT;
            }

            if (_down.Block[2, 1] == _down.Block[1, 1])
            {
                count++;
                flag |= SBACK;
            }

            if (_down.Block[1, 0] == _down.Block[1, 1])
            {
                count++;
                flag |= SLEFT;
            }
            return count;
        }

        /// <summary>
        /// return how many bottom edge pieces are positioned correctly
        /// and will tell which ones are positioned correctly
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int GetDownEdgePos(ref int flag)
        {
            int count = 0;
            flag = 0;
            CubeSide side;

            if ((side = FindEdge(_front.Color, _down.Color)) != _front)
            {
                count++;
                if (side == _left)
                {
                    flag |= MLEFTFRONT;
                }

                if (side == _right)
                {
                    flag |= MRIGHTFRONT;
                }

                if (side == _back)
                {
                    flag |= MBACKFRONT;
                }
            }

            if ((side = FindEdge(_back.Color, _down.Color)) != _back)
            {
                count++;
                if (side == _front)
                {
                    flag |= MFRONTBACK;
                }

                if (side == _right)
                {
                    flag |= MRIGHTBACK;
                }

                if (side == _left)
                {
                    flag |= MLEFTBACK;
                }
            }

            if ((side = FindEdge(_right.Color, _down.Color)) != _right)
            {
                count++;
                if (side == _front)
                {
                    flag |= MFRONTRIGHT;
                }

                if (side == _left)
                {
                    flag |= MLEFTRIGHT;
                }

                if (side == _back)
                {
                    flag |= MBACKRIGHT;
                }
            }

            if ((side = FindEdge(_left.Color, _down.Color)) != _left)
            {
                count++;
                if (side == _front)
                {
                    flag |= MFRONTLEFT;
                }

                if (side == _right)
                {
                    flag |= MRIGHTLEFT;
                }

                if (side == _back)
                {
                    flag |= MBACKLEFT;
                }
            }

            return count;
        }

        /// <summary>
        /// solve the bottom edge pieces
        /// </summary>
        /// <param name="progressBar"></param>
        /// <returns>True if solve down edges, false if otherwise</returns>
        private bool SolveDownEdges(ToolStripProgressBar progressBar)
        {
            int count;
            int flag = 0;
            int iter = 0;

            // first solve the edge pieces
            while ((count = GetDownEdgeOrient(ref flag)) < 4)
            {
                iter++;
                progressBar.PerformStep();

                if (count < 2)
                {
                    m_S += "F+.D+.L+.D-.L-.F-.";
                    DoMove("F+.D+.L+.D-.L-.F-.");
                }
                else if (flag == RIGHTLEFT)
                {
                    m_S += "F+.L+.D+.L-.D-.F-.";
                    DoMove("F+.L+.D+.L-.D-.F-.");
                }
                else if (flag == FRONTBACK)
                {
                    m_S += "C+.F+.L+.D+.L-.D-.F-.";
                    DoMove("C+.F+.L+.D+.L-.D-.F-.");
                }
                else if (flag == RIGHTBACK)
                {
                    m_S += "F+.D+.L+.D-.L-.F-.";
                    DoMove("F+.D+.L+.D-.L-.F-.");
                }
                else if (flag == FRONTRIGHT)
                {
                    m_S += "C+.F+.D+.L+.D-.L-.F-.";
                    DoMove("C+.F+.D+.L+.D-.L-.F-.");
                }
                else if (flag == FRONTLEFT)
                {
                    m_S += "C+.C+.F+.D+.L+.D-.L-.F-.";
                    DoMove("C+.C+.F+.D+.L+.D-.L-.F-.");
                }
                else if (flag == BACKLEFT)
                {
                    m_S += "C+.C+.C+.F+.D+.L+.D-.L-.F-.";
                    DoMove("C+.C+.C+.F+.D+.L+.D-.L-.F-.");
                }

                if (iter > 10)
                {
                    return false;
                }
            }

            iter = 0;
            //Now Position the pieces correctly
            while ((count = GetDownEdgePos(ref flag)) > 0)
            {
                iter++;
                progressBar.PerformStep();

                if (count == 4)
                {
                    m_S += "D+.";
                    DoMove("D+.");
                }
                else if (count == 3)
                {
                    while (true)
                    {
                        if ((flag & MRIGHTLEFT) != 0 &&
                            (flag & MLEFTFRONT) != 0 &&
                            (flag & MFRONTRIGHT) != 0)
                        {
                            m_S += "L-.D-.L+.D-.L-.D+.D+.L+.";
                            DoMove("L-.D-.L+.D-.L-.D+.D+.L+.");
                            break;
                        }

                        if ((flag & MLEFTRIGHT) != 0 &&
                            (flag & MRIGHTFRONT) != 0 &&
                            (flag & MFRONTLEFT) != 0)
                        {
                            m_S += "L-.D+.D+.L+.D+.L-.D+.L+.";
                            DoMove("L-.D+.D+.L+.D+.L-.D+.L+.");
                            break;
                        }

                        m_S += "C+.";
                        DoMove("C+.");
                        GetDownEdgePos(ref flag);
                    }
                }
                else if (count == 2)
                {
                    while (true)
                    {
                        if ((flag & MRIGHTFRONT) != 0)
                        {
                            m_S += "D+.L-.D+.D+.L+.D+.L-.D+.L+.";
                            DoMove("D+.L-.D+.D+.L+.D+.L-.D+.L+.");
                            break;
                        }

                        if ((flag & MFRONTBACK) != 0)
                        {
                            m_S += "L-.D+.D+.L+.D+.L-.D+.L+.D-.L-.D+.D+.L+.D+.L-.D+.L+.";
                            DoMove("L-.D+.D+.L+.D+.L-.D+.L+.D-.L-.D+.D+.L+.D+.L-.D+.L+.");
                            break;
                        }

                        m_S += "C+.";
                        DoMove("C+.");
                        GetDownEdgePos(ref flag);
                    }
                }

                if (iter > 10)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetCornersSide(CubeSide side1, CubeSide side2)
        {
            int flag = 0;
            // back vs right <=> front vs left
            // front vs right <=> back vs left
            // right vs front <=> left vs back
            // left vs front <=> right vs back
            if (side1 == _back)
            {
                if (side2 == _right)
                {
                    flag |= SRIGHT;
                }
                else
                {
                    flag |= SBACK;
                }
            }

            if (side1 == _front)
            {
                if (side2 == _right)
                {
                    flag |= SFRONT;
                }
                else
                {
                    flag |= SLEFT;
                }
            }

            if (side1 == _right)
            {
                if (side2 == _front)
                {
                    flag |= SFRONT;
                }
                else
                {
                    flag |= SRIGHT;
                }
            }

            if (side1 == _left)
            {
                if (side2 == _front)
                {
                    flag |= SLEFT;
                }
                else
                {
                    flag |= SBACK;
                }
            }
            return flag;
        }

        /// <summary>
        /// returns how many bottom corner pieces are oriented correctly
        /// and which ones they are
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int GetDownCornerOrient(ref int flag)
        {
            int count = 0;
            flag = 0;

            CubeSide side1;
            CubeSide side2;

            if ((side1 = FindCorner(_down.Color, _front.Color, _right.Color)) != _down)
            {
                count++;
                if ((side2 = FindCorner(_front.Color, _right.Color, _down.Color)) == _down)
                {
                    side2 = FindCorner(_right.Color, _front.Color, _down.Color);
                }
                flag |= GetCornersSide(side1, side2);
            }

            if ((side1 = FindCorner(_down.Color, _front.Color, _left.Color)) != _down)
            {
                count++;
                if ((side2 = FindCorner(_front.Color, _left.Color, _down.Color)) == _down)
                {
                    side2 = FindCorner(_left.Color, _front.Color, _down.Color);
                }
                flag |= GetCornersSide(side1, side2);
            }

            if ((side1 = FindCorner(_down.Color, _back.Color, _right.Color)) != _down)
            {
                count++;
                if ((side2 = FindCorner(_back.Color, _right.Color, _down.Color)) == _down)
                {
                    side2 = FindCorner(_right.Color, _back.Color, _down.Color);
                }
                flag |= GetCornersSide(side1, side2);
            }

            if ((side1 = FindCorner(_down.Color, _back.Color, _left.Color)) != _down)
            {
                count++;
                if ((side2 = FindCorner(_back.Color, _left.Color, _down.Color)) == _down)
                {
                    side2 = FindCorner(_left.Color, _back.Color, _down.Color);
                }
                flag |= GetCornersSide(side1, side2);
            }

            return count;
        }

        /// <summary>
        /// returns how many bottom corner pieces are positioned correctly
        /// and which ones they are
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int GetDownCornerPos(ref int flag)
        {
            int count = 0;
            flag = 0;

            CubeSide side1;
            CubeSide side2;

            if ((side1 = FindCorner(_front.Color, _right.Color, _down.Color)) != _front &&
                (side2 = FindCorner(_right.Color, _front.Color, _down.Color)) != _right)
            {
                count++;
                if (side1 == _back)
                {
                    if (side2 == _left)
                    {
                        flag |= MBACKFRONT;
                    }
                }
                else if (side1 == _left)
                {
                    if (side2 == _back)
                    {
                        flag |= MBACKFRONT;
                    }
                    if (side2 == _front)
                    {
                        flag |= MLEFTFRONT;
                    }
                }
                else if (side1 == _right)
                {
                    if (side2 == _back)
                    {
                        flag |= MRIGHTFRONT;
                    }
                }
                else if (side2 == _left)
                {
                    flag |= MLEFTFRONT;
                }
            }

            if ((side1 = FindCorner(_front.Color, _left.Color, _down.Color)) != _front &&
                (side2 = FindCorner(_left.Color, _front.Color, _down.Color)) != _left)
            {
                count++;
                if (side1 == _back)
                {
                    if (side2 == _right)
                    {
                        flag |= MRIGHTLEFT;
                    }
                }
                else if (side1 == _right)
                {
                    if (side2 == _front)
                    {
                        flag |= MFRONTLEFT;
                    }
                    if (side2 == _back)
                    {
                        flag |= MRIGHTLEFT;
                    }
                }
                else if (side1 == _left)
                {
                    if (side2 == _back)
                    {
                        flag |= MBACKLEFT;
                    }
                }
                else if (side2 == _right)
                {
                    flag |= MFRONTLEFT;
                }
            }

            if ((side1 = FindCorner(_back.Color, _right.Color, _down.Color)) != _back &&
                 (side2 = FindCorner(_right.Color, _back.Color, _down.Color)) != _right)
            {
                count++;
                if (side1 == _front)
                {
                    if (side2 == _left)
                    {
                        flag |= MLEFTRIGHT;
                    }
                }
                else if (side1 == _left)
                {
                    if (side2 == _front)
                    {
                        flag |= MLEFTRIGHT;
                    }
                    if (side2 == _back)
                    {
                        flag |= MBACKRIGHT;
                    }
                }
                else if (side1 == _right)
                {
                    if (side2 == _front)
                    {
                        flag |= MFRONTRIGHT;
                    }
                }
                else if (side2 == _left)
                {
                    flag |= MBACKRIGHT;
                }
            }

            if ((side1 = FindCorner(_back.Color, _left.Color, _down.Color)) != _down &&
                 (side2 = FindCorner(_left.Color, _back.Color, _down.Color)) != _left)
            {
                count++;
                if (side1 == _front)
                {
                    if (side2 == _right)
                    {
                        flag |= MFRONTBACK;
                    }
                }
                else if (side1 == _right)
                {
                    if (side2 == _front)
                    {
                        flag |= MFRONTBACK;
                    }
                    if (side2 == _back)
                    {
                        flag |= MRIGHTBACK;
                    }
                }
                else if (side1 == _back)
                {
                    if (side2 == _right)
                    {
                        flag |= MRIGHTBACK;
                    }
                }
                else if (side2 == _front)
                {
                    flag |= MLEFTBACK;
                }
            }

            return count;
        }

        /// <summary>
        /// solve the bottom corners
        /// </summary>
        /// <param name="progressBar"></param>
        /// <returns></returns>
        private bool SolveDownCorners(ToolStripProgressBar progressBar)
        {
            int count;
            int flag = 0;
            int iter = 0;

            //first orient the corner pieces
            while ((count = GetDownCornerOrient(ref flag)) > 0)
            {
                iter++;
                progressBar.PerformStep();

                if (count > 3)
                {
                    m_S += "B+.L+.B-.L-.D+.D+.R-.B+.B+.R+.B-.R-.B+.B+.R+.B-.D+.D+.";
                    DoMove("B+.L+.B-.L-.D+.D+.R-.B+.B+.R+.B-.R-.B+.B+.R+.B-.D+.D+.");
                }
                else if (count == 3)
                {
                    if (flag == (SBACK | SLEFT | SFRONT))
                    {
                        m_S += "B+.L+.B-.L-.D+.D+.R-.B+.B+.R+.B-.R-.B+.B+.R+.B-.D+.D+.";
                        DoMove("B+.L+.B-.L-.D+.D+.R-.B+.B+.R+.B-.R-.B+.B+.R+.B-.D+.D+.");
                    }
                    else
                    {
                        m_S += "C+.";
                        DoMove("C+.");
                    }
                }
                else if (count == 2)
                {
                    if (flag == (SRIGHT | SFRONT))
                    {
                        m_S += "D+.L+.D-.L-.D+.L+.D-.R-.D+.L-.D-.L+.D+.L-.D-.R+.";
                        DoMove("D+.L+.D-.L-.D+.L+.D-.R-.D+.L-.D-.L+.D+.L-.D-.R+.");
                    }
                    else if (flag == (SRIGHT | SLEFT))
                    {
                        m_S += "B+.L-.T+.T+.L+.B-.D+.D+.B+.L-.T+.T+.L+.B-.D+.D+.";
                        DoMove("B+.L-.T+.T+.L+.B-.D+.D+.B+.L-.T+.T+.L+.B-.D+.D+.");
                    }
                    else
                    {
                        m_S += "C+.";
                        DoMove("C+.");
                    }
                }
                if (iter > 10)
                {
                    return false;
                }
            }

            iter = 0;
            //now position the corner pieces
            while ((count = GetDownCornerPos(ref flag)) > 0)
            {
                iter++;
                progressBar.PerformStep();

                if (count == 4)
                {
                    if ((flag & MLEFTRIGHT) != 0 && (flag & MFRONTBACK) != 0)
                    {
                        m_S += "L+.L+.R+.R+.T+.L+.L+.R+.R+.D+.D+.L+.L+.R+.R+.T+.L+.L+.R+.R+.D+.D+.";
                        DoMove("L+.L+.R+.R+.T+.L+.L+.R+.R+.D+.D+.L+.L+.R+.R+.T+.L+.L+.R+.R+.D+.D+.");
                    }
                    else if ((flag & MLEFTFRONT) != 0 && (flag & MBACKRIGHT) != 0)
                    {
                        m_S += "F+.L-.F-.R+.F+.L+.F-.R+.R+.B-.L+.B+.R+.B-.L-.B+.";
                        DoMove("F+.L-.F-.R+.F+.L+.F-.R+.R+.B-.L+.B+.R+.B-.L-.B+.");
                    }
                    else
                    {
                        m_S += "C+.";
                        DoMove("C+.");
                    }
                }
                else if (count == 3)
                {
                    if ((flag & MLEFTRIGHT) != 0 && (flag & MRIGHTBACK) != 0 && (flag & MBACKLEFT) != 0)
                    {
                        m_S += "L-.F+.L-.B+.B+.L+.F-.L-.B+.B+.L+.L+.";
                        DoMove("L-.F+.L-.B+.B+.L+.F-.L-.B+.B+.L+.L+.");
                    }
                    else if ((flag & MLEFTBACK) != 0 && (flag & MBACKRIGHT) != 0 && (flag & MRIGHTLEFT) != 0)
                    {
                        m_S += "L+.L+.B+.B+.L+.F+.L-.B+.B+.L+.F-.L+.";
                        DoMove("L+.L+.B+.B+.L+.F+.L-.B+.B+.L+.F-.L+.");
                    }
                    else
                    {
                        m_S += "C+.";
                        DoMove("C+.");
                    }
                }

                if (iter > 10)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Clean moves, increment performance
        /// </summary>
        private void CleanSolution()
        {
            string s = string.Empty;

            // Clean cube moves
            char F = 'F';
            char L = 'L';
            char R = 'R';
            char B = 'B';
            char T = 'T';
            char D = 'D';
            int start = 0;

            string move = m_S.Substring(start, 3);

            while (true)
            {
                switch (move[0])
                {
                    // remove rotate cube string
                    case 'C':
                        char tmp;
                        switch (move[1])
                        {
                            case '+':
                                tmp = F;
                                F = L;
                                L = B;
                                B = R;
                                R = tmp;
                                break;
                            case '-':
                                tmp = F;
                                F = R;
                                R = B;
                                B = L;
                                L = tmp;
                                break;
                            case '^':
                                tmp = F;
                                F = D;
                                D = B;
                                B = T;
                                T = tmp;
                                break;
                            case 'v':
                                tmp = F;
                                F = T;
                                T = B;
                                B = D;
                                D = tmp;
                                break;
                        }
                        break;
                    case 'F':
                        s += F.ToString() + move[1] + ".";
                        break;
                    case 'L':
                        s += L.ToString() + move[1] + ".";
                        break;
                    case 'R':
                        s += R.ToString() + move[1] + ".";
                        break;
                    case 'B':
                        s += B.ToString() + move[1] + ".";
                        break;
                    case 'T':
                        s += T.ToString() + move[1] + ".";
                        break;
                    case 'D':
                        s += D.ToString() + move[1] + ".";
                        break;
                    default:
                        s += move;
                        break;
                }
                start += 3;
                if (start >= m_S.Length)
                {
                    break;
                }
                move = m_S.Substring(start, 3);
            }
            m_S = s;

            while (CleanOppositeMoves(ref m_S))
            {
            }

            while (Clean270Moves(ref m_S))
            {
            }

            while (CleanCircularMoves(ref m_S))
            {
            }

            while (CleanSameMoves(ref m_S))
            {
            }
        }

        /// <summary>
        ///  clean contradicting (đối nghịch) moves (example: L+.L-. -> none)
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private bool CleanOppositeMoves(ref string moves)
        {
            if (string.IsNullOrEmpty(moves))
            {
                return false;
            }
            bool found = false;
            int start = 0;
            string s = string.Empty;
            string move1 = moves.Substring(start, 3);

            while (true)
            {
                start += 3;
                if (start >= moves.Length)
                {
                    s += move1;
                    break;
                }
                string move2 = moves.Substring(start, 3);
                if (!string.IsNullOrEmpty(move2))
                {
                    // if both of move are on the same side
                    if (move1[0].Equals(move2[0]))
                    {
                        // if they are not the same direction
                        if (!move1[1].Equals(move2[1]))
                        {
                            found = true;
                            start += 3;
                            move1 = moves.Substring(start, 3);
                            continue;
                        }
                    }
                }
                s += move1;
                move1 = move2;
            }
            moves = s;
            return found;
        }

        /// <summary>
        /// change turn long 270 to counter clockwise move (example: L+.L+.L+. => L-.)
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private bool Clean270Moves(ref string moves)
        {
            if (string.IsNullOrEmpty(moves))
            {
                return false;
            }
            bool found = false;
            string s = string.Empty;
            int start = 0;
            string move1 = moves.Substring(start, 3);
            start += 3;
            string move2 = moves.Substring(start, 3);

            while (true)
            {
                start += 3;
                if (start >= moves.Length)
                {
                    s += move1;
                    s += move2;
                    break;
                }
                string move3 = moves.Substring(start, 3);

                if (!string.IsNullOrEmpty(move2) && !string.IsNullOrEmpty(move3))
                {
                    if (move1.Equals(move2) && move2.Equals(move3))
                    {
                        found = true;
                        s += move1[0];
                        if (move1[1].ToString().Equals("+"))
                        {
                            s += "-.";
                        }
                        else
                        {
                            s += "+.";
                        }
                        start += 3;
                        if (start >= moves.Length)
                        {
                            break;
                        }
                        move1 = moves.Substring(start, 3);
                        start += 3;
                        if (start >= moves.Length)
                        {
                            s += move1;
                            break;
                        }
                        move2 = moves.Substring(start, 3);
                        continue;
                    }
                }

                s += move1;
                move1 = move2;
                move2 = move3;
            }
            moves = s;
            return found;
        }

        /// <summary>
        /// clean circular moves (L+.L+.L+.L+. -> none)
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private bool CleanCircularMoves(ref string moves)
        {
            if (string.IsNullOrEmpty(moves))
            {
                return false;
            }

            bool found = false;
            string s = string.Empty;
            int start = 0;
            string move1 = moves.Substring(start, 3);
            start += 3;
            string move2 = moves.Substring(start, 3);
            start += 3;
            string move3 = moves.Substring(start, 3);
            string move4;

            while (true)
            {
                start += 3;
                if (start >= moves.Length)
                {
                    s += move1;
                    s += move2;
                    s += move3;
                    break;
                }
                move4 = moves.Substring(start, 3);

                if (!string.IsNullOrEmpty(move2) &&
                    !string.IsNullOrEmpty(move3) &&
                    !string.IsNullOrEmpty(move4))
                {
                    // ignore 4 circular moves and turn the fifth move
                    if (move1.Equals(move2) && move2.Equals(move3) && move3.Equals(move4))
                    {
                        found = true;
                        start += 3;
                        if (start >= moves.Length)
                        {
                            break;
                        }
                        move1 = moves.Substring(start, 3);
                        start += 3;
                        if (start >= moves.Length)
                        {
                            s += move1;
                            break;
                        }
                        move2 = moves.Substring(start, 3);
                        start += 3;
                        if (start >= moves.Length)
                        {
                            s += move1;
                            s += move2;
                            break;
                        }
                        move3 = moves.Substring(start, 3);
                        continue;
                    }
                }

                s += move1;

                move1 = move2;
                move2 = move3;
                move3 = move4;
            }
            moves = s;
            return found;
        }

        /// <summary>
        /// clear two same move into 180 (Example: L+.L+. = L-.L-. = L*.)
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private bool CleanSameMoves(ref string moves)
        {
            if (string.IsNullOrEmpty(moves))
            {
                return false;
            }

            bool found = false;
            string s = string.Empty;
            int start = 0;
            string move1 = moves.Substring(start, 3);

            while (true)
            {
                start += 3;
                if (start >= moves.Length)
                {
                    s += move1;
                    break;
                }
                string move2 = moves.Substring(start, 3);

                if (!string.IsNullOrEmpty(move2))
                {
                    // if both moves are on the same side
                    if (move1.Equals(move2))
                    {
                        found = true;
                        s += move1[0] + "*.";
                        start += 3;
                        if (start >= moves.Length)
                        {
                            break;
                        }
                        move1 = moves.Substring(start, 3);
                        continue;
                    }
                }

                s += move1;

                move1 = move2;
            }
            moves = s;
            return found;
        }

        /// <summary>
        /// Solve the cube
        /// </summary>
        /// <param name="progressBar"></param>
        /// <returns></returns>
        private bool SolveCube(ToolStripProgressBar progressBar)
        {
            SolveTopEdges(progressBar);
            SolveTopCorner(progressBar);
            m_S += "~1.";
            SolveMiddleEdges(progressBar);
            m_S += "~2.";
            bool solved = SolveDownEdges(progressBar);
            
            if (solved)
            {
                solved = SolveDownCorners(progressBar);
                m_S += "~3.";
                CleanSolution();
            }
            return solved;
        }

        /// <summary>
        /// Find the shortest solution from 24 solutions
        /// </summary>
        /// <param name="progressBar"></param>
        /// <returns></returns>
        public int Solve(ToolStripProgressBar progressBar)
        {
            // make sure that all the cubelets are valid
            for (int i = 1; i <= 6; i++)
            {
                if (FindCenter(char.Parse(i.ToString())).Side == '0')
                {
                    return 1;
                }
            }

            for (int i = 0; i < NUM_EDGES; i++)
            {
                if (FindEdge(GetSide(Edges[i * 2].Side).Color,
                             GetSide(Edges[(i * 2) + 1].Side).Color) == null)
                {
                    return 1;
                }
            }

            for (int i = 0; i < NUM_CORNERS; i++)
            {
                if (FindCorner(GetSide(Corners[i * 3].Side).Color,
                               GetSide(Corners[(i * 3) + 1].Side).Color,
                               GetSide(Corners[(i * 3) + 2].Side).Color) == null)
                {
                    return 1;
                }
            }

            // clear solution
            m_Solution.Clear();

            // copy the cube so we have the original
            SaveCube();

            bool solved = false;

            // try to find a solution from every starting position
            // and later find the shortest solution
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (i)
                    {
                        case 0:
                            m_S = string.Empty;
                            break;
                        case 1:
                            m_S = "C^.";
                            break;
                        case 2:
                            m_S = "C^.C^.";
                            break;
                        case 3:
                            m_S = "C^.C^.C^.";
                            break;
                        case 4:
                            m_S = "C+.C^.";
                            break;
                        case 5:
                            m_S = "C-.C^.";
                            break;
                    }
                    for (int k = 0; k < j; k++)
                    {
                        m_S += "C+.";
                    }

                    DoMove(m_S);
                    solved = SolveCube(progressBar);

                    if (solved)
                    {
                        m_S = m_S.Insert(0, string.Format("T{0}.D{1}.L{2}.R{3}.F{4}.B{5}.", _top.Color, _down.Color, _left.Color, _right.Color, _front.Color, _back.Color));
                        m_Solution.Add(m_S);
                    }
                    //restore the cube to original formation
                    RestoreCube();
                }
            }

            // find the shortest solution
            int numMoves = 500;
            int index = -1;

            for (int i = 0; i < m_Solution.Count; i++)
            {
                if (m_Solution[i].Length < numMoves)
                {
                    numMoves = m_Solution[i].Length;
                    index = i;
                }
            }
            m_S = string.Empty;
            if (index != -1)
            {
                m_S = m_Solution[index];
            }

            progressBar.Value = 0;
            return solved ? 0 : 2;
        }
    }
}