using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketData;

namespace Client
{
    public partial class GameWindow : Form
    {
        public GameWindow(InitGamePacket packet)
        {
            InitializeComponent(packet);
        }
    }
}
