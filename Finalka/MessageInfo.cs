using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finalka
{
    public class MessageInfo
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public MessageInfo(string text, DateTime date)
        {
            this.Text = text;
            this.Date = date;
        }
        public override string ToString() 
        {
            return $"{Text} . Date {Date}";
        }
    }
}
