using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourTrader
{
    class OtherPanels
    {
        static public HeaderPanel mainPanel = new HeaderPanel();

        static public void Init()
        {
            Update();
        }

        static public void Update()
        {
            mainPanel.update(); 
        }
    }
}



