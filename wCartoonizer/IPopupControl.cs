using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimaCartoonizer
{
    public interface IPopupControl
    {
        bool CloseApp
        {
            get;
            set;
        }

        bool ShowOpenDialogOnExit
        {
            get;
            set;
        }
    }
}
