using BoschRexroth.Root;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoschRexroth.WinUI
{
    public partial class MainForm : Form
    {
        private MainController _mainController;

        public MainForm()
        {
            InitializeComponent();

            _mainController = new MainController(new Logger((str) => 
            {
                var format = string.Format("[{0}] {1}{2}", DateTime.Now.ToString("hh:mm:ss"), str, Environment.NewLine);
                tbLog.Text += format;
            }));
            _mainController.Initialize();
        }

        #region Commands

        private abstract class CmdAbstract
        {
            protected CmdParams Params;

            public CmdAbstract(CmdParams parameters)
            {
                Params = parameters;
            }

            public abstract void Execute(MainController controller);
        }

        private class CmdCalibrare : CmdAbstract
        {
            public CmdCalibrare(CmdParams parameters) : base(parameters)
            {
            }

            public override void Execute(MainController controller)
            {
                controller.CalibrateMarker();
            }

            public override string ToString()
            {
                return "CALIBRATE";
            }
        }

        private class CmdTurn : CmdAbstract
        {
            public CmdTurn(CmdParams parameters) : base(parameters)
            {
            }

            public override void Execute(MainController controller)
            {
                controller.Turn(Params.Angle, Params.Speed);
            }

            public override string ToString()
            {
                return (Params.Angle > 0 ? "CLOCKWISE" : "COUNTERCLOCKWISE") + " " + Math.Abs(Params.Angle);
            }
        }

        private class CmdPause : CmdAbstract
        {
            public CmdPause(CmdParams parameters) : base(parameters)
            {
            }

            public override void Execute(MainController controller)
            {
                controller.Pause(Params.Sleep);
            }

            public override string ToString()
            {
                return "SLEEP " + Params.Sleep;
            }
        }

        private class CmdStop : CmdAbstract
        {
            public CmdStop(CmdParams parameters) : base(parameters)
            { 
            }

            public override void Execute(MainController controller)
            {
                controller.Stop();
            }

            public override string ToString()
            {
                return "STOP";
            }
        }

        private class CmdBase : CmdAbstract
        {
            public CmdBase(CmdParams parameters) : base(parameters)
            {
            }

            public override void Execute(MainController controller)
            {
                controller.Base(Params.Speed);
            }

            public override string ToString()
            {
                return "BASE";
            }
        }

        private class CmdSpeed : CmdAbstract
        {
            public CmdSpeed(CmdParams parameters) : base(parameters)
            {
            }

            public override void Execute(MainController controller)
            {
                controller.Speed(Params.Speed);
            }

            public override string ToString()
            {
                return "SPEED " + Params.Speed;
            }
        }

        #endregion

        private CmdParams GetParams()
        {
            return new CmdParams()
            {
                Speed = ushort.Parse(tbSpeed.Text),
                Angle = int.Parse(tbTurn.Text),
                Sleep = int.Parse(tbPause.Text)
            };
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clbOperations.Items.Clear();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            var parameters = GetParams();
            for(int i =0; i < clbOperations.Items.Count; i++)
            {
                var cmd = (CmdAbstract)clbOperations.Items[i];
                cmd.Execute(_mainController);
                clbOperations.SetItemChecked(i, true);
            }
        }

        private void btnTurn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdTurn(GetParams()));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdStop(GetParams()));
        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSpeed(GetParams()));
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPause(GetParams()));
        }

        private void btnBase_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBase(GetParams()));
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCalibrare(GetParams()));
        }

        private void AddCommand(CmdAbstract cmd)
        {
            clbOperations.Items.Add(cmd);
        }
    }

    public class CmdParams
    {
        public ushort Speed { get; set; }

        public int Angle { get; set; }

        public int Sleep { get; set; }
    }
}
