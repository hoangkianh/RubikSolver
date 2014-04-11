using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using RubikSolver.Properties;

namespace RubikSolver
{
    /// <summary>
    /// Description of RubikForm.
    /// </summary>
    /// 
    
    public partial class RubikForm : Form
    {
        private readonly OpenGLController openGLController;
        private Point oldPoint;
        private readonly List<string> moveList;
        private int cur_index;

        public RubikForm()
        {
            InitializeComponent();
            openGLController = new OpenGLController(csgL12Control1);

            csgL12Control1.OpenGLStarted += openGLController.OpenGLStarted;
            csgL12Control1.Paint += openGLController.Paint;

            moveList = new List<String>();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            csgL12Control1.Refresh();
            csgL12Control1.Invalidate();
        }

        private void csgL12Control1_MouseDown(object sender, MouseEventArgs e)
        {
            oldPoint = e.Location;
        }

        private void csgL12Control1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point curPoint = e.Location;
                openGLController.RotateX_Angle += (curPoint.Y - oldPoint.Y);
                int tmpRotate = (int)openGLController.RotateX_Angle;
                if (tmpRotate < 0)
                {
                    tmpRotate += 360;
                }
                tmpRotate %= 360;

                if (tmpRotate > 90 && tmpRotate < 270)
                {
                    openGLController.RotateY_Angle -= (curPoint.X - oldPoint.X);
                }
                else
                {
                    openGLController.RotateY_Angle += (curPoint.X - oldPoint.X);
                }

                oldPoint = curPoint;
            }
        }

        private void EnableControl(bool enable)
        {
            btnShuffle.Enabled = enable;
            btnSolve.Enabled = enable;
            btnReset.Enabled = enable;
            timer1.Enabled = enable;
            lbxMoveList.Enabled = enable;
            if (enable)
            {
                EnableNextPrev();
            }
            else
            {
                btnNext.Enabled = false;
                btnPrev.Enabled = false;
            }
            lbxMoveList.Focus();
        }
        
        private void trbZoom_Scroll(object sender, EventArgs e)
        {
            openGLController.ZoomZ = trbZoom.Maximum - trbZoom.Value + 3;
        }

        private void trbSpeed_Scroll(object sender, EventArgs e)
        {
            openGLController.Speed = trbSpeed.Value;
            label5.Text = (Math.Round((double)1000 / trbSpeed.Value, 2)).ToString(CultureInfo.InvariantCulture);
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            ClearMove();
            EnableControl(false);

            openGLController.Shuffle(toolStripProgressBar1);

            EnableControl(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkAnimate_CheckedChanged(object sender, EventArgs e)
        {
            trbSpeed.Enabled = chkAnimate.Checked;
            openGLController.AllowAnimate = chkAnimate.Checked;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            openGLController.AllowAnimate = false;
            SolveCube();
            openGLController.AllowAnimate = true;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            int index = lbxMoveList.SelectedIndex;
            if (index <= 0)
            {
                return;
            }
            DoMove(index, true);
            cur_index = --index;
            lbxMoveList.SelectedIndex = cur_index;
            if (openGLController.AllowAnimate)
            {
                openGLController.MyGLControl.Invalidate();
                openGLController.MyGLControl.Update(); 
            }
            EnableControl(true);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            int index = lbxMoveList.SelectedIndex;
            if (index >= moveList.Count - 1)
            {
                return;
            }
            cur_index = ++index;
            lbxMoveList.SelectedIndex = cur_index;
            DoMove(index, false);
            if (openGLController.AllowAnimate)
            {
                openGLController.MyGLControl.Invalidate();
                openGLController.MyGLControl.Update(); 
            }
            EnableControl(true);
        }

        private void lbxMoveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbxMoveList.SelectedIndex;
            lbxMoveList.SelectedIndex = cur_index;
            if (index > cur_index)
            {
                while (cur_index <= index)
                {
                    btnNext_Click(sender, e);
                    cur_index++;
                }
            }
            else if(index < cur_index)
            {
                while (cur_index >= index)
                {
                    btnPrev_Click(sender, e);
                    cur_index--;
                }
            }
            cur_index = index;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearMove();
            EnableControl(false);
            openGLController.Reset();
            openGLController.MyGLControl.Invalidate();
            EnableControl(true);
        }

        private void SetSide(char side, Label lbl)
        {
            switch (side)
            {
                case 'T':
                    lbl.Text = "Top";
                    break;
                case 'D':
                    lbl.Text = "Down";
                    break;
                case 'L':
                    lbl.Text = "Left";
                    break;
                case 'R':
                    lbl.Text = "Right";
                    break;
                case 'F':
                    lbl.Text = "Front";
                    break;
                case 'B':
                    lbl.Text = "Back";
                    break;
            }
        }

        /// <summary>
        /// Get color for solution: which color in the top, front, right, etc...
        /// </summary>
        /// <param name="str">color and side string</param>
        private void SetColorForSolution(string str)
        {
            int start = 0;
            string color = str.Substring(start, 3);

            while (true)
            {
                switch (color[1])
                {
                    case '1':// YELLOW
                        btnYellow.Text = color[0].ToString();
                        SetSide(color[0], lblYellow);
                        break;
                    case '2':// BLUE
                        btnBlue.Text = color[0].ToString();
                        SetSide(color[0], lblBlue);
                        break;
                    case '3'://RED
                        btnRed.Text = color[0].ToString();
                        SetSide(color[0], lblRed);
                        break;
                    case '4'://ORANGE
                        btnOrange.Text = color[0].ToString();
                        SetSide(color[0], lblOrange);
                        break;
                    case '5'://GREEN
                        btnGreen.Text = color[0].ToString();
                        SetSide(color[0], lblGreen);
                        break;
                    case '6'://WHITE
                        btnWhite.Text = color[0].ToString();
                        SetSide(color[0], lblWhite);
                        break;
                }
                start += 3;
                if (start >= str.Length)
                {
                    break;
                }
                color = str.Substring(start, 3);
            }
        }

        private void SolveCube()
        {
            ClearMove();

            toolStripProgressBar1.Maximum = 150;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Value = 0;
            EnableControl(false);

            int solve = openGLController.Solve(toolStripProgressBar1);

            switch (solve)
            {
                case 1:
                    MessageBox.Show(Resources.Does_not_contain_proper_cubelets,
                                    Resources.Configuration_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2:
                    MessageBox.Show(Resources.Cube_mis_oriented,
                                    Resources.Unable_To_Solve_Cube, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    {
                        moveList.Add("");
                        lbxMoveList.Items.Add(Resources.Your_cube);
                        string moves = openGLController.GetMoves();
                        string colors = moves.Substring(0, 18);
                        SetColorForSolution(colors);

                        moves = moves.Substring(18, moves.Length - 18);
                        int start = 0;

                        if (string.IsNullOrEmpty(moves))
                        {
                            EnableControl(true);
                            return;
                        }
                        string move = moves.Substring(start, 3);

                        while (true)
                        {
                            string str = string.Empty;
                            moveList.Add(move);

                            switch (move[0])
                            {
                                case 'T':
                                    str = Resources.Top_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Left;
                                            break;
                                        case '-':
                                            str += Resources.Right;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case 'F':
                                    str = Resources.Front_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Clockwise;
                                            break;
                                        case '-':
                                            str += Resources.Counter_clockwise;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case 'L':
                                    str = Resources.Left_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Down;
                                            break;
                                        case '-':
                                            str += Resources.Up;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case 'R':
                                    str = Resources.Right_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Up;
                                            break;
                                        case '-':
                                            str += Resources.Down;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case 'B':
                                    str = Resources.Back_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Clockwise;
                                            break;
                                        case '-':
                                            str += Resources.Counter_clockwise;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case 'D':
                                    str = Resources.Bottom_Side;
                                    switch (move[1])
                                    {
                                        case '+':
                                            str += Resources.Right;
                                            break;
                                        case '-':
                                            str += Resources.Left;
                                            break;
                                        case '*':
                                            str += "180";
                                            break;
                                    }
                                    break;
                                case '~':
                                    str = "----- ";
                                    switch (move[1])
                                    {
                                        case '1':
                                            str += "LAYER 1 ";
                                            break;
                                        case '2':
                                            str += "LAYER 2 ";
                                            break;
                                        case '3':
                                            str += "CUBE ";
                                            break;
                                    }
                                    str += "SOLVED -----";
                                    break;
                            }
                            lbxMoveList.Items.Add(str);
                            start += 3;
                            if (start >= moves.Length)
                            {
                                break;
                            }
                            move = moves.Substring(start, 3);
                        }
                        lbxMoveList.SelectedIndex = 0;
                        cur_index = lbxMoveList.SelectedIndex;
                        lblMoveCount.Text = string.Format("{0} moves", (start / 3));
                        DoMove(cur_index, false);
                        EnableNextPrev();
                    }
                    break;
            }
            EnableControl(true);
        }

        private void DoMove(int index, bool undo)
        {
            if (index < 0)
            {
                return;
            }
            string str = moveList[index];

            if (undo)
            {
                openGLController.UndoMove(str);
            }
            else
            {
                openGLController.DoMove(str);
            }
        }

        private void ClearMove()
        {
            moveList.Clear();
            lbxMoveList.Items.Clear();
            lblMoveCount.Text = Resources.Moves;
            cur_index = 0;
            EnableNextPrev();
        }

        private void EnableNextPrev()
        {
            btnPrev.Enabled = cur_index > 0;
            btnNext.Enabled = cur_index < lbxMoveList.Items.Count - 1;
        }
    }
}
