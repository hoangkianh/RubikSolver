using System;
using CSGL12;

namespace RubikSolver
{
    class Cube3D
    {
        private readonly GL gl;

        private Cubelet[,,] _cubelets = new Cubelet[3, 3, 3];
        private Cubelet[,,] _savedCubes = new Cubelet[3, 3, 3];
        private float[] _rotateX = new float[3];
        private float[] _rotateY = new float[3];
        private float[] _rotateZ = new float[3];
        private float _cubeX;
        private float _cubeY;
        private float _cubeZ;

        public Cube3D(GL gl)
        {
            this.gl = gl;
            _cubeX = _cubeY = _cubeZ = 0;
            for (int i = 0; i < 3; i++)
            {
                _rotateX[i] = _rotateY[i] = _rotateZ[i] = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[i, j, k] = new Cubelet(gl);
                        _savedCubes[i, j, k] = new Cubelet(gl);
                    }
                }
            }

            Reset();
        }

        public void Render()
        {
            gl.glRotatef(_cubeX, 1, 0, 0);
            gl.glRotatef(_cubeY, 0, 1, 0);
            gl.glRotatef(_cubeZ, 0, 0, 1);

            // render each cubelet
            for (int i = 0; i < 3; i++)
            {
                gl.glRotatef(_rotateX[i], 1, 0, 0);
                for (int j = 0; j < 3; j++)
                {
                    gl.glRotatef(_rotateY[j], 0, 1, 0);
                    for (int k = 0; k < 3; k++)
                    {
                        gl.glRotatef(_rotateZ[k], 0, 0, 1);

                        gl.glTranslatef(i - 1.0f, j - 1.0f, k - 1.0f);
                        _cubelets[i, j, k].Render();
                        gl.glTranslatef(1.0f - i, 1.0f - j, 1.0f - k);

                        gl.glRotatef(-_rotateZ[k], 0, 0, 1);
                    }
                    gl.glRotatef(-_rotateY[j], 0, 1, 0);
                }
                gl.glRotatef(-_rotateX[i], 1, 0, 0);
            }

            gl.glRotatef(-_cubeZ, 0, 0, 1);
            gl.glRotatef(-_cubeY, 0, 1, 0);
            gl.glRotatef(-_cubeX, 1, 0, 0);
        }

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[i, j, k].Reset();
                    }
                }
            }

            char side = ' ';
            for (int face = 0; face < 7; face++)
            {
                switch (face)
                {
                    case 1:
                        side = 'T';
                        break;
                    case 2:
                        side = 'F';
                        break;
                    case 3:
                        side = 'R';
                        break;
                    case 4:
                        side = 'L';
                        break;
                    case 5:
                        side = 'B';
                        break;
                    case 6:
                        side = 'D';
                        break;
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        SetColor(side, i, j, face);
                    }
                }
            }
        }

        public void Save()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _savedCubes[i, j, k].Copy(_cubelets[i, j, k]);
                    }
                }
            }
        }

        public void Restore()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[i, j, k].Copy(_savedCubes[i, j, k]);
                    }
                }
            }
        }
        
        /// <summary>
        /// Rotate a block of cubelets in X axis
        /// </summary>
        /// <param name="block">block of cubelets want to rotate</param>
        /// <param name="degress">Degress to rotate</param>
        public void RotateX(int block, int degress)
        {
            // Rotate in X axis
            _rotateX[block] += degress;

            // if rotate counter clockwise 90 degress -> stop and swap color in cubelets
            if (degress < 0 && _rotateX[block] == -90)
            {
                _rotateX[block] = 0;

                Cubelet tmpCubelet = _cubelets[block, 0, 0];
                _cubelets[block, 0, 0] = _cubelets[block, 2, 0];
                _cubelets[block, 2, 0] = _cubelets[block, 2, 2];
                _cubelets[block, 2, 2] = _cubelets[block, 0, 2];
                _cubelets[block, 0, 2] = tmpCubelet;

                tmpCubelet = _cubelets[block, 1, 0];
                _cubelets[block, 1, 0] = _cubelets[block, 2, 1];
                _cubelets[block, 2, 1] = _cubelets[block, 1, 2];
                _cubelets[block, 1, 2] = _cubelets[block, 0, 1];
                _cubelets[block, 0, 1] = tmpCubelet;

                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[block, j, k].RotateCCW(1);
                    }
                }
            }
            else if (degress > 0 && _rotateX[block] == 90)
            {
                // if rotate clockwise 90 degress -> stop and swap color in cubelets
                _rotateX[block] = 0;

                Cubelet tmpCube = _cubelets[block, 0, 2];
                _cubelets[block, 0, 2] = _cubelets[block, 2, 2];
                _cubelets[block, 2, 2] = _cubelets[block, 2, 0];
                _cubelets[block, 2, 0] = _cubelets[block, 0, 0];
                _cubelets[block, 0, 0] = tmpCube;

                tmpCube = _cubelets[block, 0, 1];
                _cubelets[block, 0, 1] = _cubelets[block, 1, 2];
                _cubelets[block, 1, 2] = _cubelets[block, 2, 1];
                _cubelets[block, 2, 1] = _cubelets[block, 1, 0];
                _cubelets[block, 1, 0] = tmpCube;

                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[block, j, k].RotateCW(1);
                    }
                }
            }

            if ( (_rotateX[0] == _rotateX[1] ||
                  _rotateX[0] == _rotateX[1] - 90 ||
                  _rotateX[0] == _rotateX[1] + 90) &&
                 (_rotateX[1] == _rotateX[2] ||
                  _rotateX[1] == _rotateX[2] - 90 ||
                  _rotateX[1] == _rotateX[2] + 90)
                )
            {
                _cubeX += _rotateX[0];
                _rotateX[0] = _rotateX[1] = _rotateX[2] = 0;
            }
        }

        /// <summary>
        /// Rotate a block of cubelets in Y axis
        /// </summary>
        /// <param name="block">block of cubelets want to rotate</param>
        /// <param name="degress">Degress to rotate</param>
        public void RotateY(int block, int degress)
        {
            // Rotate in Y axis
            _rotateY[block] += degress;
            gl.glRotatef(_rotateY[block], 0, 1, 0);
            // if rotate counter clockwise 90 degress -> stop and swap color in cubelets
            if (degress < 0 && _rotateY[block] == -90)
            {
                _rotateY[block] = 0;

                Cubelet tmpCubelet = _cubelets[0, block, 2];
                _cubelets[0, block, 2] = _cubelets[2, block, 2];
                _cubelets[2, block, 2] = _cubelets[2, block, 0];
                _cubelets[2, block, 0] = _cubelets[0, block, 0];
                _cubelets[0, block, 0] = tmpCubelet;

                tmpCubelet = _cubelets[0, block, 1];
                _cubelets[0, block, 1] = _cubelets[1, block, 2];
                _cubelets[1, block, 2] = _cubelets[2, block, 1];
                _cubelets[2, block, 1] = _cubelets[1, block, 0];
                _cubelets[1, block, 0] = tmpCubelet;

                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[i, block, k].RotateCCW(2);
                    }
                }
            }
            else if (degress > 0 && _rotateY[block] == 90)
            {
                // if rotate clockwise 90 degress -> stop and swap color in cubelets
                _rotateY[block] = 0;

                Cubelet tmpCubelet = _cubelets[0, block, 0];
                _cubelets[0, block, 0] = _cubelets[2, block, 0];
                _cubelets[2, block, 0] = _cubelets[2, block, 2];
                _cubelets[2, block, 2] = _cubelets[0, block, 2];
                _cubelets[0, block, 2] = tmpCubelet;

                tmpCubelet = _cubelets[1, block, 0];
                _cubelets[1, block, 0] = _cubelets[2, block, 1];
                _cubelets[2, block, 1] = _cubelets[1, block, 2];
                _cubelets[1, block, 2] = _cubelets[0, block, 1];
                _cubelets[0, block, 1] = tmpCubelet;

                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _cubelets[i, block, k].RotateCW(2);
                    }
                }
            }

            if ((_rotateY[0] == _rotateY[1] || 
                 _rotateY[0] == _rotateY[1] - 90 ||
                 _rotateY[0] == _rotateY[1] + 90) &&
                (_rotateY[1] == _rotateY[2] ||
                 _rotateY[1] == _rotateY[2] - 90 ||
                 _rotateY[1] == _rotateY[2] + 90))
            {
                _cubeY += _rotateY[0];
                _rotateY[0] = _rotateY[1] = _rotateY[2] = 0;
            }
        }

        /// <summary>
        /// Rotate a block of cubelets in Z axis
        /// </summary>
        /// <param name="block">block of cubelets want to rotate</param>
        /// <param name="degress">Degress to rotate</param>
        public void RotateZ(int block, int degress)
        {
            // Rotate in Z axis
            _rotateZ[block] += degress;

            // if rotate counter clockwise 90 degress -> stop and swap color in cubelets
            if (degress < 0 && _rotateZ[block] == -90)
            {
                _rotateZ[block] = 0;

                Cubelet tmpCubelet = _cubelets[0, 0, block];
                _cubelets[0, 0, block] = _cubelets[2, 0, block];
                _cubelets[2, 0, block] = _cubelets[2, 2, block];
                _cubelets[2, 2, block] = _cubelets[0, 2, block];
                _cubelets[0, 2, block] = tmpCubelet;

                tmpCubelet = _cubelets[1, 0, block];
                _cubelets[1, 0, block] = _cubelets[2, 1, block];
                _cubelets[2, 1, block] = _cubelets[1, 2, block];
                _cubelets[1, 2, block] = _cubelets[0, 1, block];
                _cubelets[0, 1, block] = tmpCubelet;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _cubelets[i, j, block].RotateCCW(3);
                    }
                }
            }
            else if (degress > 0 && _rotateZ[block] == 90)
            {
                // if rotate clockwise 90 degress -> stop and swap color in cubelets
                _rotateZ[block] = 0;

                Cubelet tmpCubelet = _cubelets[0, 2, block];
                _cubelets[0, 2, block] = _cubelets[2, 2, block];
                _cubelets[2, 2, block] = _cubelets[2, 0, block];
                _cubelets[2, 0, block] = _cubelets[0, 0, block];
                _cubelets[0, 0, block] = tmpCubelet;

                tmpCubelet = _cubelets[0, 1, block];
                _cubelets[0, 1, block] = _cubelets[1, 2, block];
                _cubelets[1, 2, block] = _cubelets[2, 1, block];
                _cubelets[2, 1, block] = _cubelets[1, 0, block];
                _cubelets[1, 0, block] = tmpCubelet;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _cubelets[i, j, block].RotateCW(3);
                    }
                }
            }

            if ((_rotateZ[0] == _rotateZ[1] ||
                 _rotateZ[0] == _rotateZ[1] - 90 ||
                 _rotateZ[0] == _rotateZ[1] + 90) &&
                (_rotateZ[1] == _rotateZ[2] ||
                 _rotateZ[1] == _rotateZ[2] - 90 ||
                 _rotateZ[1] == _rotateZ[2] + 90) )
            {
                _cubeZ += _rotateZ[0];
                _rotateZ[0] = _rotateZ[1] = _rotateZ[2] = 0;
            }
        }

        private void SetColor(char side, int row, int col, int color)
        {
            int x = 0;
            int y = 0;
            int z = 0;
            int face = 0;

            switch (side)
            {
                case 'F':
                    x = col;
                    y = Math.Abs(row - 2);
                    z = 2;
                    face = 2;
                    break;
                case 'B':
                    x = Math.Abs(col - 2);
                    y = Math.Abs(row - 2);
                    z = 0;
                    face = 3;
                    break;
                case 'T':
                    x = col;
                    y = 2;
                    z = row;
                    face = 1;
                    break;
                case 'D':
                    x = col;
                    y = 0;
                    z = Math.Abs(row - 2);
                    face = 4;
                    break;
                case 'L':
                    x = 0;
                    y = Math.Abs(row - 2);
                    z = col;
                    face = 5;
                    break;
                case 'R':
                    x = 2;
                    y = Math.Abs(row - 2);
                    z = Math.Abs(col - 2);
                    face = 0;
                    break;
            }

            _cubelets[x, y, z].SetColor(face, color);
        }
    }
}
