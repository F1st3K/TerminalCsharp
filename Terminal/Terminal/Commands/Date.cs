using System;

namespace Terminal.Commands
{
    internal class Date : Command
    {
        public Date(string name) : base(name)
        {
            _command += Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                return DateTime.Now.ToLongDateString() +" "+ DateTime.Now.ToLongTimeString();
            else
            {
                foreach (var date in _values)
                {
                    if (date.ToCharArray()[0] != '+')
                        return "date: invalid date " + date;
                    return DateTime.Now.ToString(date.Replace("+", string.Empty).Replace("%", " "));
                }
            }
            return output;
        }
    }
}
