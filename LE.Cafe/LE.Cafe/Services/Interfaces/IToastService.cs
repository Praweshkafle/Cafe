using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Services.Interfaces
{
    public interface IToastService
    {
        void ShortMessage(string message);
        void LongMessage(string message);
    }
}
