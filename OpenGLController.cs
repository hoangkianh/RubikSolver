using System;
using CSGL12;
using System.Windows.Forms;

namespace RubikSolver
{
    /// <summary>
    /// Description of OpenGLController.
    /// </summary>
    public class OpenGLController: Solver
    {
        private float _zoomZ;
        private int _speed;

        private readonly Cube3D _cube3D;

        public float RotateX_Angle { get; set; }

        public float RotateY_Angle { get; set; }

        public float ZoomZ
        {
            set { _zoomZ = value; }
        }

        public int Speed
        {
            set { _speed = value; }
        }

        public bool AllowAnimate { get; set; }

        public OpenGLController(CSGL12Control glControl)
        {
            RotateX_Angle = 25.0f;
            RotateY_Angle = -45.0f;
            _zoomZ = 8;
            _speed = 1000;
            AllowAnimate = true;
            _cube3D = new Cube3D(glControl.GetGL());
            MyGLControl = glControl;
        }

        public void OpenGLStarted(CSGL12Control csgl12Control)
        {
            
        }

        public void Paint(object sender,PaintEventArgs e)
        {
            var csgl12Control = (sender as CSGL12Control);
            if (csgl12Control != null)
            {
                GL gl = csgl12Control.GetGL();
                LoadCSGL(gl, csgl12Control);
                InitGL(gl);

                gl.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
                gl.glLoadIdentity();

                DrawSense(gl);

                gl.wglSwapBuffers(csgl12Control.GetHDC());
            }
        }

        void LoadCSGL(GL gl,CSGL12Control csglControl1)
        {
            int clientWidth=csglControl1.ClientRectangle.Width;
            int clientHeight=csglControl1.ClientRectangle.Height;
            if(clientWidth<=0)
                clientWidth=1;
            if(clientHeight<=0)
                clientHeight=1;
            gl.glViewport(0,0,clientWidth,clientHeight);
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glLoadIdentity();
            
            double aspectRatio = 1.0;
            if (0 != clientHeight)
            {
                aspectRatio = (clientWidth / (double)(clientHeight));
            }
            
            gl.gluPerspective(45.0f, aspectRatio, 0.1f, 100.0f);
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glLoadIdentity();
        }

        void InitGL(GL gl)
        {
            gl.glClearColor(0.8f, 0.8f, 0.8f, 0.5f);
            gl.glClearDepth(1.0);

            gl.glEnable(GL.GL_DEPTH_TEST);
            gl.glDepthFunc(GL.GL_LEQUAL);
            gl.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);
            
            float[] light_ambient = { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light_specular = { 0.35f, 0.35f, 0.35f, 1.0f };
            float[] light_position = { 0, 10.0f, 20.0f, 1.0f };

            gl.glShadeModel(GL.GL_SMOOTH);

            gl.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, light_position);
            gl.glLightfv(GL.GL_LIGHT0,GL.GL_AMBIENT, light_ambient);
            gl.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, light_diffuse);
            gl.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, light_specular);
            gl.glLightModelf(GL.GL_LIGHT_MODEL_LOCAL_VIEWER, GL.GL_TRUE);

            gl.glEnable(GL.GL_LIGHT0);
            gl.glEnable(GL.GL_LIGHTING);

        }
        
        void DrawSense(GL gl)
        {
            gl.glTranslatef(0, 0, -_zoomZ);
            gl.glRotatef(RotateX_Angle, 1, 0, 0);
            gl.glRotatef(RotateY_Angle, 0, 1, 0);
            _cube3D.Render();
        }

        /// <summary>
        /// Zoom & Rotate Cube when cube moving
        /// </summary>
        private void AsycnCube()
        {
            long interval = _speed / ((_speed / 10) + 1);
            long start = Environment.TickCount;
            long current = 0;

            while (current < start + interval)
            {
                Application.DoEvents();
                current = Environment.TickCount;
            }
        }

        private void UpdateCube()
        {
            if (AllowAnimate)
            {
                MyGLControl.Invalidate();
                MyGLControl.Update();
                AsycnCube();
            }
        }

        protected override void RotateCW(CubeSide side)
        {
            base.RotateCW(side);
            Rotate3DCW(side);
        }

        protected override void RotateCCW(CubeSide side)
        {
            base.RotateCCW(side);
            Rotate3DCCW(side);
        }

        private void Rotate3DCW(CubeSide side)
        {
            if (side != null)
            {
                int numTurns = _speed/50;

                if (numTurns < 2)
                {
                    numTurns = 2;
                }

                while (90 % numTurns != 0)
                {
                    numTurns++;
                }

                int degrees = 90/numTurns;

                switch (side.Side)
                {
                    case 'F':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateZ(2, -degrees);
                            UpdateCube();
                        }
                        break;
                    case 'B':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateZ(0, degrees);
                            UpdateCube();
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateX(2, -degrees);
                            UpdateCube();
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateX(0, degrees);
                            UpdateCube();
                        }
                        break;
                    case 'T':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateY(2, -degrees);
                            UpdateCube();
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateY(0, degrees);
                            UpdateCube();
                        }
                        break;
                }
            }
        }
        
        private void Rotate3DCCW(CubeSide side)
        {
            if (side != null)
            {
                int numTurns = _speed/50;

                if (numTurns < 2)
                {
                    numTurns = 2;
                }

                while (90 % numTurns != 0)
                {
                    numTurns++;
                }

                int degress = 90/numTurns;

                switch (side.Side)
                {
                    case 'F':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateZ(2, degress);
                            UpdateCube();
                        }
                        break;
                    case 'B':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateZ(0, -degress);
                            UpdateCube();
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateX(2, degress);
                            UpdateCube();
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateX(0, -degress);
                            UpdateCube();
                        }
                        break;
                    case 'T':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateY(2, degress);
                            UpdateCube();
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < numTurns; i++)
                        {
                            _cube3D.RotateY(0, -degress);
                            UpdateCube();
                        }
                        break;
                }
            }
        }

        protected override void SaveCube()
        {
            base.SaveCube();
            _cube3D.Save();
        }

        protected override void RestoreCube()
        {
            base.RestoreCube();
            _cube3D.Restore();
        }

        public override void Reset()
        {
            base.Reset();
            _cube3D.Reset();
        }
    }
}