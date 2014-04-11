using System;
using CSGL12;

namespace RubikSolver
{
    internal class Cubelet
    {
        private readonly float _sideLength;
        private readonly float _roundWidth;
        private int _roundSteps;
        private readonly GL gl;

        private readonly float[] RED = { 1, 0, 0 };
        private readonly float[] YELLOW = { 1, 1, 0 };
        private readonly float[] BLUE = { 0, 0, 1 };
        private readonly float[] GREEN = { 0, 1, 0 };
        private readonly float[] WHITE = { 1, 1, 1 };
        private readonly float[] ORANGE = { 1, 0.6f, 0 };

        private readonly float[][] _sides = new float[6][]; // Jagged Array
        private readonly float[] _sideMaterial = new float[4];
        private readonly float[] _shininess = new float[1];
        private readonly float[] _bevelDiffuse = new float[4];
        private readonly float[] _bevelMaterial = new float[4];
        private readonly float[] _bevelShininess = new float[4];
        
        public Cubelet(GL gl)
        {
            this.gl = gl;
            _sideLength = 0.8f;
            _roundWidth = 0.1f;
            _roundSteps = 2;
            _sideMaterial[0] = _sideMaterial[1] = _sideMaterial[2] = 0.15f;
            _sideMaterial[3] = 1.0f;
            _shininess[0] = 2; // The sides have small highlights.

            _bevelDiffuse[0] = _bevelDiffuse[1] = _bevelDiffuse[2] = 0.25f;
            _bevelDiffuse[3] = 1.0f;
            _bevelMaterial[0] = _bevelMaterial[1] = _bevelMaterial[2] = 1.0f;
            _bevelMaterial[3] = 1.0f;
            _bevelShininess[0] = 115f;

            _sides[0] = RED;
            _sides[1] = YELLOW;
            _sides[2] = BLUE;
            _sides[3] = GREEN;
            _sides[4] = WHITE;
            _sides[5] = ORANGE;
        }

        /// <summary>
        /// Draw corners
        /// </summary>
        private void DrawConers()
        {
            double step = Math.PI/2/_roundSteps;
            double v, w, vv, ww;

            gl.glBegin(GL.GL_QUADS);
            gl.glColor3f(0, 0, 0);

            for (int i = 0; i < _roundSteps; i++)
            {
                v = i*step;
                vv = (i + 1)*step;

                for (int j = 0; j < _roundSteps; j++)
                {
                    w = j*step;
                    ww = (j + 1)*step;

                    gl.glNormal3f((float)Math.Cos(v) * (float)Math.Cos(w), (float)Math.Cos(v) * (float)Math.Sin(w), (float)Math.Sin(v));
                    gl.glVertex3f((float)Math.Cos(v) * (float)Math.Cos(w) * _roundWidth,
                        (float)Math.Cos(v) * (float)Math.Sin(w) * _roundWidth,
                        (float)Math.Sin(v) * _roundWidth);

                    gl.glNormal3f((float)Math.Cos(vv) * (float)Math.Cos(w), (float)Math.Cos(vv) * (float)Math.Sin(w), (float)Math.Sin(vv));
                    gl.glVertex3f((float)Math.Cos(vv) * (float)Math.Cos(w) * _roundWidth,
                        (float)Math.Cos(vv) * (float)Math.Sin(w) * _roundWidth,
                        (float)Math.Sin(vv) * _roundWidth);

                    gl.glNormal3f((float)Math.Cos(vv) * (float)Math.Cos(ww), (float)Math.Cos(vv) * (float)Math.Sin(ww), (float)Math.Sin(vv));
                    gl.glVertex3f((float)Math.Cos(vv) * (float)Math.Cos(ww) * _roundWidth,
                        (float)Math.Cos(vv) * (float)Math.Sin(ww) * _roundWidth,
                        (float)Math.Sin(vv) * _roundWidth);

                    gl.glNormal3f((float)Math.Cos(v) * (float)Math.Cos(ww), (float)Math.Cos(v) * (float)Math.Sin(ww), (float)Math.Sin(v));
                    gl.glVertex3f((float)Math.Cos(v) * (float)Math.Cos(ww) * _roundWidth,
                        (float)Math.Cos(v) * (float)Math.Sin(ww) * _roundWidth,
                        (float)Math.Sin(v) * _roundWidth);
                }
            }

            gl.glEnd();
        }

        /// <summary>
        /// Draw edges
        /// </summary>
        private void DrawEdges()
        {
            double _step = Math.PI/2/_roundSteps;
            double v, vv;

            gl.glBegin(GL.GL_QUADS);
            gl.glColor3f(0, 0, 0);

            for (int i = 0; i < _roundSteps; i++)
            {
                v = i*_step;
                vv = (i + 1)*_step;

                gl.glNormal3f((float)Math.Cos(v), (float)Math.Sin(v), 0);
                gl.glVertex3f((float)Math.Cos(v) * _roundWidth, (float)Math.Sin(v) * _roundWidth,  _sideLength / 2);
                gl.glVertex3f((float)Math.Cos(v) * _roundWidth, (float)Math.Sin(v) * _roundWidth, -_sideLength / 2);

                gl.glNormal3f((float)Math.Cos(vv), (float)Math.Sin(vv), 0);
                gl.glVertex3f((float)Math.Cos(vv) * _roundWidth, (float)Math.Sin(vv) * _roundWidth, -_sideLength / 2);
                gl.glVertex3f((float)Math.Cos(vv) * _roundWidth, (float)Math.Sin(vv) * _roundWidth,  _sideLength / 2);
            }

            gl.glEnd();
        }

        /// <summary>
        /// Render a cubelet
        /// </summary>
        public void Render()
        {
            gl.glPushMatrix();

            #region Draw sides
            
            // Draw the side
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, _sideMaterial);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, _shininess);

            gl.glBegin(GL.GL_QUADS);

            // The right
            gl.glColor3fv(RED);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, RED);
            gl.glNormal3f(1, 0, 0);

            gl.glVertex3f(_sideLength / 2 + _roundWidth, _sideLength / 2, _sideLength / 2);
            gl.glVertex3f(_sideLength / 2 + _roundWidth, _sideLength / 2, -_sideLength / 2);
            gl.glVertex3f(_sideLength / 2 + _roundWidth, -_sideLength / 2, -_sideLength / 2);
            gl.glVertex3f(_sideLength / 2 + _roundWidth, -_sideLength / 2, _sideLength / 2);

            // The top
            gl.glColor3fv(YELLOW);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, YELLOW);
            gl.glNormal3f(0, 1, 0);

            gl.glVertex3f(-_sideLength / 2, _sideLength / 2 + _roundWidth, -_sideLength / 2);
            gl.glVertex3f(-_sideLength / 2, _sideLength / 2 + _roundWidth, _sideLength / 2);
            gl.glVertex3f(_sideLength / 2, _sideLength / 2 + _roundWidth, _sideLength / 2);
            gl.glVertex3f(_sideLength / 2, _sideLength / 2 + _roundWidth, -_sideLength / 2);

            //The front
            gl.glColor3fv(BLUE);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, BLUE);
            gl.glNormal3f(0, 0, 1);

            gl.glVertex3f(_sideLength / 2, _sideLength / 2, _sideLength / 2 + _roundWidth);
            gl.glVertex3f(-_sideLength / 2, _sideLength / 2, _sideLength / 2 + _roundWidth);
            gl.glVertex3f(-_sideLength / 2, -_sideLength / 2, _sideLength / 2 + _roundWidth);
            gl.glVertex3f(_sideLength / 2, -_sideLength / 2, _sideLength / 2 + _roundWidth);

            // The back
            gl.glColor3fv(GREEN);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, GREEN);
            gl.glNormal3f(0, 0, -1);

            gl.glVertex3f(_sideLength / 2, _sideLength / 2, -_sideLength / 2 - _roundWidth);
            gl.glVertex3f(-_sideLength / 2, _sideLength / 2, -_sideLength / 2 - _roundWidth);
            gl.glVertex3f(-_sideLength / 2, -_sideLength / 2, -_sideLength / 2 - _roundWidth);
            gl.glVertex3f(_sideLength / 2, -_sideLength / 2, -_sideLength / 2 - _roundWidth);

            //The bottom
            gl.glColor3fv(WHITE);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, WHITE);
            gl.glNormal3f(0, -1, 0);

            gl.glVertex3f(-_sideLength / 2, -_sideLength / 2 - _roundWidth, -_sideLength / 2);
            gl.glVertex3f(-_sideLength / 2, -_sideLength / 2 - _roundWidth, _sideLength / 2);
            gl.glVertex3f(_sideLength / 2, -_sideLength / 2 - _roundWidth, _sideLength / 2);
            gl.glVertex3f(_sideLength / 2, -_sideLength / 2 - _roundWidth, -_sideLength / 2);

            //The left
            gl.glColor3fv(ORANGE);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, ORANGE);
            gl.glNormal3f(-1, 0, 0);

            gl.glVertex3f(-_sideLength / 2 - _roundWidth, _sideLength / 2, _sideLength / 2);
            gl.glVertex3f(-_sideLength / 2 - _roundWidth, _sideLength / 2, -_sideLength / 2);
            gl.glVertex3f(-_sideLength / 2 - _roundWidth, -_sideLength / 2, -_sideLength / 2);
            gl.glVertex3f(-_sideLength / 2 - _roundWidth, -_sideLength / 2, _sideLength / 2);

            gl.glEnd();
            
            #endregion

            #region Draw bevels

            gl.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT_AND_DIFFUSE, _bevelDiffuse);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, _bevelMaterial);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, _bevelShininess);

            // Upper, Right, Back Conner
            gl.glTranslatef(_sideLength / 2, _sideLength / 2, _sideLength / 2);
            DrawConers();

            // Upper, Left, Back Conner
            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawConers();
            gl.glRotatef(-90, 0, 0, 1);

            // Lower, Left, Back Conner
            gl.glTranslatef(0, -_sideLength, 0);
            gl.glRotatef(180, 0, 0, 1);
            DrawConers();
            gl.glRotatef(-180, 0, 0, 1);

            // Lower, Right, Back Conner
            gl.glTranslatef(_sideLength, 0, 0);
            gl.glRotatef(270, 0, 0, 1);
            DrawConers();
            gl.glRotatef(-270, 0, 0, 1);

            // Upper, Right, Front Conner
            gl.glTranslatef(0, _sideLength, -_sideLength);
            gl.glRotatef(90, 0, 1, 0);
            DrawConers();
            gl.glRotatef(-90, 0, 1, 0);

            // Upper, Left, Front Conner
            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(180, 0, 1, 0);
            DrawConers();
            gl.glRotatef(-180, 0, 1, 0);

            // Lower, Left, Front Conner
            gl.glTranslatef(0, -_sideLength, 0);
            gl.glRotatef(180, 0, 1, 0);
            gl.glRotatef(90, 1, 0, 0);
            DrawConers();
            gl.glRotatef(-90, 1, 0, 0);
            gl.glRotatef(-180, 0, 1, 0);

            // Lower, Right, Front Conner
            gl.glTranslatef(_sideLength, 0, 0);
            gl.glRotatef(180, 1, 0, 0);
            DrawConers();
            gl.glRotatef(-180, 1, 0, 0);
            
            #endregion

            #region Draw edges

            gl.glTranslatef(0, _sideLength, _sideLength / 2);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();
            gl.glRotatef(-90, 0, 0, 1);

            gl.glTranslatef(0, -_sideLength, 0);
            gl.glRotatef(180, 0, 0, 1);
            DrawEdges();
            gl.glRotatef(-180, 0, 0, 1);

            gl.glTranslatef(_sideLength, 0, 0);
            gl.glRotatef(270, 0, 0, 1);
            DrawEdges();
            gl.glRotatef(-270, 0, 0, 1);

            gl.glTranslatef(0, _sideLength / 2, _sideLength / 2);
            gl.glRotatef(90, 1, 0, 0);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            gl.glRotatef(-90, 1, 0, 0);
            gl.glTranslatef(0, _sideLength / 2, -_sideLength / 2);

            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            gl.glTranslatef(-_sideLength, 0, 0);
            gl.glRotatef(90, 0, 0, 1);
            DrawEdges();

            #endregion

            gl.glPopMatrix();
        }

        private void CopyColor(float[] dest, float[] src)
        {
            dest[0] = src[0];
            dest[1] = src[1];
            dest[2] = src[2];
        }

        private void RotateColors(int a, int b, int c, int d)
        {
            float[] tmp = new float[3];

            CopyColor(tmp, _sides[a]);
            CopyColor(_sides[a], _sides[b]);
            CopyColor(_sides[b], _sides[c]);
            CopyColor(_sides[c], _sides[d]);
            CopyColor(_sides[d], tmp);
        }

        /// <summary>
        /// Rotate cubelet clockwise
        /// </summary>
        /// <param name="axis">1: X axis - 2: Y axis - 3: Z axis</param>
        public void RotateCW(int axis)
        {
            switch (axis)
            {
                case 1: 
                    RotateColors(3, 4, 2, 1);
                    break;
                case 2:
                    RotateColors(0, 2, 5, 3);
                    break;
                case 3:
                    RotateColors(0, 4, 5, 1);
                    break;
            }
        }

        /// <summary>
        /// Rotate cubelet counter clockwise
        /// </summary>
        /// <param name="axis">1: X axis - 2: Y axis - 3: Z axis</param>
        public void RotateCCW(int axis)
        {
            switch (axis)
            {
                case 1:
                    RotateColors(1, 2, 4, 3);
                    break;
                case 2:
                    RotateColors(3, 5, 2, 0);
                    break;
                case 3:
                    RotateColors(1, 5, 4, 0);
                    break;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < 6; i++)
            {
                _sides[i][0] = _sides[i][1] = _sides[i][2] = 0.25f;
            }
        }

        public void SetColor(int face, int color)
        {
            switch (color)
            {
                case 1:// yellow
                    _sides[face][0] = 1;
                    _sides[face][1] = 1;
                    _sides[face][2] = 0;
                    break;
                case 2:// blue
                    _sides[face][0] = 0;
                    _sides[face][1] = 0;
                    _sides[face][2] = 1;
                    break;
                case 3:// red
                    _sides[face][0] = 1;
                    _sides[face][1] = 0;
                    _sides[face][2] = 0;
                    break;
                case 4:// orange
                    _sides[face][0] = 1;
                    _sides[face][1] = 0.5f;
                    _sides[face][2] = 0;
                    break;
                case 5:// green
                    _sides[face][0] = 0;
                    _sides[face][1] = 1;
                    _sides[face][2] = 0;
                    break;
                case 6:// white
                    _sides[face][0] = 1;
                    _sides[face][1] = 1;
                    _sides[face][2] = 1;
                    break;
            }
        }

        public void Copy(Cubelet src)
        {
            for (int i = 0; i < 6; i++)
            {
                _sides[i][0] = src._sides[i][0];
                _sides[i][1] = src._sides[i][1];
                _sides[i][2] = src._sides[i][2];
            }
        }
    }
}
