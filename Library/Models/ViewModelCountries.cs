using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISO3166;
using System.Web.Mvc;

namespace Library.Models
{
    public class Countries
    {
        public string Id { get; set; }
        public string Country { get; set; }
    }

    public class ViewModelCountries
    {
        readonly List<Countries> listCountries;
        public ViewModelCountries()
        {
            listCountries = new List<Countries>();
            for (int i = 0; i < Country.List.Length - 1; i++)
            {
                listCountries.Add(new Countries { Id = Country.List[i].Name,  Country = Country.List[i].Name });
            }
        }

        public IEnumerable<Countries> ListCountries
        {
            get { return listCountries; }
        }
    }
}