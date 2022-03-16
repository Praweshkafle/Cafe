using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
   public class ResponseModel<T>
    {
        //  public ObservableCollection<T> Data { get; set; }
        public T Data { get; set; }
    }
}
