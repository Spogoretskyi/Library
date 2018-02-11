using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models
{
    public class Years
    {
        public int Id { get; set; }
        public int Year { get; set; }
    }
    public class ViewModelYears
    {
        readonly List<Years> listYears;

        public ViewModelYears()
        {
            listYears = new List<Years>();
            int curYear = Convert.ToInt32(DateTime.Now.Year);
            for (int i = curYear; i >= 1900; i--)
            {
                listYears.Add(new Years { Id = i, Year = i });
            }
        }

        public IEnumerable<Years> ListYears
        {
            get { return listYears; }
        }
    }
}